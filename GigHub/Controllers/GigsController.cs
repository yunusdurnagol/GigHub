using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.ViewModels;
using GigHub.Persistence;

namespace GigHub.Controllers
{

    public class GigsController : Controller
    {


        private readonly IUnitOfWork _unitOfWork;


        public GigsController(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;

        }
        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _unitOfWork.Gigs.GetUpcomingGigsByArtist(userId);
            return View(gigs);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var gigViewModel = new GigsViewModel
            {
                UpComingGigs = _unitOfWork.Gigs.GetGigsUserAttending(userId),
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending",
                //We make it lookup because we need to apply contain method to check
                Attendances = _unitOfWork.Attendances.GetFutureAttendances(userId)
                    .ToLookup(a => a.GigId)
            };

            return View("Gigs", gigViewModel);
        }





        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.searchTerm });
        }


        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _unitOfWork.Genres.GetGenres(),
                Heading = "Add a Gig"
            };
            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.Genres.GetGenres();
                viewModel.Heading = "Add a Gig";
                return View("GigForm", viewModel);
            }

            #region ForeignKey Properties defined 
            /*
               We defined foreign keys we dont assign navigation properties. 
               the other way we go to database and time and data consume.
               */
            //var artist = _context.Users.Single(u => u.Id == userId);
            //var genre = _context.Genres.Single(g => g.Id == viewModel.Genre); 
            #endregion
            var userId = User.Identity.GetUserId();
            var gig = new Gig()
            {
                ArtistId = userId,
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue,

            };
            _unitOfWork.Gigs.Add(gig);


            var notification = Notification.GigCreated(gig);

            var followers = _unitOfWork.Followings.GetFollowers(userId);
            foreach (var myFollower in followers)
            {

                myFollower.Follower.Notify(notification);

            }

            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }
        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _unitOfWork.Gigs.GetGig(id);
            if (gig == null)
                return HttpNotFound();
            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();


            var viewModel = new GigFormViewModel
            {
                Id = id,
                Genres = _unitOfWork.Genres.GetGenres(),
                Date = gig.DateTime.Value.ToString("d MMM yyyy"),
                Time = gig.DateTime.Value.ToString("HH:mm"),
                Venue = gig.Venue,
                Genre = gig.GenreId,
                Heading = "Edit a Gig"
            };
            return View("GigForm", viewModel);
        }



        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.Genres.GetGenres();
                viewModel.Heading = "Edit a Gig";
                return View("GigForm", viewModel);
            }
            var userId = User.Identity.GetUserId();
            var gig = _unitOfWork.Gigs.GetGigWithAttendees(viewModel.Id);
            if (gig == null)
                return HttpNotFound();
            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            gig.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.Genre);


            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs", new { updated = "yes", page = "update" });
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            var gig = _unitOfWork.Gigs.GetGig(id);

            if (gig == null)
                return HttpNotFound();
            var viewModel = new GigDetailsViewModel { Gig = gig };
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                viewModel.IsAttending = _unitOfWork.Attendances.GetAttendance(gig.Id, userId) != null;
                viewModel.IsFollowing = _unitOfWork.Followings.GetFollowing(userId, gig.ArtistId) != null;
            }
            return View("Details", viewModel);
        }
    }
}