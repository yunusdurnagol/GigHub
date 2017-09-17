using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using GigHub.Core.Models;

namespace GigHub.Core.DataLayer.DataLayer
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