using PaymentManager.Responses;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentManager.Mapper
{
    public static class MerchantResponseMapper
    {
        public static MerchantResponse ConvertToMerchantResponse(this Merchant merchant)
        {
            if (merchant == null)
                return null;

            var merchantRespone = new MerchantResponse
            {
                MerchantId = merchant.MerchantId,
                FirstName = merchant.FirstName,
                LastName = merchant.LastName,
                AccountNumber = merchant.AccountNumber
            };

            return merchantRespone;
        }
    }
}
