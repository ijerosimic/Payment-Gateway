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
        private readonly Mock<IPaymentRepository> _fakeRepository;
        private readonly PaymentGatewayController _sut;

        public PaymentGatewayControllerShould()
        {
            _fakeLogger = new Mock<ILogger<PaymentGatewayController>>();
            _fakeProcessor = new Mock<IPaymentProcessor>();
            _fakeRepository = new Mock<IPaymentRepository>();
            _sut = new PaymentGatewayController(
                _fakeLogger.Object, _fakeRepository.Object, _fakeProcessor.Object);
        }

        [Fact]
        public void ReturnOkResult_WhenGivenValidPayment()
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
            _fakeRepository
                .Setup(x => x.GetPaymentByPaymentIdentifier("12345"))
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
        public void ReturnNotFoundResult_WhenGivenInvalidID(string id)
        {
            _fakeRepository
              .Setup(x => x.GetPaymentByPaymentIdentifier(id))
              .Returns(() => null);

            var exptected = StatusCodes.Status404NotFound;
            var actual = _sut.GetPaymentDetails(id) as ObjectResult;

            Assert.Equal(exptected, actual.StatusCode);
            Assert.Equal("Payment with specified ID not found", actual.Value);
        }
    }
}
