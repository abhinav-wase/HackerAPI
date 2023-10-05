using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace HackerNewsAPi.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // This method is called for each HTTP request
        public async Task Invoke(HttpContext context)
        {
            try
            {
                // Continue processing the request
                await _next(context);
            }
            catch (Exception ex)
            {
                // Log the exception (you might want to use a logging framework here)
                Debug.WriteLine($"Unhandled exception: {ex}");

                // Handle the exception, for example, you can return a custom error response
                context.Response.StatusCode = 500;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync("Internal Server Error");
            }
        }
    }

    // Extension method to add the middleware to the HTTP request pipeline
    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
