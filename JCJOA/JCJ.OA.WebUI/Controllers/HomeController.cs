using JCJ.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JCJ.OA.WebUI.Controllers
{
    public class HomeController : BaseController
    {
        IBLL.IUserInfoService UserInfoService { get; set; }
        public ActionResult Index()
        {
            ViewBag.UserName = LoginUser.UName;
            return View();
        }

        public ActionResult HomePage()
        {
            return View();
        }

        /// <summary>
        /// 过滤登陆用户所有的菜单权限
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMenu()
        {
            //1：根据用户-->角色-->权限这条线获取登陆用户所有的菜单权限，放到一个集合中
            int userId = LoginUser.ID;
            var userInfo = UserInfoService.LoadEntities(u => u.ID == userId).FirstOrDefault();   //获取用户登陆信息
                //获得登陆用户的所有的角色
            var loginUserRole = userInfo.RoleInfo;
            //获得角色对应的菜单权限
            short actionType = (short)ActionTypeEnum.MenuAction;
            var loginUserAction = (from r in loginUserRole
                                   from a in r.ActionInfo
                                   where a.ActionTypeEnum == actionType
                                   select a).ToList();
            //2：更加用户-->权限这条线查找登陆用户具有的菜单权限，放到一个集合中
            //3：将两个集合合并，成一个集合
            //4：对合并后的集合进行过滤，过滤掉那些禁用的权限
            //5：去掉重复的
            //6: 去重后的集合生成JSON
            return Content("ok");


        }

    }
}