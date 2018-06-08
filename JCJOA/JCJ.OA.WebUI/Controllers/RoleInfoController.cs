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
    }
}