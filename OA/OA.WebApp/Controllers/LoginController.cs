using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OA.Common;

namespace OA.WebApp.Controllers
{
    public class LoginController : Controller
    {
        private IBLL.IUserInfoService UserInfoService { get; set; }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        #region 完成用户登录
        public ActionResult UserLogin()
        {
            string validateCode = Session["validateCode"] != null ? Session["validateCode"].ToString() : string.Empty;
            if(string.IsNullOrEmpty(validateCode))
            {
                return Content("no:验证码错误");
            }
            Session["validateCode"] = null;
            string txtCode = Request["vCode"];
            if(!validateCode.Equals(txtCode,StringComparison.InvariantCultureIgnoreCase))
            {
                return Content("no:验证码错误");
            }
            string userName = Request["LoginCode"];
            string userPwd = Request["LoginPwd"];
            var userInfo = UserInfoService.LoadEntities(u=>u.UserName==userName&&u.Password==userPwd);
            if(userInfo==null)
            {
                return Content("no:用户名密码错误");
            }
            if (userInfo.Count()>0)
            {
                Session["UserInfo"] = userInfo;
                return Content("ok:登录成功");
            }
            else
            {
                return Content("no:用户名密码错误");
            }
            
        }
        #endregion

        public ActionResult ShowValidateCode()
        {
            Common.ValidateCode vliateCodde = new Common.ValidateCode();
            string code = vliateCodde.CreateValidateCode(4);
            Session["validateCode"] = code;
            byte[]buffer= vliateCodde.CreateValidateGraphic(code);
            return File(buffer,"image/jpeg");
        }
    }
}