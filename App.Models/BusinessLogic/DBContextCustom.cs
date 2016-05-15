using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using SingleApp.Models.Mapping;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SingleApp.Models
{
    public partial class SingleAppContextCustom : SingleAppContext
    {

        public SingleAppContextCustom()
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public class SessionConfiguration : EntityTypeConfiguration<Session>
        {
            public SessionConfiguration() : base()
            {
                Property(p => p.SessionId).IsOptional();
            }
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           



            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<User>().Property(c => c.Created)
            //  .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            //modelBuilder.Entity<Session>().Property(c => c.Created)
            // .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            modelBuilder.Entity<Session>().Property(c => c.Created).HasColumnType("datetime2");
            modelBuilder.Entity<User>().Property(c => c.Created).HasColumnType("datetime2");



        }




    }
}
