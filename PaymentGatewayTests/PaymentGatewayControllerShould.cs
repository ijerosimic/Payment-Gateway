using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PaymentGateway.Controllers;
using PaymentGateway.Repository;
using PaymentGateway.Repository.Models;
using PaymentGatewayTests.Helpers;
using System;
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

            using var context = new PaymentGatewayDBContext(options);
            context.Database.EnsureCreated();
            context.Seed();

            var controller = new PaymentGatewayController(fakeLogger, context);

            var result = controller.GetPaymentDetails(1) as OkObjectResult;

            Assert.Equal(result.StatusCode, StatusCodes.Status200OK);
            Assert.IsType<PaymentHistory>(result.Value);
        }

        [Fact]
        public void Return404_WhenGivenInvalidID()
        {
            var options = InMemorySQLLite.CreateOptions<PaymentGatewayDBContext>();
            var fakeLogger = new Mock<ILogger<PaymentGatewayController>>().Object;

            using var context = new PaymentGatewayDBContext(options);
            context.Database.EnsureCreated();
            context.Seed();

            var controller = new PaymentGatewayController(fakeLogger, context);

            var result = controller.GetPaymentDetails(999999) as NotFoundObjectResult;

            Assert.Equal(result.StatusCode, StatusCodes.Status404NotFound);
            Assert.Equal("Payment with specified ID not found", result.Value);
        }
    }
}
