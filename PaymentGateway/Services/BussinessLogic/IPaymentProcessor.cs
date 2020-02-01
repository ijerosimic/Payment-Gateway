
using PaymentGateway.Services.Data.DTOs;

namespace PaymentGateway.Services.BusinessLogic
{
    public interface IPaymentProcessor
    {
        public bool ValidateRequest(PaymentDto request);
    }
}
