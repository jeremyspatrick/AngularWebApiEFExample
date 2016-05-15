using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using SingleApp.Models.Mapping;

namespace SingleApp.Models
{
    public partial class SingleAppContext : DbContext
    {
        static SingleAppContext()
        {
            Database.SetInitializer<SingleAppContext>(null);
        }

        public SingleAppContext()
            : base("Name=SingleAppContext")
        {
        }

       
        public DbSet<Role> Roles { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
      
      

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new SessionMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new UserDetailMap());
           
        }
    }
}
