using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.ViewModels
{
    public class FollowerViewModel
    {
        public IEnumerable<Following> Followers { get; set; }

    }
}