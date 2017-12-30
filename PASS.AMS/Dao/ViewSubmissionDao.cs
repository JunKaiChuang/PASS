using PASS.Common.Common;
using PASS.Common.DaoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PASS.Models.AssignmentManagement;
using System.Data;
using System.Data.SQLite;

namespace PASS.AMS.Dao
{
    /// <summary>
    /// 作業資訊dao
    /// </summary>
    public class ViewSubmissionDao : GenericDao<ViewSubmission>
    {
        CommonService _commonService = new CommonService();

        /// <summary>
        /// 依照UserNo及CourseNo取得作業資訊清單
        /// </summary>
        /// <param name="userNo"></param>
        /// <param name="courseNo"></param>
        /// <returns></returns>
        public List<ViewSubmission> GetViewSubmissionListByUserNoAndCourseNo(Int64 userNo, Int64 courseNo)
        {
            using (var cn = GetOpenConnection())
            {
                var sql = @"
                            select A.AssignmentNo
	                            ,A.AssignmentTitle
	                            ,A.AssignOrder
                            	,case when ifnull(SUB.SubmissionNo, 0)=0 then 0 else 1 end as IsUploaded
                            	,A.EndDate
                            	,S.Score
                            from UserProfile as UP
                            	join CourseSelection as CS on UP.UserNo = CS.UserNo
                            	join CourseInfo as CI on CS.CourseNo = CI.CourseNo
                            	join Assignment as A on CI.CourseNo = A.CourseNo
                            	left join SubmissionDetail as SUB on A.AssignmentNo = SUB.AssignmentNo and UP.UserNo = SUB.UserNo
                            	left join AssignmentScore as S on UP.UserNo = S.UserNo and A.AssignmentNo = S.AssignmentNo
                            where UP.UserNo = @UserNo
                            	and CS.SchoolYear = @SchoolYear
                            	and CS.Semester = @Semester
                            	and CI.CourseNo = @CourseNo";
                var semesterInfo = _commonService.GetCurrentSemesterInfo();

                SQLiteDataAdapter da = new SQLiteDataAdapter(sql, cn);
                da.SelectCommand.Parameters.AddWithValue("@UserNo", userNo);
                da.SelectCommand.Parameters.AddWithValue("@SchoolYear", semesterInfo.SchoolYear);
                da.SelectCommand.Parameters.AddWithValue("@Semester", semesterInfo.Semester);
                da.SelectCommand.Parameters.AddWithValue("@CourseNo", courseNo);

                DataTable dt = new DataTable();
                da.Fill(dt);
                cn.Close();

                return DtToObj(dt);
            }
        }

        public ViewSubmission GetViewSubmissionByUserNoAndAssignmentNo(Int64 userNo, Int64 assignmentNo)
        {
            using (var cn = GetOpenConnection())
            {
                var sql = @"
                            select A.AssignmentNo
	                            ,A.AssignmentTitle
	                            ,A.AssignOrder
                            	,case when ifnull(SUB.SubmissionNo, 0)=0 then 0 else 1 end as IsUploaded
                            	,A.EndDate
                            	,S.Score
                            from UserProfile as UP
                            	join CourseSelection as CS on UP.UserNo = CS.UserNo
                            	join CourseInfo as CI on CS.CourseNo = CI.CourseNo
                            	join Assignment as A on CI.CourseNo = A.CourseNo
                            	left join SubmissionDetail as SUB on A.AssignmentNo = SUB.AssignmentNo and UP.UserNo = SUB.UserNo
                            	left join AssignmentScore as S on UP.UserNo = S.UserNo and A.AssignmentNo = S.AssignmentNo
                            where UP.UserNo = @UserNo
                            	and CS.SchoolYear = @SchoolYear
                            	and CS.Semester = @Semester
                            	and A.assignmentNo = @assignmentNo";
                var semesterInfo = _commonService.GetCurrentSemesterInfo();

                SQLiteDataAdapter da = new SQLiteDataAdapter(sql, cn);
                da.SelectCommand.Parameters.AddWithValue("@UserNo", userNo);
                da.SelectCommand.Parameters.AddWithValue("@SchoolYear", semesterInfo.SchoolYear);
                da.SelectCommand.Parameters.AddWithValue("@Semester", semesterInfo.Semester);
                da.SelectCommand.Parameters.AddWithValue("@assignmentNo", assignmentNo);

                DataTable dt = new DataTable();
                da.Fill(dt);
                cn.Close();

                return DtToObj(dt).FirstOrDefault();
            }
        }

        private List<ViewSubmission> DtToObj(DataTable dt)
        {
            var userProfiles = (from row in dt.AsEnumerable()

                                select new ViewSubmission
                                {
                                    AssignmentNo = row.Field<Int64>("AssignmentNo"),
                                    AssignmentTitle = row.Field<string>("AssignmentTitle"),
                                    AssignOrder = (int)row.Field<Int64>("AssignOrder"),
                                    IsUploaded = row.Field<Int64>("IsUploaded") == 0 ? false : true,
                                    EndDate = row.Field<DateTime>("EndDate"),
                                    Score = row.Field<Int64?>("Score").ToString()
                                }).ToList();
            return userProfiles;
        }
    }
}
