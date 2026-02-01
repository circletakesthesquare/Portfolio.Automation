namespace API.Tests
{
    public abstract class AlbumsTestBase : TestBase
    {
        protected readonly Fixture _fixture = new Fixture();

        protected AlbumsTestBase(ITestOutputHelper output) : base(output)
        {
        }

        /// <summary>
        /// Creates a new Album object with randomized data.
        /// </summary>
        /// <returns>The generated album.</returns>
        public Album GenerateAlbum()
        {
            return _fixture.Build<Album>()
                           .With(a => a.Title, $"Album-Test-{Guid.NewGuid()}")
                           .Create();
        }

        /// <summary>
        /// Creates an updated Album object with randomized data.
        /// </summary>
        /// <param name="id">The album ID to update.</param>
        /// <returns>The updated album.</returns>
        public Album GenerateUpdatedAlbum(int id)
        {
            return _fixture.Build<Album>()
                           .With(a => a.Id, id)
                           .With(a => a.Title, $"Updated-Album-Test-{Guid.NewGuid()}")
                           .Create();
        }
    }
}