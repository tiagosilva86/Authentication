using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Model
{
    public class UserRole
    {
        public Guid User_Id { get; set; }
        public int Role_Id { get; set; }
        public virtual Role Role { get; set; }
        public User User { get; set; }
    }
}
