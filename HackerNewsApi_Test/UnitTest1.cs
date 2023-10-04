using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using NUnit.Framework;
using HackerNewsAPi;
using HackerNewsAPi.Service;
using HackerNewsAPi.Models;
using HackerNewsAPi.Interfaces;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace HackerNewsApi_Test
{
    [TestFixture]
    public class Tests
    {
        private const string HackerNewsApiUrl = "https://hacker-news.firebaseio.com/v0/";
        private const string NewestStoriesEndpoint = "topstories.json";
        private const string ItemEndpoint = "item/{0}.json";

        [Test]
        [TestCase(1,10)]
        [TestCase(1,20)]
        public async Task GetNewestStories_CacheEmpty_FetchesAndCachesStories(int a , int b)
        {
            // Arrange
            //Mock<IHttpClientWrapper> httpClientWrapperMock = new Mock<IHttpClientWrapper>();
            //Mock<IMemoryCache> memoryCacheMock = new Mock<IMemoryCache>();
            MemoryCacheOptions memoryCacheOptionsMock = new MemoryCacheOptions();
            memoryCacheOptionsMock.ExpirationScanFrequency = TimeSpan.FromSeconds(50);
            IMemoryCache memoryCache = new MemoryCache(memoryCacheOptionsMock);
            HttpClient objCLi = new HttpClient();
            IHttpClientWrapper httpClientWrapper = new HttpClientWrapper(objCLi);
            NewsItem newsItem = new NewsItem() { title = "abc", url = "www.ex.com" };
            NewsService newsService = new NewsService(memoryCache, httpClientWrapper);

            //httpClientWrapperMock.Setup(c => c.GetStringAsync(It.IsAny<string>())).ReturnsAsync("[1]");


            // ... rest of the setup


            var result = await newsService.GetNewestStories(a,b);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(b));

            // Verify that caching is called
            //memoryCacheMock.Verify(
            //    cache => cache.Set(
            //        It.IsAny<string>(),
            //        It.IsAny<IEnumerable<NewsItem>>(),
            //        It.IsAny<MemoryCacheEntryOptions>()),
            //    Times.Once);

        }

        [Test]
        public async Task GetNewestStories_CacheNotEmpty_ReturnsCachedStories()
        {
            MemoryCacheOptions memoryCacheOptionsMock = new MemoryCacheOptions();
            memoryCacheOptionsMock.ExpirationScanFrequency = TimeSpan.FromSeconds(50);
            IMemoryCache memoryCache = new MemoryCache(memoryCacheOptionsMock);
            HttpClient objCLi = new HttpClient();
            IHttpClientWrapper httpClientWrapper = new HttpClientWrapper(objCLi);
            NewsItem newsItem = new NewsItem() { title = "abc", url = "www.ex.com" };
            NewsService newsService = new NewsService(memoryCache, httpClientWrapper);
            var result = await newsService.GetNewestStories(1,10);

            Assert.That(memoryCache.TryGetValue("NewestStories", out var cachedStories));
        }
    }
}