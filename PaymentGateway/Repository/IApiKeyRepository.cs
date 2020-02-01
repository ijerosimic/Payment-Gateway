using System.Threading.Tasks;

namespace PaymentGateway.Repository
{
    public interface IApiKeyRepository
    {
        public Task<bool> IsKeyValidAsync(string key);
    }
}
