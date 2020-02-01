using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PaymentGateway.DataAccess;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Repository.Concrete
{
    public class ApiKeyRepository : IApiKeyRepository
    {
        private readonly PaymentGatewayDBContext _ctx;
        private readonly ILogger<IApiKeyRepository> _logger;

        public ApiKeyRepository(
            PaymentGatewayDBContext ctx, 
            ILogger<IApiKeyRepository> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public async Task<bool> IsKeyValidAsync(string key)
        {
            _logger.LogInformation("Validating API key: {@key}", key);

            return await _ctx.ApiKeys
               .Where(x => x.Key.Equals(key))
               .Select(x => x.Key)
               .AnyAsync();
        }
    }
}
