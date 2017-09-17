using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IFollowingRepository
    {
        Following GetFollowing(string followerId, string followeeId);
        IEnumerable<Following> GetFollowers(string userId);
    }
}