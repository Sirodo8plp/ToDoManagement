using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using ToDo.Analyzer.Actions;

namespace ToDo.Analyzer.Helpers
{
    public static class GetDiagnosticIds
    {
        public static ImmutableArray<DiagnosticDescriptor> Get()
        {
            return ImmutableArray.Create(
                LineLengthRule.Rule,
                OneLinerIfReturnRule.Rule,
                XUnitRule.Rule
            );
        }
    }
}