using FindPet.Core;
using FindPet.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FindPet.WebApp.Controllers
{
    public class DonationController : Controller
    {
        private readonly IDonationRepository donateRepo;

        public DonationController(DonationRepository donateR)
        {
            donateRepo = donateR;
        }

        public IActionResult Index()
        {
            ViewBag.Sum = donateRepo.SumAllDonations();
            var donats = donateRepo.GetBiggestDonates();
            return View(donats);
        }

        [HttpGet]
        [Authorize]
        public IActionResult HelpPet()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Donate([Bind("Quontity,Username")] Donation model)
        {
            if (ModelState.IsValid)
            {
                var check = donateRepo.FindDonate(model.Username);
                if (check == null)
                {
                    donateRepo.AddDonate(model);
                }
                else
                {
                    donateRepo.UpdateQuontity(check.Id, model);
                }
                return RedirectToAction("Index");
            }
            return View("HelpPet");
        }
    }
}
