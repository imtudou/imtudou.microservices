using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Imtudou.IdentityServer
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        public (bool a, string b) GetResult(int a, string b)
        {
            return (a > 0, b);       
        }
    }
}
