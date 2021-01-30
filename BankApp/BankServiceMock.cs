using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    public class BankServiceMock : IBankService
    {
        public async Task<BankPaymentResponse> MakeBankPayment(BankPaymentRequest request)
        {
            // Mock BankApi response
            await Task.Delay(1);
            return new BankPaymentResponse { BankPaymentRef = Guid.NewGuid(), PaymentStatus = AppCodes.PaymentStatus.Successful };
        }
    }
}
