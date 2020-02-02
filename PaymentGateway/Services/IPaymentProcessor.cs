
using PaymentGateway.Repository.DTOs;

namespace PaymentGateway.Services
{
    public interface IPaymentProcessor
    {
        public bool ValidateRequest(PaymentRequestDto request);
    }
}
