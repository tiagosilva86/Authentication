using System;
using System.Collections.Generic;

namespace Auth.Model
{
    public class User
    {
        public Guid Id {get;set;}
        public string Login { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public DateTime LastAccess { get; set; }
        public IEnumerable<UserRole> Roles { get; set; }
    }
}
