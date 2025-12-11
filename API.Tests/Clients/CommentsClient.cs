using System.Net;
using System.Text;
using System.Text.Json;
using API.Tests.Models;
using API.Tests.Utilities;

namespace API.Tests.Clients
{
    /// <summary>
    /// Client for interacting with the Comments API.
    /// </summary>
    public class CommentsClient
    {
        private readonly HttpClient _client;

        public CommentsClient(HttpClient client)
        {
            _client = client;
        }

        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        /// <summary>
        /// Retrieves a comment by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The comment if found, otherwise null.</returns>
        public async Task<Comment?> GetCommentById(int id)
        {
            var response = await _client.GetAsync(Endpoints.GetComment(id));
            
            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Comment>(content, _jsonOptions);
        }

        /// <summary>
        /// Retrieves comments by Post ID.
        /// </summary>
        /// <param name="postId"></param>
        /// <returns>The list of comments if found, otherwise null.</returns>
        public async Task<List<Comment>?> GetCommentsByPostId(int postId)
        {
            var response = await _client.GetAsync(Endpoints.GetCommentsByPostId(postId));
            
            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Comment>>(content, _jsonOptions);
        }

        /// <summary>
        /// Retrieves all comments.
        /// </summary>
        /// <returns>The list of comments if found, otherwise null.</returns>
        public async Task<List<Comment>?> GetAllComments()
        {
            var response = await _client.GetAsync(Endpoints.GetAllComments);
            
            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Comment>>(content, _jsonOptions);
        }

        /// <summary>
        /// Retrieves comments by email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>The list of comments if found, otherwise null.</returns>
        public async Task<List<Comment>?> GetCommentsByEmail(string email)
        {
            var response = await _client.GetAsync(Endpoints.GetCommentsByEmail(email));
            
            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Comment>>(content, _jsonOptions);
        }
        
        /// <summary>
        /// Creates a new comment.
        /// </summary>
        /// <param name="comment"></param>
        /// <returns>The created comment if successful, otherwise null.</returns>
        /// </summary>
        /// <param name="comment"></param>
        /// <returns>The created comment if successful, otherwise null.</returns>
        public async Task<Comment?> CreateComment(Comment comment)
        {
            var json = JsonSerializer.Serialize(comment, _jsonOptions);
            var requestBody = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(Endpoints.CreateComment, requestBody);

            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Comment>(content, _jsonOptions);
        }

        /// <summary>
        /// Updates an existing comment.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="comment"></param>
        /// <returns>The updated comment if successful, otherwise null.</returns>
        public async Task<Comment?> UpdateComment(int id, Comment comment)
        {
            var json = JsonSerializer.Serialize(comment, _jsonOptions);
            var requestBody = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(Endpoints.UpdateComment(id), requestBody);

            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Comment>(content, _jsonOptions);
        }

        /// <summary>
        /// Deletes a comment by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The HTTP response message.</returns>
        /// <exception cref="HttpRequestException">Thrown when the delete operation fails.</exception>
        public async Task<HttpResponseMessage> DeleteComment(int id)
        {
            var response = await _client.DeleteAsync(Endpoints.DeleteComment(id));
            return response.IsSuccessStatusCode 
            ? response 
            : throw new HttpRequestException($"Failed to delete comment with ID {id}. Status code: {response.StatusCode}");
        }

    }

}