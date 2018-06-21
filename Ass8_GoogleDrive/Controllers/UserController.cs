using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ass8_GoogleDrive.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index ()
        {
            return View ();
        }

        public ActionResult UserHome ()
        {
            if (!Security.SessionManager.IsValidUser)
                return View ( "Index" );
            int uid = Security.SessionManager.User.id;
            var ListOfFolder = BAL.FolderBO.getFoldersOfUser (uid);
            var ListOfFiles = BAL.FileBO.getFiles ( 0 ,uid);
            UserFileFolderDTO list = new UserFileFolderDTO ();
            list.Folders = ListOfFolder;
            list.Files = ListOfFiles;
            list.Usr = Security.SessionManager.User;

            return View (list);
        }

        public JsonResult ValidateUser ( String login, String password )
        {
            Object data = null;
            try
            {
                Boolean flag = false;
                var url = "";
                var obj = BAL.UsersBO.ValidateUser ( login, password );
                if (obj != null)
                {
                    flag = true;
                    Security.SessionManager.User = obj;
                    Session["uid"] = obj.id;
                    url = Url.Content ( "~/User/UserHome/" );
                }
                data = new
                {
                    valid = flag,
                    urlToRedirect = url
                };
            }
            catch (Exception exp)
            {
                data = new
                {
                    valid = false,
                    urlToRedirect = ""
                };
            }//end of catch.
            return Json ( data, JsonRequestBehavior.AllowGet );
        }//end of validateUser()


    }
}