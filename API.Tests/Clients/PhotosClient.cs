namespace API.Tests.Clients
{
    /// <summary>
    /// Client for interacting with the Photos API.
    /// </summary>
    public class PhotosClient
    {
        private readonly HttpClient _client;

        public PhotosClient(HttpClient client)
        {
            _client = client;
        }

        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        /// <summary>
        /// Retrieves a photo by its ID.
        /// </summary>
        /// <param name="id">The ID of the photo to retrieve.</param>
        /// <returns>The photo if found; otherwise, null.</returns>
        public async Task<Photo?> GetPhotoById(int id)
        {
            var response = await _client.GetAsync(Endpoints.GetPhoto(id));

            // Return null if the photo wasn't found
            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            // Throw for other unexpected non-success codes
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Photo>(content, _jsonOptions);
        }

        /// <summary>
        /// Retrieves a photo by its title.
        /// </summary>
        /// <param name="title">The title of the photo to retrieve.</param>
        /// <returns>The photo if found; otherwise, null.</returns>
        public async Task<Photo?> GetPhotoByTitle(string title)
        {
            var response = await _client.GetAsync(Endpoints.GetAllPhotos);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var photos = JsonSerializer.Deserialize<List<Photo>>(content, _jsonOptions);

            return photos?.FirstOrDefault(p => p.Title == title);
        }

        /// <summary>
        /// Retrieves a photo by its URL.
        /// </summary>
        /// <param name="url">The URL of the photo to retrieve.</param>
        /// <returns>The photo if found; otherwise, null.</returns>
        public async Task<Photo?> GetPhotoByUrl(string url)
        {
            var response = await _client.GetAsync(Endpoints.GetAllPhotos);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var photos = JsonSerializer.Deserialize<List<Photo>>(content, _jsonOptions);

            return photos?.FirstOrDefault(p => p.Url == url);
        }

        /// <summary>
        /// Retrieves a photo by its thumbnail URL.
        /// </summary>
        /// <param name="thumbnailUrl">The thumbnail URL of the photo to retrieve.</param>
        /// <returns>The photo if found; otherwise, null.</returns>
        public async Task<Photo?> GetPhotoByThumbnailUrl(string thumbnailUrl)
        {
            var response = await _client.GetAsync(Endpoints.GetAllPhotos);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var photos = JsonSerializer.Deserialize<List<Photo>>(content, _jsonOptions);

            return photos?.FirstOrDefault(p => p.ThumbnailUrl == thumbnailUrl);
        }

        /// <summary>
        /// Creates a new photo.
        /// </summary>
        /// <param name="photo">The photo to create.</param>
        /// <returns>The created photo if successful; otherwise, null.</returns>
        public async Task<Photo?> CreatePhoto(Photo photo)
        {
            var json = JsonSerializer.Serialize(photo, _jsonOptions);
            var requestBody = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(Endpoints.CreatePhoto, requestBody);

            // Return null if the request fails
            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Photo?>(content, _jsonOptions);
        }

        /// <summary>
        /// Updates an existing photo.
        /// </summary>
        /// <param name="id">The ID of the photo to update.</param>
        /// <param name="photo">The updated photo data.</param>
        /// <returns>The updated photo if successful; otherwise, null.</returns>
        public async Task<Photo?> UpdatePhoto(int id, Photo photo)
        {
            var json = JsonSerializer.Serialize(photo, _jsonOptions);
            var requestBody = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(Endpoints.UpdatePhoto(id), requestBody);

            // Return null if the photo wasn't found
            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            // Throw for other unexpected non-success codes
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Photo?>(content, _jsonOptions);
        }
    }
}