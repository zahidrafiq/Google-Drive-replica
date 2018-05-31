using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ass8_GoogleDrive.Security
{
    public static class SessionManager
    {
        public static UserDTO User{
            get{
                UserDTO dto = null;
                if(HttpContext.Current.Session["user"]!=null)
                {
                    dto = HttpContext.Current.Session["user"] as UserDTO;
                }
                return dto;
            }

            set
            {
                HttpContext.Current.Session["user"] = value;
            }
        }//End of User property 

        public static Boolean IsValidUser
        {
            get
            {
                if (User != null)
                    return true;
                else
                    return false;
            }
        } 
    }//end of class
}