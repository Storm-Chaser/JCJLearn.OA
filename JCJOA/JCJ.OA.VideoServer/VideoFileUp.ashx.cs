using JCJ.OA.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace JCJ.OA.VideoServer
{
    /// <summary>
    /// VideoFileUp 的摘要说明
    /// </summary>
    public class VideoFileUp : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string fileName = context.Request["fileName"];  //文件路径
            string fileExt = context.Request["ext"];//文件扩展名
            //将传递过来的视频文件接收并且存到视频转换服务器的磁盘上，同时将视频的信息写到队列中
            using (FileStream fileStream = File.OpenWrite(context.Request.MapPath("D:/" + fileName + fileExt)))
            {
                //将web服务器传递过来的视频保存到转换服务器磁盘上
                context.Request.InputStream.CopyTo(fileStream);
                //将视频信息写到队列中
                VideoFileModel videoFileModel = new VideoFileModel();
                videoFileModel.VideoFileName = fileName;
                videoFileModel.videoFileExt = fileExt;
                string str = Common.SerializeHelper.SerializeToString(videoFileModel);
                Common.RedisHelper.Enqueue("videoFileInfo",str);
            }
    
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}