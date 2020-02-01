using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using PaymentGateway.DataAccess;
using PaymentGateway.Repository;
using PaymentGateway.Repository.Concrete;
using PaymentGatewayTests.Helpers;
using System;
using Xunit;

namespace PaymentGatewayTests
{
    public class ApiKeysRepositoryShould : IDisposable
    {
        private readonly DbContextOptions<PaymentGatewayDBContext> _options;
        private readonly PaymentGatewayDBContext _context;
        private readonly IApiKeyRepository _sut;
        private readonly Mock<ILogger<IApiKeyRepository>> _fakeLogger;

        public ApiKeysRepositoryShould()
        {
            _fakeLogger = new Mock<ILogger<IApiKeyRepository>>();
            _options = InMemorySQLLite.CreateOptions<PaymentGatewayDBContext>();
            _context = new PaymentGatewayDBContext(_options);
            _sut = new ApiKeyRepository(_context, _fakeLogger.Object);

            _context.Database.EnsureCreated();
        }

        [Theory]
        [InlineData("12345")]
        [InlineData("24242")]
        [InlineData("62622")]
        public async void ReturnTrue_IfKeyIsValid(string validKey)
        {
            _context.SeedKeys(validKey);

            var actual = await _sut.IsKeyValidAsync(validKey);

            Assert.True(actual);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public async void ReturnFalse_IfKeyIsInvalid(string invalidKey)
        {
            _context.SeedKeys("12345");

            var actual = await _sut.IsKeyValidAsync(invalidKey);

            Assert.False(actual);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
