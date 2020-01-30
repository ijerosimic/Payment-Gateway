using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentGatewayDataAccess;
using PaymentGatewayServices;
using PaymentGatewayServices.DTOs;
using PaymentGatewayServices.ExtensionMethods;

namespace PaymentGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentGatewayController : ControllerBase
    {
        private readonly ILogger<PaymentGatewayController> _logger;
        private readonly PaymentGatewayDBContext _ctx;
        private readonly IPaymentService _paymentService;

        public PaymentGatewayController(
            ILogger<PaymentGatewayController> logger,
            PaymentGatewayDBContext ctx,
            IPaymentService paymentService)
        {
            _logger = logger;
            _ctx = ctx;
            _paymentService = paymentService;
        }

        [HttpPost("SubmitPayment")]
        public IActionResult SubmitPayment(PaymentRequest paymentRequest)
        {
            if (_paymentService.ProcessPayment(paymentRequest) == false)
                return StatusCode(400,
                    "Invalid data");

            _ctx.Add(paymentRequest.MapToEntity());

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
