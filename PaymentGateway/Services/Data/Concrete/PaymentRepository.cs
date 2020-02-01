using PaymentGateway.DataAccess;
using System.Linq;
using PaymentGateway.Services.Data.DTOs;
using PaymentGateway.Services.Data.ExtensionMethods;

namespace PaymentGateway.Services.Data.Concrete
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentGatewayDBContext _ctx;

        public PaymentRepository(PaymentGatewayDBContext ctx)
        {
            _ctx = ctx;
        }

        public int SavePaymentToDatabase(PaymentDto payment)
        {
            _ctx.Add(payment.MapToEntity());

            return _ctx.SaveChanges();
        }

        public PaymentDto GetPaymentByPaymentIdentifier(string paymentIdentifier)
        {
            return _ctx.Payments
               .Where(x => x.PaymentIdentifier.Equals(paymentIdentifier))
               .MapToDto()
               .FirstOrDefault();
        }
    }
}
