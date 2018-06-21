using Entity;
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
        public static int save(String name,int uid ,int Pfid)
        {
            String query = String.Format ( @"INSERT INTO dbo.Folder (Name,ParentFolderId,CreatedBy,CreatedOn,IsActive) 
             VALUES('{0}','{1}','{2}','{3}',1);select @@IDENTITY", name,Pfid,uid,DateTime.Now );
            
            using (DBHelper helper = new DBHelper ())
            {
                var rv = helper.ExecuteScalar ( query );
                return Convert.ToInt32(rv);    
            }
        }

        public static List<Entity.FolderDTO> getFoldersOfUser(int uid)
        {
            String query=String.Format("SELECT * FROM dbo.Folder Where CreatedBy={0} AND IsActive='{1}' AND ParentFolderId='{2}'",uid,1,0);
            using (DBHelper helper = new DBHelper ())
            {
                 var reader=helper.ExecuteReader ( query );
                List<FolderDTO> list = new List<FolderDTO>();
                while(reader.Read())
                {
                    var dto=FillDTO ( reader );
                    if (dto != null)
                        list.Add (dto);
                }
                return list;
            }
        }
        /////////////////////////////////////////////////////////////////////
        public static FolderDTO getFolderById(int pid)
        {
            String query = String.Format ( @"SELECT * FROM dbo.Folder WHERE Id='{0}'", pid );
            using (DBHelper helper = new DBHelper ())
            {
                var reader = helper.ExecuteReader ( query );
                reader.Read ();
                var folder = FillDTO ( reader );
                return folder;
            }
        }
        /////////////////////////////////////////////////////////////////////
        public static List<FolderDTO> getChildFolders(int pid ,int uid)
        {
            String query = String.Format(@"SELECT * FROM dbo.Folder WHERE ParentFolderId='{0}' AND CreatedBy='{1}'", pid,uid);
            using (DBHelper helper = new DBHelper ())
            { 
                var reader=helper.ExecuteReader ( query );
                List<FolderDTO> list = new List<FolderDTO> ();
                while(reader.Read())
                {
                    var dto= FillDTO ( reader );
                    if(dto!=null)
                        list.Add ( dto );
                }
                return list;
            }
        }
        /////////////////////////////////////////////////////////////////////
        private static FolderDTO FillDTO ( SqlDataReader reader )
        {
            var dto = new FolderDTO ();
            
            dto.id = reader.GetInt32 ( reader.GetOrdinal("id") );
            dto.name = reader.GetString ( reader.GetOrdinal("Name") );
            dto.ParentId = reader.GetInt32 ( reader.GetOrdinal("ParentFolderId") );
            dto.CreatedBy = reader.GetInt32 ( reader.GetOrdinal("CreatedBy") );
            dto.CreatedOn = reader.GetDateTime ( reader.GetOrdinal ( "CreatedOn" ) );
            return dto;
        }

        public static int delete(int id)
        {
            String query=String.Format(@"DELETE FROM dbo.Folder WHERE id='{0}'",id);
            using (DBHelper helper = new DBHelper())
            {
                int rv=helper.ExecuteQuery(query);
                return rv;
            }
        }
    }
}
