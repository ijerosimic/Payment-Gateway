using System.Threading.Tasks;

namespace PaymentGateway.Services.Data
{
    public interface IApiKeyService
    {
        Task<string> GetValidApiKey(string key);
    }
}
