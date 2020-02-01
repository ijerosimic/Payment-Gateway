using System.Threading.Tasks;

namespace PaymentGateway.Services.Data
{
    public interface IApiKeyRepository
    {
        Task<string> GetValidApiKey(string key);
    }
}
