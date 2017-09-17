namespace GigHub.Core.Models
{
    public class Attendance
    {
        public Gig Gig { get; set; }
        public int GigId { get; set; }
        public ApplicationUser Attendee { get; set; }
        public string AttendeeId { get; set; }

        protected Attendance()
        {

        }

        public Attendance(int gigId, string attendeeId)
        {
            GigId = gigId;
            AttendeeId = attendeeId;
        }
    }
}