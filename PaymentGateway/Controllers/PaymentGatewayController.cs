using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PaymentGateway.BussinesLogic;
using PaymentGateway.Repository;
using PaymentGateway.Repository.DTOs;
using System.Threading.Tasks;

namespace PaymentGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentGatewayController : ControllerBase
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPaymentProcessor _paymentProcessor;
        private readonly IBankService _bankService;
        private readonly ILogger<PaymentGatewayController> _logger;

        public PaymentGatewayController(
            IPaymentRepository paymentRepository,
            IPaymentProcessor paymentProcessor,
            IBankService bankService,
            ILogger<PaymentGatewayController> logger)
        {
            _paymentRepository = paymentRepository;
            _paymentProcessor = paymentProcessor;
            _bankService = bankService;
            _logger = logger;
        }

        [Authorize]
        [HttpPost("SubmitPayment")]
        public async Task<IActionResult> SubmitPayment(PaymentRequestDto request)
        {
            if (_paymentProcessor.ValidateRequest(request) == false)
                return BadRequest();

            var payment = await _bankService.SubmitPaymentToBank(request);

            await _paymentRepository.SavePaymentAsync(payment);

            _logger.LogInformation(
               "Payment submitted: {@payment}", payment);

            return Ok(payment);
        }

        [Authorize]
        [HttpGet("PaymentDetails/{identifier}")]
        public async Task<IActionResult> GetPaymentDetails(string identifier)
        {
            _logger.LogInformation(
                "Payment details requested: {identifier}", identifier);

            var payment = await _paymentRepository.GetPaymentAsync(identifier);

            if (payment is null)
                return NotFound();

            return Ok(payment);
        }
    }
}
