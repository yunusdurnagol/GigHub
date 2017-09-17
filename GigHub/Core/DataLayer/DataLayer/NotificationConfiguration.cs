using System.Data.Entity.ModelConfiguration;
using GigHub.Core.Models;

namespace GigHub.Core.DataLayer.DataLayer
{
    public class NotificationConfiguration : EntityTypeConfiguration<Notification>
    {
        public NotificationConfiguration()
        {
            Property(n => n.GigId).IsRequired();


        }
    }
}