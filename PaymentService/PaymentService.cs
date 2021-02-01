using BankApp;
using Microsoft.EntityFrameworkCore;
using PaymentManager.Contracts;
using PaymentManager.Mapper;
using PaymentManager.Requests;
using PaymentManager.Responses;
using PaymentManager.Validators;
using Repository.Models;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PaymentManager
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMerchantService _merchantService;
        private readonly IBankService _bankService;
        private readonly ICardValidatorProvider _cardValidatorProvider;

        public PaymentService(IPaymentRepository paymentRepository, 
            IMerchantService merchantService, 
            IBankService bankService,
            ICardValidatorProvider cardValidatorProvider)
        {
            _paymentRepository = paymentRepository;
            _merchantService = merchantService;
            _bankService = bankService;
            _cardValidatorProvider = cardValidatorProvider;
        }
        public async Task<PaymentResponse> GetPaymentByIdAsync(Guid paymentId)
        {
            var payment = await _paymentRepository.GetPaymentById(paymentId);

            return payment.ConverToPaymentResponse(); 
        }

        public async Task<PaymentResponse> MakePayment(MakePaymentRequest paymentRequest)
        {
            ValidatePaymentRequest(paymentRequest);

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

        private void ValidatePaymentRequest(MakePaymentRequest paymentRequest)
        {
            var cardValidator = _cardValidatorProvider.GetCardValidator(paymentRequest.CardNumber);
            cardValidator.Validate(paymentRequest);
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
