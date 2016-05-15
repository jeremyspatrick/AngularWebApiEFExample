using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace SingleApp.Models.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.UserId);

            // Properties
            this.Property(t => t.Username)
                .IsRequired()
                .HasMaxLength(50).HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                new IndexAttribute("IX_Username", 1) { IsUnique = true })); ;

            this.Property(t => t.Password)
                .IsRequired()
                .HasMaxLength(256);

            this.Property(t => t.Salt)
                .IsRequired()
                .HasMaxLength(256);

            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("User");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.Username).HasColumnName("Username");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.Salt).HasColumnName("Salt");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Created).HasColumnName("Created");
            //this.Property(t => t.UserDetail.UserDetailsId).HasColumnName("UserDetailsId");

            // Relationships
            this.HasOptional(t => t.UserDetail);
               // .WithMany(t => t.)
               // .HasForeignKey(d => d.UserDetailsId);

        }
    }
}
