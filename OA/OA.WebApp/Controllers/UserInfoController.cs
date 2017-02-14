using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OA.WebApp.Controllers
{
   
    public class UserInfoController : Controller
    {
        IBLL.IUserInfoService UserInfoService = new BLL.UserInfoService();
        // GET: UserInfo
        public ActionResult Index()
        {
            return View();
        }
        #region 获取用户列表数据
        public ActionResult GetUserInfoList()
        {
            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 5;
            int totalCount = 0;
            var userInfoList =  UserInfoService.LoadPageEntities<int>(pageIndex,pageSize,out totalCount,c=>true,c=>c.id,true);
            var temp = from u in userInfoList
                       select new
                       {
                           id = u.id,
                           UName = u.UserName,
                           UPwd = u.Password,
                           Remark = u.Remark,
                           SubTime = u.SubTime
                       };
            return Json(new { rows = temp, total = totalCount });
        }
        #endregion
    }
}