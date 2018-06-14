using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class UserFileFolderDTO
    {
        public List<FolderDTO> Folders { get; set; }
        public List<FileDTO> Files { get; set; }
        public UserDTO Usr{ get; set; }
    }
}
