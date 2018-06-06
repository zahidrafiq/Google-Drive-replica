using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class FolderDAO
    {
        public static int save(String name,int uid)
        {
            String query = String.Format ( @"INSERT INTO dbo.Folder (Name,ParentFolderId,CreatedBy,CreatedOn,IsActive) 
             VALUES('{0}','{1}','{2}','{3}',1);select @@IDENTITY", name,null,uid,DateTime.Now );
            
            using (DBHelper helper = new DBHelper ())
            {
                var rv = helper.ExecuteScalar ( query );
                return Convert.ToInt32(rv);    
            }
        }
    }
}
