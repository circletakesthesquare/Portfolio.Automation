namespace API.Tests.Tests
{
    /// <summary>
    /// Tests for retrieving posts via the Posts API.
    /// </summary>
    public class GetPostTests : PostsTestBase
    {
        public GetPostTests(ITestOutputHelper output) : base(output)
        {
            _client = new PostsClient(CreateHttpClient());
        }

        private readonly PostsClient _client;

        [Fact]
        [Trait("Category", Categories.Get)]
        [Trait("Category", Categories.Post)]
        [Trait("Category", Categories.Positive)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.Smoke)]
        public async Task Get_Post_By_Id()
        {
            var validId = IdGenerator.RandomValidPostId();
            var post = await _client.GetPostById(validId);

            post.ShouldExist(validId);
        }

        [Fact]
        [Trait("Category", Categories.Get)]
        [Trait("Category", Categories.Post)]
        [Trait("Category", Categories.Positive)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.EdgeCase)]
        public async Task Get_Post_By_Id_Minimum_Valid_Id()
        {
            var minValidId = IdRanges.MinValidPostId;
            var post = await _client.GetPostById(minValidId);

            post.ShouldExist(minValidId);
        }

        [Fact]
        [Trait("Category", Categories.Get)]
        [Trait("Category", Categories.Post)]
        [Trait("Category", Categories.Positive)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.EdgeCase)]
        public async Task Get_Post_By_Id_Maximum_Valid_Id()
        {
            var maxValidId = IdRanges.MaxValidPostId;
            var post = await _client.GetPostById(maxValidId);
            
            post.ShouldExist(maxValidId);
        }

        // Negative Tests

        [Fact]
        [Trait("Category", Categories.Get)]
        [Trait("Category", Categories.Post)]
        [Trait("Category", Categories.Negative)]
        [Trait("Category", Categories.Integration)]
        [Trait("Category", Categories.Smoke)]
        public async Task Get_Post_By_Invalid_Id_Should_Return_NotFound()
        {
            var invalidId = IdGenerator.RandomInvalidPostId();
            var response = await _client.GetPostById(invalidId);

            response.Should().BeNull("Expected null response for invalid post ID.");
        }
    }
}