using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validators
{
    public class BasicPaymentSchemeValidator : IPaymentSchemeValidator
    {
        private AllowedPaymentSchemes _allowedPaymentSchemes;

        public BasicPaymentSchemeValidator(AllowedPaymentSchemes allowedPaymentSchemes)
        {
            _allowedPaymentSchemes = allowedPaymentSchemes;
        }

        public virtual bool Validate(Account account)
        {
            return account != null && account.AllowedPaymentSchemes.HasFlag(_allowedPaymentSchemes);
        }
    }
}
