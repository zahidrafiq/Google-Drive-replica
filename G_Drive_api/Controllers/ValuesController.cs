using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;


namespace G_Drive_api.Controllers
{
    public class ValuesController : ApiController
    {
        [System.Web.Http.HttpGet]
        public int sum ( int a, int b )
        {
            return a + b;

        }
    }
}
