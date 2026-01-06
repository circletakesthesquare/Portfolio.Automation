using System.Net;
using System.Text;
using System.Text.Json;
using Api.Tests.Models;
using API.Tests.Utilities;

namespace API.Tests.Clients
{
    /// <summary>
    /// Client for interacting with Albums endpoints of the API.
    /// </summary>
    public class AlbumsClient
    {
        private readonly HttpClient _client;

        public AlbumsClient(HttpClient client)
        {
            _client = client;
        }

        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        /// <summary>
        /// Retrieves an album by its ID.
        /// </summary>
        /// <param name="id">The ID of the album to retrieve.</param>
        /// <returns>The album if found, otherwise null.</returns>
        public async Task<Album?> GetAlbumById(int id)
        {
            var response = await _client.GetAsync(Endpoints.GetAlbum(id));

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            // Throw for other unexpected non-success codes
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Album>(content, _jsonOptions);   
        }

        /// <summary>
        /// Retrieves albums by User ID.
        /// </summary>
        /// <param name="userId">The ID of the user whose albums to retrieve.</param>
        /// <returns>The list of albums if found, otherwise null.</returns>
        public async Task<List<Album>?> GetAlbumsByUserId(int userId)
        {
            var response = await _client.GetAsync(Endpoints.GetAlbumsByUserId(userId));

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Album>>(content, _jsonOptions);
        }

        /// <summary>
        /// Retrieves all albums.
        /// </summary>
        /// <returns>The list of albums if found, otherwise null.</returns>
        public async Task<List<Album>?> GetAllAlbums()
        {
            var response = await _client.GetAsync(Endpoints.GetAllAlbums);
            
            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Album>>(content, _jsonOptions);
        }

        /// <summary>
        /// Creates a new album.
        /// </summary>
        /// <param name="album">The album to create.</param>
        /// <returns>The created album if successful, otherwise null.</returns>
        public async Task<Album?> CreateAlbum(Album album)
        {
            var json = JsonSerializer.Serialize(album, _jsonOptions);
            var requestBody = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(Endpoints.CreateAlbum, requestBody);

            // Return null if the request fails
            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Album?>(content, _jsonOptions);
        }

        /// <summary>
        /// Updates an existing album.
        /// </summary>
        /// <param name="id">The ID of the album to update.</param>
        /// <param name="album">The updated album data.</param>
        /// <returns>The updated album if successful, otherwise null.</returns>
        public async Task<Album?> UpdateAlbum(int id, Album album)
        {
            var json = JsonSerializer.Serialize(album, _jsonOptions);
            var requestBody = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(Endpoints.UpdateAlbum(id), requestBody);

            // Return null if the album wasn't found
            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            // Throw for other unexpected non-success codes
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Album?>(content, _jsonOptions);
        }

        /// <summary>
        /// Deletes an album by its ID.
        /// </summary>
        /// <param name="id">The ID of the album to delete</param>
        /// <returns>The HTTP response message.</returns>
        /// <exception cref="HttpRequestException">Thrown when the delete operation fails.</exception>
        public async Task<HttpResponseMessage> DeleteAlbum(int id)
        {
            var response = await _client.DeleteAsync(Endpoints.DeleteAlbum(id));

            return response.IsSuccessStatusCode 
            ? response 
            : throw new HttpRequestException($"Failed to delete album with ID {id}. Status code: {response.StatusCode}");
        }
    }
}