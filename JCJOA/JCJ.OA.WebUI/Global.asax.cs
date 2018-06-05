using JCJ.OA.WebUI.Models;
using log4net;
using Spring.Web.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace JCJ.OA.WebUI
{
    public class MvcApplication : SpringMvcApplication//System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();   //获取配置信息
            //开启一个线程，查看异常队列
            string filePath = Server.MapPath("/Log/");
            
            ThreadPool.QueueUserWorkItem((a) =>
            {
                while(true)     //需要一种跑着
                {
                    if (MyExceptionAttribute.ExceptionQueue.Count() > 0)
                    {
                        //Exception ex = MyExceptionAttribute.ExceptionQueue.Dequeue();  //从队列中取出数据
                        Exception ex=null;
                        if (MyExceptionAttribute.ExceptionQueue.TryDequeue(out ex) && ex!=null)
                        {
                            //string fullPath = filePath + DateTime.Now.ToString("yyyy-mm-dd") + ".txt";
                            //File.AppendAllText(fullPath, ex.ToString());
                            ILog logger = LogManager.GetLogger("errorMsg");
                            logger.Error(ex.ToString());
                        }
                        
                    }
                    else
                    {
                        Thread.Sleep(5000);  //避免CPU的空转每隔5秒扫描一次队列
                    }
                }
                

            },filePath);
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
