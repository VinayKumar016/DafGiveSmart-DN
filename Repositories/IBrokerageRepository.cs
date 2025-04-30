using CDTApi.Models;

namespace CDTApi.Repositories
{
    public interface IBrokerageRepository
    {
        BrokerageAccount? GetByUserId(int userId);
        void AddBrokerageAccount(BrokerageAccount account);
        void UpdateBalance(int brokerageAccountId, decimal amount);
    }
}
