using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using FluentAssertions;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Validators
{
    public class ChapsPaymentSchemeValidatorTests
    {
        private readonly ChapsPaymentSchemeValidator _chapsPaymentSchemeValidator;

        public ChapsPaymentSchemeValidatorTests()
        {
            _chapsPaymentSchemeValidator = new ChapsPaymentSchemeValidator();
        }

        [Fact]
        public void ReturnsFalse_IfAccountIsNull()
        {
            var result = _chapsPaymentSchemeValidator.Validate(null);

            result.Should().BeFalse();
        }

        [Fact]
        public void ReturnsFalse_IfAccountDoesNotContainedAllowedPaymentScheme()
        {
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments
            };

            var result = _chapsPaymentSchemeValidator.Validate(account);

            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(AccountStatus.InboundPaymentsOnly)]
        [InlineData(AccountStatus.Disabled)]
        public void ReturnsFalse_IfAccountIsNotLive(AccountStatus status)
        {
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                Status = status
            };

            var result = _chapsPaymentSchemeValidator.Validate(account);

            result.Should().BeFalse();
        }

        [Fact]
        public void ReturnsTrue_IfAccountIsValid()
        {
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                Status = AccountStatus.Live
            };

            var result = _chapsPaymentSchemeValidator.Validate(account);

            result.Should().BeTrue();
        }
    }
}
