using BankApp;
using Microsoft.EntityFrameworkCore;
using PaymentManager.Contracts;
using PaymentManager.Mapper;
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
        private readonly IMerchantService _merchantService;
        private readonly IBankService _bankService;

        public PaymentService(IPaymentRepository paymentRepository, 
            IMerchantService merchantService, 
            IBankService bankService)
        {
            _paymentRepository = paymentRepository;
            _merchantService = merchantService;
            _bankService = bankService;
        }
        public async Task<PaymentResponse> GetPaymentByIdAsync(Guid paymentId)
        {
            var payment = await _paymentRepository.GetPaymentById(paymentId);

            return payment.ConverToPaymentResponse(); 
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

            return newPayment.ConverToPaymentResponse();
        }

        private async Task<BankPaymentResponse> MakeBankPaymentRequest(MakePaymentRequest paymentRequest)
        {
            var merchant = await _merchantService.GetMerchantById(paymentRequest.MerchantId);

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
