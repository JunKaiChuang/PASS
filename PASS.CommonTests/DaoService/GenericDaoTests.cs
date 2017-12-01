using Microsoft.VisualStudio.TestTools.UnitTesting;
using PASS.Common.DaoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PASS.Models.UserGroupManagement;

namespace PASS.Common.DaoService.Tests
{
    [TestClass()]
    public class GenericDaoTests
    {
        UserProfile _userProfile;

        [TestInitialize]
        public void Init()
        {
            _userProfile = new UserProfile();
            _userProfile.UserID = "0001";
            _userProfile.UserName = "王小明";
            _userProfile.UserNo = 106000001;
            _userProfile.Authorization = string.Empty;
        }

        [TestMethod()]
        public void InsertTest()
        {
            var userProfileDao = new GenericDao<UserProfile>();
            Assert.IsTrue(userProfileDao.Insert(_userProfile));
        }

        [TestMethod()]
        public void DeleteTest()
        {
            var userProfileDao = new GenericDao<UserProfile>();
            Assert.IsTrue(userProfileDao.Delete(_userProfile));
        }

        [TestMethod()]
        public void UpdateTest()
        {
            var userProfileDao = new GenericDao<UserProfile>();
            _userProfile.UserName = "王大明n";
            Assert.IsTrue(userProfileDao.Update(_userProfile));
        }
    }
}