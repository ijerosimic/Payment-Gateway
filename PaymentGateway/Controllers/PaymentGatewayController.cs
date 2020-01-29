using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentGateway.DTOs;
using PaymentGateway.Repository;
using PaymentGateway.Repository.Models;

namespace PaymentGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentGatewayController : ControllerBase
    {
        private readonly ILogger<PaymentGatewayController> _logger;
        private readonly PaymentGatewayDBContext _ctx;

        public PaymentGatewayController(
            ILogger<PaymentGatewayController> logger,
            PaymentGatewayDBContext ctx)
        {
            _logger = logger;
            _ctx = ctx;
        }

        [HttpPost("SubmitPayment")]
        public IActionResult SubmitPayment(PaymentRequest paymentRequest)
        {
            if (string.IsNullOrWhiteSpace(paymentRequest.CardNumber) ||
                paymentRequest.CVV < 1 ||
                paymentRequest.ExpirationMonth < 1 ||
                paymentRequest.ExpirationYear < DateTime.Now.Year ||
                paymentRequest.Amount < 0 ||
                string.IsNullOrWhiteSpace(paymentRequest.Currency) ||
                string.IsNullOrWhiteSpace(paymentRequest.CardHolderFullName))
                return BadRequest(paymentRequest);

            _ctx.Add(new PaymentHistory
            {
                PaymentIdentifier = new Random().Next(100000, 999999).ToString(),
                CardNumber = paymentRequest.CardNumber,
                CVV = paymentRequest.CVV,
                Amount = paymentRequest.Amount,
                Currency = paymentRequest.Currency
            });

            if (_ctx.SaveChanges() > 0)
                return StatusCode(200,
                    "Payment processed successfully");

            return StatusCode(500,
                "Error processing payment");
        }

        [HttpGet("PaymentDetails/{id}")]
        public IActionResult GetPaymentDetails(int id)
        {
            var payment = _ctx.PaymentHistory
                .Where(x => x.ID == id)
                .FirstOrDefault();

            if (payment is null)
                return NotFound("Payment with specified ID not found");

            return Ok(payment);
        }
    }
}
