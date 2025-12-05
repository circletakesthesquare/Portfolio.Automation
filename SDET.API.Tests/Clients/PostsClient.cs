using System;
using System.Net.Http.Json;
using SDET.API.Tests.Models;
using SDET.API.Tests.Utilities;

namespace SDET.API.Tests.Clients
{
    public class PostsClient
    {
        private readonly HttpClient _client;

        public PostsClient()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(ConfigReader.Get("BaseUrl")),
                Timeout = TimeSpan.FromSeconds(Convert.ToInt32(ConfigReader.Get("TimeoutSeconds")))
            };
        }

        public async Task<PostModel?> GetPost(int id)
        {
            return await _client.GetFromJsonAsync<PostModel>($"/posts/{id}");
        }

        public async Task<HttpResponseMessage> CreatePost(PostModel post)
        {
            return await _client.PostAsJsonAsync("/posts", post);
        }

        public async Task<HttpResponseMessage> UpdatePost(int id, PostModel post)
        {
            return await _client.PutAsJsonAsync($"/posts/{id}", post);
        }

        public async Task<HttpResponseMessage> DeletePost(int id)
        {
            return await _client.DeleteAsync($"/posts/{id}");
        }
    }
}
