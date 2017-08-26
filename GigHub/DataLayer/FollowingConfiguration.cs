using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.Models
{
    internal class FollowingConfiguration : EntityTypeConfiguration<Following>
    {
        public FollowingConfiguration()
        {
            HasKey(u => new { u.FollowerId, u.FolloweeId });
            Property(a => a.FollowerId).IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("AK_Following", 1) { IsUnique = true }));

            Property(a => a.FolloweeId).IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("AK_Following", 2) { IsUnique = true }));

        }
    }
}