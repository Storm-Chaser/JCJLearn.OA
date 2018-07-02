using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JCJ.OA.WebUI.Controllers
{
    public class PhotoInfoController : Controller
    {
        // GET: PhotoInfo
        IBLL.IPhotoInfoService PhotoInfoService { get; set; }
        IBLL.IPhotoClassService PhotoClassService { get; set; }
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 展示图片信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPhotoInfo()
        {
            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;//当前页码。
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 5;
            int totalCount;
            var photoInfoList = PhotoInfoService.LoadPageEntities<int>(pageIndex, pageSize, out totalCount, p => p.DelFlag == 0, p => p.ID, true);
            var temp = from p in photoInfoList
                       select new { ID = p.ID, Title = p.Title, AddDate = p.AddDate, Author = p.Author, Orgin = p.Orgin, ClassName = p.PhotoClass.ClassName, ImageUrl = p.PicUrls };
            return Json(new { rows = temp, total = totalCount }, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// 展示添加图片
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowAddPhoto()
        {
            var classList = PhotoClassService.LoadEntities(a => a.DelFlag == 0).ToList();
            var temp = from a in classList
                       select new SelectListItem { Text = a.ClassName, Value = a.ID.ToString() };
            ViewData["photoClass"] = temp;
            return View();
        }
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        public ActionResult FileUp()
        {
            HttpPostedFileBase file = Request.Files["FileData"];
            string fileName = Path.GetFileName(file.FileName);
            string fileExt = Path.GetExtension(fileName);
            if (fileExt == ".jpg")
            {
                string dir = "/ImageUp/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";
                Directory.CreateDirectory(Path.GetDirectoryName(Request.MapPath(dir)));
                string newfileName = Guid.NewGuid().ToString().Substring(0, 8);
                string fullDir = dir + newfileName + fileExt;
                file.SaveAs(Request.MapPath(fullDir));
                return Content("ok:" + fullDir + ":" + fileName);
            }
            return Content("no:上传失败!!");
        }
        /// <summary>
        /// 删除图片文件
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteFile()
        {
            string picUrl = Request["p"];
            System.IO.File.Delete(Request.MapPath(picUrl));
            return Content("ok");
        }
    }
}