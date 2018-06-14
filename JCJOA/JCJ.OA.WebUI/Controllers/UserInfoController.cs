using JCJ.OA.Model;
using JCJ.OA.Model.Enum;
using JCJ.OA.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JCJ.OA.WebUI.Controllers
{
    public class UserInfoController : BaseController
    {
        // GET: UserInfo /Spring.Net
        IBLL.IUserInfoService UserInfoService { get; set; }
        IBLL.IRoleInfoService RoleInfoService { get; set; }
        IBLL.IActionInfoService ActionInfoService { get; set; }
        IBLL.IR_UserInfo_ActionInfoService R_UserInfo_ActionInfoService { get; set; }

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 加载用户收据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetUserInfo()
        {
            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 5;
            //接受的搜索条件
            string uName = Request["name"];
            string uRemark = Request["remark"];
            int totalCount = 0;
            //构建搜索的条件
            UserInfoSearch userInfoSearch = new UserInfoSearch() {
                UName = uName,
                URemark = uRemark,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount
            };

            short delFlag = (short)DelFlagEnum.Normal;
            var userInfoList = UserInfoService.LoadSearchEntities(userInfoSearch);  //搜索
            //var userList = UserInfoService.LoadPageEntities<int>(pageIndex, pageSize, out totalCount, u => u.DelFlag == delFlag, u => u.ID, true);
            var temp = from u in userInfoList
                       select new { u.ID, UserName = u.UName, UserPwd = u.UPwd, u.SubTime, u.Remark };
            return Json(new { rows = temp, total = userInfoSearch.TotalCount }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 删除用户数据
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteUser()
        {
            string strId = Request["strId"];
            string[] strIds = strId.Split(',');
            List<int> list = new List<int>();
            foreach (string id in strIds)
            {
                list.Add(Convert.ToInt32(id));
            }
            return Content(UserInfoService.DeleteEntities(list) ? "ok" : "no");
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        public ActionResult AddUser(UserInfo userInfo)
        {
            short delFlag = (short)DelFlagEnum.Normal;
            userInfo.DelFlag = delFlag;
            userInfo.ModifiedOn = DateTime.Now;
            userInfo.SubTime = DateTime.Now;
            UserInfoService.AddEntity(userInfo);
            return Content("ok");
        }

        /// <summary>
        /// 展示要修改的数据
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowEdit()
        {
            int id = int.Parse(Request["id"]);
            var userInfo = UserInfoService.LoadEntities(u => u.ID == id).FirstOrDefault();
            if (userInfo != null)
            {
                return Json(new { Msg="ok",UserInfo=userInfo},JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Mag = "no" });
            }
        }

        /// <summary>
        /// 完成用户更新
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public ActionResult EditUser(UserInfo userInfo)
        {
            userInfo.ModifiedOn = DateTime.Now;
            return Content(UserInfoService.EditEntity(userInfo) ? "ok" : "no");
        }

        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <returns></returns>
        public ActionResult SetUserRoleInfo()
        {
            //接受userid
            int userId = int.Parse(Request["uid"]);
            var userInfo = UserInfoService.LoadEntities(u => u.ID == userId).FirstOrDefault();  //获得要分配角色的用户
            //要分配角色的用户以前具有哪些角色
            var userRoleIdList = (from r in userInfo.RoleInfo
                                  select r.ID).ToList();
            short delFlag = (short)DelFlagEnum.Normal;
            //获得所有的角色
            var roleInfoList = RoleInfoService.LoadEntities(r => r.DelFlag == delFlag).ToList();
            ViewBag.UserRoleIdList = userRoleIdList;
            ViewBag.RoleInfoList = roleInfoList;
            ViewBag.UserInfo = userInfo;
            return View();
        }
        /// <summary>
        /// 保存设置用户的角色
        /// </summary>
        /// <returns></returns>
        public ActionResult SetUserRole()
        {
            string[] roleName = Request.Form.AllKeys; //获取所有表单的name属性的值
            int userId = Convert.ToInt32(Request["userId"]); //给该用户分配角色
            List<int> list = new List<int>();
            foreach (string name in roleName)
            {
                if (name.StartsWith("cba_"))
                {
                    string id = name.Replace("cba_","");
                    list.Add(Convert.ToInt32(id));
                }
            }
            var userInfo = UserInfoService.LoadEntities(u => u.ID == userId).FirstOrDefault();  //获取要分配角色的用户
            //要分角色的用户以前具有哪些角色
            var userRoleIdList = (from r in userInfo.RoleInfo
                                  select r.ID).ToList();
            var newList = list.Union(userRoleIdList);  //差集运算
            if (newList.Count() == list.Count()&&newList.Count()==userRoleIdList.Count())
            {
                return Content("ok");
            }
            else {
                return UserInfoService.SetUserRoleInfo(userId, list) ? Content("ok") : Content("no");
            }
            
        }
        /// <summary>
        /// 展示用户对应的权限
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowUserAction() {
            int userId = int.Parse(Request["userId"]);
            var userInfo = UserInfoService.LoadEntities(u => u.ID == userId).FirstOrDefault();
            var actionInfoList = ActionInfoService.LoadEntities(a => a.DelFlag == 0).ToList();
            var actionIdList = (from a in userInfo.R_UserInfo_ActionInfo
                                select a).ToList();
            ViewBag.UserInfo = userInfo;
            ViewBag.ActionInfoList = actionInfoList;
            ViewBag.ActionIdList = actionIdList;
            return View();
        }
        /// <summary>
        /// 完成用户权限的分配
        /// </summary>
        /// <returns></returns>
        public ActionResult SetUserAction() {
            int userId = int.Parse(Request["userId"]);
            int actionId = int.Parse(Request["actionId"]);
            bool isPass = Request["isPass"] == "true" ? true : false;
            return Content(UserInfoService.SetUserActionInfo(userId, actionId, isPass) ? "ok" : "no");
            
        }
        /// <summary>
        /// 删除用户权限
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteUserAction() {
            int userId = int.Parse(Request["userId"]);
            int actionId = int.Parse(Request["actionId"]);
            var r_UserInfo_ActionInfo = R_UserInfo_ActionInfoService.LoadEntities(a => a.UserInfoID == userId && a.ActionInfoID == actionId).FirstOrDefault();
            if (r_UserInfo_ActionInfo != null) {
                return Content(R_UserInfo_ActionInfoService.DeleteEntity(r_UserInfo_ActionInfo) ? "ok" : "no");
            }
            return Content("no");  
        }
    }
}