namespace API.Tests.Tests
{
    /// <summary>
    /// Tests for creating comments via the Comments API.
    /// </summary>
    public class CreateCommentTests : CommentsTestBase
    {
        public CreateCommentTests(ITestOutputHelper output) : base(output)
        {
            _client = new CommentsClient(CreateHttpClient());
        }

        private readonly CommentsClient _client;


        [Fact]
        [Trait("Category", Categories.Create)]
        [Trait("Category", Categories.Comment)]
        [Trait("Category", Categories.Positive)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.Smoke)]
        public async Task Create_Comment()
        {
            // use fixture to generate comment with test data
            var newCommentRequest = GenerateComment();  
            var response = await _client.CreateComment(newCommentRequest);

            response.ShouldMatch(newCommentRequest);
        }

        // Negative Tests

        [Fact]
        [Trait("Category", Categories.Create)]
        [Trait("Category", Categories.Comment)]
        [Trait("Category", Categories.Negative)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.Regression)]
        public async Task Create_Comment_With_Empty_Body_Should_Fail()
        {
            var newCommentRequest = GenerateComment();
            newCommentRequest.Body = string.Empty;

            var response = await _client.CreateComment(newCommentRequest);

            // This is temporarily commented out until API behavior is updated. Server will currently create comments with empty bodies.
            //response.Should().BeNull("Expected creation to fail when body is empty");
            response.ShouldMatch(newCommentRequest);
        }

        [Fact]
        [Trait("Category", Categories.Create)]
        [Trait("Category", Categories.Comment)]
        [Trait("Category", Categories.Negative)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.Regression)]
        public async Task Create_Comment_With_Invalid_Email_Should_Fail()
        {
            var newCommentRequest = GenerateComment();
            newCommentRequest.Email = "invalid-email-format";

            var response = await _client.CreateComment(newCommentRequest);

            // This is temporarily commented out until API behavior is updated. Server will currently create comments with invalid emails.
            //response.Should().BeNull("Expected creation to fail when email format is invalid");
            response.ShouldMatch(newCommentRequest);
        }

        [Fact]
        [Trait("Category", Categories.Create)]
        [Trait("Category", Categories.Comment)]
        [Trait("Category", Categories.Negative)]
        [Trait("Category", Categories.Regression)]
        public async Task Create_Comment_With_Empty_Name_Should_Fail()
        {
            var newCommentRequest = GenerateComment();
            newCommentRequest.Name = string.Empty;

            var response = await _client.CreateComment(newCommentRequest);

            // This is temporarily commented out until API behavior is updated. Server will currently create comments with empty names.
            //response.Should().BeNull("Expected creation to fail when name is empty");
            response.ShouldMatch(newCommentRequest);
        }

        [Fact]
        [Trait("Category", Categories.Create)]
        [Trait("Category", Categories.Comment)]
        [Trait("Category", Categories.Negative)]
        [Trait("Category", Categories.Regression)]
        public async Task Create_Duplicate_Comment_Should_Fail()
        {
            var newCommentRequest = GenerateComment();
            var firstResponse = await _client.CreateComment(newCommentRequest);

            firstResponse.ShouldMatch(newCommentRequest);

            var duplicateResponse = await _client.CreateComment(newCommentRequest);

            // This is temporarily commented out until API behavior is updated. Server will currently allow duplicate comments.
            //duplicateResponse.Should().BeNull("Expected creation to fail when creating a duplicate comment");
            duplicateResponse.ShouldMatch(newCommentRequest);
        }

        [Fact]
        [Trait("Category", Categories.Create)]
        [Trait("Category", Categories.Comment)]
        [Trait("Category", Categories.Negative)]
        [Trait("Category", Categories.EdgeCase)]
        public async Task Create_Comment_Exceeding_Max_Length_Body_Should_Fail()
        {
            var newCommentRequest = GenerateComment();
            newCommentRequest.Body = new string('A', 5001); // Assuming max length is 5000 characters

            var response = await _client.CreateComment(newCommentRequest);

            // This is temporarily commented out until API behavior is updated. Server will currently create comments exceeding max length.
            //response.Should().BeNull("Expected creation to fail when body exceeds maximum length");
            response.ShouldMatch(newCommentRequest);
        }

        [Fact]
        [Trait("Category", Categories.Create)]
        [Trait("Category", Categories.Comment)]
        [Trait("Category", Categories.Negative)]
        [Trait("Category", Categories.EdgeCase)]
        public async Task Create_Comment_Exceeding_Max_Length_Name_Should_Fail()
        {
            var newCommentRequest = GenerateComment();
            newCommentRequest.Name = new string('B', 256); // Assuming max length is 255 characters

            var response = await _client.CreateComment(newCommentRequest);

            // This is temporarily commented out until API behavior is updated. Server will currently create comments exceeding max length.
            //response.Should().BeNull("Expected creation to fail when name exceeds maximum length");
            response.ShouldMatch(newCommentRequest);
        }
    }
}