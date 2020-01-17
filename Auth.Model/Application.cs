using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Model
{
    public class Application
    {
        public Guid Id { get; set; }
        public string  Description  { get; set; }
        public bool Active { get; set; }
        public List<Role> Roles { get; set; }
    }
}
