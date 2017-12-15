using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PASS.Models.Attribute;

namespace PASS.Models.AssignmentManagement
{
    /// <summary>
    /// 上傳的作業資訊
    /// </summary>
    public class SubmissionDetail
    {
        [PrimaryKey]
        public Int64 SubmissionNo { get; set; }

        [DapperKey]
        public Int64 UserNo { get; set; }
        [DapperKey]
        public Int64 AssignmentNo { get; set; }

        public string FileName { get; set; }

        public Int64 FileNo { get; set; }
    }
}
