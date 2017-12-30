using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PASS.Models.UserGroupManagement;
using PASS.Models.Enum;
using PASS.UGMS.Service;
using PASS.AMS.Service;

namespace PASS.Web.Controllers
{
    public class CourseController : Controller
    {
        UGMService _UGMService = new UGMService();
        AMService _AMService = new AMService();

        // GET: Course
        public ActionResult CourseSelection()
        {
            HttpCookie cookie = Request.Cookies["PASS.LoginInfo"];
            
            var userNo = Convert.ToInt64(cookie.Values["UserNo"]);
            var userInfo = _UGMService.GetUserInfo(userNo);

            cookie.Values.Clear();
            cookie.Values.Add("UserNo", userInfo.UserNo.ToString());
            cookie.Values.Add("UserID", userInfo.UserID);
            cookie.Values.Add("UserType", ((int)userInfo.UserType).ToString());

            Response.Cookies.Add(cookie);
            return View();
        }

        public JsonResult GetCourseList()
        {
            HttpCookie cookie = Request.Cookies["PASS.LoginInfo"];

            var userNo = Convert.ToInt64(cookie.Values["UserNo"]);

            var result = _AMService.GetCourseInfoByUserNo(userNo);

            return Json(result);
        }

        public JsonResult SelectCourse(Int64 courseNo)
        {
            HttpCookie cookie = Request.Cookies["PASS.LoginInfo"];

            var courseInfo = _AMService.GetCourseInfoByCourseNo(courseNo);
            var userType = (UserType)Convert.ToInt64(cookie["UserType"]);

            if (cookie["CourseNo"] == null)
            {
                cookie.Values.Add("CourseNo", courseNo.ToString());
                cookie.Values.Add("CourseName", HttpUtility.UrlEncode(courseInfo.CourseName));
            }
            else
            {
                cookie["CourseNo"] = courseNo.ToString();
                cookie["CourseName"] = HttpUtility.UrlEncode(courseInfo.CourseName);
            }


            Response.Cookies.Add(cookie);

            var dest = "";

            switch (userType)
            {
                case UserType.學生:
                    dest = "../Student/Submission";
                    break;
                default:
                    dest = "../Manager/AssignmentList";
                    break;
            }

            return Json(dest);
        }
    }
}