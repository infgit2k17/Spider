namespace Spider
{
    /// <summary>
    /// Checks if the given page fulfills search criteria.
    /// </summary>
    public interface IChecker
    {
        bool IsSearched(string url, string content);
    }
}