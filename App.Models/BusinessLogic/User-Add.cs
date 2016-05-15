using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Validation;
using System.Linq;

namespace SingleApp.Models
{
    public partial class User
    {


        public void Save() {

            //defaults
            if(this.Created == DateTime.MinValue)
                this.Created = DateTime.Now;

            using (var context = new SingleAppContextCustom())
            {
                
                EntityState UpdateOrDelete = this.UserId == 0 ? 
                           EntityState.Added : 
                           EntityState.Modified;

                context.Entry(this).State = UpdateOrDelete;

                try {
                    context.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
            }
        }

        public void Delete()
        {
            using (var context = new SingleAppContextCustom())
            {
                context.Users.Attach(this);
                context.Users.Remove(this);
                context.SaveChanges();
            }
        }

        public static Session Login(User user)
        {
            
            using (var context = new SingleAppContextCustom())
            {
                User userEntity;
                log4net.LogManager.GetLogger("myLog").Debug("made it here");
                userEntity = (from u in context.Users.IncludeChildren()
                              where u.Username == user.Username
                              select u).FirstOrDefault();
                log4net.LogManager.GetLogger("myLog").Debug("   aaaaaaaaaaaaaaaaaa  ");
                if (userEntity == null)
                    return null;
                log4net.LogManager.GetLogger("myLog").Debug("   bbbbbbbbbbbbbbbbbbbbbb  ");
                string hashedPassword = Utility.HashAndSaltPassword(user.Password, userEntity.Salt);

                //compare hashed passwords.  Also compare clear text password if a short password has been stored in the db
                if (userEntity.Password == hashedPassword || (userEntity.Password.Length <= 10 && userEntity.Password == user.Password))
                {
                    log4net.LogManager.GetLogger("myLog").Debug("   cccccccccccccccccc  ");
                    //success
                    Session session = new Session();
                    session.UserId = userEntity.UserId;
                    session.Save();
                    session.User = userEntity; //add entity for returning
                    log4net.LogManager.GetLogger("myLog").Debug("   ddddddddddddddddddddd  ");
                    return session;
                    
                }


                //fail
                return null;
            }
        }

        public static User GetAUserWithDetail() {
            using (var context = new SingleAppContextCustom())
            {
                var user = context.Users.Include("UserDetail").FirstOrDefault();

                return user;
            }
        }

        public static User GetAUser()
        {
            using (var context = new SingleAppContextCustom())
            {
                context.Database.Connection.Open();
                var user = context.Users.IncludeChildren().FirstOrDefault();
                //var s = user.UserDetail.City;
                return user;
            }
        }

        public static string Register(User user) {

            user.Salt = Utility.RandomString(64);
            user.Password = Utility.HashAndSaltPassword(user.Password, user.Salt);

           // user.Save();

            Session session = new Session();
            session.User = user;
            session.Save();

            return session.SessionId;
        }

        public static bool AddRole(User user, Role role)
        {

            using (var context = new SingleAppContextCustom())
            {
                User userEntity = context.Users.Find(user.UserId);
                // User userEntity = context.Entry(this);
                //User userEntity = context.Users.Find(user.UserId);
                role.UserId = user.UserId;
               userEntity.Roles.Add(role);
                //Role newRole = new Role();
               // newRole.UserId = user.UserId;
               // newRole.RoleType = role.RoleType;
               // newRole.Save();
     
                context.SaveChanges();
                return true;
            }

          
        }

        public static bool DeleteRole(User user, Role role)
        {

            using (var context = new SingleAppContextCustom())
            {
                // User userEntity = context.Users.Find(user.UserId);


                var roleEntity = context.Roles.Where(r => r.RoleType == role.RoleType && r.UserId == user.UserId).FirstOrDefault();

                if (roleEntity != null)
                    context.Roles.Remove(roleEntity);

                context.SaveChanges();
                return true;
            }


        }

        public static List<Role> GetRoles(User user)
        {

            using (var context = new SingleAppContextCustom())
            {
                // User userEntity = context.Users.Find(user.UserId);

                var userEntity = (from u in context.Users.IncludeChildren()
                                  where u.UserId == user.UserId
                                  select u).FirstOrDefault();

                //userEntity.Roles.Remove(role);

               
                return userEntity.Roles;
            }


        }

     



        public static List<User> GetAllUsers()
        {

            using (var context = new SingleAppContextCustom())
            {
                return (from u in context.Users.IncludeChildren()
                        select u).ToList().StripSensitiveFields();
            }


        }


    }

}
