namespace API.Tests.Utilities
{
    /// <summary>
    /// Assertion extensions for Post model.
    /// </summary>
    public static class PostAssertions
    {
        
        /// <summary>
        /// Asserts that the actual Post matches the expected Post.
        /// </summary>
        /// <param name="actual">The actual Post object created from the response.</param>
        /// <param name="expected">The expected Post object created from the request.</param>
        public static void ShouldMatch(this Post? actual, Post expected)
        {
            using (new AssertionScope())
            {
                // handle case where actual is null
                ArgumentNullException.ThrowIfNull(actual);

                actual.Id.Should().BePositive("Expected post ID to be positive.");
                actual.UserId.Should().Be(expected.UserId, "Expected post user ID to match.");                
                actual.Title.Should().Be(expected.Title, "Expected post title to match.");
                actual.Body.Should().Be(expected.Body, "Expected post body to match.");
            }
        }

        public static void ShouldExist(this Post? post, int id)
        {
            post.Should().NotBeNull("Expected post to exist, but it was null.");
            post!.Id.Should().Be(id, "Expected post ID to match the provided ID.");
        }


        public static void ShouldBeDeleted(this Post? post)
        {
            post.Should().BeNull("Expected post to not exist, but it was found.");
        }
    }
}