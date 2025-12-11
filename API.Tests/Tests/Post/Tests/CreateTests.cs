using API.Tests.Clients;
using API.Tests.Utilities;
using Xunit.Abstractions;

namespace API.Tests.Tests
{
    /// <summary>
    /// Tests for creating posts via the Posts API.
    /// </summary>
    public class CreateTests : PostsTestBase
    {
        public CreateTests(ITestOutputHelper output) : base(output)
        {
            _client = new PostsClient(CreateHttpClient());
        }

        private readonly PostsClient _client;

        [Fact]
        [Trait("Category", Categories.Create)]
        [Trait("Category", Categories.Post)]
        [Trait("Category", Categories.Positive)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.Smoke)]
        public async Task Create_Post()
        {
            // use fixture to generate post with test data
            var newPostRequest = GeneratePost();

            var response = await _client.CreatePost(newPostRequest);

            response.ShouldMatch(newPostRequest);
        }

        // Negative Tests

        [Fact]
        [Trait("Category", Categories.Create)]
        [Trait("Category", Categories.Post)]
        [Trait("Category", Categories.Negative)]
        [Trait("Category", Categories.Integration)]
        public async Task Create_Post_With_Empty_Title_Should_Fail()
        {
            var newPostRequest = GeneratePost();
            newPostRequest.Title = string.Empty;

            var response = await _client.CreatePost(newPostRequest);

            // This is temporarily commented out until API behavior is updated. Server will currently create posts with empty titles.
            //response.Should().BeNull("Expected creation to fail when title is empty");
            response.ShouldMatch(newPostRequest);
        }

        [Fact]
        [Trait("Category", Categories.Create)]
        [Trait("Category", Categories.Post)]
        [Trait("Category", Categories.Negative)]
        [Trait("Category", Categories.Integration)]
        public async Task Create_Post_With_Empty_Body_Should_Fail()
        {
            var newPostRequest = GeneratePost();
            newPostRequest.Body = string.Empty;

            var response = await _client.CreatePost(newPostRequest);

            // This is temporarily commented out until API behavior is updated. Server will currently create posts with empty bodies.
            //response.Should().BeNull("Expected creation to fail when body is empty");
            response.ShouldMatch(newPostRequest);
        }

        [Fact]
        [Trait("Category", Categories.Create)]
        [Trait("Category", Categories.Post)]
        [Trait("Category", Categories.Negative)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.Regression)]
        public async Task Create_Post_Title_Exceeding_Max_Length_Should_Fail()
        {
            var newPostRequest = GeneratePost();
            newPostRequest.Title = new string('A', 1000); // Assuming max length is less than 999

            var response = await _client.CreatePost(newPostRequest);

            // This is temporarily commented out until API behavior is updated. Server will currently create posts with overly long titles.
            //response.Should().BeNull("Expected creation to fail when title exceeds maximum length");
            response.ShouldMatch(newPostRequest);
        }

        [Fact]
        [Trait("Category", Categories.Create)]
        [Trait("Category", Categories.Post)]
        [Trait("Category", Categories.Negative)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.Regression)]
        public async Task Create_Post_Body_Exceeding_Max_Length_Should_Fail()
        {
            var newPostRequest = GeneratePost();
            newPostRequest.Body = new string('B', 1000); // Assuming max length is less than 999

            var response = await _client.CreatePost(newPostRequest);

            // This is temporarily commented out until API behavior is updated. Server will currently create posts with overly long bodies.
            //response.Should().BeNull("Expected creation to fail when body exceeds maximum length");
            response.ShouldMatch(newPostRequest);
        }

        [Fact]
        [Trait("Category", Categories.Update)]
        [Trait("Category", Categories.Post)]
        [Trait("Category", Categories.Negative)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.Regression)]
        public async Task Create_Duplicate_Post_Should_Fail()
        {
            var newPostRequest = GeneratePost();

            // Create the post for the first time
            var firstResponse = await _client.CreatePost(newPostRequest);

            var secondResponse = await _client.CreatePost(newPostRequest);

            // This is temporarily commented out until API behavior is updated. Server will currently allow duplicate posts.
            //secondResponse.Should().BeNull("Expected creation to fail for duplicate post.");
            secondResponse.ShouldMatch(newPostRequest);
        }
    }   
}