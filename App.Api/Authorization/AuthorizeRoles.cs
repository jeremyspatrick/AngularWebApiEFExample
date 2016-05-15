using SingleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace SingleApp
{

    public class AuthorizeRoles : System.Web.Http.AuthorizeAttribute
    {
        private List<RoleType> _roles;

        public AuthorizeRoles(params RoleType[] roles)
        {
            _roles = roles.ToList<RoleType>();
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            return true;
            //getuser from security principal or database
            SecurityPrincipal securityPrincipal = (SecurityPrincipal)HttpContext.Current.User;
            List<Role> userRoles = securityPrincipal.Session.User.Roles;

            foreach (Role userRole in userRoles)
            {
                if (_roles.Contains(userRole.RoleType))
                    return true;
            }
            return false;
        }

    }
}

