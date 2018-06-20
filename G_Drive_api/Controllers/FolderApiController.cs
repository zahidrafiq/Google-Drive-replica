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
        public Object saveFile ()
        {
            String uniqueName;
            Object data = null;
            FileDTO dto = new FileDTO ();
            var id=HttpContext.Current.Request.Params.Get ( "uid" );
            var pid=HttpContext.Current.Request.Params.Get ( "pid" );
            if (HttpContext.Current.Request.Files.AllKeys.Any ())
            {
                // Get the uploaded image from the Files collection
                var httpPostedFile = HttpContext.Current.Request.Files["file"];
                 if (httpPostedFile != null)
                {
                    var ext = System.IO.Path.GetExtension ( httpPostedFile.FileName );
                    dto.Name = httpPostedFile.FileName.Substring ( 0, httpPostedFile.FileName.Length - ext.Length );
                    
                    //Generate a unique name using Guid
                    uniqueName = Guid.NewGuid ().ToString ();

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
                        if (rv > 0) {
                            data = new
                            { ID = rv,   NAME = dto.Name};
                        }
                        else
                        {
                            data = new
                            { ID = -1, NAME = "" };
                        }       
                }
                
            }
            return data;
        }
        ///////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        public Object downLoadFile(int fileId)
        {
            //int fileId =Convert.ToInt32( HttpContext.Current.Request.Params.Get ( "fId" ));
            FileDTO fileDTO=BAL.FileBO.getFileById( fileId );
            String rootPath = HttpContext.Current.Server.MapPath ( "~/UploadedFiles" );
           
            if(fileDTO!=null)
            {
                HttpResponseMessage response = new HttpResponseMessage ( HttpStatusCode.OK );
                var fileFullPath = System.IO.Path.Combine ( rootPath, fileDTO.UniqName );

                byte[] file = System.IO.File.ReadAllBytes ( fileFullPath );
                System.IO.MemoryStream ms = new System.IO.MemoryStream ( file );

                response.Content = new ByteArrayContent ( file );
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue ("attachment");
               
                // String mimeType = 

                //response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue ( fileDTO.FileExt );
                response.Content.Headers.ContentDisposition.FileName = fileDTO.Name+fileDTO.FileExt;
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
            return BAL.FileBO.getFileById (Convert.ToInt32( fId ) );
        }
        
    }
}