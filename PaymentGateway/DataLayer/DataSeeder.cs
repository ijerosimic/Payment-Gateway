using PaymentGateway.DataAccess.Models;
using PaymentGateway.DataLayer.Models;
using System;
using System.Linq;

namespace PaymentGateway.DataAccess
{
    public static class DataSeeder
    {
        public static void Seed(this PaymentGatewayDBContext ctx)
        {
            if (ctx.Payments.Any() == false)
            {
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
                        Currency = currencies[new Random().Next(currencies.Length - 1)],
                        Status = "Authorized",
                        CardHolderName = "John Wick"
                    });
                };

                ctx.Add(new Payment
                {
                    ID = 51,
                    PaymentIdentifier = "12345",
                    CardNumber = new Random().Next(100000000, 999999999).ToString(),
                    CVV = new Random().Next(000, 999),
                    Amount = Math.Round(Convert.ToDecimal(new Random().NextDouble() * (99999.99 - 0.1) + 99999.99), 2),
                    Currency = currencies[new Random().Next(currencies.Length - 1)],
                    Status = "Declined",
                    CardHolderName = "John Wick"
                });
            }

            if (ctx.ApiKeys.Any() == false)
            {
                ctx.Add(new ApiKey
                {
                    Key = "12345"
                });
            }

            ctx.SaveChanges();
        }
    }
}
