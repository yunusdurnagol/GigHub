using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using GigHub.Models;
using System.Data.Entity.Infrastructure.Annotations;

namespace GigHub.DataLayer
{
    public class UserNotificationConfiguration : EntityTypeConfiguration<UserNotification>
    {
        public UserNotificationConfiguration()
        {
            HasKey(u => new { u.UserId, u.NotificationId });
            Property(u => u.UserId).IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("AK_Notification", 1) { IsUnique = true }));

            Property(a => a.NotificationId).IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("AK_Notification", 2) { IsUnique = true }));

            HasRequired(n => n.User)
                .WithMany()
                .WillCascadeOnDelete(false);
        }
    }
}