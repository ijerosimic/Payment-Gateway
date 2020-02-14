using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PaymentGateway.BussinesLogic;
using PaymentGateway.Controllers;
using PaymentGateway.Repository;
using PaymentGateway.Repository.DTOs;
using System.Threading.Tasks;
using Xunit;

namespace PaymentGatewayTests
{
    public class PaymentGatewayControllerShould
    {
        private readonly Mock<ILogger<PaymentGatewayController>> _fakeLogger;
        private readonly Mock<IPaymentProcessor> _fakeProcessor;
        private readonly Mock<IPaymentRepository> _fakeRepository;
        private readonly Mock<IBankService> _fakeBankService;
        private readonly PaymentGatewayController _sut;

        public PaymentGatewayControllerShould()
        {
            _fakeLogger = new Mock<ILogger<PaymentGatewayController>>();
            _fakeProcessor = new Mock<IPaymentProcessor>();
            _fakeRepository = new Mock<IPaymentRepository>();
            _fakeBankService = new Mock<IBankService>();
            _sut = new PaymentGatewayController(
                _fakeRepository.Object, _fakeProcessor.Object, 
                _fakeBankService.Object, _fakeLogger.Object);
        }

        [Fact]
        public async void ReturnOkResult_WhenGivenValidPaymentData()
        {
            var request = new PaymentRequestDto();

            _fakeProcessor
                .Setup(x => x.ValidateRequest(request))
                .Returns(true);

            _fakeBankService
                .Setup(x => x.SubmitPaymentToBankAsync(request))
                .Returns(Task.FromResult(request));

            var expected = StatusCodes.Status200OK;
            var actual = await _sut.SubmitPayment(request) as ObjectResult;

            Assert.Equal(expected, actual.StatusCode);
            Assert.IsType<PaymentRequestDto>(actual.Value);
        }

        [Fact]
        public async void ReturnBadRequestResult_WhenGivenInvalidPaymentData()
        {
            var request = new PaymentRequestDto();

            _fakeProcessor
                .Setup(x => x.ValidateRequest(null))
                .Returns(false);

            var expected = StatusCodes.Status400BadRequest;
            var actual = await _sut.SubmitPayment(null) as BadRequestResult;

            Assert.Equal(expected, actual.StatusCode);
        }

        [Fact]
        public async void ReturnExistingPayment_WhenGivenValidID()
        {
            _fakeRepository
                .Setup(x => x.GetPaymentAsync("12345"))
                .Returns(Task.FromResult(new PaymentDetailsDto()));

            var expected = StatusCodes.Status200OK;
            var actual = await _sut.GetPaymentDetails("12345") as ObjectResult;

            Assert.Equal(expected, actual.StatusCode);
            Assert.IsType<PaymentDetailsDto>(actual.Value);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public async void ReturnNotFoundResult_WhenGivenInvalidID(string id)
        {
            _fakeRepository
              .Setup(x => x.GetPaymentAsync(id))
              .Returns(Task.FromResult<PaymentDetailsDto>(null));

            var expected = StatusCodes.Status404NotFound;
            var actual = await _sut.GetPaymentDetails(id) as NotFoundResult;

            Assert.Equal(expected, actual.StatusCode);
        }
    }
}
