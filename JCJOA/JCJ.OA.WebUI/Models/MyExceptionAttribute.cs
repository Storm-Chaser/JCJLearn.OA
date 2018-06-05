using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JCJ.OA.WebUI.Models
{
    public class MyExceptionAttribute: HandleErrorAttribute
    {
        public static ConcurrentQueue<Exception> ExceptionQueue = new ConcurrentQueue<Exception>();   //定义队列
        /// <summary>
        /// 在该方法中捕获异常
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            Exception exception = filterContext.Exception;  //捕获异常信息
            //将异常信息写到队列
            ExceptionQueue.Enqueue(exception);
            //跳转到错误页
            filterContext.HttpContext.Response.Redirect("/Error.html");
        }
    }
}