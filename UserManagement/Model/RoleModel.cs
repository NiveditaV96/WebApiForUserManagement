using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserManagement.Model
{
    public class RoleModel
    {
        public string ApplicationId { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string LoweredRolename { get; set;}
        public string Description { get; set; }


    }
}