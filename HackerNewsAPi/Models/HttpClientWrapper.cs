using HackerNewsAPi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNewsAPi.Models
{
    // Implementation of the IHttpClientWrapper interface, providing a wrapper around HttpClient for asynchronous HTTP GET operations.
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient _httpClient;

        // Constructor that injects an instance of HttpClient.
        public HttpClientWrapper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Sends a GET request to the specified URI using the underlying HttpClient and returns the response body as a string.
        /// </summary>
        /// <param name="uri">The URI to send the GET request to.</param>
        /// <returns>A task representing the asynchronous operation. The result is the response body as a string.</returns>
        public async Task<string> GetStringAsync(string uri)
        {
            // Log the URI for debugging purposes.
            System.Diagnostics.Debug.WriteLine(uri);

            // Asynchronously send the GET request and return the response body as a string.
            return await Task.Run(()=>_httpClient.GetStringAsync(uri));
        }
    }
}
