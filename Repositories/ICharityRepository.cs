using CDTApi.Models;

namespace CDTApi.Repositories
{
    public interface ICharityRepository
    {
        IEnumerable<Charity> GetAllCharities();
        IEnumerable<Charity> SearchCharities(string keyword);
        Charity? GetById(int charityId);
        void AddCharity(Charity charity);

    }
}
