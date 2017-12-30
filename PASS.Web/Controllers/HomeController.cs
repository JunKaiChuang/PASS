using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PASS.UGMS.Service;
using Newtonsoft.Json;

namespace PASS.Web.Controllers
{
    public class HomeController : Controller
    {
        UGMService _UGMService = new UGMService();

        // GET: Home
        public ActionResult Index()
        {
            HttpCookie cookie = Request.Cookies["PASS.LoginInfo"];
            cookie.Values.Clear();
            Response.Cookies.Add(cookie);
            return View();
        }

        public JsonResult Login(string id, string password)
        {
            HttpCookie cookie = Request.Cookies["PASS.LoginInfo"];
            if (cookie == null)
            {
                cookie = new HttpCookie("PASS.LoginInfo");
            }
            else
            {
                cookie.Values.Clear();
            }

            var result = _UGMService.VerifyUser(id, password);
            if (result.UserNo != 0)
            {
                cookie.Values.Add("UserNo", result.UserNo.ToString());
            }

            Response.Cookies.Add(cookie);
            return Json(result);
        }
    }
}