using System;
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
    [Authorize]
    public class FollowingsController : Controller
    {
        private ApplicationDbContext _context;
        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Followings
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();


            var followers = _context.Followings
                .Where(f => f.FolloweeId == userId)
                .Include(f => f.Follower)
                .ToList();
            var followerView = new FollowerViewModel
            {
                Followers = followers,

            };
            return View(followerView);
        }
    }
}