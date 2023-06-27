using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Factories;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IDataStoreFactory _dataStoreFactory;
        private readonly IPaymentValidationService _paymentValidationService;

        public PaymentService(IDataStoreFactory dataStoreFactory, IPaymentValidationService paymentValidationService)
        {
            _dataStoreFactory = dataStoreFactory;
            _paymentValidationService = paymentValidationService;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var accountDataStore = _dataStoreFactory.GetDataStore();
            var account = accountDataStore.GetAccount(request.DebtorAccountNumber);
            
            if(!_paymentValidationService.IsValid(request, account))
            {
                return new MakePaymentResult { Success = false };
            }

            account.Balance -= request.Amount;
            accountDataStore.UpdateAccount(account);

            return new MakePaymentResult { Success = true };
        }
    }
}
