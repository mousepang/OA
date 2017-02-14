using OA.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace OA.DAL
{
   public class DBContextFactory
    {
        /// <summary>
        /// 负责创建Ef，保证线程内唯一
        /// </summary>
        /// <returns></returns>
        public static DbContext CreateContext()
        {
            DbContext dbContext = (DbContext)CallContext.GetData("dbContext");
            if(dbContext==null)
            {
                dbContext = new OAEntities();
                CallContext.SetData("dbContext",dbContext);
            }
            return dbContext;
        }
    }
}
