using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Persistence;

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

        [HttpDelete]
        public IHttpActionResult DeleteFollow(string Id)
        {
            var userId = User.Identity.GetUserId();
            var following = _context.Followings
                .SingleOrDefault(f => f.FollowerId == userId && f.FolloweeId == Id);
            if (following == null)
                return NotFound();
            _context.Followings.Remove(following);
            _context.SaveChanges();
            return Ok(Id);

        }

    }
}