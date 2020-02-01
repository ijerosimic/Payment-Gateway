using Microsoft.Extensions.Logging;
using PaymentGateway.Repository.DTOs;
using PaymentGateway.Services.Helpers;

namespace PaymentGateway.Services.Concrete
{
    public class PaymentProcessor : IPaymentProcessor
    {
        private readonly ILogger<PaymentProcessor> _logger;

        public PaymentProcessor(ILogger<PaymentProcessor> logger)
        {
            _logger = logger;
        }

        public bool ValidateRequest(PaymentDto paymentRequest)
        {
            _logger.LogInformation(
                "Validating request: {@paymentRequest}", paymentRequest);

            return paymentRequest.Amount >= 0 &&
                PaymentDataValidator.ValidateCurrency(paymentRequest.Currency) &&
                PaymentDataValidator.ValidateCreditCardNumber(paymentRequest.CardNumber);
        }
    }
}
