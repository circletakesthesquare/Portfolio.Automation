/// <summary>
/// Provides methods to generate random valid and invalid IDs for testing purposes.
/// </summary>
public static class IdGenerator
{
    private static readonly Random _random = new();

    /// <summary>
    /// Generates a random valid Post ID within the defined range.
    /// </summary>
    /// <returns>A random valid Post ID.</returns>
    public static int RandomValidPostId() => _random.Next(IdRanges.MinValidPostId, IdRanges.MaxValidPostId + 1);

    /// <summary>
    /// Generates a random invalid Post ID within the defined range.
    /// </summary>
    /// <returns>A random invalid Post ID.</returns>
    public static int RandomInvalidPostId() => _random.Next(IdRanges.MinInvalidPostId, IdRanges.MaxInvalidPostId + 1);

    /// <summary>
    /// Generates a random valid Comment ID within the defined range.
    /// </summary>
    /// <returns>A random valid Comment ID.</returns>
    public static int RandomValidCommentId() => _random.Next(IdRanges.MinValidCommentId, IdRanges.MaxValidCommentId + 1);

    /// <summary>
    /// Generates a random invalid Comment ID within the defined range.
    /// </summary>
    /// <returns>A random invalid Comment ID.</returns>
    public static int RandomInvalidCommentId() => _random.Next(IdRanges.MinInvalidCommentId, IdRanges.MaxInvalidCommentId + 1);
}