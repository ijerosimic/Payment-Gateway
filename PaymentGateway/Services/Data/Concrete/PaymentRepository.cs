using PaymentGateway.DataAccess;
using System.Linq;
using PaymentGateway.Services.Data.DTOs;
using PaymentGateway.Services.Data.ExtensionMethods;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PaymentGateway.Services.Data.Concrete
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentGatewayDBContext _ctx;

        public PaymentRepository(PaymentGatewayDBContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<int> SavePaymentAsync(PaymentDto payment)
        {
            await _ctx.AddAsync(payment.MapToEntity());

            return await _ctx.SaveChangesAsync();
        }

        public async Task<PaymentDto> GetPaymentAsync(string paymentIdentifier)
        {
            return await _ctx.Payments
               .Where(x => x.PaymentIdentifier.Equals(paymentIdentifier))
               .MapToDto()
               .FirstOrDefaultAsync();
        }
    }
}
