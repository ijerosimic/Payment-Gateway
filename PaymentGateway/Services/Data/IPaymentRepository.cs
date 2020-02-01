using PaymentGateway.Services.Data.DTOs;
using System.Threading.Tasks;

namespace PaymentGateway.Services.Data
{
    public interface IPaymentRepository
    {
        public Task<int> SavePaymentAsync(PaymentDto payment);
        public Task<PaymentDto> GetPaymentAsync(string paymentIdentifier);
    }
}
