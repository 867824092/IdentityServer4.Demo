using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthenticationCenter
{
    /// <summary>
    /// 密码模式
    /// </summary>
    public class PasswordInitConfig
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
                new ApiResource("AuthApi", "用户获取API"
                ,new List<string>(){IdentityModel.JwtClaimTypes.Role,"eMail" }
                )
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>()
            {
                new TestUser()
                {
                     Username="林淼",
                     Password="123456",
                     SubjectId="0",
                     Claims=new List<Claim>(){
                        new Claim(IdentityModel.JwtClaimTypes.Role,"Admin"),
                        new Claim(IdentityModel.JwtClaimTypes.NickName,"Eleven"),
                        new Claim("eMail","57265177@qq.com")
                    }
                }
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
                    ClientSecrets = new [] { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,//密码模式
                    AllowedScopes = new [] {  "AuthApi" },//允许访问的资源
                    //cliam无效
                }
            };
        }
    }
}


