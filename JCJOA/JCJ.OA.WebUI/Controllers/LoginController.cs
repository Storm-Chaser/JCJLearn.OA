﻿using JCJ.OA.Model;
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
            //if (Session["userInfo"] == null)
            //{
            //    //filterContext.HttpContext.Response.Redirect("/Login/Index");
            //    if (Request.Cookies["cp1"] != null)
            //    {
            //        string userName = Request.Cookies["cp1"].Value;  //获得cookies中存的
            //        UserInfo userInfo = UserInfoService.LoadEntities(u => u.UName == userName).FirstOrDefault();
            //        if (Common.WebCommon.ValidateCookieInfo(userInfo))
            //        {
            //            return RedirectToAction("Index", "Home");
            //        }
            //    }
            //}
            //else
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            if (Request.Cookies["sessionId"] == null)
            {
                if (Request.Cookies["cp1"] != null)
                {
                    string userName = Request.Cookies["cp1"].Value;  //获得cookies中存的
                    UserInfo userInfo = UserInfoService.LoadEntities(u => u.UName == userName).FirstOrDefault();
                    if (Common.WebCommon.ValidateCookieInfo(userInfo))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                string sessionId = Request.Cookies["sessionId"].Value;
                object obj = Common.MemcacheHelper.Get(sessionId);    //获取memcache中的数据   
                if (obj != null)
                {
                    UserInfo userInfo = Common.SerializeHelper.DeserializeToObject<UserInfo>(obj.ToString());   //反序列化
                    //模拟滑动过期时间
                    Common.MemcacheHelper.Set(sessionId, obj, DateTime.Now.AddMinutes(20));
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View();
                }
                
            }
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
                string uPwd = Common.WebCommon.GetMd5String(Common.WebCommon.GetMd5String(userPwd));
                var userInfo = UserInfoService.LoadEntities(u => u.UName == userName && u.UPwd == uPwd).FirstOrDefault();   //校验用户名密码
                if (userInfo != null)
                {
                    //Session["userInfo"] = userInfo;
                    string sessionId = Guid.NewGuid().ToString();
                    Common.MemcacheHelper.Set(sessionId, Common.SerializeHelper.SerializeToString(userInfo), DateTime.Now.AddMinutes(20));  //Memcache中添加登陆用户数据
                    Response.Cookies["sessionId"].Value = sessionId;  //将自创的Session赋值给cookies返回给浏览器

                    //判断复选框是否被选中
                    if(!string.IsNullOrEmpty(Request["autoLogin"]))
                    {
                        HttpCookie cookie1 = new HttpCookie("cp1", userName);
                        HttpCookie cookie2 = new HttpCookie("cp2", Common.WebCommon.GetMd5String(Common.WebCommon.GetMd5String(userPwd)));
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