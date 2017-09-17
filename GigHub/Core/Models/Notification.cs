using System;

namespace GigHub.Core.Models
{
    /*
     The one we set private sets are we cant change it after initialization
     When we create a new notification this cons. is responsible setting the 
     datetime to now. Which means nowhere in the code i ll be able to change it
     This attribute is not a thing to modify in the code. So some of them are 
     decorated private set
     Id: is the key so technically we shouldnt be able to change
     Notification Type and Date Time are same.
         */
    public class Notification
    {
        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get; private set; }
        public string OriginalValue { get; private set; }

        public Gig Gig { get; private set; }
        public int GigId { get; set; }


        protected Notification()
        {

        }

        public Notification(Gig gig, NotificationType notificationType)
        {
            if (gig == null)
                throw new ArgumentNullException("Gig on notification is null..");

            DateTime = DateTime.Now;
            Gig = gig;
            Type = notificationType;
        }

        /// <summary>
        /// Factory methods because update notification is a bit different than other notifications 
        /// and we can forget to add original venue and datetime and can get null ref exp.
        /// </summary>
        public static Notification GigCreated(Gig gig)
        {
            return new Notification(gig, NotificationType.GigCreated);
        }

        public static Notification GigCanceled(Gig gig)
        {
            return new Notification(gig, NotificationType.GigCanceled);
        }

        public static Notification GigUpdated(Gig newGig, DateTime? originalDateTime, string originalVenue)
        {

            var notification = new Notification(newGig, NotificationType.GigUpdated);
            notification.OriginalDateTime = originalDateTime;
            notification.OriginalValue = originalVenue;
            return notification;
        }
    }
}