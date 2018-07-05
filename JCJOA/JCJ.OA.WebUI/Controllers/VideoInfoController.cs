using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JCJ.OA.WebUI.Controllers
{
    public class VideoInfoController : Controller
    {
        // GET: VidoFile
        IBLL.IVideoFileInfoService VideoFileInfoService { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 加载视频数据.
        /// </summary>
        /// <returns></returns>
        public ActionResult GetVideoInfo()
        {
            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;//当前页码。
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 5;
            int totalCount;
            var videoFileInfoList = VideoFileInfoService.LoadPageEntities<int>(pageIndex, pageSize, out totalCount, p => p.DelFlag == 0, p => p.ID, true);

            var temp = from p in videoFileInfoList
                       select new { ID = p.ID, Title = p.Title, AddDate = p.AddDate, Author = p.Author, Orgin = p.Orgin, ClassName = from c in p.VideoClass select c.ClassName };
            return Json(new { rows = temp, total = totalCount }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 展示添加视频的页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowAddVideo()
        {
            return View();
        }
    }
}