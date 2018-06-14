using System;
using Entity;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Net.Http.Headers;
//using System.Windows.Shell;
namespace G_Drive_api.Controllers
{
    //[EnableCors( origins: "*", headers: "*", methods: "*" )]
    [EnableCors ( origins: "*", headers: "*", methods: "*" )]
    public class FolderApiController : ApiController
    {   
        [HttpPost]
        public int saveFolder()
        {
            var name = HttpContext.Current.Request.Params.Get ( "Name" );
            var uid = HttpContext.Current.Request.Params.Get ( "Uid" );
            var pfid = HttpContext.Current.Request.Params.Get ( "Pfid" );
            var rv=BAL.FolderBO.save ( name,Convert.ToInt32(uid) ,Convert.ToInt32(pfid));
            return rv;
        }

        [HttpPost]
        public int delete()
        {
            var id = HttpContext.Current.Request.Params.Get ( "ID" );
            return BAL.FolderBO.delete ( Convert.ToInt32( id ));
        }
        [HttpPost]
        public void saveFile () {
            String uniqueName;
            FileDTO dto = new FileDTO ();
            var id=HttpContext.Current.Request.Params.Get ( "uid" );
            var pid=HttpContext.Current.Request.Params.Get ( "pid" );
            if (HttpContext.Current.Request.Files.AllKeys.Any ())
            {
                // Get the uploaded image from the Files collection
                var httpPostedFile = HttpContext.Current.Request.Files["file"];
                dto.Name=httpPostedFile.FileName;
                if (httpPostedFile != null)
                {
                    var ext = System.IO.Path.GetExtension ( httpPostedFile.FileName );

                    //Generate a unique name using Guid
                    uniqueName = Guid.NewGuid ().ToString () + ext;

                    //Get physical path of our folder where we want to save images
                    var rootPath = HttpContext.Current.Server.MapPath ( "~/UploadedFiles" );

                    var fileSavePath = System.IO.Path.Combine ( rootPath, uniqueName );

                    // Save the uploaded file to "UploadedFiles" folder
                    httpPostedFile.SaveAs ( fileSavePath );
                    dto.UniqName = uniqueName;
                    dto.ParentFolderId = Convert.ToInt32( pid );
                    dto.CreatedBy = Convert.ToInt32( id );
                    dto.FileExt = ext;
                    //var fileInfo = new FileInfo ( fileSavePath );
                    dto.UploadedOn = DateTime.Now;
                    FileInfo f = new FileInfo ( fileSavePath );
                    long s1 = f.Length;
                    

                    // dto.size = Convert.ToInt32(fileInfo.Length);
                    dto.IsActive = 1;
                    int rv = BAL.FileBO.SaveFile ( dto );
                }
            }
        }
///////////////////////////////////////////////////////////////////////////////
        public Object downLoadFile(String uniqName)
        {
            String rootPath = HttpContext.Current.Server.MapPath ( "~/UploadedFiles" );
            var fileDTO = BAL.FileBO.getFileByUniqID ( uniqName );

            if(fileDTO!=null)
            {
                HttpResponseMessage response = new HttpResponseMessage ( HttpStatusCode.OK );
                var fileFullPath = System.IO.Path.Combine ( rootPath, fileDTO.UniqName + fileDTO.FileExt );

                byte[] file = System.IO.File.ReadAllBytes ( fileFullPath );
                System.IO.MemoryStream ms = new System.IO.MemoryStream ( file );

                response.Content = new ByteArrayContent ( file );
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue ("attachment");
                //String mimeType = MimeType.

                //response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue ( fileDTO.ContentType );
                response.Content.Headers.ContentDisposition.FileName = fileDTO.Name;
                return response;
            }
            else
            {
                HttpResponseMessage response = new HttpResponseMessage (HttpStatusCode.NotFound);
                return response;
            }
        }
///////////////////////////////////////////////////////////////////////////////
         [HttpPost]
         public List<FolderDTO> getChildFolders()
        {
            var pId=HttpContext.Current.Request.Params.Get("pid");
            var uId = HttpContext.Current.Request.Params.Get ("uid");
            return BAL.FolderBO.getChildFolders (Convert.ToInt32( pId ),Convert.ToInt32(uId));
        }
        [HttpPost]
        public List<FileDTO> getFiles ()
        {
            var fId = HttpContext.Current.Request.Params.Get ( "fid" );
            var uid = HttpContext.Current.Request.Params.Get ( "uid" );
            return BAL.FileBO.getFiles ( Convert.ToInt32 ( fId ) , Convert.ToInt32(uid));
        }
        [HttpPost]
        public FileDTO getFileByUniqIDAndUid()
        {
            var fId = HttpContext.Current.Request.Params.Get ( "fid" );
            var uid = HttpContext.Current.Request.Params.Get ( "uid" );
            return BAL.FileBO.getFileByUniqIDAndUid (Convert.ToInt32( fId ), Convert.ToInt32(uid) );
        }
        //// GET api/<controller>
        //public IEnumerable<string> Get ()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<controller>/5
        //public string Get ( int id )
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //public void Post ( [FromBody]string value )
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put ( int id, [FromBody]string value )
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete ( int id )
        //{
        //}
    }
}