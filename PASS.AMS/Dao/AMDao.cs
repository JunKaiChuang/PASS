using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SQLite;
using System.Text;
using System.Threading.Tasks;
using PASS.Common.DaoService;
using PASS.Models.AssignmentManagement;
using System.Data;

namespace PASS.AMS.Dao
{
    //作業管理Dao
    public class AMDao : GenericDao<Assignment>
    {
        public bool CreateOrModify(Assignment assignment)
        {
            if (assignment.AssignmentNo == 0) Insert(assignment);
            else Update(assignment);
            return true;
        }

        public Assignment GetAssignmentByNo(Int64 assignmentNo)
        {
            using (var cn = GetOpenConnection())
            {
                var sql = @"
                            select * from Assignment
                            where (AssignmentNo=@AssignmentNo)";

                SQLiteDataAdapter da = new SQLiteDataAdapter(sql, cn);
                da.SelectCommand.Parameters.AddWithValue("@AssignmentNo", assignmentNo);

                DataTable dt = new DataTable();
                da.Fill(dt);
                cn.Close();

                return DtToObj(dt).FirstOrDefault();
            }
        }

        public List<Assignment> GetAssignmentListByCourseNo(Int64 courseNo)
        {
            using (var cn = GetOpenConnection())
            {
                var sql = @"
                            select * from Assignment
                            where (CourseNo=@CourseNo)";

                SQLiteDataAdapter da = new SQLiteDataAdapter(sql, cn);
                da.SelectCommand.Parameters.AddWithValue("@CourseNo", courseNo);

                DataTable dt = new DataTable();
                da.Fill(dt);
                cn.Close();

                return DtToObj(dt);
            }
        }

        public Int64 GetUserNoByFileNo (Int64 fileNo)
        {
            using (var cn = GetOpenConnection())
            {
                var sql = @"
                            select UP.UserNo
                            from File as F
                            	join SubmissionDetail as SD on F.FileNo = SD.FileNo
                            	join UserProfile as UP on SD.UserNo = UP.UserNo
                            where F.FileNo = @FileNo";

                SQLiteDataAdapter da = new SQLiteDataAdapter(sql, cn);
                da.SelectCommand.Parameters.AddWithValue("@FileNo", fileNo);

                DataTable dt = new DataTable();
                da.Fill(dt);
                cn.Close();

                var result = dt.Rows[0].Field<Int64>("UserNo");

                return result;
            }

        }

        private List<Assignment> DtToObj(DataTable dt)
        {
            var userProfiles = (from row in dt.AsEnumerable()

                                select new Assignment
                                {
                                    AssignmentNo = row.Field<Int64>("AssignmentNo"),
                                    CourseNo = row.Field<Int64>("CourseNo"),
                                    AssignOrder = (int)row.Field<Int64>("AssignOrder"),
                                    AssignmentTitle = row.Field<string>("AssignmentTitle"),
                                    AssignmentDescription = row.Field<string>("AssignmentDescription"),
                                    EndDate = row.Field<DateTime>("EndDate")

                                }).ToList();
            return userProfiles;
        }


    }
}
