using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PASS.UGMS.Service;
using PASS.AMS.Service;
using PASS.Models.AssignmentManagement;
using System.IO;

namespace PASS.Web.Controllers
{
    public class StudentController : Controller
    {
        AMService _AMService = new AMService();

        // GET: Submission
        public ActionResult Submission()
        {            
            return View();
        }

        public JsonResult GetSubmissionList()
        {
            HttpCookie cookie = Request.Cookies["PASS.LoginInfo"];

            var userNo = Convert.ToInt64(cookie["UserNo"]);
            var courseNo = Convert.ToInt64(cookie["CourseNo"]);
            var result = _AMService.GetSubmissionList(userNo, courseNo);

            return Json(result);
        }

        public JsonResult GetAssignmentInfo(Int64 assignmentNo)
        {
            var result = _AMService.GetAssignment(assignmentNo);

            return Json(result);
        }

        public JsonResult UploadFile(Int64 assignmentNo, HttpPostedFileBase file)
        {
            HttpCookie cookie = Request.Cookies["PASS.LoginInfo"];

            var userNo = Convert.ToInt64(cookie["UserNo"]);

            var viewSub = _AMService.GetViewSubmissionByUserNoAndAssignmentNo(userNo, assignmentNo);
            var sub = new SubmissionDetail() {UserNo = userNo, AssignmentNo = assignmentNo, FileName = file.FileName };

            using (Stream inputStream = file.InputStream)
            {
                MemoryStream memoryStream = inputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }
                _AMService.SubmitWork(sub, memoryStream, viewSub.IsUploaded);
            }
            return Json(true);
        }
    }
}