using System;
using Entity;
using DAL;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public static class UsersBO
    {
        public static UserDTO ValidateUser ( String pLogin, String pPassword )
        {
            return DAL.UsersDAO.ValidateUser ( pLogin, pPassword );
        }
    }
}
