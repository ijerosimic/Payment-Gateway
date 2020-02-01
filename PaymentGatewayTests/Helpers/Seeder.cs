using PaymentGateway.DataAccess;
using PaymentGateway.DataAccess.Models;
using PaymentGateway.DataLayer.Models;
using System.Linq;

namespace PaymentGatewayTests.Helpers
{
    public static class Seeder
    {
        public static void SeedPayments(
            this PaymentGatewayDBContext ctx,
            string identifier)
        {
            if (ctx.Payments.Any())
                return;

            ctx.Add(new Payment
            {
                ID = 1,
                PaymentIdentifier = identifier,
                CardNumber = "12345",
                CVV = 12345,
                Amount = 1.0M,
                Currency = "EUR"
            });

            ctx.SaveChanges();
        }

        public static void SeedKeys(
            this PaymentGatewayDBContext ctx,
            string key)
        {
            if (ctx.ApiKeys.Any())
                return;

            ctx.Add(new ApiKey
            {
                ID = 1,
                Key = key
            });

            ctx.SaveChanges();
        }
    }
}
