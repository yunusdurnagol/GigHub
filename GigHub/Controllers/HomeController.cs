﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//We need to add it by hand when we add include method for _context
using System.Data.Entity;
using System.Web.Mvc;
using GigHub.Models;
using GigHub.Repositories;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        private readonly AttendanceRepository _attendanceRepository;
        public HomeController()
        {
            _context = new ApplicationDbContext();
            _attendanceRepository = new AttendanceRepository(_context);
        }
        public ActionResult Index(string query = null)
        {
            var upcomingGigs = _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now && g.isCanceled == false)
                .OrderBy(g => g.DateTime);

            if (!string.IsNullOrWhiteSpace(query))
            {
                upcomingGigs = upcomingGigs
                    .Where(
                        g =>
                            g.Venue.Contains(query) ||
                            g.Artist.Name.Contains(query) ||
                            g.Genre.Name.Contains(query)).OrderBy(g => g.DateTime);

            }

            var userId = User.Identity.GetUserId();
            var attendances = _attendanceRepository.GetFutureAttendances(userId).ToLookup(a => a.GigId);


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