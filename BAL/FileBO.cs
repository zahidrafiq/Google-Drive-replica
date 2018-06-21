using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public static class FileBO
    {
        public static int SaveFile(FileDTO dto)
        {
            return DAL.FileDAO.SaveFile ( dto );
        }

        public static int delete ( int id )
        {
            return DAL.FileDAO.delete ( id );
        }

        public static List<FileDTO> getFiles ( int pid , int uid)
        {
            return DAL.FileDAO.getFiles ( pid ,uid);
        }

        public static FileDTO getFileByUniqID ( String uniqName )
        {
            return DAL.FileDAO.getFileByUniqID ( uniqName );
        }

        public static FileDTO getFileById ( int fid )
        {
            return DAL.FileDAO.getFileById ( fid );
        }
    }
}
