using JCJ.OA.BLL;
using JCJ.OA.Model;
using JCJ.OA.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JCJ.OA.WebUI.Controllers
{
    public class RoleInfoController : Controller
    {
        IBLL.IRoleInfoService RoleInfoService { get; set; }
        IBLL.IActionInfoService ActionInfoService { get; set; }
        // GET: RoleInfo
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获得用户角色
        /// </summary>
        /// <returns></returns>
        public ActionResult GetRoleInfo()
        {
            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 5;
            int totalCount;
            short delFlag = (short)DelFlagEnum.Normal;
            var roleInfoList = RoleInfoService.LoadPageEntities<int>(pageIndex, pageSize, out totalCount, r => r.DelFlag == delFlag, r => r.ID, true);
            var temp = from r in roleInfoList
                       select new {
                           r.ID,
                           r.RoleName,
                           r.Remark,
                           r.SubTime,
                           r.Sort };
            return Json(new { rows = temp, total = totalCount }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 展示添加页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowAddRole()
        {
            return View();
        }

        /// <summary>
        /// 完成角色信息的保存
        /// </summary>
        /// <returns></returns>
        public ActionResult AddRoleInfo(RoleInfo roleInfo)
        {
            roleInfo.ModifiedOn = DateTime.Now;
            roleInfo.SubTime = DateTime.Now;
            roleInfo.DelFlag = (short)DelFlagEnum.Normal;
            RoleInfoService.AddEntity(roleInfo);
            return Content("ok");
        }
        /// <summary>
        /// 展示角色具有权限信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowRoleAction()
        {
            int roleId = int.Parse(Request["roleId"]);
            var roleInfo = RoleInfoService.LoadEntities(r => r.ID == roleId).FirstOrDefault();
            ViewBag.RoleInfo = roleInfo;
            var actionInfoList = ActionInfoService.LoadEntities(a => a.DelFlag == 0).ToList();//所有的权限
            //角色具有的权限
            var actionIdList = (from a in roleInfo.ActionInfo
                                select a.ID).ToList();//
            ViewBag.ActionInfoList = actionInfoList;
            ViewBag.ActionIdList = actionIdList;
            return View();
        }
        /// <summary>
        /// 完成角色权限的分配
        /// </summary>
        /// <returns></returns>
        public ActionResult SetRoleActionInfo()
        {
            int roleId = int.Parse(Request["roleId"]);
            string[] allKeys = Request.Form.AllKeys;
            List<int> list = new List<int>();
            foreach (string key in allKeys)
            {
                if (key.StartsWith("cba_"))
                {
                    list.Add(Convert.ToInt32(key.Replace("cba_", "")));
                }
            }
            return Content(RoleInfoService.SetRoleActionInfo(roleId, list) ? "ok" : "no");
        }
    }
}