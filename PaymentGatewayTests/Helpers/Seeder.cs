using PaymentGateway.Repository;
using PaymentGateway.Repository.Models;
using System;
using System.Linq;

namespace PaymentGatewayTests.Helpers
{
    public static class Seeder
    {
        public static void Seed(this PaymentGatewayDBContext ctx)
        {
            if (ctx.PaymentHistory.Any())
                return;

            var currencies = new string[]
            {
                "HRK", "EUR", "GBP", "USD", "JPY", "NOK", "CHF", "CZK"
            };

            for (var i = 1; i < 50; i++)
            {
                ctx.Add(new PaymentHistory
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
