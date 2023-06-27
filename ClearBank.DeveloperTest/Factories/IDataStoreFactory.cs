using ClearBank.DeveloperTest.Data;

namespace ClearBank.DeveloperTest.Factories
{
    public interface IDataStoreFactory
    {
        public IDataStore GetDataStore();
    }
}
