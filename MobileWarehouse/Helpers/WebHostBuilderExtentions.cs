using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileWarehouse.Helpers
{
    public static class WebHostBuilderExtentions
    {
        public static IHostBuilder ConfigureSeqLogging(this IHostBuilder webHostBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            try
            {
                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();
            }
            catch (Exception)
            {
                Log.CloseAndFlush();
                Log.Error("Close loggers");
            }

            return webHostBuilder;
        }
    }
}
