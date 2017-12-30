using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PASS.Models.AssignmentManagement;
using PASS.Common.DaoService;
using PASS.AMS.Dao;
using PASS.Common.Security;
using PASS.Common.Common;
using PASS.Models.Course;

namespace PASS.AMS.Service
{
    /// <summary>
    /// 作業管理Service
    /// </summary>
    public class AMService : IAMService
    {
        //作業管理Dao宣告
        private AMDao _AMDao = new AMDao();
        private FileDao _FileDao = new FileDao();
        private CourseDao _CourseDao = new CourseDao();
        private ViewSubmissionDao _ViewSubDao = new ViewSubmissionDao();
        private GenericDao<SubmissionDetail> _SubDao = new GenericDao<SubmissionDetail>();
        private GenericDao<AssignmentScore> _ScoreDao = new GenericDao<AssignmentScore>();
        private ViewAssignmentScoreDao _ViewAssignmentScoreDao = new ViewAssignmentScoreDao();

        private SecurityService _security = new SecurityService();
        private CommonService _commonService = new CommonService();
              
        public bool CreateOrModifyAssignment(Assignment assignment)
        {
            return _AMDao.CreateOrModify(assignment);
        }

        public List<Assignment> GetAssignmentList(long courseNo)
        {
            return _AMDao.GetAssignmentListByCourseNo(courseNo);
        }

        public SubmissionDetail GetSubmissionDetail(long submissionNo)
        {
            throw new NotImplementedException();
        }

        public List<ViewSubmission> GetSubmissionList(long userNo, long courseNo)
        {
            return _ViewSubDao.GetViewSubmissionListByUserNoAndCourseNo(userNo, courseNo);
        }

        public bool SubmitWork(SubmissionDetail subDetail, MemoryStream file, bool isUploaded)
        {
            try
            {
                if(!isUploaded)_SubDao.Insert(subDetail);

                if (isUploaded)
                {
                    _FileDao.Delete(subDetail.UserNo, subDetail.AssignmentNo);
                }

                var userSecurity = new SecurityService(subDetail.UserNo.ToString(), subDetail.AssignmentNo.ToString());
                var byteFile = _commonService.StreamToByte(file);
                var encryptFile = userSecurity.EncryptFile(byteFile);
                var fileNo = _FileDao.Insert(encryptFile);

                subDetail.FileNo = fileNo;

                _SubDao.Update(subDetail);
            }
            catch(Exception e)
            {
                throw e;
            }
            return true;
        }

        public List<CourseInfo> GetCourseInfoByUserNo(long userNo)
        {
            return _CourseDao.GetCourseListByUserNo(userNo);
        }

        public CourseInfo GetCourseInfoByCourseNo(long courseNo)
        {
            return _CourseDao.GetCourseInfoByCourseNo(courseNo);
        }

        public Assignment GetAssignment(long assignmentNo)
        {
            return _AMDao.GetAssignmentByNo(assignmentNo);
        }

        public ViewSubmission GetViewSubmissionByUserNoAndAssignmentNo(long userNo, long assignmentNo)
        {
            return _ViewSubDao.GetViewSubmissionByUserNoAndAssignmentNo(userNo, assignmentNo);
        }

        public bool DeleteAssignment(Assignment assignment)
        {
            return _AMDao.Delete(assignment);
        }

        public List<ViewAssignmentScore> GetViewAssignmentScore(long assignmentNo)
        {
            return _ViewAssignmentScoreDao.GetViewAssignmentScore(assignmentNo);
        }

        public bool UpdateScore(List<AssignmentScore> scoreList)
        {
            foreach (var score in scoreList)
            {
                _ScoreDao.Delete(score);
                _ScoreDao.Insert(score);
            }

            return true;
        }

        public byte[] GetFile(long fileNo, string assignmentNo)
        {
            var userNo = GetUserNoByFileNo(fileNo).ToString();
            var security = new SecurityService(userNo, assignmentNo);
            var dbFile = _FileDao.GetFile(fileNo);

            return security.DecryptFile(dbFile);
        }

        private Int64 GetUserNoByFileNo(Int64 fileNo)
        {
            return _AMDao.GetUserNoByFileNo(fileNo);
        }

        public string GetFileName(long fileNo)
        {
            return _FileDao.GetFileName(fileNo);
        }
    }
}
