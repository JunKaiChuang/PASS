using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PASS.Models.UserGroupManagement;
using PASS.UGMS.Dao;
using PASS.Common.Security;

namespace PASS.UGMS.Service
{
    /// <summary>
    /// 使用者管理Service
    /// </summary>
    public class UGMService : IUGMService
    {
        //使用者管理Dao宣告
        private UGMDao _UGMDao = new UGMDao();

        private SecurityService _security = new SecurityService();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="userPW"></param>
        /// <returns></returns>
        public UserProfile VerifyUser(string userID, string userPW)
        {
            var userProfile = _UGMDao.GetUserProfileByID(userID);
            if (!_security.VerifyText(userPW, userProfile.UserPW)) return new UserProfile();
            userProfile.UserPW = string.Empty;
            return userProfile;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userProfile"></param>
        /// <returns></returns>
        public bool CreateOrModifyUserProfile(UserProfile userProfile)
        {
            return _UGMDao.CreateOrModify(userProfile);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public UserProfile GetUserProfile(Int64 userNo)
        {
            return _UGMDao.GetUserProfileByNo(userNo);
        }
    }
}
