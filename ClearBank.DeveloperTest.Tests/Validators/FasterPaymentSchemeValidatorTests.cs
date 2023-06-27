using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using FluentAssertions;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Validators
{
    public class FasterPaymentSchemeValidatorTests
    {
        private readonly FasterPaymentSchemeValidator _fasterPaymentSchemeValidator;

        public FasterPaymentSchemeValidatorTests()
        {
            _fasterPaymentSchemeValidator = new FasterPaymentSchemeValidator();
        }

        [Fact]
        public void ReturnsFalse_IfAccountIsNull()
        {
            var result = _fasterPaymentSchemeValidator.Validate(null, new MakePaymentRequest());

            result.Should().BeFalse();
        }

        [Fact]
        public void ReturnsFalse_IfAccountDoesNotContainedAllowedPaymentScheme()
        {
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs
            };

            var result = _fasterPaymentSchemeValidator.Validate(account, new MakePaymentRequest());

            result.Should().BeFalse();
        }

        [Fact]
        public void ReturnsFalse_IfBalanceIsLessThanRequestAmount()
        {
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
                Balance = 1
            };

            var result = _fasterPaymentSchemeValidator.Validate(account, new MakePaymentRequest { Amount = 100 });

            result.Should().BeFalse();
        }

        [Fact]
        public void ReturnsTrue_IfAccountIsValid()
        {
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
                Balance = 500
            };

            var result = _fasterPaymentSchemeValidator.Validate(account, new MakePaymentRequest { Amount = 100 });

            result.Should().BeTrue();
        }
    }
}
