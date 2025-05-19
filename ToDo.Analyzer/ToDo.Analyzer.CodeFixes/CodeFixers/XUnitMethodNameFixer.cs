using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Rename;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ToDo.Analyzer.CodeFixes.CodeFixers
{
    public static class XUnitMethodNameFixer
    {
        private const string Title = "Prefix with 'When'";

        public static async Task Fix(CodeFixContext context)
        {
            var diagnostic = context.Diagnostics.First();

            var span = diagnostic.Location.SourceSpan;

            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
            var node = root.FindToken(span.Start).Parent.AncestorsAndSelf().OfType<MethodDeclarationSyntax>().FirstOrDefault();

            if (node == null)
            {
                return;
            }

            var semanticModel = await context.Document.GetSemanticModelAsync(context.CancellationToken);
            var symbol = semanticModel.GetDeclaredSymbol(node, context.CancellationToken);

            if (symbol != null)
            {
                var newName = "When_" + symbol.Name;

                context.RegisterCodeFix(
                    CodeAction.Create(
                        title: Title,
                        createChangedSolution: c => RenameSymbolAsync(context.Document.Project.Solution, symbol, newName, c),
                        equivalenceKey: Title),
                    diagnostic);
            }
        }

        private static async Task<Solution> RenameSymbolAsync(
            Solution solution,
            ISymbol symbol,
            string newName,
            CancellationToken cancellationToken)
        {
            return await Renamer.RenameSymbolAsync(solution, symbol, newName, solution.Workspace.Options, cancellationToken);
        }
    }
}