using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class FileFolderDTO
    {
        public List<FolderDTO> Folder { get; set; }
        public List<FileDTO>  File { get; set; }
        
    }
}
