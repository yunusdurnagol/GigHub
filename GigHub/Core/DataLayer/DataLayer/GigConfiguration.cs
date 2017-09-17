using System.Data.Entity.ModelConfiguration;
using GigHub.Core.Models;

namespace GigHub.Core.DataLayer.DataLayer
{
    public class GigConfiguration : EntityTypeConfiguration<Gig>
    {
        public GigConfiguration()
        {
            Property(g => g.ArtistId).IsRequired();
            Property(g => g.GenreId).IsRequired();
            Property(g => g.Venue).IsRequired().HasMaxLength(255);
        }
    }
}