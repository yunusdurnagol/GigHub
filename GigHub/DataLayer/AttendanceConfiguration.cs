using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.Models
{
    internal class AttendanceConfiguration : EntityTypeConfiguration<Attendance>
    {
        public AttendanceConfiguration()
        {

            HasKey(a => new { a.GigId, a.AttendeeId });

            Property(a => a.GigId).IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("AK_Attendance", 1) { IsUnique = true }));
            Property(a => a.AttendeeId).IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("AK_Attendance", 2) { IsUnique = true }));

            HasRequired(a => a.Gig).
            WithMany().
            WillCascadeOnDelete(false);

        }
    }
}