using System.Collections.Generic;
using System.Linq;
using GigHub.Models;

namespace GigHub.ViewModels
{
    public class FollowerViewModel
    {
        public IEnumerable<Following> Followers { get; set; }

    }
}