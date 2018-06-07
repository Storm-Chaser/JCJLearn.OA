using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JCJ.OA.WebUI.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.UserName = LoginUser.UName;
            return View();
        }

        public ActionResult HomePage()
        {
            return View();
        }

    }
}