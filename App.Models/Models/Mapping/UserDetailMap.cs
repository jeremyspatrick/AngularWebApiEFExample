using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SingleApp.Models.Mapping
{
    public class UserDetailMap : EntityTypeConfiguration<UserDetail>
    {
        public UserDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.UserDetailsId);

            // Properties
            this.Property(t => t.Firstname)
                .HasMaxLength(50);

            this.Property(t => t.Lastname)
                .HasMaxLength(50);

            this.Property(t => t.Street)
                .HasMaxLength(50);

            this.Property(t => t.City)
                .HasMaxLength(50);

            this.Property(t => t.State)
                .HasMaxLength(50);

            this.Property(t => t.Zip)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("UserDetails");
            this.Property(t => t.UserDetailsId).HasColumnName("UserDetailsId");
            this.Property(t => t.Firstname).HasColumnName("Firstname");
            this.Property(t => t.Lastname).HasColumnName("Lastname");
            this.Property(t => t.Street).HasColumnName("Street");
            this.Property(t => t.City).HasColumnName("City");
            this.Property(t => t.State).HasColumnName("State");
            this.Property(t => t.Zip).HasColumnName("Zip");
        }
    }
}
