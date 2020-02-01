﻿using PaymentGateway.DataAccess.Models;
using PaymentGateway.Services.Data.DTOs;
using System;
using System.Linq;

namespace PaymentGateway.Services.Data.ExtensionMethods
{
    public static class PaymentMapper
    {
        public static Payment MapToEntity(
           this PaymentDto paymentRequest)
        {
            return new Payment
            {
                PaymentIdentifier = paymentRequest.PaymentIdentifier,
                CardNumber = paymentRequest.CardNumber,
                CVV = paymentRequest.CVV,
                Amount = paymentRequest.Amount,
                Currency = paymentRequest.Currency
            };
        }

        public static IQueryable<PaymentDto> MapToDto(
            this IQueryable<Payment> entity)
        {
            return entity.Select(x =>
                new PaymentDto
                {
                    PaymentIdentifier = x.PaymentIdentifier,
                    CardNumber = x.CardNumber,
                    CVV = x.CVV,
                    Amount = x.Amount,
                    Currency = x.Currency
                });
        }
    }
}
