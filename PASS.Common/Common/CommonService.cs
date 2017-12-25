using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PASS.Models.Course;

namespace PASS.Common.Common
{
    public class CommonService
    {
        /// <summary>
        /// 取的現在時間的學年及學期
        /// </summary>
        /// <returns></returns>
        public SemesterInfo GetCurrentSemesterInfo()
        {
            var now = DateTime.Now;
            var republicEra = now.Year - 1911; //西元年 - 1911 = 民國年
            var semester = now.Month >= 8 ? 1 : 2;  //八月後為第一學期
            var schoolYear = semester == 1 ? republicEra : republicEra - 1; //第一學期學年與民國年相同，第二學期則為民國年減一

            return new SemesterInfo() {SchoolYear = schoolYear, Semester = semester };
        }
    }
}
