using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PaymentGateway.Authentication;
using PaymentGateway.Authentication.ExtensionMethods;
using PaymentGateway.BussinesLogic;
using PaymentGateway.BussinesLogic.Concrete;
using PaymentGateway.DataAccess;
using PaymentGateway.Repository;
using PaymentGateway.Repository.Concrete;
using System.Threading.Tasks;
using static PaymentGateway.Authentication.ApiKeyAuthOptions;

namespace PaymentGateway
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PaymentGatewayDBContext>(options =>
                options.UseInMemoryDatabase("PaymentGatewayDatabase"));

            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IApiKeyRepository, ApiKeyRepository>();
            services.AddScoped<IPaymentProcessor, PaymentProcessor>();
            services.AddScoped<IBankService, BankService>();
            services.AddScoped<IBankEndpoint, BankEndpoint>();

            services.AddApplicationInsightsTelemetry();
            services.AddLogging();

            /// <summary>
            /// Set up the defeault authentication scheme
            /// Configure the custom authentication handler via AddApiKeySupport
            /// </summary>
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = ApiKeyAuthenticationOptions.DefaultScheme;
                options.DefaultChallengeScheme = ApiKeyAuthenticationOptions.DefaultScheme;
            })
           .AddApiKeySupport(options => { });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler(handler => handler.Run(async context =>
            {
                var exception = context.Features.Get<IExceptionHandlerPathFeature>().Error;
                logger.LogError("An exception was thrown. Ex: {@exception}", exception);

                context.Response.StatusCode = 500;
                var res = JsonConvert.SerializeObject(new { message = "An error occured" });
                await context.Response.WriteAsync(res);
            }));

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
