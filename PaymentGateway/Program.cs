using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PaymentGateway.DataAccess;
using Serilog;

namespace PaymentGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            Log.Logger = new LoggerConfiguration()
                    .WriteTo
                    .ApplicationInsights(TelemetryConverter.Traces)
                    .CreateLogger();

            using (var scope = host.Services.CreateScope())
            {
                var ctx = scope.ServiceProvider
                    .GetRequiredService<PaymentGatewayDBContext>();

                DataSeeder.Seed(scope.ServiceProvider);
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //.ConfigureLogging(logging =>
                //{
                //    logging.ClearProviders();
                //    logging.AddApplicationInsights(
                //        "935cfc58-e3a5-4c61-81b9-d1902e6b3f73");

                //    logging.AddFilter<ApplicationInsightsLoggerProvider>(
                //        "", LogLevel.Information);
                //})
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();
    }
}
