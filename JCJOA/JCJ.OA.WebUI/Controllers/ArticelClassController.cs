using JCJ.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        /// <summary>
        /// 获取子类别
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowChildClass() {
            int id = int.Parse(Request["id"]);
            var articelClassList = ArticelClassService.LoadEntities(a => a.ParentId == id);
            var temp = from a in articelClassList
                       select new { id = a.ID, className = a.ClassName, Remark = a.Remark };
            return Json(temp, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 展示创建类别
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowAddClass() {
            int id = int.Parse(Request["cId"]);
            var articelParentClassList = ArticelClassService.LoadEntities(a => a.ParentId == 0);
            StringBuilder sb = new StringBuilder();
            foreach (var articelParentClass in articelParentClassList)
            {
                if (articelParentClass.ID == id)
                {
                    sb.Append("<option selected='selected' value='" + articelParentClass.ID + "'>" + "--" + articelParentClass.ClassName + "</option>");
                }
                else
                {
                    sb.Append("<option value='" + articelParentClass.ID + "'>" + "--" + articelParentClass.ClassName + "</option>");
                }
                var articelChildClassList = ArticelClassService.LoadEntities(a => a.ParentId == articelParentClass.ID);
                foreach (var childClass in articelChildClassList)
                {
                    if (childClass.ID == id)
                    {
                        sb.Append("<option selected='selected' value=" + childClass.ID + ">" + "----" + childClass.ClassName + "</option>");
                    }
                    else
                    {
                        sb.Append("<option value=" + childClass.ID + ">" + "----" + childClass.ClassName + "</option>");
                    }
                }
            }
            ViewBag.classInfo = sb.ToString();
            return View();
        }
        /// <summary>
        /// 添加类别
        /// </summary>
        /// <param name="articelClass"></param>
        /// <returns></returns>
        public ActionResult AddClass(ArticelClass articelClass)
        {
            articelClass.CreateUserId = LoginUser.ID;
            articelClass.CreateDate = DateTime.Now;
            articelClass.DelFlag = 0;
            ArticelClassService.AddEntity(articelClass);
            return Content("ok");
        }
    }
}