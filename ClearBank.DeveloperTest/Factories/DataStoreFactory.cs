using ClearBank.DeveloperTest.Configuration;
using ClearBank.DeveloperTest.Data;

namespace ClearBank.DeveloperTest.Factories
{
    public class DataStoreFactory : IDataStoreFactory
    {
        private const string BackupDataStoreType = "Backup";

        private readonly IPaymentServiceConfiguration _paymentServiceConfiguration;

        public DataStoreFactory(IPaymentServiceConfiguration paymentServiceConfiguration)
        {
            _paymentServiceConfiguration = paymentServiceConfiguration;
        }

        public IDataStore GetDataStore()
        {
            if (_paymentServiceConfiguration.DataStoreType == BackupDataStoreType)
            {
                return new BackupAccountDataStore();
            }

            return new AccountDataStore();
        }
    }
}