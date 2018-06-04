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
    public class UserInfoController : Controller
    {
        // GET: UserInfo /Spring.Net
        IBLL.IUserInfoService UserInfoService { get; set; }
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
               
    }
}