using System;
using System.Collections.Generic;
using System.Text;

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
        public ICollection<PaymentDetail> PaymentDetails {get; set;}

        public Payment()
        {
            PaymentDetails = new List<PaymentDetail>();
        }
    }
}
