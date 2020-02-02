using PaymentGateway.DataAccess;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PaymentGateway.Repository.DTOs;
using PaymentGateway.Repository.ExtensionMethods;

namespace PaymentGateway.Repository.Concrete
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentGatewayDBContext _ctx;

        public PaymentRepository(PaymentGatewayDBContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<int> SavePaymentAsync(PaymentRequestDto payment)
        {
            await _ctx.AddAsync(payment.MapToEntity());

            return await _ctx.SaveChangesAsync();
        }

        public async Task<PaymentDetailsDto> GetPaymentAsync(string paymentIdentifier)
        {
            return await _ctx.Payments
               .Where(x => x.PaymentIdentifier.Equals(paymentIdentifier))
               .MapToDto()
               .FirstOrDefaultAsync();
        }
    }
}
