using HackerNewsAPi.Interfaces;
using HackerNewsAPi.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace HackerNewsAPi.Service
{
    public class NewsService
    {
        /// <summary>
        /// API endpoints and base URL 
        /// </summary>
        private const string HackerNewsApiUrl = "https://hacker-news.firebaseio.com/v0/";
        private const string NewestStoriesEndpoint = "topstories.json";
        private const string ItemEndpoint = "item/{0}.json";

        // Dependencies for caching and HTTP requests
        private IMemoryCache _memoryCache;
        private  IHttpClientWrapper _httpClientWrapper;

        // Constructor with dependency injection
        public NewsService(IMemoryCache memoryCache, IHttpClientWrapper httpClientWrapper)
        {
            _memoryCache = memoryCache;
            _httpClientWrapper = httpClientWrapper;
        }

        // Default constructor (useful for testing or cases without dependencies)
        public NewsService() { }

        /// <summary>
        ///   // Fetches a paginated list of newest stories
        /// </summary>
        /// <param name="page">default page number</param>
        /// <param name="pageSize">no. of news per page</param>
        /// <returns></returns>
        public async Task<IEnumerable<NewsItem>> GetNewestStories(int page, int pageSize)
        {
            var cacheKey = "NewestStories";
            // Check if stories are already in cache
            if (_memoryCache.TryGetValue<IEnumerable<NewsItem>>(cacheKey, out var cachedStories))
            {
                // Return the paginated subset from cache
                return cachedStories.Skip((page - 1) * pageSize).Take(pageSize);
            }

            // If not in cache, fetch story IDs and news items
            var newestStoryIds = await GetNewestStoryIds();
            var newestStories = await FetchNewsItems(newestStoryIds);

            // Cache the fetched stories for 15 minutes
            _memoryCache.Set(cacheKey, newestStories, TimeSpan.FromMinutes(15));

            // Return the paginated subset from the fetched stories
            return newestStories.Skip((page - 1) * pageSize).Take(pageSize);
        }

        // Fetches the IDs of the newest stories from the Hacker News API
        private async Task<IEnumerable<int>> GetNewestStoryIds()
        {

            // Use the injected HTTP client wrapper for the request
            var response = await _httpClientWrapper.GetStringAsync($"{HackerNewsApiUrl}{NewestStoriesEndpoint}");
           
            // Check if response is null or empty
            if (string.IsNullOrEmpty(response))
            {
                // Handle the error or return an appropriate value
                throw new Exception("Unable to fetch data from the API.");
            }

            // Deserialize the response into a collection of integers (story IDs)
            return JsonSerializer.Deserialize<IEnumerable<int>>(response);
        }

        // Fetches news items for a collection of story IDs
        private async Task<IEnumerable<NewsItem>> FetchNewsItems(IEnumerable<int> storyIds)
        {
            // Use parallel async processes to fetch news items concurrently
            var tasks = storyIds.Select(id => FetchNewsItem(id));
            var newsItems = await Task.WhenAll(tasks);

            // Filter out null items (failed fetches)
            return newsItems.Where(item => item != null);
        }

        // Fetches a news item for a given story ID
        private async Task<NewsItem> FetchNewsItem(int storyId)
        {
          
            // Use the injected HTTP client wrapper for the request
            var response = await _httpClientWrapper.GetStringAsync($"{HackerNewsApiUrl}{string.Format(ItemEndpoint, storyId)}");
               
            // Deserialize the response into a NewsItem object
            var newsItem = JsonSerializer.Deserialize<NewsItem>(response);

            return newsItem;
            
        }
    }
}
