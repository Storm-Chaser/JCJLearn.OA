using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace JCJ.OA.WebUI.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            int a = 2;
            int b = 0;
            int c = a / b;
            return Content("ok");
        }
        //public ActionResult ShowTest()
        //{
        //    Thread mythread = new Thread(start);
        //    mythread.IsBackground = true;
        //    mythread.Start();
        //    return Content("ok");
        //}

        //private void start()
        //{
        //    Common.WebCommon.Show();
        //}
    }
}