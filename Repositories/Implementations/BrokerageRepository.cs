using CDTApi.Models;

namespace CDTApi.Repositories.Implementations
{
    public class BrokerageRepository : IBrokerageRepository
    {
        private readonly JsonDataContext _context;

        public BrokerageRepository(JsonDataContext context)
        {
            _context = context;
        }

        public BrokerageAccount? GetByUserId(int userId)
        {
            return _context.Data.BrokerageAccounts.FirstOrDefault(b => b.UserId == userId);
        }

        public void AddBrokerageAccount(BrokerageAccount account)
        {
            account.BrokerageAccountId = _context.Data.BrokerageAccounts.Any()
                ? _context.Data.BrokerageAccounts.Max(b => b.BrokerageAccountId) + 1
                : 1;
            account.AvailableBalance = 100000;
            account.LinkedDate = DateTime.Now;
            _context.Data.BrokerageAccounts.Add(account);
            _context.SaveChanges();
        }

        public void UpdateBalance(int brokerageAccountId, decimal amount)
        {
            var acc = _context.Data.BrokerageAccounts.FirstOrDefault(b => b.BrokerageAccountId == brokerageAccountId);
            if (acc != null)
            {
                acc.AvailableBalance -= amount;
                _context.SaveChanges();
            }
        }
    }
}
