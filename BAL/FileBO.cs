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

        public static List<FileDTO> getFiles ( int pid )
        {
            return DAL.FileDAO.getFiles ( pid );
        }
    }
}
