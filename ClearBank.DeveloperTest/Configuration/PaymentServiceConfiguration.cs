using System.Configuration;

namespace ClearBank.DeveloperTest.Configuration
{
    internal class PaymentServiceConfiguration : IPaymentServiceConfiguration
    {
        public string DataStoreType => ConfigurationManager.AppSettings["DataStoreType"];
    }
}
