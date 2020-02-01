using System.Threading.Tasks;

namespace PaymentGateway.Services.Data
{
    public interface IApiKeyRepository
    {
        Task<bool> IsKeyValid(string key);
    }
}
