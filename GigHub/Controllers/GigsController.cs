using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GigHub.Persistence;
using GigHub.Repositories;

namespace GigHub.Controllers
{

    public class GigsController : Controller
    {


        private readonly UnitOfWork _unitOfWork;


        public GigsController()
        {

            _unitOfWork = new UnitOfWork(new ApplicationDbContext());

        }
        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _unitOfWork.Gig.GetUpcomingGigsByArtist(userId);
            return View(gigs);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var gigViewModel = new GigsViewModel
            {
                UpComingGigs = _unitOfWork.Gig.GetGigsUserAttending(userId),
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending",
                //We make it lookup because we need to apply contain method to check
                Attendances = _unitOfWork.Attendance.GetFutureAttendances(userId)
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
                Genres = _unitOfWork.Genre.GetGenres(),
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
                viewModel.Genres = _unitOfWork.Genre.GetGenres();
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
            _unitOfWork.Gig.Add(gig);


            var notification = Notification.GigCreated(gig);
            var followers = _context.Followings.Where(f => f.FolloweeId == userId).Include(f => f.Follower);
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
            var gig = _unitOfWork.Gig.GetGig(id);
            if (gig == null)
                return HttpNotFound();
            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();


            var viewModel = new GigFormViewModel
            {
                Id = id,
                Genres = _context.Genres.ToList(),
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
                viewModel.Genres = _context.Genres.ToList();
                viewModel.Heading = "Edit a Gig";
                return View("GigForm", viewModel);
            }
            var userId = User.Identity.GetUserId();
            var gig = _unitOfWork.Gig.GetGigWithAttendees(viewModel.Id);
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
            var gig = _unitOfWork.Gig.GetGig(id);
            if (gig == null)
                return HttpNotFound();
            var viewModel = new GigDetailsViewModel { Gig = gig };
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                viewModel.IsAttending = _unitOfWork.Attendance.GetAttendance(gig.Id, userId) != null;
                viewModel.IsFollowing = _unitOfWork.Following.GetFollowing(userId, gig.ArtistId) != null;
            }
            return View("Details", viewModel);
        }
    }
}