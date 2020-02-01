
using PaymentGateway.Services.BussinessLogic.Helpers;
using PaymentGateway.Services.Data.DTOs;

namespace PaymentGateway.Services.BusinessLogic.Concrete
{
    public class PaymentProcessor : IPaymentProcessor
    {
        public bool ValidateRequest(PaymentDto paymentRequest) =>
                paymentRequest.Amount >= 0 &&
                PaymentDataValidator.ValidateCurrency(paymentRequest.Currency) &&
                PaymentDataValidator.ValidateCreditCardNumber(paymentRequest.CardNumber);
    }
}
