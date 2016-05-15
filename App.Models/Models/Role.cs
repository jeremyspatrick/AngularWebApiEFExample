using System;
using System.Collections.Generic;

namespace SingleApp.Models
{
    public partial class Role
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public RoleType RoleType { get; set; }
    }
}
