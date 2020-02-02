using PaymentGateway.DataAccess.Models;
using PaymentGateway.Repository.DTOs;
using System.Linq;

namespace PaymentGateway.Repository.ExtensionMethods
{
    public static class PaymentMapper
    {
        public static Payment MapToEntity(
           this PaymentRequestDto request)
        {
            return new Payment
            {
                PaymentIdentifier = request.PaymentIdentifier,
                CardHolderName = request.CardHolderName,
                CardNumber = request.CardNumber,
                CVV = request.CVV,
                Amount = request.Amount,
                Currency = request.Currency
            };
        }

        public static IQueryable<PaymentDetailsDto> MapToDto(
            this IQueryable<Payment> entity)
        {
            return entity.Select(x =>
                new PaymentDetailsDto
                {
                    PaymentIdentifier = x.PaymentIdentifier,
                    CardNumber = x.CardNumber,
                    CVV = x.CVV,
                    CardHolderName = x.CardHolderName,
                    Amount = x.Amount,
                    Currency = x.Currency
                });
        }
    }
}
