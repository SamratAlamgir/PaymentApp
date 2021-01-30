using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repositories
{
    public class MerchantRepository : Repository<Merchant>, IMerchantRepository
    {
        public MerchantRepository(PaymentDataContext dbContext) : base(dbContext)
        {

        }
    }
}
