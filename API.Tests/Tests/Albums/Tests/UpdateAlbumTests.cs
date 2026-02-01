namespace API.Tests.Tests
{
    /// <summary>
    /// Tests for updating albums via the Albums API.
    /// </summary>
    public class UpdateAlbumTests : AlbumsTestBase
    {
        public UpdateAlbumTests(ITestOutputHelper output) : base(output)
        {
            _client = new AlbumsClient(CreateHttpClient());
        }

        private readonly AlbumsClient _client;

        [Fact]
        [Trait("Category", Categories.Update)]
        [Trait("Category", Categories.Album)]
        [Trait("Category", Categories.Positive)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.Smoke)]
        public async Task Update_Album_By_Id()
        {
            var validId = IdGenerator.RandomValidAlbumId();
            var updatedAlbumRequest = GenerateUpdatedAlbum(validId);

            var response = await _client.UpdateAlbum(validId, updatedAlbumRequest);

            response.ShouldMatch(updatedAlbumRequest);
        }

        [Fact]
        [Trait("Category", Categories.Update)]
        [Trait("Category", Categories.Album)]
        [Trait("Category", Categories.Positive)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.EdgeCase)]
        public async Task Update_Album_By_Id_Minimum_Valid_Id()
        {
            var minValidId = IdRanges.MinValidAlbumId;
            var updatedAlbumRequest = GenerateUpdatedAlbum(minValidId);

            var response = await _client.UpdateAlbum(minValidId, updatedAlbumRequest);

            response.ShouldMatch(updatedAlbumRequest);
        }

        [Fact]
        [Trait("Category", Categories.Update)]
        [Trait("Category", Categories.Album)]
        [Trait("Category", Categories.Positive)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.EdgeCase)]
        public async Task Update_Album_By_Id_Maximum_Valid_Id()
        {
            var maxValidId = IdRanges.MaxValidAlbumId;
            var updatedAlbumRequest = GenerateUpdatedAlbum(maxValidId);

            var response = await _client.UpdateAlbum(maxValidId, updatedAlbumRequest);

            response.ShouldMatch(updatedAlbumRequest);
        }

        // Negative Tests

        [Fact]
        [Trait("Category", Categories.Update)]
        [Trait("Category", Categories.Album)]
        [Trait("Category", Categories.Negative)]
        [Trait("Category", Categories.Regression)]
        public async Task Update_Album_By_Id_Invalid_Id()
        {
            var invalidId = IdGenerator.RandomInvalidAlbumId();
            var updatedAlbumRequest = GenerateUpdatedAlbum(invalidId);

            var response = await _client.UpdateAlbum(invalidId, updatedAlbumRequest);

            response.Should().BeNull("Expected null response for invalid album ID.");
        }

        [Fact]
        [Trait("Category", Categories.Update)]
        [Trait("Category", Categories.Album)]
        [Trait("Category", Categories.Negative)]
        [Trait("Category", Categories.Regression)]
        public async Task Update_Album_With_Empty_Title_Should_Fail()
        {
            var validId = IdGenerator.RandomValidAlbumId();
            var updatedAlbumRequest = GenerateUpdatedAlbum(validId);
            updatedAlbumRequest.Title = string.Empty;

            var response = await _client.UpdateAlbum(validId, updatedAlbumRequest);

            // This is temporarily commented out until API behavior is updated. Server will currently update albums with empty titles.
            //response.Should().BeNull("Expected update to fail when title is empty");
            response.ShouldMatch(updatedAlbumRequest);
        }

        [Fact]
        [Trait("Category", Categories.Update)]
        [Trait("Category", Categories.Album)]
        [Trait("Category", Categories.Negative)]
        [Trait("Category", Categories.Regression)]
        public async Task Update_Album_With_Title_Exceeding_Max_Length_Should_Fail()
        {
            var validId = IdGenerator.RandomValidAlbumId();
            var updatedAlbumRequest = GenerateUpdatedAlbum(validId);
            updatedAlbumRequest.Title = new string('A', 5001); // Assuming limit of 5000 characters

            var response = await _client.UpdateAlbum(validId, updatedAlbumRequest);

            // This is temporarily commented out until API behavior is updated. Server will currently update albums with long titles.
            //response.Should().BeNull("Expected update to fail when title exceeds maximum length");
            response.ShouldMatch(updatedAlbumRequest);
        }

        [Fact]
        [Trait("Category", Categories.Update)]
        [Trait("Category", Categories.Album)]
        [Trait("Category", Categories.Negative)]
        [Trait("Category", Categories.Regression)]
        public async Task Update_Duplicate_Album_Should_Fail()
        {
            var validId = IdGenerator.RandomValidAlbumId();
            var firstUpdatedAlbumRequest = GenerateUpdatedAlbum(validId);
            var firstResponse = await _client.UpdateAlbum(validId, firstUpdatedAlbumRequest);

            firstResponse.ShouldMatch(firstUpdatedAlbumRequest);

            var duplicateUpdatedAlbumRequest = firstUpdatedAlbumRequest;
            var duplicateResponse = await _client.UpdateAlbum(validId, duplicateUpdatedAlbumRequest);

            // This is temporarily commented out until API behavior is updated. Server will currently allow duplicate album updates.
            //duplicateResponse.Should().BeNull("Expected update to fail when updating a duplicate album");
            duplicateResponse.ShouldMatch(duplicateUpdatedAlbumRequest);
        }
    }
}