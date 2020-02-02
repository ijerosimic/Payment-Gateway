
using PaymentGateway.Repository.DTOs;

namespace PaymentGateway.BussinesLogic
{
    public interface IPaymentProcessor
    {
        public bool ValidateRequest(PaymentRequestDto request);
    }
}
