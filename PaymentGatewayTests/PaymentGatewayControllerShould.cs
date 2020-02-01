using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PaymentGateway.Controllers;
using PaymentGateway.Services.BusinessLogic;
using PaymentGateway.Services.Data;
using PaymentGateway.Services.Data.DTOs;
using Xunit;

namespace PaymentGatewayTests
{
    public class PaymentGatewayControllerShould
    {
        private readonly Mock<ILogger<PaymentGatewayController>> _fakeLogger;
        private readonly Mock<IPaymentProcessor> _fakeProcessor;
        private readonly Mock<IPaymentService> _fakeService;
        private readonly PaymentGatewayController _sut;

        public PaymentGatewayControllerShould()
        {
            _fakeLogger = new Mock<ILogger<PaymentGatewayController>>();
            _fakeProcessor = new Mock<IPaymentProcessor>();
            _fakeService = new Mock<IPaymentService>();
            _sut = new PaymentGatewayController(
                _fakeLogger.Object, _fakeService.Object, _fakeProcessor.Object);
        }

        [Fact]
        public void ReturnsOkResult_WhenGivenValidPayment()
        {
            _fakeProcessor
                .Setup(x => x.ValidateRequest(null))
                .Returns(true);

            var expected = StatusCodes.Status200OK;
            var actual = _sut.SubmitPayment(null) as ObjectResult;

            Assert.Equal(expected, actual.StatusCode);
            Assert.Equal("Sucessfully processed payment", actual.Value);
        }

        [Fact]
        public void ReturnExistingPayment_WhenGivenValidID()
        {
            _fakeService
                .Setup(x => x.GetPaymentById("12345"))
                .Returns(new PaymentDto());

            var expected = StatusCodes.Status200OK;
            var actual = _sut.GetPaymentDetails("12345") as ObjectResult;

            Assert.Equal(expected, actual.StatusCode);
            Assert.IsType<PaymentDto>(actual.Value);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Return404_WhenGivenInvalidID(string id)
        {
            _fakeService
              .Setup(x => x.GetPaymentById(id))
              .Returns(() => null);

            var exptected = StatusCodes.Status404NotFound;
            var actual = _sut.GetPaymentDetails(id) as ObjectResult;

            Assert.Equal(exptected, actual.StatusCode);
            Assert.Equal("Payment with specified ID not found", actual.Value);
        }
    }
}
