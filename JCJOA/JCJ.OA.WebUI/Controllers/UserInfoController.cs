using JCJ.OA.Model;
using JCJ.OA.Model.Enum;
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
            short delFlag = (short)DelFlagEnum.Normal;
            var userList = UserInfoService.LoadPageEntities<int>(pageIndex, pageSize, out int totalCount, u => u.DelFlag == delFlag, u => u.ID, true);
            var temp = from u in userList
                       select new { u.ID, UserName = u.UName, UserPwd = u.UPwd, u.SubTime,  u.Sort };
            return Json(new { rows = temp, total = totalCount }, JsonRequestBehavior.AllowGet);
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
               
    }
}