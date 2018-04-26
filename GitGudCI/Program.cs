using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GitGudCI.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GitGudCI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();

            var host = BuildWebHost(args);
            
                        // Seeds the database
                       using (var scope = host.Services.CreateScope())
                            {
                var services = scope.ServiceProvider;
                
                                // Update migrations
                var dbContext = services.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                           }
            
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
