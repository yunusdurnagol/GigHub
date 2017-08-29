using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public NotificationType Type { get; set; }
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalValue { get; set; }

        public Gig Gig { get; set; }
        public int GigId { get; set; }
    }
}