using System.Data.Entity.ModelConfiguration;
using GigHub.Core.Models;

namespace GigHub.Core.DataLayer.DataLayer
{
    internal class GenreConfiguration : EntityTypeConfiguration<Genre>
    {
        public GenreConfiguration()
        {
            Property(g => g.Name).IsRequired().HasMaxLength(255);
        }
    }
}