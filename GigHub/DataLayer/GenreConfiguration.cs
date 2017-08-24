using System.Data.Entity.ModelConfiguration;

namespace GigHub.Models
{
    internal class GenreConfiguration : EntityTypeConfiguration<Genre>
    {
        public GenreConfiguration()
        {
            Property(g => g.Name).IsRequired().HasMaxLength(255);
        }
    }
}