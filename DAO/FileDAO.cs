using Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class FileDAO
    {
        public static int SaveFile (FileDTO dto) {
            String query = String.Format( @"INSERT INTO dbo.Files (Name,ParentFolderId,FileExt,FileSizeInKB,CreatedBy,UploadedOn,IsActive) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}');select @@IDENTITY",dto.Name,dto.ParentFolderId,dto.FileExt,dto.size,dto.CreatedBy,dto.UploadedOn,dto.IsActive);
            using (DBHelper helper = new DBHelper())
            {
                var rv = helper.ExecuteScalar ( query );
                return Convert.ToInt32 (rv);
            }
        }

        ////////////////////////////////////////////////////
        public static List<Entity.FileDTO> getFilesOfUser ( int uid )
        {
            String query = String.Format ( @"SELECT * FROM dbo.Files Where CreatedBy={0} AND IsActive='{1}' AND ParentFolderId='{2}'", uid, 1, 0 );
            using (DBHelper helper = new DBHelper ())
            {
                var reader = helper.ExecuteReader ( query );
                List<FileDTO> list = new List<FileDTO> ();
                while (reader.Read ())
                {
                    var dto = FillDTO ( reader );
                    if (dto != null)
                        list.Add ( dto );
                }
                return list;
            }
        }
        /////////////////////////////////////////////////////////////////////
        public static List<FileDTO> getFiles ( int pid )
        {
            String query = String.Format ( @"SELECT * FROM dbo.Files WHERE ParentFolderId='{0}'", pid  );
            using (DBHelper helper = new DBHelper ())
            {
                var reader = helper.ExecuteReader ( query );
                List<FileDTO> list = new List<FileDTO> ();
                while (reader.Read ())
                {
                    var dto = FillDTO ( reader );
                    if (dto != null)
                        list.Add ( dto );
                }
                return list;
            }
        }

        /////////////////////////////////////////////////////////////////////
        private static FileDTO FillDTO ( SqlDataReader reader )
        {
            var dto = new FileDTO ();
            dto.id = reader.GetInt32 ( reader.GetOrdinal ( "id" ) );
            dto.Name = reader.GetString ( reader.GetOrdinal ( "Name" ) );
            dto.ParentFolderId = reader.GetInt32 ( reader.GetOrdinal ( "ParentFolderId" ) );
            //dto.IsActive = reader.GetByte ( reader.GetOrdinal ( "IsActive" ) );
            dto.CreatedBy = reader.GetInt32 ( reader.GetOrdinal ( "CreatedBy" ) );
            dto.UploadedOn = reader.GetDateTime ( reader.GetOrdinal ( "UploadedOn" ) );
            dto.FileExt = reader.GetString ( reader.GetOrdinal ( "FileExt" ) );
            dto.size = reader.GetInt32 ( reader.GetOrdinal ( "FileSizeInKB" ) );
            return dto;
        }

    }
}
