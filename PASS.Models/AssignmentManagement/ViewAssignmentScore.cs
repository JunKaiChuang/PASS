using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASS.Models.AssignmentManagement
{
    /// <summary>
    /// 供部分資訊檢閱的作業成績
    /// </summary>
    public class ViewAssignmentScore : AssignmentScore
    {
        public string UserName { get; set; }

        public string UserID { get; set; }

        public Int64? FileNo { get; set; }
    }
}
