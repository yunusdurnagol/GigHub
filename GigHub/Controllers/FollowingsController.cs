using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GigHub.Core;
using GigHub.Core.ViewModels;
using GigHub.Persistence;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    [Authorize]
    public class FollowingsController : Controller
    {
        private ApplicationDbContext _context;
        private IUnitOfWork _unitOfWork { get; set; }
        public FollowingsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: Followings
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();


            var followers = _unitOfWork.Followings.GetFollowers(userId);


            var followerView = new FollowerViewModel
            {
                Followers = followers,

            };
            return View(followerView);
        }
    }
}