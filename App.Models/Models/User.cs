using System;
using System.Collections.Generic;

namespace SingleApp.Models
{
    public partial class User
    {
        public User()
        {
            this.Roles = new List<Role>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Email { get; set; }
        public System.DateTime Created { get; set; }
        //public int UserDetailsId { get; set; }
        public UserDetail UserDetail { get; set; }
        public List<Role> Roles { get; set; }

    }
}
