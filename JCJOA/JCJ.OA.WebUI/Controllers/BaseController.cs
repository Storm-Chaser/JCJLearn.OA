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
        public UserInfo LoginUser { get; set; }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            IApplicationContext ctx = ContextRegistry.GetContext();//读取sping.net配置信息，创建容器。
            IUserInfoService userInfoService = (IUserInfoService)ctx.GetObject("UserInfoService");
            //if (Session["userInfo"] == null)
            if (Request.Cookies["sessionId"] == null)
            {
                //filterContext.HttpContext.Response.Redirect("/Login/Index");
                if (Request.Cookies["cp1"] != null)
                {
                    string userName = Request.Cookies["cp1"].Value;  //获得cookies中存的用户名
                    //判断用户名是不是正确
                    
                    UserInfo userInfo = userInfoService.LoadEntities(u => u.UName == userName).FirstOrDefault();
                    if (!Common.WebCommon.ValidateCookieInfo(userInfo))
                    {
                        filterContext.Result = Redirect(Url.Action("Index", "Login"));
                        return;
                    }
                    LoginUser = userInfo;
                }
                else
                {
                    filterContext.Result = Redirect(Url.Action("Index", "Login"));
                    return;
                }
            }
            else        //如果有值就取出来
            {
                string sessionId = Request.Cookies["sessionId"].Value;
                object obj = Common.MemcacheHelper.Get(sessionId);    //获取memcache中的数据
                if (obj != null)
                {
                    UserInfo userInfo = Common.SerializeHelper.DeserializeToObject<UserInfo>(obj.ToString());   //反序列化
                    LoginUser = userInfo;
                    //模拟滑动过期时间
                    Common.MemcacheHelper.Set(sessionId, obj, DateTime.Now.AddMinutes(20));
                }
                else {
                    filterContext.Result = Redirect(Url.Action("Index", "Login"));
                    return;
                }
            }
            //过滤非菜单权限
            if (LoginUser != null)
            {
                //string url1 = Request.Url.AbsolutePath.ToString().ToLower();  //获取当前请求的URL地址
                //留后门，发布一定要删除
                if (LoginUser.UName == "itcast")
                {
                    
                    return;
                }
                string url = Request.Url.AbsolutePath.ToString().ToLower();  //获取当前请求的URL地址
                string httpMethod = Request.HttpMethod;//获取请求的方式
                //查找url地址对应的权限信息
                IActionInfoService actionInfoService = (IActionInfoService)ctx.GetObject("ActionInfoService");
                var actionInfo = actionInfoService.LoadEntities(a => a.Url == url && a.HttpMethod == httpMethod).FirstOrDefault();
                if (actionInfo == null)
                {
                    filterContext.Result = Redirect("/Error.html");
                    return;
                }
                //判断登录用户是否有actionInfo的访问权限。
                //也是按照两条线进行过滤。
                //1先按照用户-->权限这条进行过滤.
                var userInfo = userInfoService.LoadEntities(u => u.ID == LoginUser.ID).FirstOrDefault(); //获取登陆用户信息
                var userAction = (from a in userInfo.R_UserInfo_ActionInfo
                                  where a.ActionInfoID == actionInfo.ID
                                  select a).FirstOrDefault();
                if (userAction != null)//如果成立，表示登录用户有userInfo这个权限，但是考虑是否 被禁止。
                {
                    if (userAction.IsPass)//表示允许，后面就不要校验了，直接访问用户请求的Url地址。
                    {
                        return;
                    }
                    else
                    {
                        filterContext.Result = Redirect("/Error.html");
                        return;
                    }
                }

                //2:按照用户-->角色--->权限进行校验.
                var loginUserRoles = userInfo.RoleInfo;
                var loginUserAction = (from r in loginUserRoles
                                       from a in r.ActionInfo
                                       where a.ID == actionInfo.ID
                                       select a).Count();
                if (loginUserAction < 1)
                {
                    filterContext.Result = Redirect("/Error.html");
                    return;
                }
            }
        }
    }
}