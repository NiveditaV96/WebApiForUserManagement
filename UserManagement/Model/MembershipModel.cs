using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserManagement.Model
{
    public class MembershipModel
    {
            public string ApplicationId { get; set; }
            public string UserId { get; set; }
            public string Password { get; set; }
            public string PasswordFormat { get; set; }
            public string PasswordSalt { get; set; }
            public string IsApproved { get; set; }
            public string IsLockedOut { get; set; }
            public string CreateDate { get; set; }
            public string LastLoginDate { get; set; }
            public string LastPasswordChangedDate { get; set; }
            public string LastLockoutDate { get; set; }
            public string FailedPasswordAttemptCount { get; set; }
            public string FailedPasswordAttemptWindowStart { get; set; }
            public string FailedPasswordAnswerAttemptCount { get; set; }
            public string FailedPasswordAnswerAttemptWindowStart { get; set; }
            public string Comment { get; set; }
    }
}