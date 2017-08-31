using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.Api
{
    public class NotificationsController : ApiController
    {
        private ApplicationDbContext _context;

        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<NotificationDto> GetNewNotifications()
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

            return notifications.Select(Mapper.Map<Notification, NotificationDto>);
            // return notifications;
        }
    }
}
