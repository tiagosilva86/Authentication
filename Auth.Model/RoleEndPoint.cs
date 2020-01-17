using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Model
{
    public class RoleEndPoint
    {
        public int Role_Id { get; set; }
        public Guid EndPoint_Id { get; set; }
        public virtual Role Role { get; set; }
        public virtual EndPoint EndPoint { get; set; }
    }
}
