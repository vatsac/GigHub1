using GigHub.Models;
using GigHub.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        // GET: Gigs
        [Authorize]
        public ActionResult Create()
        {
            using (DbEntities _context = new DbEntities())
            {

                GigFormViewModel viewModel = new GigFormViewModel
                {
                    Genres = _context.Genres.ToList(),
                    Heading = "Add A Gig"


                };
                return View("GigsForm", viewModel);
            }

        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            using (DbEntities _context = new DbEntities())
            {
                if (!ModelState.IsValid)
                {
                    viewModel.Genres = _context.Genres.ToList();
                    return View("GigsForm", viewModel);
                }
                Gig gig = new Gig
                {
                    Artist_Id = User.Identity.GetUserId(),
                    Venue = viewModel.Venue,
                    DateTime = viewModel.GetDateTime(),
                    Genre_id = viewModel.GenreID,


                };
                _context.Gigs.Add(gig);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
        }
        [Authorize]
        public ActionResult Edit(int id)
        {

            using (DbEntities _context = new DbEntities())
            {

                var userid = User.Identity.GetUserId();
                var gig = _context.Gigs.Single(g => g.ID == id && g.Artist_Id == userid);
                GigFormViewModel viewModel = new GigFormViewModel
                {
                    Genres = _context.Genres.ToList(),
                    Venue = gig.Venue,
                    Date = gig.DateTime.ToString("d MMM yyyy"),
                    Time = gig.DateTime.ToString("HH:mm"),
                    GenreID = gig.Genre_id,
                    Heading = "Edit A Gig",
                    id = gig.ID


                };
                return View("GigsForm", viewModel);
            }


        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            using (DbEntities _context = new DbEntities())
            {
                if (!ModelState.IsValid)
                {
                    viewModel.Genres = _context.Genres.ToList();
                    return View("GigsForm", viewModel);
                }
                var userid = User.Identity.GetUserId();
                var gig = _context.Gigs.Single(g => g.ID == viewModel.id && g.Artist_Id == userid);


                gig.Venue = viewModel.Venue;
                gig.DateTime = viewModel.GetDateTime();
                gig.Genre_id = viewModel.GenreID;




                _context.SaveChanges();
                return RedirectToAction("Mine", "Gigs");
            }
        }


        [Authorize]
        public ActionResult Attending()
        {
            using (DbEntities _context = new DbEntities())
            {
                var userid = User.Identity.GetUserId();
                var gigs = _context.Attendances
                    .Where(a => a.AttendeeId == userid && a.Gig.DateTime > DateTime.Now)
                    .Select(a => a.Gig)
                    .Include(a => a.AspNetUser)
                    .Include(a => a.Genre)
                    .ToList();
                HomeViewModel viewModel = new HomeViewModel
                {
                    gigs = gigs,
                    ShowAction = User.Identity.IsAuthenticated,
                    Heading = "Gigs I'm Attending"
                };
                return View("Gigs", viewModel);
            }
        }
        [Authorize]
        public ActionResult Mine()
        {
            using (DbEntities _context = new DbEntities())
            {
                var userid = User.Identity.GetUserId();
                var gigs = _context.Gigs
                    .Where(g => g.Artist_Id == userid && g.DateTime > DateTime.Now && !(g.IsCanceled == true))
                    .Include(g => g.Genre).ToList();
                return View(gigs);
            }
        }
        [Authorize]
        public ActionResult MineFollowing()
        {
            using (DbEntities _context = new DbEntities())
            {
                var userid = User.Identity.GetUserId();
                var gigs = _context.follows.Include(f => f.AspNetUser1)
                    .Where(f=>f.UserId==userid)
                    .ToList();
                return View(gigs);
            }
        }
    }
}