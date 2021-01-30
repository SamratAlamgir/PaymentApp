using AppCodes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp
{
    public class BankPaymentResponse
    {
        public Guid BankPaymentRef { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
}
