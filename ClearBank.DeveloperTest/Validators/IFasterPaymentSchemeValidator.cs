using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validators
{
    public interface IFasterPaymentSchemeValidator
    {
        bool Validate(Account account, MakePaymentRequest request);
    }
}
