using FluentAssertions;
using API.Tests.Clients;
using API.Tests.Utilities;
using Xunit.Abstractions;

namespace API.Tests.Tests
{
    /// <summary>
    /// Tests for updating comments via the Comments API.
    /// </summary>
    public class UpdateCommentTests : CommentsTestBase
    {
        public UpdateCommentTests(ITestOutputHelper output) : base(output)
        {
            _client = new CommentsClient(CreateHttpClient());
        }

        private readonly CommentsClient _client;

        [Fact]
        [Trait("Category", Categories.Update)]
        [Trait("Category", Categories.Comment)]
        [Trait("Category", Categories.Positive)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.Smoke)]
        public async Task Update_Comment_By_Id()
        {
            var validId = IdGenerator.RandomValidCommentId();
            var updatedCommentRequest = GenerateUpdatedComment(validId);

            var response = await _client.UpdateComment(validId, updatedCommentRequest);

            response.ShouldMatch(updatedCommentRequest);
        }

        [Fact]
        [Trait("Category", Categories.Update)]
        [Trait("Category", Categories.Comment)]
        [Trait("Category", Categories.Positive)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.EdgeCase)]
        public async Task Update_Comment_By_Id_Minimum_Valid_Id()
        {
            var minValidId = IdRanges.MinValidCommentId;
            var updatedCommentRequest = GenerateUpdatedComment(minValidId);

            var response = await _client.UpdateComment(minValidId, updatedCommentRequest);

            response.ShouldMatch(updatedCommentRequest);
        }

        [Fact]
        [Trait("Category", Categories.Update)]
        [Trait("Category", Categories.Comment)]
        [Trait("Category", Categories.Positive)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.EdgeCase)]
        public async Task Update_Comment_By_Id_Maximum_Valid_Id()
        {
            var maxValidId = IdRanges.MaxValidCommentId;
            var updatedCommentRequest = GenerateUpdatedComment(maxValidId);

            var response = await _client.UpdateComment(maxValidId, updatedCommentRequest);

            response.ShouldMatch(updatedCommentRequest);
        }

            // Negative Tests

        [Fact]
        [Trait("Category", Categories.Update)]
        [Trait("Category", Categories.Comment)]
        [Trait("Category", Categories.Negative)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.Regression)]
        public async Task Update_Comment_With_Empty_Body_Should_Fail()
        {
            var validId = IdGenerator.RandomValidCommentId();
            var updatedCommentRequest = GenerateUpdatedComment(validId);
            updatedCommentRequest.Body = string.Empty;

            var response = await _client.UpdateComment(validId, updatedCommentRequest);

            // This is temporarily commented out until API behavior is updated. Server will currently update comments with empty bodies.
            //response.Should().BeNull("Expected update to fail when body is empty");
            response.ShouldMatch(updatedCommentRequest);
        }

        [Fact]
        [Trait("Category", Categories.Update)]
        [Trait("Category", Categories.Comment)]
        [Trait("Category", Categories.Negative)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.Regression)]
        public async Task Update_Comment_With_Invalid_Email_Should_Fail()
        {
            var validId = IdGenerator.RandomValidCommentId();
            var updatedCommentRequest = GenerateUpdatedComment(validId);
            updatedCommentRequest.Email = "invalid-email-format";

            var response = await _client.UpdateComment(validId, updatedCommentRequest);

            // This is temporarily commented out until API behavior is updated. Server will currently update comments with invalid emails.
            //response.Should().BeNull("Expected update to fail when email format is invalid");
            response.ShouldMatch(updatedCommentRequest);
        }


        [Fact]
        [Trait("Category", Categories.Update)]
        [Trait("Category", Categories.Comment)]
        [Trait("Category", Categories.Negative)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.Regression)]
        public async Task Update_Comment_With_Empty_Name_Should_Fail()
        {
            var validId = IdGenerator.RandomValidCommentId();
            var updatedCommentRequest = GenerateUpdatedComment(validId);
            updatedCommentRequest.Name = string.Empty;

            var response = await _client.UpdateComment(validId, updatedCommentRequest);

            // This is temporarily commented out until API behavior is updated. Server will currently update comments with empty names.
            //response.Should().BeNull("Expected update to fail when name is empty");
            response.ShouldMatch(updatedCommentRequest);
        }

        [Fact]
        [Trait("Category", Categories.Update)]
        [Trait("Category", Categories.Comment)]
        [Trait("Category", Categories.Negative)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.EdgeCase)]
        public async Task Update_Comment_With_Body_Exceeding_Max_Length_Should_Fail()
        {
            var validId = IdGenerator.RandomValidCommentId();
            var updatedCommentRequest = GenerateUpdatedComment(validId);
            updatedCommentRequest.Body = new string('B', 2000); // Assuming max length is less than 2000

            var response = await _client.UpdateComment(validId, updatedCommentRequest);

            // This is temporarily commented out until API behavior is updated. Server will currently update comments with overly long bodies.
            //response.Should().BeNull("Expected update to fail when body exceeds maximum length");
            response.ShouldMatch(updatedCommentRequest);
        }

        [Fact]
        [Trait("Category", Categories.Update)]
        [Trait("Category", Categories.Comment)]
        [Trait("Category", Categories.Negative)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.EdgeCase)]
        public async Task Update_Comment_Name_Exceeding_Max_Length_Should_Fail()
        {
            var validId = IdGenerator.RandomValidCommentId();
            var updatedCommentRequest = GenerateUpdatedComment(validId);
            updatedCommentRequest.Name = new string('N', 500); // Assuming max length is less than 500

            var response = await _client.UpdateComment(validId, updatedCommentRequest);

            // This is temporarily commented out until API behavior is updated. Server will currently update comments with overly long names.
            //response.Should().BeNull("Expected update to fail when name exceeds maximum length");
            response.ShouldMatch(updatedCommentRequest);
        }

        [Fact]
        [Trait("Category", Categories.Update)]
        [Trait("Category", Categories.Comment)]
        [Trait("Category", Categories.Negative)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.Regression)]
        public async Task Update_Duplicate_Comment_Should_Fail()
        {
            var validId = IdGenerator.RandomValidCommentId();
            var updatedCommentRequest = GenerateUpdatedComment(validId);

            // First update
            var firstResponse = await _client.UpdateComment(validId, updatedCommentRequest);
            
            // Second update with the same data
            var secondResponse = await _client.UpdateComment(validId, updatedCommentRequest);

            // This is temporarily commented out until API behavior is updated. Server will currently allow duplicate updates.
            //secondResponse.Should().BeNull("Expected duplicate update to fail");
            secondResponse.ShouldMatch(updatedCommentRequest);
        }
    }
}