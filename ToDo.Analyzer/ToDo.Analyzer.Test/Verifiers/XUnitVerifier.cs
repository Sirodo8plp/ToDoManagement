using Microsoft.CodeAnalysis.Testing;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ToDo.Analyzer.Test
{
    public sealed class XUnitVerifier : IVerifier
    {
        private readonly string _context;

        public XUnitVerifier() : this(string.Empty) { }

        private XUnitVerifier(string context)
        {
            _context = context;
        }

        private string FormatMessage(string message) =>
            string.IsNullOrEmpty(_context) ? message : $"{_context}: {message}";

        public void Equal<T>(T expected, T actual, string? message)
            => Assert.Equal(expected, actual);

        public void True(bool condition, string? message)
            => Assert.True(condition, FormatMessage(message));

        public void False(bool condition, string? message)
            => Assert.False(condition, FormatMessage(message));

        public void Fail(string? message)
            => Assert.True(false, FormatMessage(message));

        public void Empty<T>(string collectionName, IEnumerable<T> collection)
            => Assert.Empty(collection ?? Enumerable.Empty<T>());

        public void NotEmpty<T>(string collectionName, IEnumerable<T> collection)
            => Assert.NotEmpty(collection ?? Enumerable.Empty<T>());

        public void LanguageIsSupported(string language)
        {
            // No-op by default, or you can assert on supported languages if needed
        }

        public void SequenceEqual<T>(
            IEnumerable<T> expected,
            IEnumerable<T> actual,
            IEqualityComparer<T>? equalityComparer = null,
            string? message = null)
        {
            if (equalityComparer == null)
            {
                Assert.Equal(expected, actual);
            }
            else
            {
                var expectedList = expected.ToList();
                var actualList = actual.ToList();

                Assert.True(expectedList.Count == actualList.Count,
                    FormatMessage(message ?? $"Expected count {expectedList.Count}, but got {actualList.Count}"));

                for (int i = 0; i < expectedList.Count; i++)
                {
                    Assert.True(
                        equalityComparer.Equals(expectedList[i], actualList[i]),
                        FormatMessage(message ?? $"Mismatch at index {i}: expected '{expectedList[i]}', got '{actualList[i]}'"));
                }
            }
        }

        public IVerifier PushContext(string context)
            => new XUnitVerifier($"{_context}{(string.IsNullOrEmpty(_context) ? "" : " > ")}{context}");
    }
}
