using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ToDo.Analyzer.CodeFixes.CodeFixers
{
    public static class OneLinerIfReturnFixer
    {
        private const string Title = "Wrap 'if' body in braces";

        public static async Task Fix(CodeFixContext context)
        {
            var diagnostic = context.Diagnostics.First();
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

            var token = root.FindToken(diagnostic.Location.SourceSpan.Start);
            var ifStatement = token.Parent.AncestorsAndSelf().OfType<IfStatementSyntax>().FirstOrDefault();

            if (ifStatement == null || ifStatement.Statement is BlockSyntax)
            {
                return;
            }

            context.RegisterCodeFix(
                CodeAction.Create(
                    title: Title,
                    createChangedDocument: c => WrapIfStatementInBlockAsync(context.Document, ifStatement, c),
                    equivalenceKey: Title),
                diagnostic);
        }

        private static async Task<Document> WrapIfStatementInBlockAsync(
            Document document,
            IfStatementSyntax ifStatement,
            CancellationToken cancellationToken)
        {
            // Wrap the single statement in a new block
            var newBlock = SyntaxFactory.Block(ifStatement.Statement)
                .WithTrailingTrivia(ifStatement.Statement.GetTrailingTrivia())
                .WithLeadingTrivia(ifStatement.Statement.GetLeadingTrivia());

            var newIf = ifStatement.WithStatement(newBlock);

            var root = await document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false);
            var newRoot = root.ReplaceNode(ifStatement, newIf);

            return document.WithSyntaxRoot(newRoot);
        }
    }
}
