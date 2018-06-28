using NVelocity;
using NVelocity.App;
using NVelocity.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCJ.OA.Common
{
    public class NVelocityHelper
    {
        /// <summary>
        /// 创建NVelocity模板引擎，读取设计好的模板页面，替换占位符。
        /// </summary>
        /// <param name="templateName"></param>
        /// <param name="data"></param>
        /// <param name="templateFilePath"></param>
        /// <returns></returns>
        public static string RenderTemplate(string templateName, object data, string templateFilePath)
        {
            VelocityEngine vltEngine = new VelocityEngine();
            vltEngine.SetProperty(RuntimeConstants.RESOURCE_LOADER, "file");
            vltEngine.SetProperty(RuntimeConstants.FILE_RESOURCE_LOADER_PATH, System.Web.Hosting.HostingEnvironment.MapPath(templateFilePath));
            vltEngine.Init();

            VelocityContext vltContext = new VelocityContext();
            vltContext.Put("data", data);

            Template vltTemplate = vltEngine.GetTemplate(templateName + ".html");
            System.IO.StringWriter vltWriter = new System.IO.StringWriter();
            vltTemplate.Merge(vltContext, vltWriter);

            return vltWriter.GetStringBuilder().ToString();
        }
    }
}
