using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PASS.Models.UserGroupManagement;
using PASS.UGMS.Dao;

namespace PASS.UGMS.Service
{
    /// <summary>
    /// 使用者管理Service
    /// </summary>
    public class UGMService : IUGMService
    {
        //使用者管理Dao宣告
        private UGMDao _UGMDao = new UGMDao();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="userPW"></param>
        /// <returns></returns>
        public UserProfile VerifyUser(string userID, string userPW)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userProfile"></param>
        /// <returns></returns>
        public bool CreateOrModifyUserProfile(UserProfile userProfile)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public UserProfile GetUserProfile(long userID)
        {
            throw new NotImplementedException();
        }
    }
}
