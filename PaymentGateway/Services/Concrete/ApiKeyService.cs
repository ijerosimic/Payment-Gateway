using Microsoft.EntityFrameworkCore;
using PaymentGateway.DataAccess;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Services.Concrete
{
    public class ApiKeyService : IApiKeyService
    {
        private readonly PaymentGatewayDBContext _ctx;

        public ApiKeyService(PaymentGatewayDBContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<string> GetValidApiKey(string key)
        {
            return await _ctx.ApiKeys
               .Where(x => x.Key.Equals(key))
               .Select(x => x.Key)
               .FirstOrDefaultAsync();
        }
    }
}
