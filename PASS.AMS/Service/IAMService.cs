using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PASS.Models.AssignmentManagement;
using PASS.Models.Course;
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
        /// 依照課號取得作業題目清單(教授/助教)
        /// </summary>
        /// <param name="courseNo"></param>
        /// <returns></returns>
        List<Assignment> GetAssignmentList(Int64 courseNo);

        /// <summary>
        /// 取得作業資訊
        /// </summary>
        /// <param name="assignmentNo"></param>
        /// <returns></returns>
        Assignment GetAssignment(Int64 assignmentNo);

        /// <summary>
        /// 學生作業上傳
        /// </summary>
        /// <param name="userNo"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        Boolean SubmitWork(SubmissionDetail subDetail, MemoryStream file, bool isUploaded);

        /// <summary>
        /// 取得該學生於指定課號的作業清單
        /// </summary>
        /// <param name="userNo"></param>
        /// <param name="curseNo"></param>
        /// <returns></returns>
        List<ViewSubmission> GetSubmissionList(Int64 userNo, Int64 courseNo);

        /// <summary>
        /// 依照UserNo及AssignmentNo，取得當該作業繳交狀態
        /// </summary>
        /// <param name="userNo"></param>
        /// <param name="assignmentNo"></param>
        /// <returns></returns>
        ViewSubmission GetViewSubmissionByUserNoAndAssignmentNo(Int64 userNo, Int64 assignmentNo);

        /// <summary>
        /// 取得已上傳的作業資訊
        /// </summary>
        /// <param name="submissionNo"></param>
        /// <returns></returns>
        SubmissionDetail GetSubmissionDetail(Int64 submissionNo);

        /// <summary>
        /// 依照UserNo取得課程清單
        /// </summary>
        /// <param name="userNo"></param>
        /// <returns></returns>
        List<CourseInfo> GetCourseInfoByUserNo(Int64 userNo);

        /// <summary>
        /// 依照CourseNo取的課程資訊
        /// </summary>
        /// <param name="courseNo"></param>
        /// <returns></returns>
        CourseInfo GetCourseInfoByCourseNo(Int64 courseNo);
    }
}
