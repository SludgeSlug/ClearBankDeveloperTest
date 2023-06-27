using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using FluentAssertions;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Validators
{
    public class BasicPaymentSchemeValidatorTests
    {
        private readonly BasicPaymentSchemeValidator _basicPaymentSchemeValidator;

        public BasicPaymentSchemeValidatorTests()
        {
            _basicPaymentSchemeValidator = new BasicPaymentSchemeValidator(AllowedPaymentSchemes.Bacs);
        }

        [Fact]
        public void ReturnsFalse_IfAccountIsNull()
        {
            var result = _basicPaymentSchemeValidator.Validate(null);

            result.Should().BeFalse();
        }

        [Fact]
        public void ReturnsFalse_IfAccountDoesNotContainedAllowedPaymentScheme()
        {
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments
            };

            var result = _basicPaymentSchemeValidator.Validate(account);

            result.Should().BeFalse();
        }

        [Fact]
        public void ReturnsTrue_IfAccountIsValid()
        {
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs
            };

            var result = _basicPaymentSchemeValidator.Validate(account);

            result.Should().BeTrue();
        }

    }
}
