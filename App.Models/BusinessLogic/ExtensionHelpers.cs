using SingleApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleApp
{
    public static class ExtensionHelpers
    {
       

        public static IQueryable<User> IncludeChildren(this IQueryable<User> query)
        {
            return query.Include(a => a.Roles);
        }

        public static IQueryable<Session> IncludeChildren(this IQueryable<Session> query)
        {
            return query.Include(a => a.User.Roles);//.Include(b => b.User.More);

        }

        public static List<User> StripSensitiveFields(this List<User> users)
        {
            foreach (User user in users)
            {
                user.Password = "";
                user.Salt = "";
            }
            return users;

        }

     
    }
}
