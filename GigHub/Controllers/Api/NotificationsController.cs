using AutoMapper;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Persistence;

namespace GigHub.Controllers.Api
{
    public class NotificationsController : ApiController
    {
        private ApplicationDbContext _context;

        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }

        public IHttpActionResult GetNewNotifications()
        {
            var userId = User.Identity.GetUserId();
            //var notifications = _context.UserNotifications
            //   .Where(u => u.UserId == userId && u.IsRead == false)
            //   .Select(un => un.Notification)
            //   .Include(n => n.Gig.Artist)
            //   .ProjectTo<NotificationDto>()// This is from automapper implementation
            //   .ToList();

            var notifications = _context.UserNotifications
                .Where(u => u.UserId == userId && !u.IsRead)
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist)
                .ToList();
            int count = notifications.Count();

            if (notifications.Count == 0)
            {
                notifications = _context.UserNotifications
               .Where(u => u.UserId == userId)
               .Select(un => un.Notification)
               .OrderByDescending(un => un.Id).Take(5)
               .Include(n => n.Gig.Artist)
               .ToList();
                count = 0;
            }
            var notificationList = notifications.Select(Mapper.Map<Notification, NotificationDto>);


            return Ok(new { notificationList, count });
            // return notifications;
        }

        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _context
                .UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .ToList();
            notifications.ForEach(n => n.Read());
            _context.SaveChanges();
            return Ok();
        }

    }
}
