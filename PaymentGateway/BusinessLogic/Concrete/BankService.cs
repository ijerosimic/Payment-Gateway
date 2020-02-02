using Microsoft.Extensions.Logging;
using PaymentGateway.Repository.DTOs;
using System;

namespace PaymentGateway.BussinesLogic.Concrete
{
    public class BankService : IBankService
    {
        private readonly ILogger<IBankService> _logger;
        private readonly IBankEndpoint _bankEndpoint;

        public BankService(
            ILogger<IBankService> logger,
            IBankEndpoint bankEndpoint)
        {
            _logger = logger;
            _bankEndpoint = bankEndpoint;
        }

        /// <summary>
        /// Submit payment to bank for processing
        /// The Bank receives the payment request, processes it and returns an object
        /// This method cals a presumed Bank Api endpoint or some other resource
        /// </summary>
        public PaymentRequestDto SubmitPaymentToBank(PaymentRequestDto request)
        {
            _logger.LogInformation(
                "Forwarding payment request to bank. Request: {@request}", request);

            var response = _bankEndpoint.SubmitPaymentRequest(request);

            _logger.LogInformation(
                "Received response from bank: {@response}", response);

            request.PaymentIdentifier = response.PaymentIdentifier;
            request.Status = response.Status;

            return request;
        }
    }

    /// <summary>
    /// The code below serves as a fake bank endpoint
    /// The Bank receives the payment request, processes it and returns an object
    /// containing a status and payment identifier
    /// </summary>
    public interface IBankEndpoint
    {
        public BankResponse SubmitPaymentRequest(PaymentRequestDto paymentData);
    }

    public class BankEndpoint : IBankEndpoint
    {
        public BankResponse SubmitPaymentRequest(PaymentRequestDto paymentData)
        {
            return BankResponse.CreateResponse();
        }
    }

    public class BankResponse
    {
        private BankResponse()
        {
        }

        public static BankResponse CreateResponse()
        {
            return new BankResponse();
        }

        public string Status => GetStatus().ToString();
        public string PaymentIdentifier => new Random().Next(100000, 999999).ToString();

        public enum StatusEnum
        {
            Authorized,
            Unathorized,
            Declined
        }

        private StatusEnum GetStatus()
        {
            var statuses = Enum.GetValues(typeof(StatusEnum));

            return (StatusEnum)statuses
                .GetValue(new Random().Next(statuses.Length));
        }
    }
}
