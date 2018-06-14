using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JCJ.OA.WebUI.Controllers
{
    public class ArticelClassController : BaseController
    {
        // GET: ArticelClass
        IBLL.IArticelClassService ArticelClassService { get; set; }
        public ActionResult Index()
        {
            var jsond = ShowArticelClass();
            return View();
        }
        /// <summary>
        /// 获取根类别
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowArticelClass() {
            var articeClassList = ArticelClassService.LoadEntities(a => a.ParentId == 0);
            var temp= from a in articeClassList
                      select new { id = a.ID, className = a.ClassName, Remark = a.Remark };
            return Json(temp, JsonRequestBehavior.AllowGet);
        }
    }
}