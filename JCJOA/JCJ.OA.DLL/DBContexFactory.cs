using JCJ.OA.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace JCJ.OA.DLL
{
    public class DBContexFactory
    {
        /// <summary>
        /// 保证线程内唯一EF对象
        /// </summary>
        /// <returns></returns>
        public static DbContext CreateDbContext()
        {
            DbContext db = (DbContext)CallContext.GetData("dbContext");
            if (db == null)
            {
                db = new ItcastCmsEntities();
                CallContext.SetData("dbContext", db);
            }
            return db;
        }
    }
}
