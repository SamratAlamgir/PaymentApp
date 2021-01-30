using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    public interface IBankService
    {
        Task<BankPaymentResponse> MakeBankPayment(BankPaymentRequest request);
    }
}
