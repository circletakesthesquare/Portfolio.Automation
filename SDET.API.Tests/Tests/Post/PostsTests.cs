using FluentAssertions;
using SDET.API.Tests.Clients;
using SDET.API.Tests.Utilities;
using Xunit.Abstractions;

namespace SDET.API.Tests.Tests
{
    public class PostsTests : PostsTestBase
    {
        private readonly PostsClient _client;

        public PostsTests(ITestOutputHelper output) : base(output)
        {
            _client = new PostsClient(CreateHttpClient());
        }

        [Fact]
        [Trait("Category", Categories.Get)]
        [Trait("Category", Categories.Post)]
        [Trait("Category", Categories.Positive)]
        [Trait("Category", Categories.Integration)]
        public async Task Get_Post_By_Id()
        {
            var randomId = new Random().Next(1,101); // assuming IDs are between 1 and 100
            var post = await _client.GetPostById(randomId);

            post.ShouldExist(randomId);
        }

        [Fact]
        [Trait("Category", Categories.Post)]
        [Trait("Category", Categories.Post)]
        [Trait("Category", Categories.Positive)]
        [Trait("Category", Categories.Integration)]
        public async Task Create_Post()
        {
            // use fixture to generate post with test data
            var newPostRequest = GeneratePost();

            var response = await _client.CreatePost(newPostRequest);

            response.ShouldMatch(newPostRequest);
        }

        [Fact]
        [Trait("Category", Categories.Update)]
        [Trait("Category", Categories.Post)]
        [Trait("Category", Categories.Positive)]
        [Trait("Category", Categories.Integration)]
        public async Task Update_Post()
        {
            // use fixture to generate updated post with test data
            var updatedPostRequest = GenerateUpdatedPost(1);

            var response = await _client.UpdatePost(1, updatedPostRequest);

            response.ShouldMatch(updatedPostRequest);
        }

        [Fact]
        [Trait("Category", Categories.Delete)]
        [Trait("Category", Categories.Post)]
        [Trait("Category", Categories.Positive)]
        [Trait("Category", Categories.Integration)]
        public async Task Delete_Post()
        {   
            var idToDelete = new Random().Next(1,101); // assuming IDs are between 1 and 100
            var response = await _client.DeletePost(idToDelete);

            response.IsSuccessStatusCode.Should().BeTrue("Expected successful deletion response.");

            var deletedPost = await _client.GetPostById(idToDelete);
            deletedPost.Should().BeNull("Expected post to be deleted and not found.");
        }
    }
}