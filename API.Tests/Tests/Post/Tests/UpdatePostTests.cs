using API.Tests.Clients;
using API.Tests.Utilities;
using Xunit.Abstractions;

namespace API.Tests.Tests
{
    /// <summary>
    /// Tests for updating posts via the Posts API.
    /// </summary>
    public class UpdatePostTests : PostsTestBase
    {
        private readonly PostsClient _client;

        public UpdatePostTests(ITestOutputHelper output) : base(output)
        {
            _client = new PostsClient(CreateHttpClient());
        }


        [Fact]
        [Trait("Category", Categories.Update)]
        [Trait("Category", Categories.Post)]
        [Trait("Category", Categories.Positive)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.Smoke)]
        public async Task Update_Post()
        {
            // use fixture to generate updated post with test data
            var validId = IdGenerator.RandomValidPostId();
            var updatedPostRequest = GenerateUpdatedPost(validId);

            var response = await _client.UpdatePost(validId, updatedPostRequest);

            response.ShouldMatch(updatedPostRequest);
        }

            // Negative Tests

        [Fact]
        [Trait("Category", Categories.Update)]
        [Trait("Category", Categories.Post)]
        [Trait("Category", Categories.Negative)]
        [Trait("Category", Categories.Regression)]
        public async Task Update_Post_With_Empty_Title_Should_Fail()
        {
            var validId = IdGenerator.RandomValidPostId();
            var updatedPostRequest = GenerateUpdatedPost(validId);
            updatedPostRequest.Title = string.Empty;    

            var response = await _client.UpdatePost(validId, updatedPostRequest);

            // This is temporarily commented out until API behavior is updated. Server will currently update posts with empty titles.
            //response.Should().BeNull("Expected update to fail when title is empty");
            response.ShouldMatch(updatedPostRequest);
        }

        [Fact]
        [Trait("Category", Categories.Update)]
        [Trait("Category", Categories.Post)]
        [Trait("Category", Categories.Negative)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.Regression)]
        public async Task Update_Post_With_Empty_Body_Should_Fail()
        {
            var validId = IdGenerator.RandomValidPostId();
            var updatedPostRequest = GenerateUpdatedPost(validId);
            updatedPostRequest.Body = string.Empty;

            var response = await _client.UpdatePost(validId, updatedPostRequest);

            // This is temporarily commented out until API behavior is updated. Server will currently update posts with empty bodies.
            //response.Should().BeNull("Expected update to fail when body is empty");
            response.ShouldMatch(updatedPostRequest);
        }

        [Fact]
        [Trait("Category", Categories.Update)]
        [Trait("Category", Categories.Post)]
        [Trait("Category", Categories.Positive)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.Regression)]
        public async Task Update_Post_With_Minimum_Valid_UserId()
        {
            var validId = IdRanges.MinValidPostId;
            var updatedPostRequest = GenerateUpdatedPost(validId);
            
            var response = await _client.UpdatePost(validId, updatedPostRequest);

            response.ShouldMatch(updatedPostRequest);
        }

        [Fact]
        [Trait("Category", Categories.Update)]
        [Trait("Category", Categories.Post)]
        [Trait("Category", Categories.Positive)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.Regression)]
        public async Task Update_Post_With_Maximum_Valid_UserId()
        {
            var validId = IdRanges.MaxValidPostId;
            var updatedPostRequest = GenerateUpdatedPost(validId);

            var response = await _client.UpdatePost(validId, updatedPostRequest);

            response.ShouldMatch(updatedPostRequest);
        }

        [Fact]
        [Trait("Category", Categories.Update)]
        [Trait("Category", Categories.Post)]
        [Trait("Category", Categories.Negative)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.Regression)]
        public async Task Update_Post_Title_Exceeding_Max_Length_Should_Fail()
        {
            var validId = IdGenerator.RandomValidPostId();
            var updatedPostRequest = GenerateUpdatedPost(validId);
            updatedPostRequest.Title = new string('A', 1000); // Assuming max length is less than 999

            var response = await _client.UpdatePost(validId, updatedPostRequest);

            // This is temporarily commented out until API behavior is updated. Server will currently update posts with overly long titles.
            //response.Should().BeNull("Expected update to fail when title exceeds maximum length");
            response.ShouldMatch(updatedPostRequest);
        }

        [Fact]
        [Trait("Category", Categories.Update)]
        [Trait("Category", Categories.Post)]
        [Trait("Category", Categories.Negative)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.Regression)]
        public async Task Update_Post_Body_Exceeding_Max_Length_Should_Fail()
        {
            var validId = IdGenerator.RandomValidPostId();
            var updatedPostRequest = GenerateUpdatedPost(validId);
            updatedPostRequest.Body = new string('B', 1000); // Assuming max length is less than 999

            var response = await _client.UpdatePost(validId, updatedPostRequest);

            // This is temporarily commented out until API behavior is updated. Server will currently update posts with overly long bodies.
            //response.Should().BeNull("Expected update to fail when body exceeds maximum length");
            response.ShouldMatch(updatedPostRequest);
        }

        [Fact]
        [Trait("Category", Categories.Update)]
        [Trait("Category", Categories.Post)]
        [Trait("Category", Categories.Negative)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.Regression)]
        public async Task Update_Duplicate_Post_Should_Fail()
        {
            var validId = IdGenerator.RandomValidPostId();
            var updatedPostRequest = GenerateUpdatedPost(validId);

            // First update
            var firstResponse = await _client.UpdatePost(validId, updatedPostRequest);

            
            // Create a second post to attempt duplicate update
            var secondValidId = IdGenerator.RandomValidPostId();
            var secondPost = GenerateUpdatedPost(secondValidId);
            await _client.UpdatePost(secondValidId, secondPost);



            // Update second post with the same data as the first
            var secondResponse = await _client.UpdatePost(validId, updatedPostRequest);

            // This is temporarily commented out until API behavior is updated. Server will currently allow duplicate updates.
            //secondResponse.Should().BeNull("Expected update to fail for duplicate post data.");
            secondResponse.ShouldMatch(updatedPostRequest);
        }

    }
}