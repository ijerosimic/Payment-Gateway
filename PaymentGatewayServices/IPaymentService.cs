using PaymentGatewayServices.DTOs;

namespace PaymentGatewayServices
{
    public interface IPaymentService
    {
        public bool ProcessPayment(PaymentRequest paymentRequest);
    }
}
