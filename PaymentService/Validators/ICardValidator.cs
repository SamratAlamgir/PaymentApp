using PaymentManager.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentManager.Validators
{
    public interface ICardValidator
    {
        void Validate(IPaymentRequest paymentRequest);
    }
}
