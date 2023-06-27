using ClearBank.DeveloperTest.Configuration;
using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Factories;
using FluentAssertions;
using Moq;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Factories
{
    public class DataStoreFactoryTests
    {
        [Fact]
        public void ReturnsBackupDataStore_WhenConfiguredDataStoreType_IsBackup()
        {
            var dataStoreFactory = CreateDataStoreFactory("Backup");
            var dataStore = dataStoreFactory.GetDataStore();

            dataStore.Should().BeOfType<BackupAccountDataStore>();
        }

        [Theory]
        [InlineData("Account")]
        [InlineData("ANY_OTHER_VALUE")]
        public void ReturnsAccountDataStore_WhenConfiguredDataStoreType_IsAnyOtherValue(string dataStoreType)
        {
            var dataStoreFactory = CreateDataStoreFactory(dataStoreType);
            var dataStore = dataStoreFactory.GetDataStore();

            dataStore.Should().BeOfType<AccountDataStore>();
        }

        private DataStoreFactory CreateDataStoreFactory(string dataStoreType)
        {
            var configuration = new Mock<IPaymentServiceConfiguration>();
            configuration.Setup(c => c.DataStoreType).Returns(dataStoreType);

            return new DataStoreFactory(configuration.Object);
        }
    }
}
