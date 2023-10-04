using HackerNewsAPi.Interfaces;
using HackerNewsAPi.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace HackerNewsAPi.Service
{
    public class NewsService
    {
        private const string HackerNewsApiUrl = "https://hacker-news.firebaseio.com/v0/";
        private const string NewestStoriesEndpoint = "topstories.json";
        private const string ItemEndpoint = "item/{0}.json";

        private  IMemoryCache _memoryCache;
        private  IHttpClientWrapper _httpClientWrapper;

        public NewsService(IMemoryCache memoryCache, IHttpClientWrapper httpClientWrapper)
        {
            _memoryCache = memoryCache;
            _httpClientWrapper = httpClientWrapper;
        }
        public NewsService() { }

        public async Task<IEnumerable<NewsItem>> GetNewestStories(int page, int pageSize)
        {
            var cacheKey = "NewestStories";
            if (_memoryCache.TryGetValue<IEnumerable<NewsItem>>(cacheKey, out var cachedStories))
            {
                return cachedStories.Skip((page - 1) * pageSize).Take(pageSize);
            }

            var newestStoryIds = await GetNewestStoryIds();
            var newestStories = await FetchNewsItems(newestStoryIds);

            _memoryCache.Set(cacheKey, newestStories, TimeSpan.FromMinutes(15));
            return newestStories.Skip((page - 1) * pageSize).Take(pageSize);
        }

        private async Task<IEnumerable<int>> GetNewestStoryIds()
        {
            HttpClient objclit = new HttpClient();
            var responses =  await objclit.GetStringAsync($"{HackerNewsApiUrl}{NewestStoriesEndpoint}");
            var response = await _httpClientWrapper.GetStringAsync($"{HackerNewsApiUrl}{NewestStoriesEndpoint}");
            // Check if response is null or empty
            if (string.IsNullOrEmpty(response))
            {
                // Handle the error or return an appropriate value
                throw new Exception("Unable to fetch data from the API.");
            }
            return JsonSerializer.Deserialize<IEnumerable<int>>(response);
        }

        private async Task<IEnumerable<NewsItem>> FetchNewsItems(IEnumerable<int> storyIds)
        {
            var tasks = storyIds.Select(id => FetchNewsItem(id));
            var newsItems = await Task.WhenAll(tasks);
            
            return newsItems.Where(item => item != null);
        }

        private async Task<NewsItem> FetchNewsItem(int storyId)
        {
            try
            {
                var response = await _httpClientWrapper.GetStringAsync($"{HackerNewsApiUrl}{string.Format(ItemEndpoint, storyId)}");
                System.Diagnostics.Debug.WriteLine(response);
                var newsItem = JsonSerializer.Deserialize<NewsItem>(response);

                return newsItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
