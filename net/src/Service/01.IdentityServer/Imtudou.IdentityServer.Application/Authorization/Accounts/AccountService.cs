using Imtudou.Core.Cache;
using Imtudou.Core.Cache.StackExchangeRedis;
using Imtudou.IdentityServer.Application.Authorization.Accounts.Dto;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imtudou.IdentityServer.Application.Authorization.Accounts
{
    public class AccountService : IAccountService
    {

        private readonly IRedisCache redisHelper;

        public AccountService()
        {
            this.redisHelper = this.redisHelper = new RedisCache(new RedisOptions("127.0.0.1:6379,allowadmin=true,connectTimeout=1000,connectRetry=1,syncTimeout=10000", 0)); ;
        }


        /// <summary>
        /// 手机号注册
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<RegisterOutput> RegisterPhoneAsync(RegisterInput input)
        {
            await Task.CompletedTask;
            return new RegisterOutput { Phone = input.Phone, RegisterTime = DateTime.Now };

        }
    }
}
