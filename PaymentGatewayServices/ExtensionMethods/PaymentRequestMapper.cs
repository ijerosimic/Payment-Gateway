using PaymentGatewayDataAccess.Models;
using PaymentGatewayServices.DTOs;
using System;

namespace PaymentGatewayServices.ExtensionMethods
{
    public static class PaymentRequestMapper
    {
        public static PaymentHistory MapToEntity(
           this PaymentRequest paymentRequest)
        {
            return new PaymentHistory
            {
                PaymentIdentifier = new Random().Next(100000, 999999).ToString(),
                CardNumber = paymentRequest.CardNumber,
                CVV = paymentRequest.CVV,
                Amount = paymentRequest.Amount,
                Currency = paymentRequest.Currency
            };
        }
    }
}
