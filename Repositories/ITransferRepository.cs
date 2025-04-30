using CDTApi.Models;

namespace CDTApi.Repositories
{
    public interface ITransferRepository
    {
        void AddTransfer(Transfer transfer);
        IEnumerable<Transfer> GetTransfersByUserId(int userId);
    }
}
