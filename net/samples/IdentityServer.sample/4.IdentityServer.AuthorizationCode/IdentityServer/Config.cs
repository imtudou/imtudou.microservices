using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class Config 
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(), // subject id
                new IdentityResources.Profile(),// （名字，姓氏等）
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("api", "My API")
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("api", "My ApiScope")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "JavaScript.Client",
                    ClientName = "JavaScript Client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.Code, //4.授权码模式
                    RequirePkce = true,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                                                
                    // 登录成功回调处理地址，处理回调返回的数据
                    RedirectUris = { "https://localhost:5002/callback.html" }, //(为了避免麻烦最好直接配置成https)

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost:5002/index.html" }, //(为了避免麻烦最好直接配置成https)
                    AllowedCorsOrigins =     { "https://localhost:5002" },

                    RequireConsent = true, // 是否启用同意授权页面
                    // scopes that client has access to
                    AllowedScopes = {
                       "api",
                       IdentityServerConstants.StandardScopes.OpenId,
                       IdentityServerConstants.StandardScopes.Profile 
                   }
                },
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "zhangsan",
                    Password = "password",
                    Claims = new List<Claim>
                    {
                        new Claim("name","zhangsan"),
                        new Claim("website","https://zhangsan.com"), 
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "lisi",
                    Password = "password",
                    Claims = new List<Claim>
                    {
                        new Claim("name","lisi"),
                        new Claim("website","https://lisi.com"),
                    }
                },
                new TestUser
                {
                    SubjectId = "3",
                    Username = "admin",
                    Password = "12345",
                    Claims = new List<Claim>
                    {
                        new Claim("name","admin"),
                        new Claim("website","https://admin.com"),
                    }
                }
            };
        }

    }
}
