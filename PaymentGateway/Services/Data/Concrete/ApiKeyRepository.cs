using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PaymentGateway.DataAccess;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Services.Data.Concrete
{
    public class ApiKeyRepository : IApiKeyRepository
    {
        private readonly PaymentGatewayDBContext _ctx;
        private readonly ILogger<ApiKeyRepository> _logger;

        public ApiKeyRepository(
            PaymentGatewayDBContext ctx, 
            ILogger<ApiKeyRepository> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public async Task<string> GetValidApiKey(string key)
        {
            _logger.LogInformation("Validating API key: {@key}", key);

            return await _ctx.ApiKeys
               .Where(x => x.Key.Equals(key))
               .Select(x => x.Key)
               .FirstOrDefaultAsync();
        }
    }
}
