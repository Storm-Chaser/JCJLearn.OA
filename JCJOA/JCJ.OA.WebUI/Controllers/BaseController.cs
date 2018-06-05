using JCJ.OA.IBLL;
using JCJ.OA.Model;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JCJ.OA.WebUI.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (Session["userInfo"] == null)
            {
                //filterContext.HttpContext.Response.Redirect("/Login/Index");
                if (Request.Cookies["cp1"] != null)
                {
                    string userName = Request.Cookies["cp1"].Value;  //获得cookies中存的用户名
                    //判断用户名是不是正确
                    IApplicationContext ctx = ContextRegistry.GetContext();
                    IUserInfoService userInfoService = (IUserInfoService)ctx.GetObject("UserInfoService");
                    UserInfo userInfo = userInfoService.LoadEntities(u => u.UName == userName).FirstOrDefault();
                    if (!Common.WebCommon.ValidateCookieInfo(userInfo))
                    {
                        filterContext.Result = Redirect(Url.Action("Index", "Login"));
                    }
                }
                else
                {
                    filterContext.Result = Redirect(Url.Action("Index", "Login"));
                }
                
            }
        }
    }
}