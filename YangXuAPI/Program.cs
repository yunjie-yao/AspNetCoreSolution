using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using YangXuAPI.Data;

namespace YangXuAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scop=host.Services.CreateScope())
            {
                try
                {
                    var dbContext = scop.ServiceProvider.GetService<RoutineDbContext>();
                    dbContext.Database.EnsureDeleted();
                    dbContext.Database.Migrate();
                }
                catch (Exception e)
                {
                    var logger = scop.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(e,"DataBase Migration Error");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
