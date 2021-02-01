using PaymentManager.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentManager.Contracts
{
    public interface IMerchantService
    {
        Task<MerchantResponse> GetMerchantById(Guid merchantId);
    }
}
