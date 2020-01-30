using PaymentGatewayServices.DTOs;
using System;

namespace PaymentGatewayServices
{
    public class PaymentService : IPaymentService
    {
        public bool ProcessPayment(PaymentRequest paymentRequest)
        {
            if (string.IsNullOrWhiteSpace(paymentRequest.CardNumber) ||
                paymentRequest.CVV < 1 ||
                paymentRequest.ExpirationMonth < 1 ||
                paymentRequest.ExpirationYear < DateTime.Now.Year ||
                paymentRequest.Amount < 0 ||
                string.IsNullOrWhiteSpace(paymentRequest.Currency) ||
                string.IsNullOrWhiteSpace(paymentRequest.CardHolderFullName))
                return true;

            return false;
        }
    }
}
