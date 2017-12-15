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
        private GenericDao<SubmissionDetail> _SubDao = new GenericDao<SubmissionDetail>();

        private SecurityService _security = new SecurityService();

        public bool CreateOrModifyAssignment(Assignment assignment)
        {
            return _AMDao.CreateOrModify(assignment);
        }

        public List<Assignment> GetAssignmentList(long courseNo)
        {
            throw new NotImplementedException();
        }

        public SubmissionDetail GetSubmissionDetail(long submissionNo)
        {
            throw new NotImplementedException();
        }

        public List<ViewSubmission> GetSubmissionList(long userNo, long curseNo)
        {
            throw new NotImplementedException();
        }

        public bool SubmitWork(SubmissionDetail subDetail, MemoryStream file)
        {
            try
            {
                _SubDao.Insert(subDetail);

                if (subDetail.FileNo != 0)
                {
                    _FileDao.Delete(subDetail.FileNo);
                }                

                var fileNo = _FileDao.Insert(file);

                subDetail.FileNo = fileNo;

                _SubDao.Update(subDetail);
            }
            catch(Exception e)
            {
                throw e;
            }
            return true;
        }
    }
}
