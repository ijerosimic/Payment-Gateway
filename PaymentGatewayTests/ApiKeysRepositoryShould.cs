using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using PaymentGateway.DataAccess;
using PaymentGateway.Services.Data;
using PaymentGateway.Services.Data.Concrete;
using PaymentGatewayTests.Helpers;
using Xunit;

namespace PaymentGatewayTests
{
    public class ApiKeysRepositoryShould
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
        public async void ReturnTrue_IfKeyIsValid(string key)
        {
            _context.SeedKeys(key);

            var actual = await _sut.IsKeyValid(key);

            Assert.True(actual);
        }

        [Theory]
        [InlineData("12345")]
        [InlineData("24242")]
        [InlineData("62622")]
        public async void ReturnFalse_IfKeyIsInvalid(string key)
        {
            _context.SeedKeys(key);

            var actual = await _sut.IsKeyValid(null);

            Assert.False(actual);
        }
    }
}
