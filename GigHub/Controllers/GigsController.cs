using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GigHub.Models;
using GigHub.ViewModel;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        // GET: Gigs
        [Authorize]
        public ActionResult Create()
        {
            using(DbEntities _context=new DbEntities())
            {
                
                GigFormViewModel viewModel = new GigFormViewModel
                {
                    Genres = _context.Genres.ToList()
                };
                return View(viewModel);
            }
            
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            using(DbEntities _context=new DbEntities())
            {
                if (!ModelState.IsValid)
                {
                    viewModel.Genres = _context.Genres.ToList();
                    return View("Create", viewModel);
                }
                Gig gig = new Gig
                {
                    Artist_Id=User.Identity.GetUserId(),
                    Venue = viewModel.Venue,
                    DateTime = viewModel.GetDateTime(),
                    Genre_id=viewModel.GenreID,


                };
                _context.Gigs.Add(gig);
                _context.SaveChanges();
                return RedirectToAction("Create");

            }
        }
    }
}