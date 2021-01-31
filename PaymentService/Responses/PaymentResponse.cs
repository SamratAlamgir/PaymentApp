using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentManager.Responses
{
    public class PaymentResponse
    {
        public Guid PaymentId { get; set; }
        public Guid MerchantId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CardNumberMasked { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public Guid BankPaymentRef { get; set; }
        public string PaymentStaus { get; set; }
    }
}
