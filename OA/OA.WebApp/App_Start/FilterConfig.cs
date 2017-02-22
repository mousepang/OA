using System.Web;
using System.Web.Mvc;
using OA.WebApp.Models;

namespace OA.WebApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new MyExceptionAttribute());
        }
    }
}
