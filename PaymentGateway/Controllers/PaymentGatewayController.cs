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
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPaymentProcessor _paymentProcessor;

        public PaymentGatewayController(
            ILogger<PaymentGatewayController> logger,
            IPaymentRepository paymentService,
            IPaymentProcessor paymentProcessor)
        {
            _logger = logger;
            _paymentRepository = paymentService;
            _paymentProcessor = paymentProcessor;
        }

        [Authorize]
        [HttpPost("SubmitPayment")]
        public IActionResult SubmitPayment(PaymentDto payment)
        {
            if (_paymentProcessor.ValidateRequest(payment) == false)
                return BadRequest("Invalid payment data");

            //contact bank

            _paymentRepository.SavePaymentToDatabase(payment);

            _logger.LogInformation(
               "Payment submitted: {@paymentRequest}", payment);

            return Ok("Sucessfully processed payment");
        }

        [Authorize]
        [HttpGet("PaymentDetails/{id}")]
        public IActionResult GetPaymentDetails(string requestId)
        {
            var payment = _paymentRepository.GetPaymentByPaymentIdentifier(requestId);

            if (payment is null)
                return StatusCode(404, "Payment with specified ID not found");

            _logger.LogInformation(
                "Payment details requested: {@payment}", payment);

            return StatusCode(200, payment);
        }
    }
}
