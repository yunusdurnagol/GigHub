using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Models
{
    public class Attendance
    {
        public Gig Gig { get; set; }
        public int GigId { get; set; }
        public ApplicationUser Attendee { get; set; }
        public string AttendeeId { get; set; }
    }
}