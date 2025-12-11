/// <summary>
/// Defines ID ranges for valid and invalid IDs used in tests. These constants help maintain consistency across test cases.
/// </summary>
public static class IdRanges
{
    // Post ID Ranges
    public const int MinValidPostId = 1;
    public const int MaxValidPostId = 100;
    public const int MinInvalidPostId = -100;
    public const int MaxInvalidPostId = 0;

    // Comment ID Ranges
    public const int MinValidCommentId = 1;
    public const int MaxValidCommentId = 500;
    public const int MinInvalidCommentId = -500;
    public const int MaxInvalidCommentId = 0;
}