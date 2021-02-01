using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentManager.Requests
{
    public interface IPaymentRequest
    {
        public Guid MerchantId { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public string CVV { get; set; }
    }
}
