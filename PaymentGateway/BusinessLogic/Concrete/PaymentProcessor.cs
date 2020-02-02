using Microsoft.Extensions.Logging;
using PaymentGateway.BussinesLogic.Helpers;
using PaymentGateway.Repository.DTOs;

namespace PaymentGateway.BussinesLogic.Concrete
{
    public class PaymentProcessor : IPaymentProcessor
    {
        private readonly ILogger<PaymentProcessor> _logger;

        public PaymentProcessor(ILogger<PaymentProcessor> logger)
        {
            _logger = logger;
        }

        public bool ValidateRequest(PaymentRequestDto paymentRequest)
        {
            _logger.LogInformation(
                "Validating request: {@paymentRequest}", paymentRequest);

            //Payment request validation logic could go here
            //An example of validation is implemented

            return paymentRequest.Amount >= 0 &&
                PaymentDataValidator.ValidateCurrency(paymentRequest.Currency) &&
                PaymentDataValidator.ValidateCreditCardNumber(paymentRequest.CardNumber);
        }
    }
}
