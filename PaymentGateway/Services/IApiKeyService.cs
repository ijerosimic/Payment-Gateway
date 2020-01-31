using System.Threading.Tasks;

namespace PaymentGateway.Services
{
    public interface IApiKeyService
    {
        Task<string> GetValidApiKey(string key);
    }
}
