using Imtudou.IdentityServer.Application.Authorization.Accounts.Dto;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imtudou.IdentityServer.Application.Authorization.Accounts
{
    public interface IAccountService
    {
        /// <summary>
        /// 手机号注册
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<RegisterOutput> RegisterPhoneAsync(RegisterInput input);
    }
}
