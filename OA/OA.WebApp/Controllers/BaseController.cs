using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OA.WebApp.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if(Session["UserInfo"]==null)
            {
              //  filterContext.HttpContext.Response.Redirect("/Login/Index");
              filterContext.Result = Redirect("/Login/Index");
            }
        }
    }
}