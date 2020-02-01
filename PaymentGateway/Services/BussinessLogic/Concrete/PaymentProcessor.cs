
using Microsoft.Extensions.Logging;
using PaymentGateway.Services.BussinessLogic.Helpers;
using PaymentGateway.Services.Data.DTOs;

namespace PaymentGateway.Services.BusinessLogic.Concrete
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
