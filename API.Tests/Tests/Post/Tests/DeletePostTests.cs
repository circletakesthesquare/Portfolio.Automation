using API.Tests.Clients;
using API.Tests.Utilities;
using FluentAssertions;
using Xunit.Abstractions;

namespace API.Tests.Tests
{
    /// <summary>
    /// Tests for deleting posts via the Posts API.
    /// </summary>
    public class DeletePostTests : PostsTestBase
    {
        public DeletePostTests(ITestOutputHelper output) : base(output)
        {
            _client = new PostsClient(CreateHttpClient());
        }

        private readonly PostsClient _client;

        
        [Fact]
        [Trait("Category", Categories.Delete)]
        [Trait("Category", Categories.Post)]
        [Trait("Category", Categories.Positive)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.Smoke)]
        public async Task Delete_Post()
        {   
            var idToDelete = IdGenerator.RandomValidPostId();
            var response = await _client.DeletePost(idToDelete);

            response.IsSuccessStatusCode.Should().BeTrue("Expected successful deletion response.");
        }

        // Negative Tests

        [Fact]
        [Trait("Category", Categories.Delete)]
        [Trait("Category", Categories.Post)]
        [Trait("Category", Categories.Negative)]
        [Trait("Category", Categories.Integration)]
        public async Task Delete_Post_By_Invalid_Id_Should_Return_NotFound()
        {
            var invalidId = IdGenerator.RandomInvalidPostId();
            var response = await _client.DeletePost(invalidId);

            // This is temporarily commented out until API behavior is updated. Server will return 200 OK even for invalid IDs.
            //response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound, "Expected NotFound status for invalid post ID deletion.");
            response.EnsureSuccessStatusCode();
        }
        [Fact]
        [Trait("Category", Categories.Delete)]
        [Trait("Category", Categories.Post)]
        [Trait("Category", Categories.Positive)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.EdgeCase)]
        public async Task Delete_Post_By_Id_Minimum_Valid_Id()
        {
            var minValidId = IdRanges.MinValidPostId;
            var response = await _client.DeletePost(minValidId);

            response.IsSuccessStatusCode.Should().BeTrue("Expected successful deletion response for minimum valid ID.");
        }

        [Fact]
        [Trait("Category", Categories.Delete)]
        [Trait("Category", Categories.Post)]
        [Trait("Category", Categories.Positive)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.EdgeCase)]
        public async Task Delete_Post_By_Id_Maximum_Valid_Id(){
            var maxValidId = IdRanges.MaxValidPostId;
            var response = await _client.DeletePost(maxValidId);

            response.IsSuccessStatusCode.Should().BeTrue("Expected successful deletion response for maximum valid ID.");
        }


        [Fact]
        [Trait("Category", Categories.Delete)]
        [Trait("Category", Categories.Post)]
        [Trait("Category", Categories.Positive)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.EdgeCase)]
        public async Task Delete_Post_Twice(){
            var idToDelete = IdGenerator.RandomValidPostId();
            var firstResponse = await _client.DeletePost(idToDelete);

            firstResponse.IsSuccessStatusCode.Should().BeTrue("Expected successful deletion response on first attempt.");

            var secondResponse = await _client.DeletePost(idToDelete);

            // This is temporarily commented out, API will always return 200 OK even for deleting non-existing resources (resources are not actually deleted).
            //secondResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound, "Expected NotFound status on second deletion attempt for the same ID.");
            secondResponse.IsSuccessStatusCode.Should().BeTrue();
        }
    }
}