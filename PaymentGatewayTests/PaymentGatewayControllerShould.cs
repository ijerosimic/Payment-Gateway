using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PaymentGateway.Controllers;
using PaymentGateway.DataAccess;
using PaymentGateway.Services.BusinessLogic;
using PaymentGateway.Services.Data;
using PaymentGateway.Services.Data.DTOs;
using PaymentGatewayTests.Helpers;
using Xunit;

namespace PaymentGatewayTests
{
    public class PaymentGatewayControllerShould
    {
        private readonly Mock<ILogger<PaymentGatewayController>> _fakeLogger;
        private readonly Mock<IPaymentProcessor> _fakeProcessor;
        private readonly Mock<IPaymentService> _fakeService;
        private readonly PaymentGatewayController _controller;

        public PaymentGatewayControllerShould()
        {
            _fakeLogger = new Mock<ILogger<PaymentGatewayController>>();
            _fakeProcessor = new Mock<IPaymentProcessor>();
            _fakeService = new Mock<IPaymentService>();
            _controller = new PaymentGatewayController(
                _fakeLogger.Object, _fakeService.Object, _fakeProcessor.Object);
        }

        [Fact]
        public void ReturnExistingPayment_WhenGivenValidID()
        {
            _fakeService
                .Setup(x => x.GetPaymentById("12345"))
                            .Returns(new PaymentDto
                            {
                                PaymentIdentifier = "12345",
                                CardNumber = "12345",
                                CVV = 12345,
                                Amount = 1.0M,
                                Currency = "EUR"
                            });

            var expected = StatusCodes.Status200OK;
            var actual = _controller.GetPaymentDetails("12345") as ObjectResult;

            Assert.Equal(expected, actual.StatusCode);
            Assert.IsType<PaymentDto>(actual.Value);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Return404_WhenGivenInvalidID(string id)
        {
            var exptected = StatusCodes.Status404NotFound;
            var actual = _controller.GetPaymentDetails(id) as ObjectResult;

            Assert.Equal(exptected, actual.StatusCode);
            Assert.Equal("Payment with specified ID not found", actual.Value);
        }
    }
}
