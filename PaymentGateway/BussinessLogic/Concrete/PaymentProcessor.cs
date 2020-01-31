using PaymentGateway.DTOs;

namespace PaymentGateway.BusinessLogic.Concrete
{
    public class PaymentProcessor : IPaymentProcessor
    {
        public bool ValidateRequest(PaymentDto paymentRequest)
        {
            //logic for validation submitted payment request

            return true;
        }
    }
}
