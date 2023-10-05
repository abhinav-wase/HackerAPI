using HackerNewsAPi.Models;
using HackerNewsAPi.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace HackerNewsAPi.Controllers
{
    // Apply Cross-Origin Resource Sharing (CORS) policy named "MyPolicy" to allow cross-origin requests.
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly NewsService _newsService;

        // Constructor with dependency injection of NewsService.
        public NewsController(NewsService newsService)
        {
            _newsService = newsService;
        }

        // Endpoint to get the newest stories.
        [HttpGet("newest")]
        public async Task<IActionResult> GetNewestStories([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                // Call the NewsService to get the newest stories.
                var newestStories = await _newsService.GetNewestStories(page, pageSize);

                // Return 200 OK with the retrieved newest stories.
                return Ok(newestStories);
            }
            catch (Exception ex)
            {
                // Log or handle the exception if needed.
                // Return 500 Internal Server Error with an error message.
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
