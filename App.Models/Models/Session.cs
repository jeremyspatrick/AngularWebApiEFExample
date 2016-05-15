using System;
using System.Collections.Generic;

namespace SingleApp.Models
{
    public partial class Session
    {
        public string SessionId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public System.DateTime Created { get; set; }
        public System.DateTime LastAccess { get; set; }
    }
}
