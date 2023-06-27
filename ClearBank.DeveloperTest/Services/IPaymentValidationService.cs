using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public interface IPaymentValidationService
    {
        bool IsValid(MakePaymentRequest request, Account account);
    }
}
