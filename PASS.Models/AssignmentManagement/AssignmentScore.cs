using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASS.Models.AssignmentManagement
{
    /// <summary>
    /// 成績登錄
    /// </summary>
    public class AssignmentScore
    {
        public Int64 UserNo { get; set; }

        public Int64 AssignmentNo { get; set; }

        public int Score { get; set; }
    }
}
