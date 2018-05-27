using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JCJ.OA.DALFactory
{
    /// <summary>
    /// 抽象工厂：通过反射的形式创建数据库操作类的实例
    /// </summary>
    public class AbstractFactory
    {
        //获取配置信息
        private static readonly string AssemblyPath = ConfigurationManager.AppSettings["AssemblyPath"];
        private static readonly string NameSpace = ConfigurationManager.AppSettings["NameSpace"];
        public static IDAL.IUserInfoDal CreateUserInfoDal()
        {
            string fullClassName = NameSpace + ".userInfoDal";
            return CreateInstance(fullClassName) as IDAL.IUserInfoDal; //注意：转成的是接口，不能是具体的类
        }
        /// <summary>
        /// 通过反射创建类的实例
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        private static object CreateInstance(string className)
        {
            var assembly = Assembly.Load(AssemblyPath);
            return assembly.CreateInstance(className);
        }
    }
}
