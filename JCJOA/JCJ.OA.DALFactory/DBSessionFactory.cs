using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace JCJ.OA.DALFactory
{
    public class DBSessionFactory
    {
        public static IDAL.IDBSession CreateDbSession()
        {
            IDAL.IDBSession dbSession = (IDAL.IDBSession)CallContext.GetData("dbSession");
            if (dbSession == null)
            {
                dbSession = new DBSession();
                CallContext.SetData("dbSession", dbSession);
            }
            return dbSession;
        }
    }
}
