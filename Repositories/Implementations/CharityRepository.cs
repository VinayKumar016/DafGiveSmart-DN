using CDTApi.Models;

namespace CDTApi.Repositories.Implementations
{
    public class CharityRepository : ICharityRepository
    {
        private readonly JsonDataContext _context;

        public CharityRepository(JsonDataContext context)
        {
            _context = context;
        }

        public IEnumerable<Charity> GetAllCharities()
        {
            return _context.Data.Charities;
        }

        public IEnumerable<Charity> SearchCharities(string keyword)
        {
            return _context.Data.Charities
                .Where(c => c.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase));
        }

        public Charity? GetById(int charityId)
        {
            return _context.Data.Charities.FirstOrDefault(c => c.CharityId == charityId);
        }

        public void AddCharity(Charity charity)
        {
            charity.CharityId = _context.Data.Charities.Any()
                ? _context.Data.Charities.Max(c => c.CharityId) + 1
                : 1;

            _context.Data.Charities.Add(charity);
            _context.SaveChanges();
        }
    }
}
