﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OA.WebApp.Models
{
    public class MyExceptionAttribute:HandleErrorAttribute
    {
        public  static Queue<Exception> ExceptionQueue = new Queue<Exception>();
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            Exception ex = filterContext.Exception;
            //把错误写入队列
            ExceptionQueue.Enqueue(ex);
           //跳转到错误页面
           filterContext.HttpContext.Response.Redirect("Error.cshtml");
        }
    }
}