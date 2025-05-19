using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using ToDo.Analyzer.Actions;
using ToDo.Analyzer.Helpers;

namespace ToDo.Analyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class ToDoAnalyzerAnalyzer : DiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        {
            get
            {
                return GetDiagnosticIds.Get();
            }
        }

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();

            context.RegisterSyntaxNodeAction(XUnitMethodNameAnalyzer.Analyze, SyntaxKind.MethodDeclaration);
            context.RegisterSyntaxTreeAction(MaxCharacterCountPerLineAnalyzer.Analyze);
            context.RegisterSyntaxNodeAction(OneLinerIfReturnAnalyzer.Analyze, SyntaxKind.IfStatement);
        }
    }
}
