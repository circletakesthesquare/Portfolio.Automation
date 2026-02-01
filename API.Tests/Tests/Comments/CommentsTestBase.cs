namespace API.Tests
{
    /// <summary>
    /// Base class for Comment-related API tests, providing common setup and utilities.
    /// </summary>
    public abstract class CommentsTestBase : TestBase
    {
        protected readonly Fixture _fixture = new Fixture();

        protected CommentsTestBase(ITestOutputHelper output) : base(output)
        {
        }

        /// <summary>
        /// Creates a new Comment object with randomized data.
        /// </summary>
        /// <returns>Returns a new Comment object with randomized data.</returns>
        public Comment GenerateComment()
        {
            return _fixture.Build<Comment>()
                            .With(c => c.Name, $"Comment-Test-{Guid.NewGuid()}")
                            .With(c => c.Email, $"testuser{Guid.NewGuid()}@example.com")
                            .With(c => c.Body, $"This is a test comment body created at {DateTime.UtcNow}.")
                           .Create();
        }

        /// <summary>
        /// Creates an updated Comment object with randomized data.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns an updated Comment object with randomized data.</returns>
        public Comment GenerateUpdatedComment(int id)
        {
            return _fixture.Build<Comment>()
                           .With(c => c.Id, id)
                           .With(c => c.Name, $"Updated-Comment-Test-{Guid.NewGuid()}")
                           .With(c => c.Email, $"updateduser{Guid.NewGuid()}@example.com")
                           .With(c => c.Body, $"This is an updated test comment body created at {DateTime.UtcNow}.")
                           .Create();
        }
    }
}