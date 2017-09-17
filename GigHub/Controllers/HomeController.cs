using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//We need to add it by hand when we add include method for _context
using System.Data.Entity;
using System.Web.Mvc;
using GigHub.Core;
using GigHub.Core.ViewModels;
using GigHub.Persistence;
using GigHub.Persistence.Repositories;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ActionResult Index(string query = null)
        {
            var upcomingGigs = _unitOfWork.Gigs.GetUpcomingGigs(query);


            var userId = User.Identity.GetUserId();
            var attendances = _unitOfWork.Attendances.GetFutureAttendances(userId).ToLookup(a => a.GigId);


            var homeViewModel = new GigsViewModel
            {
                UpComingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs",
                searchTerm = query,
                Attendances = attendances
            };


            return View("Gigs", homeViewModel);
        }


    }
}