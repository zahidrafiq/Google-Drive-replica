using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public static class FolderBO
    {
        public static int save(String name,int uid)
        {
            return DAL.FolderDAO.save ( name, uid );
        }
    }
}
