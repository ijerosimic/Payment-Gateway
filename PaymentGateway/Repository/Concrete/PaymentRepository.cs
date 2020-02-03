using PaymentGateway.DataAccess;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PaymentGateway.Repository.DTOs;
using PaymentGateway.Repository.ExtensionMethods;
using Microsoft.Extensions.Logging;

namespace PaymentGateway.Repository.Concrete
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentGatewayDBContext _ctx;
        private readonly ILogger<IPaymentRepository> _logger;

        public PaymentRepository(
            PaymentGatewayDBContext ctx,
            ILogger<IPaymentRepository> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public async Task<int> SavePaymentAsync(PaymentRequestDto payment)
        {
            _logger.LogInformation(
               "Saving payment to database: {@payment}", payment);

            await _ctx.AddAsync(payment.MapToEntity());

            return await _ctx.SaveChangesAsync();
        }

        public async Task<PaymentDetailsDto> GetPaymentAsync(string paymentIdentifier)
        {
            _logger.LogInformation(
               "Querying payments table for identifier: {paymentIdentifer}", paymentIdentifier);

            return await _ctx.Payments
               .Where(x => x.PaymentIdentifier.Equals(paymentIdentifier))
               .MapToDto()
               .FirstOrDefaultAsync();
        }
    }
}
