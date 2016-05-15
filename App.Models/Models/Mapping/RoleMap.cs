using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SingleApp.Models.Mapping
{
    public class RoleMap : EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            // Primary Key
            this.HasKey(t => t.RoleId);

            // Properties
            //this.Property(t => t.RoleId)
               // .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            /*this.Property(t => t.RoleDescription)
                .IsRequired()
                .HasMaxLength(50);
                */
            // Table & Column Mappings
            this.ToTable("Roles");
            this.Property(t => t.RoleId).HasColumnName("RoleId");
            //this.Property(t => t.RoleDescription).HasColumnName("RoleDescription");
        }
    }
}
