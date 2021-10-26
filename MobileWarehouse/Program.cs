using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MobileWarehouse.Entity.Models;
using MobileWarehouse.Entity.PreDeploimentDate;
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
                    var exceptionSource = ex.Source;
                    var exceptionStack = ex.StackTrace;
                    var exceptionMessage = ex.Message;
                    Log.Error($"{nameof(Main)} | Source - {exceptionSource} | Message - {exceptionMessage} | StackTrace - {exceptionStack}.");
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
