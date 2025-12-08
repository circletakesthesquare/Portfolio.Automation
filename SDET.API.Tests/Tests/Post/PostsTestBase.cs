using AutoFixture;
using SDET.API.Tests.Models;
using Xunit.Abstractions;

namespace SDET.API.Tests
{
    /// <summary>
    /// Base class for Post-related API tests, providing common setup and utilities.
    /// </summary>
    public abstract class PostsTestBase : TestBase
    {
        protected readonly Fixture _fixture = new Fixture();

        protected PostsTestBase(ITestOutputHelper output) : base(output)
        {
        }

        /// <summary>
        /// Creates a new Post object with randomized data.
        /// </summary>
        /// <returns>Returns a new Post object with randomized data.</returns>
        public Post GeneratePost()
        {
            return _fixture.Build<Post>()
                            .With(p => p.Title, $"Post-Test-{Guid.NewGuid()}")
                            .With(p => p.Body, $"This is a test post body created at {DateTime.UtcNow}.")
                           .Create();
        }

        /// <summary>
        /// Creates an updated Post object with randomized data.
        /// </summary>
        /// <param name="id">The ID to assign to the updated Post object.</param>
        /// <returns>Returns an updated Post object with randomized data and the specified ID.</returns>
        public Post GenerateUpdatedPost(int id)
        {
            return _fixture.Build<Post>()
                           .With(p => p.Id, id)
                           .With(p => p.Title, $"Updated-Post-Test-{Guid.NewGuid()}")
                           .With(p => p.Body, $"This is an updated test post body created at {DateTime.UtcNow}.")
                           .Create();
        }
    }
}