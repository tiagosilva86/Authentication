using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Model
{
   public class Token
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime ValidTo { get; set; }
        public string IssuedBy { get; set; }
        public DateTime IssueDate { get; set; }
        public Application Application { get; set; }
        public Guid Application_Id { get; set; }
        public User User { get; set; }
        public Guid User_Id { get; set; }


    }
}
