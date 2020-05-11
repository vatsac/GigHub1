using GigHub.Models;
using GigHub.ViewModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbEntities _context;
        public HomeController()
        {
            _context = new DbEntities();
        }
        public ActionResult Index()
        {
            var upcomingGigs = _context.Gigs
                .Include(g => g.AspNetUser)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now && !(g.IsCanceled==true))
                .ToList();
            HomeViewModel viewModel = new HomeViewModel
            {
                gigs = upcomingGigs,
                ShowAction = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs"
            };

            return View("Gigs", viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}