using PaymentGateway.DTOs;
using PaymentGateway.ExtensionMethods;
using PaymentGateway.DataAccess;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace PaymentGateway.Services.Concrete
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

            _logger.LogInformation("New payment submitted. Card number: {cardNumber}", payment.CardNumber);
        }

        public PaymentDto GetPaymentById(int id)
        {
            return _ctx.Payments
               .Where(x => x.ID == id)
               .MapToDto()
               .FirstOrDefault();
        }

        public int SaveChanges()
        {
            return _ctx.SaveChanges();
        }
    }
}
