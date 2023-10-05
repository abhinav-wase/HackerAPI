namespace HackerNewsAPi.Models
{

    /// <summary>
    /// Represents a news item with a title and a URL.
    /// </summary>
    public class NewsItem
    {
    /// Gets or sets the title of the news item.
    public string title { get; set; } = string.Empty;
    /// Gets or sets the URL associated with the news item.
    public string url { get; set; } = string.Empty;
    }
}