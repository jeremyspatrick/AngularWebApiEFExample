using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleApp.Models
{
    public partial class Role
    {

        public void Save()
        {

            //defaults
      


            using (var context = new SingleAppContextCustom())
            {

                EntityState UpdateOrDelete = this.RoleId == 0 ?
                           EntityState.Added :
                           EntityState.Modified;

                context.Entry(this).State = UpdateOrDelete;

                try
                {
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
                //ToFix
                context.Roles.Attach(this);
                context.Roles.Remove(this);
                context.SaveChanges();
            }
        }
    }
}
