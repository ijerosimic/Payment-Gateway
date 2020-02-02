using Microsoft.EntityFrameworkCore;
using PaymentGateway.DataAccess;
using PaymentGateway.Repository;
using PaymentGateway.Repository.Concrete;
using PaymentGateway.Repository.DTOs;
using PaymentGatewayTests.Helpers;
using System;
using System.Linq;
using Xunit;

namespace PaymentGatewayTests
{
    public class PaymentRepositoryShould : IDisposable
    {
        private readonly DbContextOptions<PaymentGatewayDBContext> _options;
        private readonly PaymentGatewayDBContext _context;
        private readonly IPaymentRepository _sut;

        public PaymentRepositoryShould()
        {
            _options = InMemorySQLLite.CreateOptions<PaymentGatewayDBContext>();
            _context = new PaymentGatewayDBContext(_options);
            _sut = new PaymentRepository(_context);

            _context.Database.EnsureCreated();
        }

        [Fact]
        public async void SavePayment_ToDatabase()
        {
            var expected = 1;
            var actual = await _sut.SavePaymentAsync(
                new PaymentRequestDto
                {
                    PaymentIdentifier = "12345"
                });

            var paymentSaved = await _context.Payments
                .Where(x => x.PaymentIdentifier == "12345")
                .AnyAsync();

            Assert.Equal(expected, actual);
            Assert.True(paymentSaved);
        }

        [Theory]
        [InlineData("12345")]
        [InlineData("24242")]
        [InlineData("62622")]
        public async void ReturnCorrectPayment_WhenGivenValidIdentifier(string validIdentifier)
        {
            _context.SeedPayments(validIdentifier);

            var actual = await _sut.GetPaymentAsync(validIdentifier);

            Assert.NotNull(actual);
            Assert.IsType<PaymentDetailsDto>(actual);
            Assert.Equal(validIdentifier, actual.PaymentIdentifier);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public async void ReturnNull_WhenGivenInvalidIdentifier(string invalidIdentifier)
        {
            _context.SeedPayments("12345");

            var actual = await _sut.GetPaymentAsync(invalidIdentifier);

            Assert.Null(actual);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
