using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JCJ.OA.WebUI.Controllers
{
    public class LoginController : Controller
    {
        IBLL.IUserInfoService UserInfoService { get; set; }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCodeImage()
        {
            string vcode = Common.ValidateCode.CreateValidateCode(4);
            Session["vcode"] = vcode;
            byte[] buffer = Common.ValidateCode.CreateValidateGraphic(vcode);
            return File(buffer, "image/jpeg");
        }

        /// <summary>
        /// 完成用户登陆
        /// </summary>
        /// <returns></returns>
        public ActionResult UserLogin()
        {
            //校验验证码
            string validateCode = Session["vcode"] != null ? Session["vcode"].ToString() : string.Empty;
            if(string.IsNullOrEmpty(validateCode))
            {
                return Content("no:验证码错误!!");
            }
            Session["vcode"] = null;
            string userCode = Request["vCode"];
            if (!validateCode.Equals(userCode, StringComparison.InvariantCultureIgnoreCase))
            {
                return Content("no:验证码错误!!");
            }
            string userName = Request["LoginCode"];
            string userPwd = Request["LoginPwd"];
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(userPwd))
            {
                var userInfo = UserInfoService.LoadEntities(u => u.UName == userName && u.UPwd == userPwd).FirstOrDefault();   //校验用户名密码
                if (userInfo != null)
                {
                    Session["userInfo"] = userInfo;
                    //判断复选框是否被选中
                    if(!string.IsNullOrEmpty(Request["autoLogin"]))
                    {
                        HttpCookie cookie1 = new HttpCookie("cp1", userName);
                        HttpCookie cookie2 = new HttpCookie("cp2", userPwd);
                        cookie1.Expires = DateTime.Now.AddDays(7);
                        cookie2.Expires = DateTime.Now.AddDays(7);
                        Response.Cookies.Add(cookie1);
                        Response.Cookies.Add(cookie2);
                    }
                    return Content("ok:登陆成功");
                }
                else {
                    return Content("no:用户名密码错误");
                }
            }
            else {
                return Content("no:用户名密码不能为空!!");
            }
        }
    }
}