using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentGateway.BusinessLogic;
using PaymentGateway.DTOs;
using PaymentGateway.Services;

namespace PaymentGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentGatewayController : ControllerBase
    {
        private readonly ILogger<PaymentGatewayController> _logger;
        private readonly IPaymentService _paymentService;
        private readonly IPaymentProcessor _paymentProcessor;

        public PaymentGatewayController(
            ILogger<PaymentGatewayController> logger,
            IPaymentService paymentService,
            IPaymentProcessor paymentProcessor)
        {
            _logger = logger;
            _paymentService = paymentService;
            _paymentProcessor = paymentProcessor;
        }

        [HttpPost("SubmitPayment")]
        public IActionResult SubmitPayment(PaymentDto paymentRequest)
        {
            if (_paymentProcessor.ValidateRequest(paymentRequest) == false)
                return StatusCode(400,
                    "Invalid data");

            _paymentService.AddPayment(paymentRequest);

            if (_paymentService.SaveChanges() > 0)
                return StatusCode(200,
                    "Payment processed successfully");

            return StatusCode(500,
                "Error processing payment");
        }

        [Authorize]
        [HttpGet("PaymentDetails/{id}")]
        public IActionResult GetPaymentDetails(int id)
        {
            var payment = _paymentService.GetPaymentById(id);

            if (payment is null)
                return NotFound("Payment with specified ID not found");

            return Ok(payment);
        }
    }
}
