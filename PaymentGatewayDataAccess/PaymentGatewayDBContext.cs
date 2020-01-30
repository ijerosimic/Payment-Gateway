using Microsoft.EntityFrameworkCore;
using PaymentGatewayDataAccess.Models;

namespace PaymentGatewayDataAccess
{
    public class PaymentGatewayDBContext : DbContext
    {
        public PaymentGatewayDBContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbContext Context => this;

        public DbSet<Payment> Payments { get; set; }
    }
}
