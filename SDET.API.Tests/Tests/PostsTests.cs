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
            _client = new PostsClient(CreateHttpClient());
        }

        [Fact]
        public async Task Can_Get_Post_By_Id()
        {
            var post = await _client.GetPostById(1);

            Assert.NotNull(post);
            Assert.Equal(1, post.Id);
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

            Assert.NotNull(response);
            Assert.Equal("Automation Test Title", response.Title);
            Assert.Equal("Automation Test Body", response.Body);     
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

            Assert.NotNull(updated);
            Assert.Equal("Updated Title", updated!.Title);
        }

        [Fact]
        public async Task Can_Delete_Post()
        {
            var response = await _client.DeletePost(1);

            Assert.True(response.IsSuccessStatusCode);
        }
    }
}
