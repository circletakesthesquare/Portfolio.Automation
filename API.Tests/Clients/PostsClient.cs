using System.Net;
using System.Text;
using System.Text.Json;
using API.Tests.Models;
using API.Tests.Utilities;

namespace API.Tests.Clients
{
    /// <summary>
    /// Client for interacting with the Posts API.
    /// </summary>
    public class PostsClient
    {
        private readonly HttpClient _client;

        public PostsClient(HttpClient client)
        {
         
            _client = client;
        }

        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        /// <summary>
        /// Retrieves a post by its ID.
        /// </summary>
        /// <param name="id">The ID of the post to retrieve.</param>
        /// <returns>The post if found; otherwise, null.</returns>
        public async Task<Post?> GetPostById(int id)
        {
            var response = await _client.GetAsync(Endpoints.GetPost(id));
            
            // Return null if the post wasn't found
            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            // Throw for other unexpected non-success codes
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Post>(content, _jsonOptions);
        }
        
        /// <summary>
        /// Creates a new post.
        /// </summary>
        /// <param name="post">The post to create.</param>
        /// <returns>The created Post, or null if deserialization fails.</returns>
        public async Task<Post?> CreatePost(Post post)
        {
            var json = JsonSerializer.Serialize(post, _jsonOptions);
            var requestBody = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(Endpoints.CreatePost, requestBody);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Post?>(content, _jsonOptions);
        }

        /// <summary>
        /// Updates an existing post.
        /// </summary>
        /// <param name="id">The ID of the post to update.</param>
        /// <param name="post">The updated post data.</param>
        /// <returns>The updated Post, or null if deserialization fails.</returns>
        public async Task<Post?> UpdatePost(int id, Post post)
        {
            var json = JsonSerializer.Serialize(post, _jsonOptions);
            var requestBody = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(Endpoints.UpdatePost(id), requestBody);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Post?>(content, _jsonOptions);
        }

        /// <summary>
        /// Deletes a post by its ID.
        /// </summary>
        /// <param name="id">The ID of the post to delete.</param>
        /// <returns>The HTTP response message.</returns>
        /// <exception cref="HttpRequestException">Thrown when the delete operation fails.</exception>
        public async Task<HttpResponseMessage> DeletePost(int id)
        {
            var response = await _client.DeleteAsync(Endpoints.DeletePost(id));
            return response.IsSuccessStatusCode
                ? response
                : throw new HttpRequestException($"Failed to delete post with ID {id}. Status code: {response.StatusCode}");
        }
    }
}