using SingleApp.Models;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SingleApp
{
    public class SecurityPrincipal : IPrincipal
    {
        public Session Session { get; set;}

        public SecurityPrincipal(Session session)
            : base()
        {
            this.Identity = new GenericIdentity(session.SessionId, "Database");
            Session = session;
        }

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }

    }
}