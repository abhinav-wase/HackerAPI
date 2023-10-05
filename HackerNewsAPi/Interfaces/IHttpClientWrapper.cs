using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNewsAPi.Interfaces
{
    /// Represents an interface for a wrapper around HttpClient, providing an abstraction for asynchronous HTTP GET operations.
    public interface IHttpClientWrapper
    {
        /// <summary>
        /// Sends a GET request to the specified URI and returns the response body as a string.
        /// </summary>
        /// <param name="uri">The URI to send the GET request to.</param>
        /// <returns>A task representing the asynchronous operation. The result is the response body as a string.</returns>
        Task<string> GetStringAsync(string uri);
    }
}
