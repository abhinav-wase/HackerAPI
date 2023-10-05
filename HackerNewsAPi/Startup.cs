using HackerNewsAPi.Interfaces;
using HackerNewsAPi.Middleware;
using HackerNewsAPi.Models;
using HackerNewsAPi.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;

namespace HackerNewsAPi
{
    // Configuration class for setting up services and middleware for the application.
    public class Startup
    {
        /// <summary>
        /// Configures the services used by the application.
        /// </summary>
        /// <param name="services">The collection of services to be configured.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add controllers for handling HTTP requests.
            services.AddControllers();

            // Add memory caching support.
            services.AddMemoryCache();

            // Add HttpClientFactory services.
            services.AddHttpClient();

            // Configure and add a CORS policy named "MyPolicy" to allow cross-origin requests.
            services.AddCors(o => o.AddPolicy("AllowAllDomainRequests", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

            // Add HttpClient and NewsService to the service collection.
            services.AddHttpClient<NewsService>();
            services.AddScoped<NewsService>();

            // Add HttpClientWrapper as IHttpClientWrapper for dependency injection.
            services.AddHttpClient<IHttpClientWrapper, HttpClientWrapper>();

            // Register the Swagger generator
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hacker News API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable developer exception page if the application is in development mode.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Use custom exception handling middleware for centralized exception handling.
            app.UseExceptionHandlingMiddleware();

            // Enable routing for handling requests.
            app.UseRouting();

            // Apply the CORS policy named "MyPolicy" to allow cross-origin requests.
            app.UseCors("AllowAllDomainRequests");

            // Map controllers to endpoints for handling HTTP requests.
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui(HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hacker API V1");
                // Additional configuration...
            });

        }
    }
}
