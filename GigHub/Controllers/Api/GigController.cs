using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class GigController : ApiController
    {
        private ApplicationDbContext _context;

        public GigController()
        {
            _context = new ApplicationDbContext();
        }
        [HttpDelete]
        public IHttpActionResult CancelGig(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);

            if (gig.isCanceled)
                return NotFound();

            gig.isCanceled = true;

            var notification = new Notification
            {
                DateTime = DateTime.Now,
                Gig = gig,
                Type = NotificationType.GigCanceled
            };

            var attendees = _context.Attendances
                .Where(a => a.GigId == gig.Id)
                .Select(a => a.Attendee)
                .ToList();
            foreach (var attendee in attendees)
            {
                var userNotifacation = new UserNotification
                {
                    User = attendee,
                    Notification = notification
                };
                _context.UserNotifications.Add(userNotifacation);
            }
            _context.SaveChanges();

            return Ok();
        }
    }
}
