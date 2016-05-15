using System;
using System.Collections.Generic;

namespace SingleApp.Models
{
    public partial class UserDetail
    {
        public int UserDetailsId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}
