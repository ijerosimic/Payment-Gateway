﻿using System.Collections.Generic;

namespace PaymentGateway.BussinesLogic.Helpers
{
    public class PaymentDataValidator
    {
        private readonly static List<string> currencies = new List<string>
        {
            "HRK", "EUR", "GBP", "USD", "JPY", "NOK", "CHF", "CZK"
        };

        public static bool ValidateCurrency(string currency)
        {
            return currencies.Contains(currency);
        }

        public static bool ValidateCreditCardNumber(string cardNumber)
        {
            //Logic for validating credit card number structure

            return string.IsNullOrWhiteSpace(cardNumber) == false;
        }
    }
}
