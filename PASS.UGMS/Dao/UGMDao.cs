using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PASS.Common.DaoService;
using PASS.Models.UserGroupManagement;
using System.Data;

namespace PASS.UGMS.Dao
{
    /// <summary>
    /// 使用者帳號管理Dao
    /// </summary>
    public class UGMDao : GenericDao<UserProfile>
    {
        public bool CreateOrModify(UserProfile userProfile)
        {
            if (userProfile.UserNo == 0) Insert(userProfile);
            else Update(userProfile);
            return true;
        }

        public UserProfile GetUserProfileByNo(Int64 userNo)
        {

            using (var cn = GetOpenConnection())
            {
                var sql = string.Format(@"
                                            select * from UserProfile
                                            where (UserNo={0})"
                                        , userNo.ToString());

                SQLiteDataAdapter da = new SQLiteDataAdapter(sql, cn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cn.Close();

                return DtToObj(dt).FirstOrDefault();
            }
        }

        public UserProfile GetUserProfileByID(string userID)
        {
            using (var cn = GetOpenConnection())
            {
                var sql = string.Format(@"
                                            select * from UserProfile
                                            where (UserID={0})"
                                        , userID.ToString());

                SQLiteDataAdapter da = new SQLiteDataAdapter(sql, cn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cn.Close();

                return DtToObj(dt).FirstOrDefault();
            }
        }

        private List<UserProfile> DtToObj(DataTable dt)
        {
            var userProfiles = (from row in dt.AsEnumerable()

                            select new UserProfile
                            {
                                UserNo = row.Field<Int64>("UserNo"),
                                UserName = row.Field<string>("UserName"),
                                UserID = row.Field<string>("UserID"),
                                UserPW = row.Field<string>("UserPW"),
                                Authorization = row.Field<string>("Authorization")

                            }).ToList();
            return userProfiles;
        }
    }
}
