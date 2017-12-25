using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASS.Models.Course
{
    /// <summary>
    /// 課程資訊
    /// </summary>
    public class CourseInfo
    {
        public Int64 CourseNo { get; set; }

        public string CourseName { get; set; }

        public string LecturerName { get; set; }
    }
}
