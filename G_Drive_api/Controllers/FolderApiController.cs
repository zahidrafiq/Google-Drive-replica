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
                    dto.Name = uniqueName;
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
            //var a=HttpContext.Current.Request.Params.Get ( "f" );
            //if(HttpContext.Current.Request.Files.Count > 0)
            //{
            //    try
            //    {
            //        foreach (var fileName in HttpContext.Current.Request.Files.AllKeys)
            //        {
            //            HttpPostedFile file = HttpContext.Current.Request.Files[fileName];
            //            if (file != null)
            //            {
            //                FileDTO dto = new FileDTO ();
            //                dto.Name = Guid.NewGuid ().ToString ();//Unique file name.
            //                dto.UploadedOn = DateTime.Now;
            //                //dto.CreatedBy = Uid;
            //                dto.FileExt = Path.GetExtension ( file.FileName );
            //                //dto.ParentFolderId = ParentId;
            //                dto.size = 1024;
            //                var rootPath = HttpContext.Current.Server.MapPath ( "~/UploadedFiles" );
            //                var fileSavePAth = System.IO.Path.Combine (  rootPath,dto.Name + dto.FileExt );
            //                file.SaveAs ( fileSavePAth );
            //            }
            //        }
            //    }
            //    catch(Exception exp)
            //    { }
            //}
        }
///////////////////////////////////////////////////////////////////////////////
         [HttpPost]
         public List<FolderDTO> getChildFolders()
        {
            var a=HttpContext.Current.Request.Params.Get("id");
            return BAL.FolderBO.getChildFolders (Convert.ToInt32( a ));
        }

        public List<FileDTO> getFiles ()
        {
            var fId = HttpContext.Current.Request.Params.Get ( "id" );
            return BAL.FileBO.getFiles ( Convert.ToInt32 ( fId ) );
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