﻿namespace PaymentGateway.DataAccess.Models
{
    public class Payment
    {
        public int ID { get; set; }
        public string PaymentIdentifier { get; set; }
        public string Status { get; set; }
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public int CVV { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
