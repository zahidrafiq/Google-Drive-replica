using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public static class FolderBO
    {
        public static int save ( String name, int uid,int Pfid )
        {
            return DAL.FolderDAO.save ( name, uid ,Pfid);
        }

        public static List<Entity.FolderDTO> getFoldersOfUser ( int uid )
        {
            return DAL.FolderDAO.getFoldersOfUser ( uid );
        }

        public static List<FolderDTO> getChildFolders ( int pid ,int uid)
        {
            return DAL.FolderDAO.getChildFolders ( pid ,uid);
        }

        public static FolderDTO getFolderById ( int pid )
        {
            return DAL.FolderDAO.getFolderById ( pid );
        }

        public static int delete(int id)
        {
            return DAL.FolderDAO.delete(id);
        }
    }
}
