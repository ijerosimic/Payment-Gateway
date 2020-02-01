using System.Threading.Tasks;

namespace PaymentGateway.Services.Data
{
    public interface IApiKeyRepository
    {
        public Task<bool> IsKeyValidAsync(string key);
    }
}
