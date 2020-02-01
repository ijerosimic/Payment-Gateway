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

        public PaymentService(PaymentGatewayDBContext ctx)
        {
            _ctx = ctx;
        }

        public void AddPayment(PaymentDto payment)
        {
            _ctx.Add(payment.MapToEntity());
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
