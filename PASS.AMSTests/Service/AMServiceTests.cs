using Microsoft.VisualStudio.TestTools.UnitTesting;
using PASS.AMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PASS.Models.AssignmentManagement;
using System.IO;
using PASS.Common.DaoService;

namespace PASS.AMS.Service.Tests
{
    [TestClass()]
    public class AMServiceTests
    {
        Assignment _assignment;
        SubmissionDetail _subDetail;

        [TestInitialize]
        public void Init()
        {
            _assignment = new Assignment() { CourseNo = 1, AssignOrder = 1, AssignmentTitle = "單元測試", StartDate = new DateTime(2017, 12, 15), EndDate = new DateTime(2017, 12, 16) };
            _subDetail = new SubmissionDetail() { UserNo = 1, AssignmentNo = 2, FileName = "單元測試檔案"};
        }

        [TestMethod()]
        public void SubmitWorkTest()
        {
            AMService _AMService = new AMService();
            using (MemoryStream ms = new MemoryStream())
            using (FileStream file = new FileStream(@"E:\DB\單元測試檔案.txt", FileMode.Open, FileAccess.Read))
            {
                byte[] bytes = new byte[file.Length];
                file.Read(bytes, 0, (int)file.Length);
                ms.Write(bytes, 0, (int)file.Length);

                Assert.IsTrue(_AMService.SubmitWork(_subDetail, ms));
            }
        }

        [TestMethod()]
        public void CreateOrModifyAssignmentTest()
        {
            AMService _AMService = new AMService();
            Assert.IsTrue(_AMService.CreateOrModifyAssignment(_assignment));
        }


        [TestMethod()]
        public void GetFile()
        {
            var _fileDao = new FileDao();
            using (var ms = _fileDao.GetFile(1))
            using (FileStream file = new FileStream(@"E:\DB\測試檔案輸出", FileMode.Create, FileAccess.Write))
            {
                var temp = ms.ToArray();
                file.Write(temp, 0, temp.Length);
            }
            Assert.IsTrue(true);
        }
    }
}