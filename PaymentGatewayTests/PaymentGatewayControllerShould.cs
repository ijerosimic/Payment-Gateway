using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PaymentGateway.Controllers;
using PaymentGatewayDataAccess;
using PaymentGatewayServices;
using PaymentGatewayServices.DTOs;
using PaymentGatewayTests.Helpers;
using Xunit;

namespace PaymentGatewayTests
{
    public class PaymentGatewayControllerShould
    {
        [Fact]
        public void ReturnExistingPayment_WhenGivenValidID()
        {
            var options = InMemorySQLLite.CreateOptions<PaymentGatewayDBContext>();

            var fakeLogger = new Mock<ILogger<PaymentGatewayController>>().Object;
            var fakeProcessor = new Mock<IPaymentProcessor>();
            var fakeService = new Mock<IPaymentService>();

            fakeService
                .Setup(x => x.GetPaymentById(1))
                            .Returns(new PaymentDto
                            {
                                PaymentIdentifier = "12345",
                                CardNumber = "12345",
                                CVV = 12345,
                                Amount = 1.0M,
                                Currency = "EUR"
                            });

            using var context = new PaymentGatewayDBContext(options);
            context.Database.EnsureCreated();
            context.Seed();

            var controller = new PaymentGatewayController(
                fakeLogger, fakeService.Object, fakeProcessor.Object);

            var result = controller.GetPaymentDetails(1) as ObjectResult;
            
            Assert.Equal(result.StatusCode, StatusCodes.Status200OK);
            Assert.IsType<PaymentDto>(result.Value);
        }

        [Fact]
        public void Return404_WhenGivenInvalidID()
        {
            var options = InMemorySQLLite.CreateOptions<PaymentGatewayDBContext>();

            var fakeLogger = new Mock<ILogger<PaymentGatewayController>>().Object;

            var fakeProcessor = new Mock<IPaymentProcessor>();
            var fakeService = new Mock<IPaymentService>();

            using var context = new PaymentGatewayDBContext(options);
            context.Database.EnsureCreated();
            context.Seed();

            var controller = new PaymentGatewayController(
                fakeLogger, fakeService.Object, fakeProcessor.Object);

            var result = controller.GetPaymentDetails(999999) as ObjectResult;

            Assert.Equal(result.StatusCode, StatusCodes.Status404NotFound);
            Assert.Equal("Payment with specified ID not found", result.Value);
        }
    }
}
