using Microsoft.CodeAnalysis;
using System.Threading.Tasks;
using ToDo.Analyzer.CodeFixes;
using ToDo.Analyzer.Test.Verifiers;
using Xunit;

namespace ToDo.Analyzer.Test
{
    public class XUnitMethodNameTests
    {
        [Fact]
        public async Task When_FactMethodNameStartsWithWhen_ThereIsNoDiagnostic()
        {
            var testCode = @"
                using Xunit;

                public class MyTests
                {
                    [Fact]
                    public void When_Something_Happens() { }
                }
            ";

            var test = new VerifyCS.Test<ToDoAnalyzerAnalyzer, ToDoAnalyzerCodeFixProvider>
            {
                TestCode = testCode
            };

            // Manually add xUnit reference to the test
            test.TestState.AdditionalReferences.Add(
                MetadataReference.CreateFromFile(typeof(FactAttribute).Assembly.Location));

            // Expected to have no diagnostics
            await test.RunAsync();
        }
    }
}
