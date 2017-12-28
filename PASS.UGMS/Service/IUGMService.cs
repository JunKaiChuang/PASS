using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PASS.Models.UserGroupManagement;

namespace PASS.UGMS.Service
{
    /// <summary>
    /// 使用者管理Service Interface
    /// </summary>
    public interface IUGMService
    {
        /// <summary>
        /// 使用者資訊驗證
        /// </summary>
        /// <param name="UserID">使用者帳號</param>
        /// <param name="UserPW">使用者密碼</param>
        /// <returns></returns>
        UserProfile VerifyUser(string userID, string userPW);

        /// <summary>
        /// 使用者資料CUD
        /// </summary>
        /// <param name="userProfile"></param>
        /// <returns></returns>
        Boolean CreateOrModifyUserProfile(UserProfile userProfile);

        /// <summary>
        /// 取得使用者資料
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        UserProfile GetUserProfile(Int64 userID);

        /// <summary>
        /// 取得使用者資訊
        /// </summary>
        /// <param name="userNo"></param>
        /// <returns></returns>
        UserInfo GetUserInfo(Int64 userNo);
    }
}
