using OA.IBLL;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.BLL
{
    public class UserInfoService : BaseService<UserInfo>,IUserInfoService
    {
        public bool DeleteEntities(List<int> list)
        {
            var userInfoList = this.CurrentDBSession.UserInfoDal.LoadEntities(u=>list.Contains(u.id));
            foreach(var userInfo in userInfoList)
            {
                this.CurrentDBSession.UserInfoDal.DeleteEntity(userInfo);
            }
            return this.CurrentDBSession.SaveChange();

        }

        public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.UserInfoDal;
        }
    }
}
