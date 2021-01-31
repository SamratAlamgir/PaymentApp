using PaymentManager.Responses;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentManager.Mapper
{
    public static class PaymentResponseMapper
    {
        public static PaymentResponse ConverToPaymentResponse(this Payment payment)
        {
            if (payment == null)
                return null;

            var paymentResponse = new PaymentResponse
            {
                PaymentId = payment.PaymentId,
                MerchantId = payment.MerchantId,
                Amount = payment.Amount,
                CurrencyCode = payment.CurrencyCode,
                PaymentStaus = payment.PaymentStaus.ToString(),
                CardNumberMasked = payment.CardNumberMasked,
                CreatedOn = payment.CreatedOn,
                BankPaymentRef = payment.BankPaymentRef
            };

            return paymentResponse;
        }
    }
}
