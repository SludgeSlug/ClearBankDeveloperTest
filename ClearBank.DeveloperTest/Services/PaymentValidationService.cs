using System;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentValidationService : IPaymentValidationService
    {
        IPaymentSchemeValidator _bacsPaymentSchemeValidator;
        IChapsPaymentSchemeValidator _chapsPaymentSchemeValidator;
        IFasterPaymentSchemeValidator _fasterPaymentSchemeValidator;

        public PaymentValidationService(
            IPaymentSchemeValidator bacsPaymentSchemeValidator,
            IChapsPaymentSchemeValidator chapsPaymentSchemeValidator, 
            IFasterPaymentSchemeValidator fasterPaymentSchemeValidator)
        {
            _bacsPaymentSchemeValidator = bacsPaymentSchemeValidator;
            _chapsPaymentSchemeValidator = chapsPaymentSchemeValidator;
            _fasterPaymentSchemeValidator = fasterPaymentSchemeValidator;
        }


        public bool IsValid(MakePaymentRequest request, Account account)
        {
            return request?.PaymentScheme switch
            {
                PaymentScheme.Bacs => _bacsPaymentSchemeValidator.Validate(account),
                PaymentScheme.FasterPayments => _fasterPaymentSchemeValidator.Validate(account, request),
                PaymentScheme.Chaps => _chapsPaymentSchemeValidator.Validate(account),
                _ => throw new ArgumentException("Unknown Payment scheme"),
            };
        }
    }
}
