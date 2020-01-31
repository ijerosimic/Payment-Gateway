using PaymentGateway.DTOs;

namespace PaymentGateway.BusinessLogic
{
    public interface IPaymentProcessor
    {
        public bool ValidateRequest(PaymentDto request);
    }
}
