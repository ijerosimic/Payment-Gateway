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

        public DbSet<PaymentHistory> PaymentHistory { get; set; }
    }
}
