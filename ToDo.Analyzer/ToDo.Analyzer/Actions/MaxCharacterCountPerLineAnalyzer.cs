using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using ToDo.Analyzer.Helpers;

namespace ToDo.Analyzer.Actions
{
    public static class MaxCharacterCountPerLineAnalyzer
    {
        public static void Analyze(SyntaxTreeAnalysisContext context)
        {
            var text = context.Tree.GetText(context.CancellationToken);

            foreach (var line in text.Lines)
            {
                var lineText = line.ToString();
                if (lineText.Length > Constants.MaxLineLength)
                {
                    var location = Location.Create(context.Tree, line.Span);
                    var diagnostic = Diagnostic.Create(LineLengthRule.Rule, location, lineText, Constants.MaxLineLength);
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }
    }

    public static class LineLengthRule
    {
        private static readonly LocalizableString Title = new LocalizableResourceString(
            nameof(Resources.LineLengthTitle),
            Resources.ResourceManager, typeof(Resources));

        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(
            nameof(Resources.LineLengthMessageFormat),
            Resources.ResourceManager, typeof(Resources));

        private static readonly LocalizableString Description = new LocalizableResourceString(
            nameof(Resources.LineLengthDescription),
            Resources.ResourceManager, typeof(Resources));

        public static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
            Constants.UnitTestDiagnosticId,
            Title,
            MessageFormat,
            Constants.FormattingCategory,
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true,
            description: Description);
    }
}
