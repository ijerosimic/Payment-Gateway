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

            //Set up Serilog to write to App Insights
            Log.Logger = new LoggerConfiguration()
                    .WriteTo
                    .ApplicationInsights(TelemetryConverter.Traces)
                    .CreateLogger();

            //Set up In-memory DB
            using (var scope = host.Services.CreateScope())
            {
                scope.ServiceProvider
                    .GetRequiredService<PaymentGatewayDBContext>()
                    .Seed();
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();
    }
}
