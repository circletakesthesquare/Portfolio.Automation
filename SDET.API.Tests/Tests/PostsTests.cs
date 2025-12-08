using SDET.API.Tests.Clients;
using SDET.API.Tests.Models;
using Xunit;
using Xunit.Abstractions;

namespace SDET.API.Tests.Tests
{
    public class PostsTests : TestBase
    {
        private readonly PostsClient _client;

        public PostsTests(ITestOutputHelper output) : base(output)
        {
            _client = new PostsClient(Logger);
        }

        [Fact]
        public async Task Can_Get_Post_By_Id()
        {
            var post = await _client.GetPost(1);

            Assert.NotNull(post);
            Assert.Equal(1, post!.Id);
        }

        [Fact]
        public async Task Can_Create_Post()
        {
            var newPost = new Post
            {
                UserId = 1,
                Title = "Automation Test Title",
                Body = "Automation Test Body"
            };

            var response = await _client.CreatePost(newPost);

            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Can_Update_Post()
        {
            var updated = new Post
            {
                Id = 1,
                UserId = 1,
                Title = "Updated Title",
                Body = "Updated Body"
            };

            var response = await _client.UpdatePost(1, updated);

            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Can_Delete_Post()
        {
            var response = await _client.DeletePost(1);

            Assert.True(response.IsSuccessStatusCode);
        }
    }
}
