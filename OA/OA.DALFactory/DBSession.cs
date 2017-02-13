using OA.DAL;
using OA.IDAL;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.DALFactory
{
    public class DBSession
    {
        OAEntities Db = new OAEntities();
        private IUserInfoDal _UserInfoDal;
        public IUserInfoDal UserInfoDal
        {
            get
            {
                if(_UserInfoDal==null)
                {
                    _UserInfoDal = new UserInfoDAL();
                }
                return _UserInfoDal;
            }
            set
            {
                _UserInfoDal = value;
            }
        }

        public bool SaveChange()
        {
            return Db.SaveChanges()>0;
        }
    }
}
