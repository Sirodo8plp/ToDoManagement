using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Linq;

namespace ToDo.Analyzer.Helpers
{
    public class XUnitMethodNameAnalyzer
    {
        public static void Analyze(SyntaxNodeAnalysisContext context)
        {
            var methodDeclaration = (MethodDeclarationSyntax)context.Node;

            var hasFactAttribute = methodDeclaration.AttributeLists
                .SelectMany(attrList => attrList.Attributes)
                .Any(attr => attr.Name.ToString().Contains(Constants.Fact));

            if (!hasFactAttribute)
            {
                return;
            }

            var methodName = methodDeclaration.Identifier.Text;

            if (!methodName.StartsWith(Constants.When))
            {
                var diagnostic = Diagnostic.Create(XUnitRule.Rule, methodDeclaration.Identifier.GetLocation(), methodName);
                context.ReportDiagnostic(diagnostic);
            }
        }
    }

    public static class XUnitRule
    {
        private static readonly LocalizableString Title = new LocalizableResourceString(
            nameof(Resources.XUnitAnalyzerTitle),
            Resources.ResourceManager, typeof(Resources));

        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(
            nameof(Resources.XUnitAnalyzerMessageFormat),
            Resources.ResourceManager, typeof(Resources));

        private static readonly LocalizableString Description = new LocalizableResourceString(
            nameof(Resources.XUnitAnalyzerDescription),
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
