﻿using Microsoft.AspNetCore.Authorization;
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
                return BadRequest("Invalid data");

            var payment = _bankService.SubmitPaymentToBank(request);

            await _paymentRepository.SavePaymentAsync(payment);

            _logger.LogInformation(
               "Payment submitted: {@payment}", payment);

            return Ok(payment);
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
