using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class FollowingController : ApiController
    {
        private ApplicationDbContext _context;
        public FollowingController()
        {
            _context = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult CreateFollow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();
            var exist = _context.Followings.Any(f => f.FolloweeId == dto.FolloweeId && f.FollowerId == userId);
            if (exist)
                return BadRequest("You already follow this user");
            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = dto.FolloweeId
            };
            _context.Followings.Add(following);
            _context.SaveChanges();
            return Ok();
        }

    }
}