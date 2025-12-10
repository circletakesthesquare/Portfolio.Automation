using FluentAssertions;
using API.Tests.Clients;
using API.Tests.Utilities;
using Xunit.Abstractions;

namespace API.Tests.Tests
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
            var validId = IdGenerator.RandomValidPostId();
            var post = await _client.GetPostById(validId);

            post.ShouldExist(validId);
        }

        [Fact]
        [Trait("Category", Categories.Create)]
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
            var validId = IdGenerator.RandomValidPostId();
            var updatedPostRequest = GenerateUpdatedPost(validId);

            var response = await _client.UpdatePost(validId, updatedPostRequest);

            response.ShouldMatch(updatedPostRequest);
        }

        [Fact]
        [Trait("Category", Categories.Delete)]
        [Trait("Category", Categories.Post)]
        [Trait("Category", Categories.Positive)]
        [Trait("Category", Categories.Integration)]
        public async Task Delete_Post()
        {   
            var idToDelete = IdGenerator.RandomValidPostId();
            var response = await _client.DeletePost(idToDelete);

            response.IsSuccessStatusCode.Should().BeTrue("Expected successful deletion response.");
        }

        [Fact]
        [Trait("Category", Categories.Get)]
        [Trait("Category", Categories.Post)]
        [Trait("Category", Categories.Negative)]
        [Trait("Category", Categories.Integration)]
        public async Task Get_Post_By_Invalid_Id_Should_Return_NotFound()
        {
            var invalidId = IdGenerator.RandomInvalidPostId();
            var response = await _client.GetPostById(invalidId);

            response.Should().BeNull("Expected null response for invalid post ID.");
        }




    }
}