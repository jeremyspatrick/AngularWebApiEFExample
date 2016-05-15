using SingleApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SingleApp.Models
{
    public partial class UserDetail
    {


        public void Save() {
            using (var context = new SingleAppContextCustom())
            {
                
                EntityState UpdateOrDelete = this.UserDetailsId == 0 ? 
                           EntityState.Added : 
                           EntityState.Modified;

                context.Entry(this).State = UpdateOrDelete;
                
                context.SaveChanges();
            }
        }

        public void Delete()
        {
            using (var context = new SingleAppContextCustom())
            {
                context.UserDetails.Attach(this);
                context.UserDetails.Remove(this);
                context.SaveChanges();
            }
        }

        
    }

}
