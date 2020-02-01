
using PaymentGateway.Repository.DTOs;

namespace PaymentGateway.Services
{
    public interface IPaymentProcessor
    {
        public bool ValidateRequest(PaymentDto request);
    }
}
