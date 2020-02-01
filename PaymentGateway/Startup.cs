using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PaymentGateway.Authentication.ExtensionMethods;
using PaymentGateway.DataAccess;
using PaymentGateway.Repository;
using PaymentGateway.Repository.Concrete;
using PaymentGateway.Services;
using PaymentGateway.Services.Concrete;
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

            services.AddApplicationInsightsTelemetry();
            services.AddLogging();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = ApiKeyAuthenticationOptions.DefaultScheme;
                options.DefaultChallengeScheme = ApiKeyAuthenticationOptions.DefaultScheme;
            })
           .AddApiKeySupport(options => { });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
