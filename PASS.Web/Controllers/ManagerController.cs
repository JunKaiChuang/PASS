using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PASS.Models.AssignmentManagement;
using PASS.AMS.Service;
using PASS.UGMS.Service;

namespace PASS.Web.Controllers
{
    public class ManagerController : Controller
    {
        AMService _AMService = new AMService();
        UGMService _UGMService = new UGMService();

        // GET: Manager
        public ActionResult AssignmentList()
        {
            return View();
        }

        public JsonResult GetAssignmentList()
        {
            HttpCookie cookie = Request.Cookies["PASS.LoginInfo"];

            var courseNo = Convert.ToInt64(cookie["CourseNo"]);
            var result = _AMService.GetAssignmentList(courseNo);
            return Json(result);
        }

        public JsonResult GetNewAssignment()
        {
            HttpCookie cookie = Request.Cookies["PASS.LoginInfo"];

            var courseNo = Convert.ToInt64(cookie["CourseNo"]);
            var result = new Assignment() { EndDate = DateTime.Now};
            var assignOrder = _AMService.GetAssignmentList(courseNo).Count + 1;

            result.CourseNo = courseNo;
            result.AssignOrder = assignOrder;
            return Json(result);
        }

        public JsonResult GetAssignment(Int64 assignmentNo)
        {
            var result = _AMService.GetAssignment(assignmentNo);
            return Json(result);
        }

        public JsonResult CreateOrModifyAssignment(Assignment assignment)
        {
            _AMService.CreateOrModifyAssignment(assignment);
            return Json(true);
        }

        public JsonResult DeleteAssignment(Assignment assignment)
        {
            return Json(_AMService.DeleteAssignment(assignment));
        }

        public JsonResult GetScoreListByAssignmentNo(Int64 assignmentNo)
        {
            return Json(_AMService.GetViewAssignmentScore(assignmentNo));
        }

        public FileResult Download(Int64 fileNo, string assignmentNo)
        {
            var file = _AMService.GetFile(fileNo, assignmentNo);

            var assimentName = _AMService.GetAssignment(Convert.ToInt64(assignmentNo)).AssignmentTitle;
            var fileName = _AMService.GetFileName(fileNo);



            return File(file, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public JsonResult LogScore(AssignmentScore[] assignmentScores)
        {
            _AMService.UpdateScore(assignmentScores.ToList<AssignmentScore>());
            return Json(true);
        }
    }
}