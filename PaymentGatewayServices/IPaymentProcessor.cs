using PaymentGatewayServices.DTOs;

namespace PaymentGatewayServices
{
    public interface IPaymentProcessor
    {
        public bool ValidateRequest(PaymentDto request);
    }
}
