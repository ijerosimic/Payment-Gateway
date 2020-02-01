using PaymentGateway.Repository.DTOs;
using System.Threading.Tasks;

namespace PaymentGateway.Repository
{
    public interface IPaymentRepository
    {
        public Task<int> SavePaymentAsync(PaymentDto payment);
        public Task<PaymentDto> GetPaymentAsync(string paymentIdentifier);
    }
}
