using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PASS.Models.Attribute;

namespace PASS.Models.AssignmentManagement
{
    /// <summary>
    /// 作業題目資料
    /// </summary>
    public class Assignment
    {
        [PrimaryKey]
        public Int64 AssignmentNo { get; set; }

        [DapperKey]
        public Int64 CourseNo { get; set; }
        [DapperKey]
        public int AssignOrder { get; set; }

        public string AssignmentTitle { get; set; }
        public string AssignmentDescription { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
