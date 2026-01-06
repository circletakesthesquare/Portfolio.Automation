

using System.Reflection.Emit;
using Api.Tests.Models;
using FluentAssertions;
using FluentAssertions.Execution;

namespace API.Tests.Utilities
{
    public static class AlbumAssertions
    {
        /// <summary>
        /// Asserts that the actual Album matches the expected Album.
        /// </summary>
        /// <param name="actual">The actual Album object created from the response.</param>
        /// <param name="expected">The expected Album object created from the request.</param>
        public static void ShouldMatch(this Album? actual, Album expected)
        {
            using (new AssertionScope())
            {
                // handle case where actual is null
                ArgumentNullException.ThrowIfNull(actual);

                actual.Id.Should().BePositive("Expected album ID to be positive.");
                actual.UserId.Should().BePositive("Expected album user ID to be positive.");
                actual.Title.Should().Be(expected.Title, "Expected album Title to match.");
                
            }
        }
        
        /// <summary>
        /// Asserts that the album exists and matches the provided ID.
        /// </summary>
        /// <param name="album">The album to check.</param>
        /// <param name="id">The expected album ID.</param>
        public static void ShouldExist(this Album? album, int id)
        {
            album.Should().NotBeNull("Expected album to exist, but it was null.");
            album!.Id.Should().Be(id, "Expected album ID to match the provided ID.");
        }

        /// <summary>
        /// Asserts that the album has been deleted (i.e., is null).
        /// </summary>
        /// <param name="album">The album to check.</param>
        public static void ShouldBeDeleted(this Album? album)
        {
            album.Should().BeNull("Expected album to not exist, but it was found.");
        }
    }
}