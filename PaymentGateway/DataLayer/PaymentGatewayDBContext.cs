using Microsoft.EntityFrameworkCore;
using PaymentGateway.DataAccess.Models;
using PaymentGateway.DataLayer.Models;

namespace PaymentGateway.DataAccess
{
    public class PaymentGatewayDBContext : DbContext
    {
        public PaymentGatewayDBContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbContext Context => this;

        public DbSet<Payment> Payments { get; set; }
        public DbSet<ApiKey> ApiKeys { get; set; }
    }
}
