using HackerNewsAPi.Models;
using HackerNewsAPi.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HackerNewsAPi.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly NewsService _newsService;

        public NewsController(NewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet("newest")]
        public async Task<IActionResult> GetNewestStories([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var newestStories = await _newsService.GetNewestStories(page, pageSize);
            return Ok(newestStories);
        }
    }
}
