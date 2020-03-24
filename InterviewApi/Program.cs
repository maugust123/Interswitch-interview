using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace InterviewApi
{
    /// <summary>
    /// Application entry class
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Application entry method
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                //.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("logs/log-.log", LogEventLevel.Information, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 90, buffered: true)
                .WriteTo.Seq(Environment.GetEnvironmentVariable("SEQ_URL") ?? "http://localhost:5341")
                .CreateLogger();

            try
            {
                Log.Information("Starting up");
                CreateHostBuilder(args).Build().Run();
                Log.Information("Fun after Starting up");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.Fatal("Finally this is called");
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.CaptureStartupErrors(true);//For deployment
                    webBuilder.UseSetting(WebHostDefaults.DetailedErrorsKey, "true");//For deployment
                    webBuilder.ConfigureKestrel(serverOptions => { serverOptions.AddServerHeader = false; });
                    webBuilder.UseStartup<Startup>();
                });

    }
}
