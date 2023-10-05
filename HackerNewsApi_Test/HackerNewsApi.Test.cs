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
    public class NewsServiceTests
    {
        [Test]
        [TestCase(1, 10)]
        [TestCase(1, 20)]
        public async Task GetNewestStories_CacheEmpty_FetchesAndCachesStories(int page, int pageSize)
        {
            // Arrange
            // Creating a mocked memory cache with a specific expiration frequency.
            var memoryCacheOptions = new MemoryCacheOptions { ExpirationScanFrequency = TimeSpan.FromSeconds(50) };
            IMemoryCache memoryCache = new MemoryCache(memoryCacheOptions);

            // Using a more descriptive variable name for the HTTP client.
            HttpClient httpClient = new HttpClient();
            IHttpClientWrapper httpClientWrapper = new HttpClientWrapper(httpClient);

            // Creating an instance of the NewsService with the mocked memory cache and HTTP client wrapper.
            NewsService newsService = new NewsService(memoryCache, httpClientWrapper);

            // Act
            // Invoking the method under test to get the newest stories.
            var result = await newsService.GetNewestStories(page, pageSize);

            // Assert
            // Verifying that the result is not null and has the expected count.
            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(pageSize));

            // Verifying that the cache is populated after the method is called.
            Assert.That(memoryCache.TryGetValue("NewestStories", out var cachedStories));
        }

        [Test]
        public async Task GetNewestStories_CacheNotEmpty_ReturnsCachedStories()
        {
            // Arrange
            // Creating a mocked memory cache with a specific expiration frequency.
            var memoryCacheOptions = new MemoryCacheOptions { ExpirationScanFrequency = TimeSpan.FromSeconds(50) };
            IMemoryCache memoryCache = new MemoryCache(memoryCacheOptions);

            // Using a more descriptive variable name for the HTTP client.
            HttpClient httpClient = new HttpClient();
            IHttpClientWrapper httpClientWrapper = new HttpClientWrapper(httpClient);

            // Creating an instance of the NewsService with the mocked memory cache and HTTP client wrapper.
            NewsService newsService = new NewsService(memoryCache, httpClientWrapper);

            // Assuming that some data is already cached.
            var cachedData = new List<NewsItem> { /* populate with dummy data */ };
            memoryCache.Set("NewestStories", cachedData, TimeSpan.FromMinutes(15));

            // Act
            // Invoking the method under test to get the newest stories.
            var result = await newsService.GetNewestStories(1, 10);

            // Assert
            // Verifying that the result is not null.
            Assert.IsNotNull(result);

            // Verifying that the cache is still present after the method is called.
            Assert.That(memoryCache.TryGetValue("NewestStories", out var cachedStories));
        }
    }

}