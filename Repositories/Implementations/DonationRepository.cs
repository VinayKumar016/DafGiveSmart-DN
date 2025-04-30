using CDTApi.Models;

namespace CDTApi.Repositories.Implementations
{
    public class DonationRepository : IDonationRepository
    {
        private readonly JsonDataContext _context;

        public DonationRepository(JsonDataContext context)
        {
            _context = context;
        }

        public IEnumerable<Donation> GetDonationsByUserId(int userId)
        {
            return _context.Data.Donations
                .Where(d => d.UserId == userId)
                .OrderByDescending(d => d.Date);
        }

        public void AddDonation(Donation donation)
        {
            donation.DonationId = _context.Data.Donations.Any()
                ? _context.Data.Donations.Max(d => d.DonationId) + 1
                : 1;

            donation.Date = DateTime.Now;

            _context.Data.Donations.Add(donation);
            _context.SaveChanges();
        }
    }
}
