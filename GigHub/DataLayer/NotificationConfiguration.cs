using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using GigHub.Models;

namespace GigHub.DataLayer
{
    public class NotificationConfiguration : EntityTypeConfiguration<Notification>
    {
        public NotificationConfiguration()
        {
            Property(n => n.GigId).IsRequired();


        }
    }
}