using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PushEventClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BasicDebugSetup();
            CreateHostBuilder(args).Build().Run();
        }

        public static void BasicDebugSetup()
        {
            string dbName = "TestDatabase.db";
            if (File.Exists(dbName))
            {
                // Delete file for easy debug
                // File.Delete(dbName);
            }
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel(options =>
                    {
                        options.Listen(IPAddress.Any, 5000); // http
                        options.Listen(IPAddress.Any, 5001, listenOptions => // https
                        {
                            // Look in readme.md for how to generate
                            listenOptions.UseHttps("data/ssl.pfx", "TestPass");
                        });
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
