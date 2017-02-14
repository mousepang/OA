using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA.IDAL;
using System.Runtime.Remoting.Messaging;

namespace OA.DALFactory
{
    public class DBSessionFactory
    {
        public static IDBSession CreateDBSession()
        {
            IDBSession DBSession = (IDBSession)CallContext.GetData("dbSession");
            if (DBSession == null)
            {
                DBSession = new DBSession();
                CallContext.SetData("dbSession", DBSession);
            }
            return DBSession;
        }
    }
}
