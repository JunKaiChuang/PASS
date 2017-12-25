using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using PASS.Models.Course;
using PASS.Common.DaoService;
using System.Data;
using PASS.Common.Common;

namespace PASS.AMS.Dao
{
    /// <summary>
    /// 課程資訊Dao
    /// </summary>
    public class CourseDao : GenericDao<CourseInfo>
    {
        CommonService _commonService = new CommonService();

        /// <summary>
        /// 依照UserNo取得該學期的課程
        /// </summary>
        /// <param name="userNo"></param>
        /// <returns></returns>
        public List<CourseInfo> GetCourseListByUserNo(Int64 userNo)
        {
            using (var cn = GetOpenConnection())
            {
                var sql = @"
                            select CI.*
                            from UserProfile as UP
	                            join CourseSelection as CS on UP.UserNo = CS.UserNo
	                            join CourseInfo as CI on CS.CourseNo = CI.CourseNo
                            where UP.UserNo = @UserNo
	                            and CS.SchoolYear = @SchoolYear
	                            and CS.Semester = @Semester";
                var semesterInfo = _commonService.GetCurrentSemesterInfo();

                SQLiteDataAdapter da = new SQLiteDataAdapter(sql, cn);
                da.SelectCommand.Parameters.AddWithValue("@UserNo", userNo);
                da.SelectCommand.Parameters.AddWithValue("@SchoolYear", semesterInfo.SchoolYear);
                da.SelectCommand.Parameters.AddWithValue("@Semester", semesterInfo.Semester);

                DataTable dt = new DataTable();
                da.Fill(dt);
                cn.Close();

                return DtToObj(dt);
            }
        }



        private List<CourseInfo> DtToObj(DataTable dt)
        {
            var userProfiles = (from row in dt.AsEnumerable()

                                select new CourseInfo
                                {
                                    CourseNo = row.Field<Int64>("CourseNo"),
                                    CourseName = row.Field<string>("CourseName"),
                                    LecturerName = row.Field<string>("LecturerName")
                                }).ToList();
            return userProfiles;
        }
    }
}
