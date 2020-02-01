using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentGateway.Services.BusinessLogic;
using PaymentGateway.Services.BussinessLogic.Concrete;
using PaymentGateway.Services.Data;
using PaymentGateway.Services.Data.DTOs;

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

        [Authorize]
        [HttpPost("SubmitPayment")]
        public IActionResult SubmitPayment(PaymentDto payment)
        {
            _logger.LogInformation(
                "Payment submitted: {@paymentRequest}", payment);

            if (_paymentProcessor.ValidateRequest(payment) == false)
                return BadRequest();

            //contact bank

            _paymentService.AddPayment(payment);
            _paymentService.SaveChanges();

            return Ok("Sucessfully processed payment");
        }

        [Authorize]
        [HttpGet("PaymentDetails/{id}")]
        public IActionResult GetPaymentDetails(string requestId)
        {
            var payment = _paymentService.GetPaymentById(requestId);

            if (payment is null)
                return StatusCode(404, "Payment with specified ID not found");

            _logger.LogInformation(
                "Payment details requested: {@payment}", payment);

            return StatusCode(200, payment);
        }
    }
}
