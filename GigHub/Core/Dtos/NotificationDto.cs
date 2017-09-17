using System;
using GigHub.Core.Models;

namespace GigHub.Core.Dtos
{
    public class NotificationDto
    {

        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get; private set; }
        public string OriginalValue { get; private set; }

        public GigDto Gig { get; private set; }
        public int GigId { get; set; }
    }
}