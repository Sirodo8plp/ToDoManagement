using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using ToDo.Analyzer.Helpers;

namespace ToDo.Analyzer.Actions
{
    public static class OneLinerIfReturnAnalyzer
    {
        public static void Analyze(SyntaxNodeAnalysisContext context)
        {
            var ifStatement = (IfStatementSyntax)context.Node;

            if (!(ifStatement.Statement is BlockSyntax))
            {
                var diagnostic = Diagnostic.Create(OneLinerIfReturnRule.Rule, ifStatement.IfKeyword.GetLocation());
                context.ReportDiagnostic(diagnostic);
            }
        }
    }

    public static class OneLinerIfReturnRule
    {
        private static readonly LocalizableString Title = new LocalizableResourceString(
            nameof(Resources.OneLinerIfReturnTitle),
            Resources.ResourceManager, typeof(Resources));

        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(
            nameof(Resources.OneLinerIfReturnMessageFormat),
            Resources.ResourceManager, typeof(Resources));

        private static readonly LocalizableString Description = new LocalizableResourceString(
            nameof(Resources.OneLinerIfReturnDescription),
            Resources.ResourceManager, typeof(Resources));

        public static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
            Constants.UnitTestDiagnosticId,
            Title,
            MessageFormat,
            Constants.NamingCategory,
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true,
            description: Description);
    }
}
