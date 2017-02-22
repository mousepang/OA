using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using OA.WebApp.Models;
using Spring.Web.Mvc;
using log4net;

namespace OA.WebApp
{
    public class MvcApplication : SpringMvcApplication
    {
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();//读取配置文件中的配置信息
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //开启一个线程，扫描异常信息队列
            string filePath = Server.MapPath("/Log/");
            ThreadPool.QueueUserWorkItem((a) =>
            {
                while (true)
                {
                    //首先要判断下队列中是否有数据
                    if (MyExceptionAttribute.ExceptionQueue.Count > 0)
                    {
                        Exception ex = MyExceptionAttribute.ExceptionQueue.Dequeue();
                        if (ex != null)
                        {
                            ////将异常信息写到日志文件中 
                            // string fileName = DateTime.Now.ToString("yyyy-MM-dd");
                            // File.AppendAllText(filePath+fileName+".txt",ex.ToString(),System.Text.Encoding.UTF8);
                            ILog logger = LogManager.GetLogger("errorMsg");
                            logger.Error(ex.ToString());
                        }
                        else
                        {      
                            //sleep一下降低cpu消耗
                            Thread.Sleep(3000);
                        }
                    }
                    else
                    {
                        //sleep一下降低cpu消耗
                        Thread.Sleep(3000);
                    }
                }
            }, filePath);
        }
    }
}
