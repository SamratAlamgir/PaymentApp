using Microsoft.EntityFrameworkCore;
using PaymentManager.Contracts;
using PaymentManager.Requests;
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

        public async Task<PaymentResponse> MakePayment(MakePaymentRequest paymentRequest)
        {
            var cardNumber = paymentRequest.CardNumber;
            var newPayment = new Payment
            {
                PaymentId = Guid.NewGuid(),
                MerchantId = paymentRequest.MerchantId,
                CardNumberMasked = cardNumber.Substring(cardNumber.Length - 4).PadLeft(cardNumber.Length, '*'),
                Amount = paymentRequest.Amount,
                CurrencyCode = paymentRequest.CurrencyCode,
                CreatedOn = DateTime.UtcNow
            };

            newPayment = await _paymentRepository.AddAsync(newPayment);

            return new PaymentResponse { PaymentId = newPayment.PaymentId };
        }
    }
}
