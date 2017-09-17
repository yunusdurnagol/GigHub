using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GigHub.Core.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            //await manager.AddClaimAsync(userIdentity.GetUserId(), new Claim("FirstName", DisplayName));

            userIdentity.AddClaim(new Claim("DisplayName", this.Name.ToString()));
            return userIdentity;
        }

        public string Name { get; set; }
        public string DisplayName { get; set; }
        public ICollection<Following> Followers { get; set; }
        public ICollection<Following> Followees { get; set; }
        public ICollection<UserNotification> UserNotifications { get; set; }

        public ApplicationUser()
        {
            Followers = new Collection<Following>();
            Followees = new Collection<Following>();
            UserNotifications = new Collection<UserNotification>();
        }

        public void Notify(Notification notification)
        {
            #region Explanation

            /* This user and notification can cause null reference exception
             UserNotification class is association class between user and notification
            so user and notifications cant be  null so we need to control
            in constructor...*/

            //var userNotifacation = new UserNotification(this, notification);

            #endregion
            UserNotifications.Add(new UserNotification(this, notification));
        }
    }
}