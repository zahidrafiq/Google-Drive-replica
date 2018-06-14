using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class FileDTO
    {
        public int id { get; set; }
        public String Name { get; set; }
        public String UniqName { get; set; }
        public int ParentFolderId { get; set;}
        public String FileExt { get; set; }
        public int size { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UploadedOn { get; set; }
        public Byte IsActive { get; set; }
    }
}
