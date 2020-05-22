using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Books.API.Controllers
{
    [Route("[controller]/[action]")]
    public class TestController : ControllerBase
    {

        public ActionResult Check() {
            return Ok("It works! :)");
        }
    }
}