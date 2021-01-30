using BankApp;
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
        private readonly IMerchantRepository _merchantRepository;
        private readonly IBankService _bankService;

        public PaymentService(IPaymentRepository paymentRepository, 
            IMerchantRepository merchantRepository, 
            IBankService bankService)
        {
            _paymentRepository = paymentRepository;
            _merchantRepository = merchantRepository;
            _bankService = bankService;
        }
        public async Task<PaymentResponse> GetPaymentByIdAsync(Guid paymentId)
        {
            var payment = await _paymentRepository.GetPaymentById(paymentId);

            return payment != null ? new PaymentResponse { PaymentId = payment.PaymentId } : null; 
        }

        public async Task<PaymentResponse> MakePayment(MakePaymentRequest paymentRequest)
        {
            var bankPaymentResponse = await MakeBankPaymentRequest(paymentRequest);

            var cardNumber = paymentRequest.CardNumber;
            var newPayment = new Payment
            {
                PaymentId = Guid.NewGuid(),
                MerchantId = paymentRequest.MerchantId,
                CardNumberMasked = cardNumber.Substring(cardNumber.Length - 4).PadLeft(cardNumber.Length, '*'),
                Amount = paymentRequest.Amount,
                CurrencyCode = paymentRequest.CurrencyCode,
                CreatedOn = DateTime.UtcNow,
                BankPaymentRef = bankPaymentResponse.BankPaymentRef,
                PaymentStaus = (short)bankPaymentResponse.PaymentStatus
            };

            newPayment = await _paymentRepository.AddAsync(newPayment);

            return new PaymentResponse { PaymentId = newPayment.PaymentId };
        }

        private async Task<BankPaymentResponse> MakeBankPaymentRequest(MakePaymentRequest paymentRequest)
        {
            var merchant = await _merchantRepository.FindByCondition(x => x.MerchantId == paymentRequest.MerchantId).SingleAsync();

            var bankPaymentRequest = new BankPaymentRequest
            {
                MerchantAccountNumber = merchant.AccountNumber,
                CardNumber = paymentRequest.CardNumber,
                ExpiryDate = paymentRequest.ExpiryDate,
                CVV = paymentRequest.CVV,
                CurrencyCode = paymentRequest.CurrencyCode,
                Amount = paymentRequest.Amount
            };

            return await _bankService.MakeBankPayment(bankPaymentRequest);
            
        }
    }
}
