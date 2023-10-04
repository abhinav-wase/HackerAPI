using HackerNewsAPi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNewsAPi.Models
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient _httpClient;

        public HttpClientWrapper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetStringAsync(string uri)
        {
            System.Diagnostics.Debug.WriteLine(uri);
            return await Task.Run(()=>_httpClient.GetStringAsync(uri));
        }
    }
}
