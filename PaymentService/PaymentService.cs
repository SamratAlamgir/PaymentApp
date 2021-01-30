using PaymentManager.Contracts;
using PaymentManager.Responses;
using Repository.Models;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentManager
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }
        public async Task<PaymentResponse> GetPaymentByIdAsync(Guid paymentId)
        {
            var payment = await _paymentRepository.GetPaymentById(paymentId);

            return payment != null ? new PaymentResponse { PaymentId = payment.PaymentId } : null; 
        }
    }
}
