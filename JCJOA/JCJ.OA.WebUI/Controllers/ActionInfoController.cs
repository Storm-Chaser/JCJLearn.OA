using JCJ.OA.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JCJ.OA.WebUI.Controllers
{
    public class ActionInfoController : Controller
    {
        IBLL.IActionInfoService ActionInfoService { get; set; }
        // GET: ActionInfo
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获得权限信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetActionInfo()
        {
            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 5;
            int totalCount;
            short delFlag = (short)DelFlagEnum.Normal;
            var actionInfoList = ActionInfoService.LoadPageEntities<int>(pageIndex, pageSize, out totalCount, a => a.DelFlag == delFlag, a => a.ID, true);
            var temp = from a in actionInfoList
                       select new
                       {
                           a.ID,
                           a.ActionInfoName,
                           a.ActionTypeEnum,
                           a.HttpMethod, 
                           a.Url,
                           a.Remark,
                           a.SubTime,
                           a.Sort
                       };
            return Json(new { rows = temp, total = totalCount }, JsonRequestBehavior.AllowGet);
        }
    }
}