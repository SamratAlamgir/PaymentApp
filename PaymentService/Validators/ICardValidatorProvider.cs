using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentManager.Validators
{
    public interface ICardValidatorProvider
    {
        ICardValidator GetCardValidator(string cardNumber);
    }
}
