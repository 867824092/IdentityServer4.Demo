using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthenticationCenter
{
    /// <summary>
    /// 客户端模式
    /// </summary>
    public class ClientInitConfig
    {
        /// <summary>
        /// 定义ApiResource   
        /// 这里的资源（Resources）指的就是管理的API
        /// </summary>
        /// <returns>多个ApiResource</returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource("AuthApi", "资源服务器Api")
            };
        }

        /// <summary>
        /// 定义验证条件的Client
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "client",//客户端惟一标识
                    ClientSecrets = new [] { new Secret("secret".Sha256()) },//客户端密码，进行了加密
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    //授权方式，客户端认证，只要ClientId+ClientSecrets
                    AllowedScopes = new [] { "AuthApi" }//允许访问的资源
                }
            };
        }
    }
}
