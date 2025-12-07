using System.Text;
using System.Text.Json;
using SDET.API.Tests.Models;
using SDET.API.Tests.Utilities;
using Serilog;

namespace SDET.API.Tests.Clients
{
    public class PostsClient
    {
        private readonly HttpClient _client;

        public PostsClient(ILogger logger)
        {
            var handler = new LoggingHttpHandler(logger);
            _client = new HttpClient(handler)
            {
                BaseAddress = new Uri(Config.BaseUrl),
                Timeout = TimeSpan.FromSeconds(Convert.ToInt32(Config.TimeoutSeconds))
            };
        }

        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<PostModel?> GetPost(int id)
        {
            var response = await _client.GetAsync($"/posts/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<PostModel>(content, _jsonOptions);
        }

        public async Task<HttpResponseMessage> CreatePost(PostModel post)
        {
            var json = JsonSerializer.Serialize(post, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await _client.PostAsync("/posts", content);
        }

        public async Task<HttpResponseMessage> UpdatePost(int id, PostModel post)
        {
            var json = JsonSerializer.Serialize(post, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await _client.PutAsync($"/posts/{id}", content);
        }

        public async Task<HttpResponseMessage> DeletePost(int id)
        {
            return await _client.DeleteAsync($"/posts/{id}");
        }
    }
}