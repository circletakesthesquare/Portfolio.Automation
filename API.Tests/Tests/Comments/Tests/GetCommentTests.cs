using API.Tests.Clients;
using API.Tests.Utilities;
using FluentAssertions;
using Xunit.Abstractions;

namespace API.Tests.Tests
{
    /// <summary>
    /// Tests for retrieving comments via the Comments API.
    /// </summary>
    public class GetCommentTests : CommentsTestBase
    {
        public GetCommentTests(ITestOutputHelper output) : base(output)
        {
            _client = new CommentsClient(CreateHttpClient());
        }

        private readonly CommentsClient _client;

        [Fact]
        [Trait("Category", Categories.Get)]
        [Trait("Category", Categories.Comment)]
        [Trait("Category", Categories.Positive)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.Smoke)]
        public async Task Get_Comment_By_Id()
        {
            var validId = IdGenerator.RandomValidCommentId();
            var comment = await _client.GetCommentById(validId);

            comment.ShouldExist(validId);
        }

        [Fact]
        [Trait("Category", Categories.Comment)]
        [Trait("Category", Categories.Get)]
        [Trait("Category", Categories.Positive)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.Smoke)]
        public async Task Get_All_Comments()
        {
            var comments = await _client.GetAllComments();  
            comments.Should().NotBeNull("Expected a list of all comments.");

            comments!.Count.Should().BeGreaterThan(0, "Expected at least one comment in the list.");    
        }


        [Fact]
        [Trait("Category", Categories.Comment)]
        [Trait("Category", Categories.Get)]
        [Trait("Category", Categories.Positive)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.Smoke)]
        public async Task Get_Comments_By_Email()
        {
            var validEmail = "test@email.com";
            var comments = await _client.GetCommentsByEmail(validEmail);

            comments.Should().NotBeNull("Expected comments for valid email.");
            comments!.All(c => c.Email == validEmail).Should().BeTrue("All comments should belong to the requested email.");
        }

        [Fact]
        [Trait("Category", Categories.Comment)]
        [Trait("Category", Categories.Get)]
        [Trait("Category", Categories.Positive)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.Smoke)]
        public async Task Get_Comments_By_PostId()
        {
            var validPostId = IdGenerator.RandomValidPostId();
            var comments = await _client.GetCommentsByPostId(validPostId);

            comments.Should().NotBeNull("Expected comments for valid Post ID.");
            comments!.All(c => c.PostId == validPostId).Should().BeTrue("All comments should belong to the requested Post ID.");
        }

        [Fact]
        [Trait("Category", Categories.Comment)]
        [Trait("Category", Categories.Get)]
        [Trait("Category", Categories.Positive)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.EdgeCase)]
        public async Task Get_Comment_By_Id_Minimum_Valid_Id()
        {
            var minValidId = IdRanges.MinValidCommentId;
            var comment = await _client.GetCommentById(minValidId);

            comment.ShouldExist(minValidId);
        }

        [Fact]
        [Trait("Category", Categories.Comment)]
        [Trait("Category", Categories.Get)]
        [Trait("Category", Categories.Positive)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.EdgeCase)]
        public async Task Get_Comment_By_Id_Maximum_Valid_Id()
        {
            var maxValidId = IdRanges.MaxValidCommentId;
            var comment = await _client.GetCommentById(maxValidId);

            comment.ShouldExist(maxValidId);
        }

        // Negative Tests

        [Fact]
        [Trait("Category", Categories.Comment)]
        [Trait("Category", Categories.Get)]
        [Trait("Category", Categories.Negative)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.Regression)]
        public async Task Get_Comment_By_Invalid_Id_Should_Return_NotFound()
        {
            var invalidId = IdGenerator.RandomInvalidCommentId();
            var response = await _client.GetCommentById(invalidId);

            response.Should().BeNull("Expected null response for invalid comment ID.");
        }

        [Fact]
        [Trait("Category", Categories.Comment)]
        [Trait("Category", Categories.Get)]
        [Trait("Category", Categories.Negative)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.Regression)]
        public async Task Get_Comments_By_Invalid_PostId_Should_Return_Empty_List()
        {
            var invalidPostId = IdGenerator.RandomInvalidPostId();
            var response = await _client.GetCommentsByPostId(invalidPostId);

            response.Should().NotBeNull("Expected empty list for invalid Post ID.");
        }        
    }
}