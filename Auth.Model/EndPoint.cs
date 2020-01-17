using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Model
{
   public class EndPoint
    {
        public string Description { get; set; }
        public Guid Hash { get; set; }
        public bool Active { get; set; }
        public List<RoleEndPoint> Roles { get; set; }
        public Application Application { get; set; }

    }
}
