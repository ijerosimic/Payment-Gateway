using Microsoft.Extensions.Logging;
using Moq;
using PaymentGateway.Repository;
using PaymentGateway.Repository.DTOs;
using PaymentGateway.Services;
using PaymentGateway.Services.Concrete;
using Xunit;

namespace PaymentGatewayTests
{
    public class BankServiceShould
    {
        private readonly Mock<ILogger<IBankService>> _fakeLogger;
        private readonly Mock<IBankEndpoint> _bankEndpoint;
        private readonly IBankService _sut;

        public BankServiceShould()
        {
            _fakeLogger = new Mock<ILogger<IBankService>>();
            _bankEndpoint = new Mock<IBankEndpoint>();
            _sut = new BankService(_fakeLogger.Object, _bankEndpoint.Object);
        }

        //[Fact]
        //public void ReturnBankResponse_WhenCalled()
        //{
        //    _bankEndpoint
        //        .Verify(() => Ca)
        //        .Returns("");

        //    var actual = _sut.SubmitPaymentToBank(new PaymentRequestDto());

        //    Assert.NotNull(actual);
        //    Assert.IsType<PaymentRequestDto>(actual);
        //}
    }
}
