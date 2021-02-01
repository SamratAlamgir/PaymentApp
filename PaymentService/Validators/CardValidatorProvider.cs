using PaymentManager.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentManager.Validators
{
    public class CardValidatorProvider : ICardValidatorProvider
    {
        public ICardValidator GetCardValidator(string cardNumber)
        {
            if (cardNumber.StartsWith("1"))
            {
                return new VisaCardValidator();
            }
            else if (cardNumber.StartsWith("2"))
            {
                return new MasterCardValidator();
            }
            else
            {
                return new DefaultCardValidator();
            }
        }
    }
}
