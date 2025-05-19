using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Analyzer.CodeFixes.CodeFixers;
using ToDo.Analyzer.Helpers;

namespace ToDo.Analyzer.CodeFixes
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(ToDoAnalyzerCodeFixProvider)), Shared]
    public class ToDoAnalyzerCodeFixProvider : CodeFixProvider
    {
        public sealed override ImmutableArray<string> FixableDiagnosticIds
        {
            get
            {
                return [Constants.UnitTestDiagnosticId, Constants.OneLineIfDiagnosticId
];
            }
        }

        public sealed override FixAllProvider GetFixAllProvider()
        {
            // See https://github.com/dotnet/roslyn/blob/main/docs/analyzers/FixAllProvider.md for more information on Fix All Providers
            return WellKnownFixAllProviders.BatchFixer;
        }

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var diagnostic = context.Diagnostics.FirstOrDefault();

            switch (diagnostic.Id)
            {
                case Constants.LineLengthDiagnosticId:
                    await XUnitMethodNameFixer.Fix(context);
                    break;

                case Constants.OneLineIfDiagnosticId:
                    await OneLinerIfReturnFixer.Fix(context);
                    break;
            }
        }
    }
}
