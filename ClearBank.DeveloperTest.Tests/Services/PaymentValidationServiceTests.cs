using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using System;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Services
{
    public class PaymentValidationServiceTests
    {
        private Account _account;
        private readonly Mock<IPaymentSchemeValidator> _bacsPaymentSchemeValidator;
        private readonly Mock<IChapsPaymentSchemeValidator> _chapsPaymentSchemeValidator;
        private readonly Mock<IFasterPaymentSchemeValidator> _fasterPaymentSchemeValidator;

        private PaymentValidationService _paymentValidationService;

        public PaymentValidationServiceTests() 
        { 
            _account = Builder<Account>.CreateNew().Build();
            _bacsPaymentSchemeValidator = new Mock<IPaymentSchemeValidator>();
            _chapsPaymentSchemeValidator = new Mock<IChapsPaymentSchemeValidator>();
            _fasterPaymentSchemeValidator = new Mock<IFasterPaymentSchemeValidator>();

            _paymentValidationService = new PaymentValidationService(
                _bacsPaymentSchemeValidator.Object,
                _chapsPaymentSchemeValidator.Object,
                _fasterPaymentSchemeValidator.Object);
        }

        [Fact]
        public void ShouldUseBacsPaymentSchemeValidator_WhenRequestIsBacsPayment()
        {
            var request = Builder<MakePaymentRequest>
                .CreateNew()
                .With(r => r.PaymentScheme = PaymentScheme.Bacs)
                .Build();

            _bacsPaymentSchemeValidator.Setup(p => p.Validate(_account))
                .Returns(true);

            var result = _paymentValidationService.IsValid(request, _account);
            result.Should().BeTrue();
        }

        [Fact]
        public void ShouldUseChapsPaymentSchemeValidator_WhenRequestIsChapsPayment()
        {
            var request = Builder<MakePaymentRequest>
                .CreateNew()
                .With(r => r.PaymentScheme = PaymentScheme.Chaps)
                .Build();

            _chapsPaymentSchemeValidator.Setup(p => p.Validate(_account))
                .Returns(true);

            var result = _paymentValidationService.IsValid(request, _account);
            result.Should().BeTrue();
        }

        [Fact]
        public void ShouldUseFasterPaymentSchemeValidator_WhenRequestIsFasterPayment()
        {
            var request = Builder<MakePaymentRequest>
                .CreateNew()
                .With(r => r.PaymentScheme = PaymentScheme.FasterPayments)
                .Build();

            _fasterPaymentSchemeValidator.Setup(p => p.Validate(_account, request))
                .Returns(true);

            var result = _paymentValidationService.IsValid(request, _account);
            result.Should().BeTrue();
        }

        [Fact]
        public void ShouldThrowException_WhenPaymentSchemeNotSetOnRequest()
        {
            Assert.Throws<ArgumentException>(() => _paymentValidationService.IsValid(null, _account));
        }

    }
}
