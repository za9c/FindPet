using FindPet.Core;

namespace FindPet.Repository
{
    public interface IDonationRepository
    {
        public void AddDonate(Donation dotan);
        public IEnumerable<Donation> GetBiggestDonates();
        public void UpdateQuontity(int id, Donation donat);
        public Donation FindDonate(string username);
        public double SumAllDonations();
    }
}
