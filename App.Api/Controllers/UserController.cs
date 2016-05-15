
using SingleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Data.Entity;

namespace SingleApp.Controllers
{
    [AuthorizeRoles(RoleType.Admin)]
    public class UserController : ApiController
    {
        [HttpPost]
        public string Registration(User user)
        {
            //return null;
            return Models.User.Register(user);

        }

       

        [HttpGet]
        public User GetAUser()
        {
            User user = Models.User.GetAUser();
            //return null;
            return user;

        }

        [HttpGet]
        public User GetAUserWithDetail()
        {
            User user = Models.User.GetAUserWithDetail();
            //return null;
            return user;

        }

       

        [HttpPost]
        public List<User> GetAllUsers()
        {
            //return null;
            return Models.User.GetAllUsers();

        }
    }
}
