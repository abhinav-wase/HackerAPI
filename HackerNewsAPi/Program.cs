using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackerNewsAPi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GCMicroService
{
    public class Program
    {
        /// <summary>
        /// Main method that configures and runs the application.
        /// </summary>
        /// <param name="args">Command-line arguments passed to the application.</param>
        public static void Main(string[] args)
        {
            // Build and run the host created by CreateHostBuilder.
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Creates the host builder for the application, configuring default settings and specifying the startup class.
        /// </summary>
        /// <param name="args">Command-line arguments passed to the application.</param>
        /// <returns>The configured host builder.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // Use the Startup class to configure the web host.
                    webBuilder.UseStartup<Startup>();
                });
    }
}
