using Imtudou.IdentityServer.Application.Authorization.Accounts;
using Imtudou.IdentityServer.Application.Authorization.Accounts.Dto;

using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace Imtudou.IdentityServer.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IAccountService accountService;

        /// <summary>
        /// ctor
        /// </summary>
        public UserController(IAccountService account)
        {
            this.accountService = account;
        }

        [HttpPost("RegisterPhone")]
        public async Task<IActionResult> RegisterPhoneAsync([FromBody] RegisterInput input)
        { 
            var iss = await  this.accountService.RegisterPhoneAsync(input);
            return Ok(iss);
        }
    }
}
