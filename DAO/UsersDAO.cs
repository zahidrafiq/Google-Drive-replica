using Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class UsersDAO
    {
        public static UserDTO ValidateUser ( String pLogin, String pPassword )
        {
            var query = String.Format ( @"Select * from dbo.Users Where Login=@login and Password=@pwd" );
            SqlCommand cmd = new SqlCommand ( query );

            SqlParameter parm = null;
            //parm.ParameterName = "login";
            parm = new SqlParameter ( "login", System.Data.SqlDbType.VarChar );
            parm.Value = pLogin;
            cmd.Parameters.Add ( parm );

            parm = new SqlParameter ( "pwd", System.Data.SqlDbType.VarChar );
            parm.Value = pPassword;
            cmd.Parameters.Add ( parm );


            using (DBHelper helper = new DBHelper ())
            {
                var reader = helper.ExecuteReaderParm ( cmd );
               // var reader = helper.ExecuteReader ( query );

                UserDTO dto = null;

                if (reader.Read ())
                {
                    dto = FillDTO ( reader );
                }

                return dto;
            }
        }

        public static UserDTO GetUserById ( int pid )
        {

            var query = String.Format ( "Select * from dbo.Users Where UserId={0}", pid );

            using (DBHelper helper = new DBHelper ())
            {
                var reader = helper.ExecuteReader ( query );

                UserDTO dto = null;

                if (reader.Read ())
                {
                    dto = FillDTO ( reader );
                }

                return dto;
            }
        }
        ///////////////////////////////////////////////
        private static UserDTO FillDTO ( SqlDataReader reader )
        {
            var dto = new UserDTO ();
            dto.id = reader.GetInt32 (reader.GetOrdinal("id"));
            dto.Name = reader.GetString ( reader.GetOrdinal("Name") );
            dto.Login = reader.GetString ( reader.GetOrdinal("Login") );
            dto.Password = reader.GetString ( reader.GetOrdinal("Password"));
            dto.Email = reader.GetString ( reader.GetOrdinal("Email") );
            
            return dto;
        }

    }
}

    

