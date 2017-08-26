using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Models
{
    public class Following
    {
        public ApplicationUser Follower { get; set; }
        public ApplicationUser Followee { get; set; }
        public string FollowerId { get; set; }
        public string FolloweeId { get; set; }
    }
}