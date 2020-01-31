using PaymentGateway.DTOs;
using PaymentGateway.ExtensionMethods;
using PaymentGateway.DataAccess;
using System.Linq;

namespace PaymentGateway.Services.Concrete
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
