﻿using Microsoft.Extensions.Logging;
using Moq;
using PaymentGateway.BussinesLogic;
using PaymentGateway.BussinesLogic.Concrete;
using PaymentGateway.Repository.DTOs;
using System.Threading.Tasks;
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

        [Fact]
        public async void ReturnsPaymentRequest_WhenCalled()
        {
            var request = new PaymentRequestDto();

            _bankEndpoint
                .Setup(x => x.SubmitPaymentRequest(request))
                .Returns(Task.FromResult(BankResponse.CreateResponse()));

            var actual = await _sut.SubmitPaymentToBankAsync(request);

            Assert.NotNull(actual);
            Assert.IsType<PaymentRequestDto>(actual);
            Assert.False(string.IsNullOrWhiteSpace(actual.PaymentIdentifier));
            Assert.False(string.IsNullOrWhiteSpace(actual.Status));
        }
    }
}
