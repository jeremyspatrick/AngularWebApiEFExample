using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SingleApp.Models.Mapping
{
    public class SessionMap : EntityTypeConfiguration<Session>
    {
        public SessionMap()
        {
            // Primary Key
            this.HasKey(t => t.SessionId);

            // Properties
            this.Property(t => t.SessionId)
                .IsRequired()
                .HasMaxLength(64);

            // Table & Column Mappings
            this.ToTable("Session");
            this.Property(t => t.SessionId).HasColumnName("SessionId");
           // this.Property(t => t.User.UserId).HasColumnName("UserId");
            this.Property(t => t.Created).HasColumnName("Created");
            this.Property(t => t.LastAccess).HasColumnName("LastAccess");

        }
    }
}
