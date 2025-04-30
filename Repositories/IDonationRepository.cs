using CDTApi.Models;

namespace CDTApi.Repositories
{
    public interface IDonationRepository
    {
        IEnumerable<Donation> GetDonationsByUserId(int userId);
        void AddDonation(Donation donation);
    }
}
