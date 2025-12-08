using SDET.API.Tests.Clients;
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
        public async Task Get_Post_By_Id()
        {
            var post = await _client.GetPostById(1);

            Assert.NotNull(post);
            Assert.Equal(1, post.Id);
        }

        [Fact]
        public async Task Create_Post()
        {
            // use fixture to generate post with test data
            var newPost = GeneratePost();

            var response = await _client.CreatePost(newPost);

            Assert.NotNull(response);
            Assert.Equal(newPost.Title, response.Title);
            Assert.Equal(newPost.Body, response.Body);     
        }

        [Fact]
        public async Task Update_Post()
        {
            // use fixture to generate updated post with test data
            var updatedPost = GenerateUpdatedPost(1);

            var response = await _client.UpdatePost(1, updatedPost);

            Assert.NotNull(response);
            Assert.Equal(updatedPost.Title, response.Title);
        }

        [Fact]
        public async Task Delete_Post()
        {
            var response = await _client.DeletePost(1);

            Assert.True(response.IsSuccessStatusCode);
        }
    }
}
