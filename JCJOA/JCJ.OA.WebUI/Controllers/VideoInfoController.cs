using JCJ.OA.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace JCJ.OA.WebUI.Controllers
{
    public class VideoInfoController : Controller
    {
        // GET: VidoFile
        IBLL.IVideoFileInfoService VideoFileInfoService { get; set; }
        IBLL.IVideoClassService VideoClassService { get; set; }
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
            var temp = VideoClassService.LoadEntities(a => a.DelFlag == 0).ToList();
            var videoClassList = from v in temp
                                 select new SelectListItem() { Text = v.ClassName, Value = v.ID.ToString() };
            ViewData["videoClass"] = videoClassList;
            return View();
        }
        /// <summary>
        /// 完成视频文件上传
        /// </summary>
        /// <returns></returns>
        public ActionResult VideoFileUp()
        {
            HttpPostedFileBase file = Request.Files["FileData"];
            string fileName = Path.GetFileName(file.FileName);
            string fileExt = Path.GetExtension(fileName);
            if (fileExt == ".avi")
            {
                string newFileName = Guid.NewGuid().ToString().Substring(0, 8);
                //将视频数据传递到视频转换服务器.
                WebClient webClient = new WebClient();
                webClient.UploadData("http://localhost:33242/VideoFileUp.ashx?fileName=" + newFileName + "&ext=" + fileExt, StreamToByte(file.InputStream));


                return Content("ok:视频上传成功!!" + ":" + newFileName);
            }
            return Content("no:视频上传错误!!");
        }

        private byte[] StreamToByte(Stream stream)
        {
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            return buffer;
        }
        /// <summary>
        /// 接收从视频转换服务器发送过来的转换后的FLV视频。
        /// </summary>
        /// <returns></returns>
        public ActionResult GetVideoFile()
        {
            string fileName = Request["fileName"];
            string t = Request["t"];
            if (t == "flv")
            {
                using (FileStream fileStream = System.IO.File.OpenWrite(Request.MapPath("/Videos/Flv/" + fileName + ".flv")))
                {
                    Request.InputStream.CopyTo(fileStream);
                    return Content("ok");
                }
            }
            else
            {
                using (FileStream fileStream = System.IO.File.OpenWrite(Request.MapPath("/Videos/Flv/" + fileName + ".jpg")))
                {
                    Request.InputStream.CopyTo(fileStream);
                    return Content("ok");
                }
            }
        }
        /// <summary>
        /// 完成信息保存
        /// </summary>
        /// <param name="videoInfo"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult AddVideo(VideoFileInfo videoInfo)
        {
            videoInfo.AddDate = DateTime.Now;
            videoInfo.DelFlag = 0;
            videoInfo.ImageUrl = "/Videos/Flv/" + Request["ImageUrl"] + ".jpg";
            videoInfo.ModifyDate = DateTime.Now;
            videoInfo.VideoPath = "/Videos/Flv/" + Request["ImageUrl"] + ".flv";
            int classId = int.Parse(Request["videoClass"]);
            return Content(VideoFileInfoService.AddVideoFileInfo(classId, videoInfo) ? "ok" : "no");
        }
        /// <summary>
        /// 播放视频
        /// </summary>
        /// <returns></returns>
        public ActionResult PlayVideo()
        {
            int id = int.Parse(Request["id"]);
            var videoInfo = VideoFileInfoService.LoadEntities(a => a.ID == id).FirstOrDefault();
            ViewBag.VideoInfo = videoInfo;
            return View();
        }
    }
}