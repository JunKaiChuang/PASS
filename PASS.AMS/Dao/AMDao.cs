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
