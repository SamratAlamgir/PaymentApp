using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp
{
    public class BankPaymentRequest
    {
        public string MerchantAccountNumber { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public string CVV { get; set; }
    }
}
