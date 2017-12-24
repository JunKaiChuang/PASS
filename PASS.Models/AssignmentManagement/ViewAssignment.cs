using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASS.Models.AssignmentManagement
{
    /// <summary>
    /// 供部分資訊檢閱的作業題目資訊
    /// </summary>
    public class ViewAssignment : Assignment
    {
        public string CourseName { get; set; }

    }
}
