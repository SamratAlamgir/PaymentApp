using PaymentManager.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentManager.Contracts
{
    public interface IPaymentService
    {
        Task<PaymentDto> GetPaymentByIdAsync(Guid paymentId);
    }
}
