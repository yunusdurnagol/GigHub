using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//We need to add it by hand when we add include method for _context
using System.Data.Entity;
using System.Web.Mvc;
using GigHub.Models;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var upcomingGigs = _context.Gigs.
                Include(g => g.Artist).
                Where(g => g.DateTime > DateTime.Now);

            return View(upcomingGigs);
        }


    }
}