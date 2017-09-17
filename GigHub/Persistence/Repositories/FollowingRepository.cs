using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Following GetFollowing(string followerId, string followeeId)
        {
            return _context.Followings
                .SingleOrDefault(f => f.FolloweeId == followeeId && f.FollowerId == followerId);
        }

        public IEnumerable<Following> GetFollowers(string userId)
        {
            return _context.Followings.Where(f => f.FolloweeId == userId).Include(f => f.Follower).ToList();
        }
    }
}