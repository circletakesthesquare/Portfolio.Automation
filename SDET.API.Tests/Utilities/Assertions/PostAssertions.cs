using FluentAssertions;
using FluentAssertions.Execution;
using SDET.API.Tests.Models;

namespace SDET.API.Tests.Utilities
{
    /// <summary>
    /// Assertion extensions for Post model.
    /// </summary>
    public static class PostAssertions
    {
        
        public static void ShouldMatch(this Post? actual, Post expected)
        {
            using (new AssertionScope())
            {
                // handle case where actual is null
                ArgumentNullException.ThrowIfNull(actual);

                actual.Id.Should().BePositive();
                actual.UserId.Should().Be(expected.UserId);                
                actual.Title.Should().Be(expected.Title);
                actual.Body.Should().Be(expected.Body);
            }
        }
    }
}