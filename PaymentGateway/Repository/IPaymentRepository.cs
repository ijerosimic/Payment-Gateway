using PaymentGateway.Repository.DTOs;
using System.Threading.Tasks;

namespace PaymentGateway.Repository
{
    public interface IPaymentRepository
    {
        public Task<int> SavePaymentAsync(PaymentRequestDto payment);
        public Task<PaymentDetailsDto> GetPaymentAsync(string paymentIdentifier);
    }
}
