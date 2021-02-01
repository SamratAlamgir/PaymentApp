using Microsoft.EntityFrameworkCore;
using PaymentManager.Contracts;
using PaymentManager.Mapper;
using PaymentManager.Responses;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentManager
{
    public class MerchantService : IMerchantService
    {
        private readonly IMerchantRepository _merchantRepository;

        public MerchantService(IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository;
        }
        public async Task<MerchantResponse> GetMerchantById(Guid merchantId)
        {
            var merchant = await _merchantRepository.FindByCondition(x => x.MerchantId == merchantId).SingleAsync();

            return merchant.ConvertToMerchantResponse();
        }
    }
}
