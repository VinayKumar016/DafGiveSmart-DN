using CDTApi.Models;

namespace CDTApi.Repositories.Implementations
{
    public class DAFRepository : IDAFRepository
    {
        private readonly JsonDataContext _context;

        public DAFRepository(JsonDataContext context)
        {
            _context = context;
        }

        public DAFAccount? GetByUserId(int userId)
        {
            return _context.Data.DAFAccounts.FirstOrDefault(d => d.UserId == userId);
        }

        public void AddDAFAccount(DAFAccount account)
        {
            account.DAFAccountId = _context.Data.DAFAccounts.Any()
                ? _context.Data.DAFAccounts.Max(d => d.DAFAccountId) + 1
                : 1;

            _context.Data.DAFAccounts.Add(account);
            _context.SaveChanges();
        }

        public void UpdateBalance(int dafAccountId, decimal amountChange, bool isDonation)
        {
            var account = _context.Data.DAFAccounts.FirstOrDefault(d => d.DAFAccountId == dafAccountId);
            if (account != null)
            {
                if (isDonation)
                {
                    account.DAFBalance -= amountChange;
                    account.TotalDonated += amountChange;
                }
                else
                {
                    account.DAFBalance += amountChange;
                }

                _context.SaveChanges();
            }
        }
    }

}
