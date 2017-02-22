using OA.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OA.WebApp.Controllers
{
   
    public class UserInfoController : BaseController
    {
        private IBLL.IUserInfoService UserInfoService { get; set; }
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
            var userInfoList = UserInfoService.LoadPageEntities<int>(pageIndex, pageSize, out totalCount, c => true, c => c.id, true);
            var temp = from u in userInfoList
                       select new
                       {
                           ID = u.id,
                           UName = u.UserName,
                           UPwd = u.Password,
                           Remark = u.Remark,
                           SubTime = u.SubTime
                       };
            Debug.WriteLine(temp);
            return Json(new { rows = temp, total = totalCount });

        }
        #endregion

        #region 删除用户数据
        public ActionResult DeleteUserInfo(string strId)
        {
            string[] strIds = strId.Split(',');
            List<int> list = new List<int>();
            foreach(string id in strIds)
            {
                list.Add(Convert.ToInt32(id));
            }
           if(UserInfoService.DeleteEntities(list))
            {
                return Content("ok");
            }
           else
            {
                return Content("no");
            }
        }
        #endregion

        #region 添加用户数据
        public ActionResult AddUserInfo(UserInfo userinfo)
        {
            userinfo.dutyid = 0;
            userinfo.SubTime = DateTime.Now;
            UserInfoService.AddEntity(userinfo);
            return Content("ok");
        }
        #endregion

        #region 展示修改数据
        public ActionResult ShowEditInfo()
        {
            int uID = 0;
            int.TryParse(Request["id"], out uID);
            var userInfo =  UserInfoService.LoadEntities(u => u.id==uID).FirstOrDefault();
            return Json(userInfo, JsonRequestBehavior.AllowGet);        
        }
        #endregion

        #region 修改用户数据
        public ActionResult EditUserInfo(UserInfo userInfo)
        {
            if(UserInfoService.EditEntity(userInfo))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
            
        }
        #endregion
    }
}