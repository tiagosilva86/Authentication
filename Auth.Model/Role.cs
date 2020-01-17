using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Model
{
    public class Role
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public bool Active { get; set; }
        public List<RoleEndPoint> EndPoints { get; set; }
        public Guid Application_Id { get; set; }
        public Application Application { get; set; }
    }
}
