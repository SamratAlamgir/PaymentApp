using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentManager.Responses
{
    public class MerchantResponse
    {
        public Guid MerchantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AccountNumber { get; set; }
    }
}
