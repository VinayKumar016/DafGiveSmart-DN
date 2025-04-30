using CDTApi.Models;

namespace CDTApi.Repositories
{
    public interface IDAFRepository
    {
        DAFAccount? GetByUserId(int userId);
        void AddDAFAccount(DAFAccount account);
        void UpdateBalance(int dafAccountId, decimal amountChange, bool isDonation);
    }
}
