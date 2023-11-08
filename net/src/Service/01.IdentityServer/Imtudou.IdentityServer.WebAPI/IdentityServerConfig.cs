using IdentityServer4;
using IdentityServer4.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Imtudou.IdentityServer.Web
{
    /// <summary>
    /// 1.配置受保护的资源列表
    /// 2.配置允许验证的Client
    /// </summary>
    public class IdentityServerConfig
    {
        /// <summary>
        /// 配置API允许访问的范围
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope> {
                new ApiScope("ApiScope")
            };
        
        }

        /// <summary>
        /// 配置身份资源
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource> { 
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Phone(),
                new IdentityResources.Address(),
                new IdentityResources.Email(),
            };
        
        }

        /// <summary>
        ///  配置受保护的资源列表
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource> {
                new ApiResource("ApiResource")
                {
                    Scopes = { "ApiScope" }
                }
            };
        
        }

        /// <summary>
        /// 配置允许验证的Client
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                { 
                    ClientId = "ClientId",
                    ClientSecrets = new List<Secret>{ new Secret("Secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                    AllowOfflineAccess = true,
                    AllowedScopes = new List<string>{
                        "ApiScope" ,   //此处对应的是ApiScopes 中的配置
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        IdentityServerConstants.StandardScopes.Phone,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Email
                    }
                }
            };      
        }


    }
}
