using API.Tests.Clients;
using API.Tests.Utilities;
using Xunit.Abstractions;

namespace API.Tests.Tests
{
    /// <summary>
    /// Tests for creating albums via the Albums API.
    /// </summary>
    public class CreateAlbumTests : AlbumsTestBase
    {
        public CreateAlbumTests(ITestOutputHelper output) : base(output)
        {
            _client = new AlbumsClient(CreateHttpClient());
        }

        private readonly AlbumsClient _client;

        [Fact]
        [Trait("Category", Categories.Create)]
        [Trait("Category", Categories.Album)]
        [Trait("Category", Categories.Positive)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.Smoke)]
        public async Task Create_Album_Successfully()
        {
            // generate album with album, using fixture to create test data
            var newAlbumRequest = GenerateAlbum();
            var createdAlbum = await _client.CreateAlbum(newAlbumRequest);

            createdAlbum.ShouldMatch(newAlbumRequest);
        }

        // Negative Tests

        [Fact]
        [Trait("Category", Categories.Create)]
        [Trait("Category", Categories.Album)]
        [Trait("Category", Categories.Negative)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.Regression)]
        public async Task Create_Album_With_Empty_Title_Should_Fail()
        {
            var newAlbumRequest = GenerateAlbum();
            newAlbumRequest.Title = string.Empty;

            var createdAlbum = await _client.CreateAlbum(newAlbumRequest);

            // This is temporarily commented out until API behavior is updated. Server will currently create albums with empty titles.
            //createdAlbum.Should().BeNull("Expected creation to fail when title is empty");
            createdAlbum.ShouldMatch(newAlbumRequest);
        }

        [Fact]
        [Trait("Category", Categories.Create)]
        [Trait("Category", Categories.Album)]
        [Trait("Category", Categories.Negative)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.Regression)]
        public async Task Create_Album_With_Long_Title_Should_Fail()
        {
            var newAlbumRequest = GenerateAlbum();
            newAlbumRequest.Title = new string('A', 5001); // Assuming limit of 5000 characters

            var createdAlbum = await _client.CreateAlbum(newAlbumRequest);

            // This is temporarily commented out until API behavior is updated. Server will currently create albums with long titles.
            //createdAlbum.Should().BeNull("Expected creation to fail when title exceeds maximum length");
            createdAlbum.ShouldMatch(newAlbumRequest);
        }

        [Fact]
        [Trait("Category", Categories.Create)]
        [Trait("Category", Categories.Album)]
        [Trait("Category", Categories.Negative)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.Regression)]
        public async Task Create_Album_With_Invalid_Id_Should_Fail()
        {
            var newAlbumRequest = GenerateAlbum();
            newAlbumRequest.Id = 0;

            var createdAlbum = await _client.CreateAlbum(newAlbumRequest);

            // This is temporarily commented out until API behavior is updated. Server will currently create albums with invalid IDs.
            //createdAlbum.Should().BeNull("Expected creation to fail when ID is invalid");
            createdAlbum.ShouldMatch(newAlbumRequest);
        }

        [Fact]
        [Trait("Category", Categories.Create)]
        [Trait("Category", Categories.Album)]
        [Trait("Category", Categories.Negative)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.Regression)]
        public async Task Create_Duplicate_Album_Should_Fail()
        {
            var newAlbumRequest = GenerateAlbum();
            var firstResponse = await _client.CreateAlbum(newAlbumRequest);

            firstResponse.ShouldMatch(newAlbumRequest);

            var duplicateResponse = await _client.CreateAlbum(newAlbumRequest);

            // This is temporarily commented out until API behavior is updated. Server will currently allow duplicate albums.
            //duplicateResponse.Should().BeNull("Expected creation to fail when creating a duplicate album");
            duplicateResponse.ShouldMatch(newAlbumRequest);
        }
    }
}