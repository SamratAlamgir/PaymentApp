using System;

namespace Repository.Models
{
    public class Payment
    {
        public Guid PaymentId { get; set; }
        public Guid MerchantId { get; set; }
        public DateTime CreatedOn { get; set; }
        public Merchant Merchant { get; set; }
        public string CardNumberMasked { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public Guid BankPaymentRef { get; set; }
        public short PaymentStaus { get; set; }
    }
}
