using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validators
{
    public class FasterPaymentSchemeValidator : BasicPaymentSchemeValidator, IFasterPaymentSchemeValidator
    {
        public FasterPaymentSchemeValidator() : base(AllowedPaymentSchemes.FasterPayments)
        {
        }

        public bool Validate(Account account, MakePaymentRequest request)
        {
            return base.Validate(account) && account.Balance >= request.Amount;
        }
    }
}
