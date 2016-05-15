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
    public class RoleController : ApiController
    {
        

        public class AddRoleRequest
        {
            public User user { get; set; }
            public Role role { get; set; }
        }

        [HttpPost]
        public bool AddRole(AddRoleRequest roleRequest)
        {
         
            return Models.User.AddRole(roleRequest.user, roleRequest.role);

        }

        public class DeleteRoleRequest
        {
            public User user { get; set; }
            public Role role { get; set; }
        }

        [HttpPost]
        public bool DeleteRole(DeleteRoleRequest roleRequest)
        {

            return Models.User.DeleteRole(roleRequest.user, roleRequest.role);

        }

        [HttpPost]
        public List<Role> GetRoles(User user)
        {

            return Models.User.GetRoles(user);

        }
    }
}
