using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace GigHub.Models
{
    /*
     This class is an association class between User and Notification 
     so we cant change ApplicationUser or Notification class that is 
     why we set set methods private.
        */
    public class UserNotification
    {
        public string UserId { get; private set; }
        public int NotificationId { get; private set; }

        public ApplicationUser User { get; private set; }
        public Notification Notification { get; private set; }

        public bool IsRead { get; private set; }

        // This is for entity framework We have to create it and we define it protected so nowhere in the code we wont use this 

        protected UserNotification()
        {

        }
        // Custom constructor...
        public UserNotification(ApplicationUser user, Notification notification)
        {
            if (user == null)
                throw new ArgumentNullException("user cant be null");
            if (notification == null)
                throw new ArgumentNullException("notification cant be null");

            User = user;
            Notification = notification;
        }

        public void Read()
        {
            IsRead = true;
        }
    }
}