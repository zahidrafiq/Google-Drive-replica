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
            String query = String.Format( @"INSERT INTO dbo.Files (Name,ParentFolderId,FileExt,FileSizeInKB,CreatedBy,UploadedOn,IsActive,UniqName) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}');select @@IDENTITY",dto.Name,dto.ParentFolderId,dto.FileExt,dto.size,dto.CreatedBy,dto.UploadedOn,dto.IsActive,dto.UniqName);
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
        public static List<FileDTO> getFiles ( int pid ,int uid)
        {
            String query = String.Format ( @"SELECT * FROM dbo.Files WHERE ParentFolderId='{0}' AND CreatedBy='{1}'", pid, uid  );
            using (DBHelper helper = new DBHelper ())
            {
                var reader = helper.ExecuteReader ( query );
                List<FileDTO> list = new List<FileDTO>();
                while (reader.Read ())
                {
                    var dto = FillDTO(reader);
                    if (dto != null)
                        list.Add ( dto );
                }
                return list;
            }
        }
        /////////////////////////////////////////////////////////////////////
        public static FileDTO getFileByUniqID (String uniqName)
        {
            SqlDataReader reader = null;
            String query=String.Format(@"SELECT * FROM dbo.Files WHERE UniqName='{0}'",uniqName);
            using (DBHelper helper = new DBHelper ())
            {
                reader=helper.ExecuteReader ( query );
                reader.Read ();
                FileDTO dto = FillDTO ( reader );
                return dto;
            }
        }
        /////////////////////////////////////////////////////////////////////
        public static FileDTO getFileById (int fid)
        {
            SqlDataReader reader = null;
            String query = String.Format ( @"SELECT * FROM dbo.Files WHERE Id='{0}'", fid);
            using (DBHelper helper = new DBHelper ())
            {
                reader = helper.ExecuteReader ( query );
                reader.Read ();
                FileDTO dto = FillDTO ( reader );
                return dto;
            }
        }
        //////////////////////////////////////////////////////////////////////
        private static FileDTO FillDTO ( SqlDataReader reader )
        {
            var dto = new FileDTO();
            dto.id = reader.GetInt32( 0 );
            dto.Name = reader.GetString ( reader.GetOrdinal ( "Name" ) );
            dto.ParentFolderId = reader.GetInt32 ( reader.GetOrdinal ( "ParentFolderId" ) );
            dto.IsActive = Convert.ToByte( reader.GetBoolean ( reader.GetOrdinal ( "IsActive" ) ));
            dto.CreatedBy = reader.GetInt32 ( reader.GetOrdinal ( "CreatedBy" ) );
            dto.UploadedOn = reader.GetDateTime ( reader.GetOrdinal ( "UploadedOn" ) );
            dto.FileExt = reader.GetString ( reader.GetOrdinal ( "FileExt" ) );
            dto.size = reader.GetInt32 ( reader.GetOrdinal ( "FileSizeInKB" ) );
            try {
                dto.UniqName = reader.GetString ( reader.GetOrdinal ( "UniqName" ) );
            } catch (Exception exp) { }
            return dto;
        }

    }
}
