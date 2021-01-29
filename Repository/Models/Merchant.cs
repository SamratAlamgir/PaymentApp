using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Models
{
    public class Merchant
    {
        public Guid MerchantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
