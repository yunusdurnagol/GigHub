﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{

    public class GigsController : Controller
    {
        private ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }
        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context
                .Gigs
                .Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now && g.isCanceled == false)
                .OrderBy(g => g.DateTime)
                .Include(g => g.Genre)
                .ToList();
            return View(gigs);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var gigsImAtttending = _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();

            var gigViewModel = new GigsViewModel
            {
                UpComingGigs = gigsImAtttending,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending"
            };

            return View("Gigs", gigViewModel);
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList(),
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
                viewModel.Genres = _context.Genres.ToList();
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

            var gig = new Gig()
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue,

            };
            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }
        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);
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
            var gig = _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .Single(g => g.Id == viewModel.Id && g.ArtistId == userId);

            gig.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.Genre);


            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs", new { updated = "yes", page = "update" });
        }



    }
}