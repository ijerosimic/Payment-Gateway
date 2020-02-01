﻿using Microsoft.EntityFrameworkCore;
using PaymentGateway.DataAccess;
using PaymentGateway.Services.Data;
using PaymentGateway.Services.Data.Concrete;
using PaymentGateway.Services.Data.DTOs;
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
        public void SavePayment_ToDatabase()
        {
            var expected = 1;
            var actual = _sut.SavePaymentToDatabase(
                new PaymentDto
                {
                    PaymentIdentifier = "12345",
                    CardNumber = "12345",
                    CVV = 12345,
                    Amount = 1.0M,
                    Currency = "EUR"
                });

            var paymentSaved = _context.Payments
                .Where(x => x.PaymentIdentifier == "12345")
                .Any();

            Assert.Equal(expected, actual);
            Assert.True(paymentSaved);
        }


        [Theory]
        [InlineData("12345")]
        [InlineData("24242")]
        [InlineData("62622")]
        public void ReturnCorrectPayment_WhenGivenValidIdentifier(string identifier)
        {
            _context.Seed(identifier);

            var actual = _sut.GetPaymentByPaymentIdentifier(identifier);

            Assert.NotNull(actual);
            Assert.IsType<PaymentDto>(actual);
            Assert.Equal(identifier, actual.PaymentIdentifier);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}