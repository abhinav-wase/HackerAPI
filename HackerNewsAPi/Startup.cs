using HackerNewsAPi.Interfaces;
using HackerNewsAPi.Models;
using HackerNewsAPi.Service;
using Microsoft.AspNetCore.Builder;

namespace HackerNewsAPi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMemoryCache();
            services.AddHttpClient();
            services.AddCors(o => o.AddPolicy("MyPolicy", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
            services.AddHttpClient<NewsService>();
            services.AddScoped<NewsService>();
            services.AddHttpClient<IHttpClientWrapper, HttpClientWrapper>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           
            app.UseRouting();
            app.UseCors("MyPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
