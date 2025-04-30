using CDTApi.Models;

namespace CDTApi.Repositories.Implementations
{
    public class TransferRepository : ITransferRepository
    {
        private readonly JsonDataContext _context;

        public TransferRepository(JsonDataContext context)
        {
            _context = context;
        }

        public void AddTransfer(Transfer transfer)
        {
            transfer.TransferId = _context.Data.Transfers.Any()
                ? _context.Data.Transfers.Max(t => t.TransferId) + 1
                : 1;

            transfer.Date = DateTime.Now;
            transfer.Status = "Success";

            _context.Data.Transfers.Add(transfer);
            _context.SaveChanges();
        }

        public IEnumerable<Transfer> GetTransfersByUserId(int userId)
        {
            return _context.Data.Transfers
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.Date);
        }
    }

}
