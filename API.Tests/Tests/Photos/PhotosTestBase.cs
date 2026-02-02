namespace API.Tests
{
    /// <summary>
    /// Base class for Photo-related API tests, providing common setup and utilities.
    /// </summary>
    public abstract class PhotosTestBase : TestBase
    {
        protected readonly Fixture _fixture = new Fixture();

        protected PhotosTestBase(ITestOutputHelper output) : base(output)
        {
        }

        /// <summary>
        /// Creates a new Photo object with randomized data.
        /// </summary>
        /// <returns>The generated photo.</returns>
        public Photo GeneratePhoto()
        {
            return _fixture.Build<Photo>()
                           .With(p => p.Title, $"Photo-Test-{Guid.NewGuid()}")
                           .With(p => p.Url, $"https://example.com/photo/{Guid.NewGuid()}")
                           .With(p => p.ThumbnailUrl, $"https://example.com/thumbnail/{Guid.NewGuid()}")
                           .Create();
        }

        /// <summary>
        /// Creates an updated Photo object with randomized data.
        /// </summary>
        /// <param name="id">The photo ID to update.</param>
        /// <returns>The updated photo.</returns>
        public Photo GenerateUpdatedPhoto(int id)
        {
            return _fixture.Build<Photo>()
                           .With(p => p.Id, id)
                           .With(p => p.Title, $"Updated-Photo-Test-{Guid.NewGuid()}")
                           .With(p => p.Url, $"https://example.com/updated-photo/{Guid.NewGuid()}")
                           .With(p => p.ThumbnailUrl, $"https://example.com/updated-thumbnail/{Guid.NewGuid()}")
                           .Create();
        }
    }
}