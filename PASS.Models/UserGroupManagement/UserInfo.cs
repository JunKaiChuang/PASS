using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PASS.Models.Enum;

namespace PASS.Models.UserGroupManagement
{
    public class UserInfo
    {
        public Int64 UserNo { get; set; }

        public string UserID { get; set; }

        public UserType UserType { get; set; }
    }
}
