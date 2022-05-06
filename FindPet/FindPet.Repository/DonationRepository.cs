using FindPet.Core;
using FindPet.Core.Data;

namespace FindPet.Repository
{
    public class DonationRepository : IDonationRepository
    {
        private readonly ApplicationDbContext context;

        public DonationRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public void AddDonate(Donation donat)
        {
            context.Add(donat);
            context.SaveChanges();
        }

        public Donation FindDonate(string username)
        {
            return context.Donations.FirstOrDefault(x => x.Username == username);
        }

        public IEnumerable<Donation> GetBiggestDonates()
        {
            var donats = context.Donations.OrderBy(x => x.Quontity).Reverse().Take(5).ToList();
            return donats;
        }

        public double SumAllDonations()
        {
            double sum = 0;
            var donates = context.Donations.ToList();
            foreach (var d in donates)
            {
                sum += d.Quontity;
            }
            return sum;
        }

        public void UpdateQuontity(int id, Donation model)
        {
            var donate = context.Donations.Find(id);
            donate.Quontity += model.Quontity;

            context.Update(donate);
            context.SaveChanges();
        }
    }
}
