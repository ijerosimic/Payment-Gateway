using PaymentGateway.DataAccess;
using System.Linq;
using PaymentGateway.Services.Data.DTOs;
using PaymentGateway.Services.Data.ExtensionMethods;
using Microsoft.Extensions.Logging;

namespace PaymentGateway.Services.Data.Concrete
{
    public class PaymentService : IPaymentService
    {
        private readonly PaymentGatewayDBContext _ctx;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(
            PaymentGatewayDBContext ctx,
            ILogger<PaymentService> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public void AddPayment(PaymentDto payment)
        {
            _ctx.Add(payment.MapToEntity());

            _logger.LogInformation("New payment submitted. Card number: {@cardNumber}", payment);
        }

        public PaymentDto GetPaymentById(string requestId)
        {
            return _ctx.Payments
               .Where(x => x.PaymentIdentifier.Equals(requestId))
               .MapToDto()
               .FirstOrDefault();
        }

        public int SaveChanges()
        {
            return _ctx.SaveChanges();
        }
    }
}
