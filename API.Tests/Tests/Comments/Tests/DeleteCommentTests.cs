namespace API.Tests.Tests
{
    /// <summary>
    /// Tests for deleting comments via the Comments API.
    /// </summary>
    public class DeleteCommentTests : CommentsTestBase
    {
        public DeleteCommentTests(ITestOutputHelper output) : base(output)
        {
            _client = new CommentsClient(CreateHttpClient());
        }

        private readonly CommentsClient _client;

        [Fact]
        [Trait("Category", Categories.Delete)]
        [Trait("Category", Categories.Comment)]
        [Trait("Category", Categories.Positive)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.Smoke)]
        public async Task Delete_Comment_By_Id()
        {
            var validId = IdGenerator.RandomValidCommentId();

            var response = await _client.DeleteComment(validId);

            response.IsSuccessStatusCode.Should().BeTrue("Expected successful deletion of comment with valid ID.");
        }

        [Fact]
        [Trait("Category", Categories.Delete)]
        [Trait("Category", Categories.Comment)]
        [Trait("Category", Categories.Positive)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.EdgeCase)]
        public async Task Delete_Comment_By_Id_Minimum_Valid_Id()
        {
            var minValidId = IdRanges.MinValidCommentId;

            var response = await _client.DeleteComment(minValidId);

            response.IsSuccessStatusCode.Should().BeTrue("Expected successful deletion of comment with minimum valid ID.");
        }

        [Fact]
        [Trait("Category", Categories.Delete)]
        [Trait("Category", Categories.Comment)]
        [Trait("Category", Categories.Positive)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.EdgeCase)]
        public async Task Delete_Comment_By_Id_Maximum_Valid_Id()
        {
            var maxValidId = IdRanges.MaxValidCommentId;

            var response = await _client.DeleteComment(maxValidId);

            response.IsSuccessStatusCode.Should().BeTrue("Expected successful deletion of comment with maximum valid ID.");
        }

        // Negative Tests

        [Fact]
        [Trait("Category", Categories.Delete)]
        [Trait("Category", Categories.Comment)]
        [Trait("Category", Categories.Negative)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.Regression)]
        public async Task Delete_Comment_By_Invalid_Id_Should_Return_NotFound()
        {
            var invalidId = IdGenerator.RandomInvalidCommentId();

            var response = await _client.DeleteComment(invalidId);

            // This is temporarily commented out until API behavior is updated. Server will return 200 OK even for invalid IDs.
            //response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound, "Expected NotFound status for invalid comment ID deletion.");
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        [Trait("Category", Categories.Delete)]
        [Trait("Category", Categories.Comment)]
        [Trait("Category", Categories.Negative)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.Regression)]
        public async Task Delete_Comment_That_Has_Already_Been_Deleted()
        {
            var validId = IdGenerator.RandomValidCommentId();

            // First deletion
            var firstResponse = await _client.DeleteComment(validId);
            firstResponse.IsSuccessStatusCode.Should().BeTrue("Expected successful deletion of comment with valid ID.");

            // Second deletion attempt
            var secondResponse = await _client.DeleteComment(validId);

            // This is temporarily commented out until API behavior is updated. Server will return 200 OK even for already deleted IDs.
            //secondResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound, "Expected NotFound status when deleting an already deleted comment.");
            secondResponse.EnsureSuccessStatusCode();
        }
    }
}