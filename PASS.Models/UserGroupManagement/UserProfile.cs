using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASS.Models.UserGroupManagement
{
    /// <summary>
    /// 使用者資料
    /// </summary>
    public class UserProfile
    {
        public Int64 UserNo { get; set; }

        public string UserName { get; set; }

        public string UserID { get; set; }

        public bool Verified { get; set; }
    }
}
