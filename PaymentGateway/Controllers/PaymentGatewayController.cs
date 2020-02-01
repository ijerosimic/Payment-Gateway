using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentGateway.Repository;
using PaymentGateway.Repository.DTOs;
using PaymentGateway.Services;
using System.Threading.Tasks;

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
        public async Task<IActionResult> SubmitPayment(PaymentDto payment)
        {
            if (_paymentProcessor.ValidateRequest(payment) == false)
                return BadRequest("Invalid payment data");

            //contact bank

            await _paymentRepository.SavePaymentAsync(payment);

            _logger.LogInformation(
               "Payment submitted: {@paymentRequest}", payment);

            return Ok("Sucessfully processed payment");
        }

        [Authorize]
        [HttpGet("PaymentDetails/{id}")]
        public async Task<IActionResult> GetPaymentDetails(string requestId)
        {
            var payment = await _paymentRepository.GetPaymentAsync(requestId);

            if (payment is null)
                return NotFound("Payment with specified ID not found");

            _logger.LogInformation(
                "Payment details requested: {@payment}", payment);

            return Ok(payment);
        }
    }
}
