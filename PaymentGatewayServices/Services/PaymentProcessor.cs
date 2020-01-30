using PaymentGatewayServices.DTOs;
using System;

namespace PaymentGatewayServices.Services
{
    public class PaymentProcessor : IPaymentProcessor
    {
        public bool ValidateRequest(PaymentDto paymentRequest)
        {
            if (string.IsNullOrWhiteSpace(paymentRequest.CardNumber) ||
                paymentRequest.CVV < 1 ||
                paymentRequest.ExpirationMonth < 1 ||
                paymentRequest.ExpirationYear < DateTime.Now.Year ||
                paymentRequest.Amount < 0 ||
                string.IsNullOrWhiteSpace(paymentRequest.Currency))
                return true;

            return false;
        }
    }
}
