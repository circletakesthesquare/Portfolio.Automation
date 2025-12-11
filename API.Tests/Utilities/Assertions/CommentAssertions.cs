using FluentAssertions;
using FluentAssertions.Execution;
using API.Tests.Models;

namespace API.Tests.Utilities
{
    /// <summary>
    /// Assertion extensions for Comment model.
    /// </summary>
    public static class CommentAssertions
    {
        /// <summary>
        /// Asserts that the actual Comment matches the expected Comment.
        /// </summary>
        /// <param name="actual">The actual Comment object created from the response.</param>
        /// <param name="expected">The expected Comment object created from the request.</param>
        public static void ShouldMatch(this Comment? actual, Comment expected)
        {
            using (new AssertionScope())
            {
                // handle case where actual is null
                ArgumentNullException.ThrowIfNull(actual);

                actual.Id.Should().BePositive("Expected comment ID to be positive.");
                actual.PostId.Should().Be(expected.PostId, "Expected comment post ID to match.");
                actual.Name.Should().Be(expected.Name, "Expected comment name to match.");
                actual.Email.Should().Be(expected.Email, "Expected comment email to match.");
                actual.Body.Should().Be(expected.Body, "Expected comment body to match.");
            }
        }

        public static void ShouldExist(this Comment? comment, int id)
        {
            comment.Should().NotBeNull("Expected comment to exist, but it was null.");
            comment!.Id.Should().Be(id, "Expected comment ID to match the provided ID.");
        }

        public static void ShouldBeDeleted(this Comment? comment)
        {
            comment.Should().BeNull("Expected comment to not exist, but it was found.");
        }
    }
}