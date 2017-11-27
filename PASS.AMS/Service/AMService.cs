using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PASS.Models.AssignmentManagement;

namespace PASS.AMS.Service
{
    /// <summary>
    /// 作業管理Service
    /// </summary>
    public class AMService : IAMService
    {
        public bool CreateOrModifyAssignment(Assignment assignment)
        {
            throw new NotImplementedException();
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

        public bool SubmitWork(long userNo, Stream file)
        {
            throw new NotImplementedException();
        }
    }
}
