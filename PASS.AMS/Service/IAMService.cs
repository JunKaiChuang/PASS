using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PASS.Models.AssignmentManagement;
using System.IO;

namespace PASS.AMS.Service
{
    /// <summary>
    /// 作業管理Service Interface
    /// </summary>
    public interface IAMService
    {
        /// <summary>
        /// 作業題目管理
        /// </summary>
        /// <param name="assignment"></param>
        /// <returns></returns>
        Boolean CreateOrModifyAssignment(Assignment assignment);

        /// <summary>
        /// 依照課號取得作業題目清單
        /// </summary>
        /// <param name="courseNo"></param>
        /// <returns></returns>
        List<Assignment> GetAssignmentList(Int64 courseNo);

        /// <summary>
        /// 學生作業上傳
        /// </summary>
        /// <param name="userNo"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        Boolean SubmitWork(Int64 userNo, Stream file);

        /// <summary>
        /// 取得該學生於指定課號的已上傳作業清單
        /// </summary>
        /// <param name="userNo"></param>
        /// <param name="curseNo"></param>
        /// <returns></returns>
        List<ViewSubmission> GetSubmissionList(Int64 userNo, Int64 curseNo);

        /// <summary>
        /// 取得已上傳的作業資訊
        /// </summary>
        /// <param name="submissionNo"></param>
        /// <returns></returns>
        SubmissionDetail GetSubmissionDetail(Int64 submissionNo);
    }
}
