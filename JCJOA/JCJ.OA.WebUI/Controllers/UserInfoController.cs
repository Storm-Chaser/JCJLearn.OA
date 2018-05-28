using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JCJ.OA.WebUI.Controllers
{
    public class UserInfoController : Controller
    {
        // GET: UserInfo
        IBLL.IUserInfoService UserInfoService = new BLL.UserInfoService();
        public ActionResult Index()
        {
            var userInfoList = UserInfoService.LoadEntities(u => true);
            ViewData.Model = userInfoList;
            return View();
        }

    }
}