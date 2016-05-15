using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace SingleApp.Models
{
    public partial class Session
    {
        static int SessionTimeout = 60; //minutes

        public bool Expired
        {
            get
            {
                return DateTime.Now.Subtract(this.LastAccess).TotalMinutes > Session.SessionTimeout;
            }
        }

       /* public User User { get; set; }*/

        public void Save() {

            
            EntityState UpdateOrDelete = this.SessionId == null ?
                           EntityState.Added :
                           EntityState.Modified;

            //Defaults
            if (this.Created == DateTime.MinValue)
                this.Created = DateTime.Now;

            if (this.LastAccess == DateTime.MinValue)
                this.LastAccess = DateTime.Now;

            if (this.SessionId == null)
                this.SessionId = Utility.RandomString(64);


            using (var context = new SingleAppContextCustom())
            {
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
                context.Sessions.Attach(this);
                context.Sessions.Remove(this);
                context.SaveChanges();
            }
        }

        public static Session Lookup(string sessionId) {
            using (var context = new SingleAppContextCustom())
            {
                var session = (from s in context.Sessions.IncludeChildren()
                               where s.SessionId == sessionId
                               select s).FirstOrDefault();

                /*if (session != null)
                { 
                    var user = (from u in context.Users//.Include("UserRoles")
                                where u.UserId == session.UserId
                                select u).FirstOrDefault();

                    session.User = user;
                }*/

                return session;
            }
        }

        

    }

}
