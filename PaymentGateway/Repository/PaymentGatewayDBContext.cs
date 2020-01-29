using Microsoft.EntityFrameworkCore;
using PaymentGateway.Repository.Models;
using System;

namespace PaymentGateway.Repository
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
