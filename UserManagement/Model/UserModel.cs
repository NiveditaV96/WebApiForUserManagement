using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserManagement.Models
{
    public class UserModel
    {
        string mobileAlias;
        public string ApplicationId { get; set; }

        public string UserID { get; set; }

        public string Username { get; set; }

        public string LoweredUsername { get; set; }

        public string MobileAlias { get; set; }


        //public string MobileAlias
        //{
        //    get { return mobileAlias; }
        //    set { mobileAlias = String.Empty; }
        //}

        public string IsAnonymous { get; set; }

        public string LastActivityDate { get; set; }
    
     
    }
}