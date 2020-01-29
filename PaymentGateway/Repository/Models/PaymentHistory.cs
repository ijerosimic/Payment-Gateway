﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Repository.Models
{
    public class PaymentHistory
    {
        public int ID { get; set; }
        public string PaymentIdentifier { get; set; }
        public string CardNumber { get; set; }
        public int CVV { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}