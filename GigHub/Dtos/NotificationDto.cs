using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Models;

namespace GigHub.Dtos
{
    public class NotificationDto
    {

        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get; private set; }
        public string OriginalValue { get; private set; }

        public GigDto GigDto { get; private set; }
        public int GigId { get; set; }
    }
}