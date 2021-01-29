using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Models
{
    public class PaymentDetail
    {
        public Guid PaymentDetailId { get; set; }
        public decimal Amount { get; set; }
        public string CardNumber { get; set; }
        public Guid ExternalRefId { get; set; }
    }
}
