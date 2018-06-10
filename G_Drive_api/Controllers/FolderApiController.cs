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
        

        [HttpGet]
        public void saveFile () {
            if(HttpContext.Current.Request.Files.Count > 0)
            {
                try
                {
                    foreach (var fileName in HttpContext.Current.Request.Files.AllKeys)
                    {
                        HttpPostedFile file = HttpContext.Current.Request.Files[fileName];
                        if (file != null)
                        {
                            FileDTO dto = new FileDTO ();
                            dto.Name = Guid.NewGuid ().ToString ();//Unique file name.
                            dto.UploadedOn = DateTime.Now;
                            //dto.CreatedBy = Uid;
                            dto.FileExt = Path.GetExtension ( file.FileName );
                            //dto.ParentFolderId = ParentId;
                            dto.size = 1024;
                            var rootPath = HttpContext.Current.Server.MapPath ( "~/UploadedFiles" );
                            var fileSavePAth = System.IO.Path.Combine (  rootPath,dto.Name + dto.FileExt );
                            file.SaveAs ( fileSavePAth );
                        }
                    }
                }
                catch(Exception exp)
                { }
            }
        }
///////////////////////////////////////////////////////////////////////////////
         [HttpPost]
         public List<FolderDTO> getChildFolders()
        {
            var a=HttpContext.Current.Request.Params.Get("id");
            return BAL.FolderBO.getChildFolders (Convert.ToInt32( a ));
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