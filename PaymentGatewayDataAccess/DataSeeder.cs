using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PaymentGatewayDataAccess.Models;
using System;
using System.Linq;

namespace PaymentGatewayDataAccess
{
    public class DataSeeder
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using var ctx = new PaymentGatewayDBContext(
                serviceProvider.GetRequiredService<DbContextOptions<PaymentGatewayDBContext>>());

            if (ctx.Payments.Any())
                return;

            var currencies = new string[]
            {
                "HRK", "EUR", "GBP", "USD", "JPY", "NOK", "CHF", "CZK"
            };

            for (var i = 1; i < 50; i++)
            {
                ctx.Add(new Payment
                {
                    ID = i,
                    PaymentIdentifier = new Random().Next(100000, 999999).ToString(),
                    CardNumber = new Random().Next(100000000, 999999999).ToString(),
                    CVV = new Random().Next(000, 999),
                    Amount = Math.Round(Convert.ToDecimal(new Random().NextDouble() * (99999.99 - 0.1) + 99999.99), 2),
                    Currency = currencies[new Random().Next(currencies.Length - 1)]
                });
            };

            ctx.SaveChanges();
        }
    }
}
