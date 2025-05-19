using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Threading.Tasks;

namespace ToDo.Analyzer.Test.Verifiers
{
    public static class VerifyCS
    {
        public class Test<TAnalyzer, TCodeFix> : CSharpCodeFixTest<TAnalyzer, TCodeFix, XUnitVerifier>
            where TAnalyzer : DiagnosticAnalyzer, new()
            where TCodeFix : CodeFixProvider, new()
        {
        }

        public static async Task VerifyAnalyzerAsync<TAnalyzer>(string source, params DiagnosticResult[] expected)
            where TAnalyzer : DiagnosticAnalyzer, new()
        {
            var test = new Test<TAnalyzer, EmptyCodeFixProvider>
            {
                TestCode = source
            };

            test.ExpectedDiagnostics.AddRange(expected);
            await test.RunAsync();
        }

        public static async Task VerifyCodeFixAsync<TAnalyzer, TCodeFix>(string source, string fixedSource, params DiagnosticResult[] expected)
            where TAnalyzer : DiagnosticAnalyzer, new()
            where TCodeFix : CodeFixProvider, new()
        {
            var test = new Test<TAnalyzer, TCodeFix>
            {
                TestCode = source,
                FixedCode = fixedSource
            };

            test.ExpectedDiagnostics.AddRange(expected);
            await test.RunAsync();
        }
    }
}
