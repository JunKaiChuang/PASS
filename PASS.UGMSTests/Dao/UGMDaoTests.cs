using Microsoft.VisualStudio.TestTools.UnitTesting;
using PASS.UGMS.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PASS.Models.UserGroupManagement;
using PASS.Common.Security;

namespace PASS.UGMS.Dao.Tests
{
    [TestClass()]
    public class UGMDaoTests
    {
        UserProfile _userProfile;
        SecurityService _security;

        [TestInitialize]
        public void Init()
        {
            _security = new SecurityService();
            _userProfile = new UserProfile();
            _userProfile.UserID = "0001";
            _userProfile.UserName = "王小明";
            _userProfile.UserNo = 106000001;
            _userProfile.UserPW = "p@ssw0rd";
            _userProfile.Authorization = string.Empty;
        }

        [TestMethod()]
        public void GetUserProfileByNoTest()
        {
            UGMDao ugmDao = new UGMDao();
            var temp = ugmDao.GetUserProfileByNo(1);
            Assert.IsTrue(_security.VerifyText(_userProfile.UserPW, temp.UserPW));
        }
    }
}