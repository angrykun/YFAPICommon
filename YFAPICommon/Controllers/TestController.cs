using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace YFAPICommon.Controllers
{
    public class TestController : ApiController
    {
        
        [Authorize]
        [HttpPost]
        public string Test1()
        {
            var ident = this.User.Identity;
            return "test auth";
        }
    }
}
