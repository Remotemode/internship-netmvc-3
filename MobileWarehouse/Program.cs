using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MobileWarehouse.Entity.Models;
using MobileWarehouse.Entity.PreDeploimentDate;
using MobileWarehouse.Helpers;
using Serilog;

namespace MobileWarehouse
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<ApplicationContext>();
                    SampleData.Initialize(context);
                }
                catch (Exception ex)
                {
                    Log.Error($"{nameof(Main)} | Source - {ex.Source} | Message - {ex.StackTrace} | StackTrace - {ex.Message}.");
                }
            }
            host.Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureSeqLogging()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
