using Imtudou.IdentityServer.Models;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Imtudou.IdentityServer.Controllers
{

    [Route("api/[controller]/[action]")]
    public class HomePageController : ControllerBase
    {
        [HttpPost]
        public IActionResult GetUser([FromBody] UserInfoModel input)
        {
            return Ok(DateTime.Now);
        }
    }
}
