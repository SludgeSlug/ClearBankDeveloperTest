using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validators
{
    public class ChapsPaymentSchemeValidator : BasicPaymentSchemeValidator, IChapsPaymentSchemeValidator
    {
        public ChapsPaymentSchemeValidator() : base(AllowedPaymentSchemes.Chaps)
        {
        }

        public override bool Validate(Account account)
        {
            return base.Validate(account) && account.Status == AccountStatus.Live;
        }
    }
}
