using API.Tests.Clients;
using API.Tests.Utilities;
using FluentAssertions;
using Xunit.Abstractions;

namespace API.Tests.Tests
{
    public class GetAlbumTests : AlbumsTestBase
    {
        
        public GetAlbumTests(ITestOutputHelper output) : base(output)
        {
            _client = new AlbumsClient(CreateHttpClient());
        }

        private readonly AlbumsClient _client;

        [Fact]
        [Trait("Category", Categories.Get)]
        [Trait("Category", Categories.Album)]
        [Trait("Category", Categories.Positive)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.Smoke)]
        public async Task Get_Album_By_Id()
        {
            var validAlbumId = IdGenerator.RandomValidAlbumId();
            var album = await _client.GetAlbumById(validAlbumId);

            album.ShouldExist(validAlbumId);
        }

        [Fact]
        [Trait("Category", Categories.Get)]
        [Trait("Category", Categories.Album)]
        [Trait("Category", Categories.Positive)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.Smoke)]
        public async Task GetAllAlbums()
        {
            var albums = await _client.GetAllAlbums();
        
            albums.Should().NotBeNull("Expected a list of albums.");
            albums!.Count.Should().BeGreaterThan(0, "Expected at least one album in the list.");
        }

        [Fact]
        [Trait("Category", Categories.Get)]
        [Trait("Category", Categories.Album)]
        [Trait("Category", Categories.Positive)]
        [Trait("Category", Categories.Integration)]
        public async Task Get_Albums_By_UserId()
        {
            var validUserId = IdGenerator.RandomValidAlbumId();
            var albums = await _client.GetAlbumsByUserId(validUserId);

            albums.Should().NotBeNull("Expected albums for valid user ID.");
            albums!.All(a => a.UserId == validUserId).Should().BeTrue("All returned albums should belong to the specified user ID.");
        }

        [Fact]
        [Trait("Category", Categories.Get)]
        [Trait("Category", Categories.Album)]
        [Trait("Category", Categories.Positive)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.EdgeCase)]
        public async Task Get_Album_By_Id_Minimum_Valid_Id()
        {
            var minValidAlbumId = IdRanges.MinValidAlbumId;
            var album = await _client.GetAlbumById(minValidAlbumId);

            album.ShouldExist(minValidAlbumId);
        }

        [Fact]
        [Trait("Category", Categories.Get)]
        [Trait("Category", Categories.Album)]
        [Trait("Category", Categories.Positive)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.EdgeCase)]
        public async Task Get_Album_By_Id_Maximum_Valid_Id()
        {
            var maxValidAlbumId = IdRanges.MaxValidAlbumId;
            var album = await _client.GetAlbumById(maxValidAlbumId);

            album.ShouldExist(maxValidAlbumId);
        }

        // Negative Tests

        [Fact]
        [Trait("Category", Categories.Get)]
        [Trait("Category", Categories.Album)]
        [Trait("Category", Categories.Negative)]
        [Trait("Category", Categories.Integration)]
        public async Task Get_Album_By_Invalid_Id_Should_Return_NotFound()
        {
            var invalidAlbumId = IdGenerator.RandomInvalidAlbumId();
            var response = await _client.GetAlbumById(invalidAlbumId);

            response.Should().BeNull("Expected no album to be found for an invalid ID.");
        }

        [Fact]
        [Trait("Category", Categories.Get)]
        [Trait("Category", Categories.Album)]
        [Trait("Category", Categories.Negative)]
        [Trait("Category", Categories.Integration)]
        public async Task Get_Albums_By_Invalid_UserId_Should_Return_Empty_List()
        {
            var invalidUserId = IdGenerator.RandomInvalidAlbumId();
            var albums = await _client.GetAlbumsByUserId(invalidUserId);

            albums.Should().NotBeNull("Expected a response for albums by invalid user ID.");
        }
    }
}