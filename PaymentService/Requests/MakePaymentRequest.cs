using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PaymentManager.Requests
{
    public class MakePaymentRequest
    {
        public Guid MerchantId { get; set; }
        [Required]
        [MinLength(13)]
        public string CardNumber { get; set; }
        [Required]
        public string ExpiryDate { get; set; }
        [Required]
        [Range(0.01, 99999)]
        public decimal Amount { get; set; }
        [Required]
        public string CurrencyCode {get; set;}
        [Required]
        public string CVV { get; set; }
    }
}
