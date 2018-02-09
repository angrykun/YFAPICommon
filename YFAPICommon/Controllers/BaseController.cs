using EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace YFAPICommon.Controllers
{
    public class BaseController : ApiController
    {
        protected readonly MyDBContext dbContext = new MyDBContext();
    }
}