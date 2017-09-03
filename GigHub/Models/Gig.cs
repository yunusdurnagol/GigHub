using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Razor;
using WebGrease.Css.Extensions;

namespace GigHub.Models
{
    public class Gig
    {
        public int Id { get; set; }

        public bool isCanceled { get; private set; }
        public ApplicationUser Artist { get; set; }
        public string ArtistId { get; set; }
        public DateTime? DateTime { get; set; }
        public string Venue { get; set; }
        public Genre Genre { get; set; }
        public byte GenreId { get; set; }

        public ICollection<Attendance> Attendances { get; private set; }

        public Gig()
        {
            Attendances = new Collection<Attendance>();
        }

        public void Cancel()
        {
            isCanceled = true;

            var notification = Notification.GigCanceled(this);

            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }

        public void Modify(DateTime? dateTime, string newVenue, byte genreId)
        {
            var notification = Notification.GigUpdated(this, DateTime, Venue);

            Venue = newVenue;
            DateTime = dateTime;
            GenreId = genreId;
            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }

            //var attendees= Attendances.Select(a => a.Attendee);
            //attendees.ForEach(a=>a.Notify(notification));
            //Attendances.Select(a => a.Attendee).ForEach(a=>a.Notify(notification));
        }
    }
}