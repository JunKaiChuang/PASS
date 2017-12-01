using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PASS.Models.Attribute;

namespace PASS.Models.UserGroupManagement
{
    /// <summary>
    /// 使用者資料
    /// </summary>
    public class UserProfile
    {
        [PrimaryKey]
        public Int64 UserNo { get; set; }

        public string UserName { get; set; }

        [DapperKey]
        public string UserID { get; set; }

        public string Authorization { get; set; }
    }
}
