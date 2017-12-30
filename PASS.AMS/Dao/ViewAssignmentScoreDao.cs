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
    public class ViewAssignmentScoreDao : GenericDao<ViewAssignmentScore>
    {
        public List<ViewAssignmentScore> GetViewAssignmentScore(Int64 assignmentNo)
        {
            using (var cn = GetOpenConnection())
            {
                var sql = @"
                            select 
	                            UP.UserNo
	                            	,A.AssignmentNo
	                            	,S.Score
	                            	,UP.UserName
	                            	,UP.UserID
	                            	,F.FileNo
	                            from Assignment as A
	                            	join CourseSelection as CS on A.CourseNo = CS.CourseNo
	                            	join UserProfile as UP on CS.UserNo = UP.UserNo
	                            	join UserInfo as UI on UP.UserNo = UI.UserNo
	                            	left join SubmissionDetail as SUB on UP.UserNo = SUB.UserNo and A.AssignmentNo = SUB.AssignmentNo
	                            	left join File as F on SUB.FileNo = F.FileNo
	                            	left join AssignmentScore as S on UP.UserNo = S.UserNo and A.AssignmentNo = S.AssignmentNo
	                            where A.AssignmentNo = @AssignmentNo
	                            	and UI.UserType = 2";

                SQLiteDataAdapter da = new SQLiteDataAdapter(sql, cn);
                da.SelectCommand.Parameters.AddWithValue("@AssignmentNo", assignmentNo);

                DataTable dt = new DataTable();
                da.Fill(dt);
                cn.Close();

                return DtToObj(dt);
            }
        }

        private List<ViewAssignmentScore> DtToObj(DataTable dt)
        {
            var userProfiles = (from row in dt.AsEnumerable()

                                select new ViewAssignmentScore
                                {
                                    UserNo = row.Field<Int64>("UserNo"),
                                    AssignmentNo = row.Field<Int64>("AssignmentNo"),
                                    Score = row.Field<Int64?>("Score").ToString(),
                                    UserName = row.Field<string>("UserName"),
                                    UserID = row.Field<string>("UserID"),
                                    FileNo = row.Field<Int64?>("FileNo")
                                }).ToList();
            return userProfiles;
        }
    }    
}
