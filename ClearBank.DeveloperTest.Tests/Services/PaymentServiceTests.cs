using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Factories;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Services
{
    public class PaymentServiceTests
    {
        private readonly Mock<IDataStore> _dataStore;
        private readonly Mock<IPaymentValidationService> _paymentValidationService;
        private readonly PaymentService _paymentService;

        public PaymentServiceTests() 
        { 
            _dataStore = new Mock<IDataStore>();
            _paymentValidationService = new Mock<IPaymentValidationService>();

            _paymentService = new PaymentService(new FakeDataStoreFactory(_dataStore), _paymentValidationService.Object);
        }

        [Fact]
        public void ShouldReturnSuccessIsFalse_WhenPaymentIsInvalid()
        {
            var account = new Account();
            var request = Builder<MakePaymentRequest>.CreateNew().Build();

            _dataStore.Setup(d => d.GetAccount(request.DebtorAccountNumber)).Returns(account);
            _paymentValidationService.Setup(p => p.IsValid(request, account)).Returns(false);

            var result = _paymentService.MakePayment(request);

            result.Success.Should().BeFalse();
        }

        [Fact]
        public void ShouldDeductRequestAmountFromBalance_WhenValidPaymentIsRequested()
        {
            var account = Builder<Account>.CreateNew().With(a => a.Balance = 500).Build();
            var request = Builder<MakePaymentRequest>.CreateNew().With(r => r.Amount = 100).Build();

            _dataStore.Setup(d => d.GetAccount(request.DebtorAccountNumber)).Returns(account);
            _paymentValidationService.Setup(p => p.IsValid(request, account)).Returns(true);

            var result = _paymentService.MakePayment(request);

            result.Success.Should().BeTrue();
            account.Balance.Should().Be(400);
            _dataStore.Verify(d => d.UpdateAccount(account));
        }
    }

    internal class FakeDataStoreFactory : IDataStoreFactory
    {
        private readonly Mock<IDataStore> _mockDataStore;

        public FakeDataStoreFactory(Mock<IDataStore> mockDataStore)
        {
            _mockDataStore = mockDataStore;
        }

        public IDataStore GetDataStore()
        {
            return _mockDataStore.Object;
        }
    }

}
