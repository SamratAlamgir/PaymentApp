using PaymentManager.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace PaymentManager.Validators
{
    public class DefaultCardValidator : ICardValidator
    {
        public void Validate(IPaymentRequest paymentRequest)
        {
            if (!Regex.IsMatch(paymentRequest.CardNumber, @"^\d+$"))
                throw new ValidationException("Invalid Card Number");
        }
    }
}
