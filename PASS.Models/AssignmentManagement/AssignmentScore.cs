using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PASS.Models.Attribute;

namespace PASS.Models.AssignmentManagement
{
    /// <summary>
    /// 成績登錄
    /// </summary>
    public class AssignmentScore
    {
        [DapperKey]
        public Int64 UserNo { get; set; }

        [DapperKey]
        public Int64 AssignmentNo { get; set; }

        public string Score { get; set; }
    }
}
