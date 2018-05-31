using System.Web;
using System.Web.Mvc;

namespace Ass8_GoogleDrive
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters ( GlobalFilterCollection filters )
        {
            filters.Add ( new HandleErrorAttribute () );
        }
    }
}
