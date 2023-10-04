using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNewsAPi.Interfaces
{
    public interface IHttpClientWrapper
    {
        Task<string> GetStringAsync(string uri);
    }
}
