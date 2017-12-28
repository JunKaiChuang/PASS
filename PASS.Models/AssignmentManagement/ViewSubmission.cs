using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PASS.Models.AssignmentManagement
{
    /// <summary>
    /// 供部分資訊檢閱的作業資訊
    /// </summary>
    public class ViewSubmission
    {
        public Int64 AssignmentNo { get; set; }

        public string AssignmentTitle { get; set; }

        public int AssignOrder { get; set; }

        public bool IsUploaded { get; set; }

        public DateTime EndDate { get; set; }

        public string Score { get; set; }
    }
}
