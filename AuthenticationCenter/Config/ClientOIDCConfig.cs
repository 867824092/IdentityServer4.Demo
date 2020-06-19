using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthenticationCenter.Config
{
    public class ClientOIDCConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
               new IdentityResources.OpenId(),
               new IdentityResources.Profile(),
               new IdentityResource("mvcProfile",new List<string>
               {
                   IdentityModel.JwtClaimTypes.Role,
                   IdentityModel.JwtClaimTypes.NickName,
                   "eMail"
               })
            };
        }
        /// <summary>
        /// 定义ApiResource   
        /// 这里的资源（Resources）指的就是管理的API
        /// </summary>
        /// <returns>多个ApiResource</returns>
        public static IEnumerable<ApiResource> GetApiResources() => new[]
        {
                new ApiResource("OIDCClient", "用户获取API")
        };
        

        public static List<TestUser> GetUsers() => new List<TestUser>()
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


        /// <summary>
        /// 定义验证条件的Client
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients() => new[]
            {
                 new Client
                 {
                      ClientId = "mvc",
                      ClientSecrets = { new Secret("secret".Sha256()) },
                      //AllowedGrantTypes = GrantTypes.Code,
                      //RequirePkce = true, 如果授权码模式取消注释
                      AllowedGrantTypes = GrantTypes.Hybrid,
                      AlwaysIncludeUserClaimsInIdToken = true, //IdToken包含Claims信息 必须为true
                      AllowAccessTokensViaBrowser = true,
                      RequireConsent = false,
                      RedirectUris = { "http://localhost:7003/signin-oidc" },
                      PostLogoutRedirectUris = { "http://localhost:7003/signout-callback-oidc" },
                      AllowOfflineAccess = true,
                      AllowedScopes = new List<string>
                      {
                          IdentityServerConstants.StandardScopes.OfflineAccess,
                          IdentityServerConstants.StandardScopes.OpenId,
                          IdentityServerConstants.StandardScopes.Profile,
                          "mvcProfile"
                      }
                 },
                 new Client
                 {
                       ClientId = "js",
                       ClientName = "JavaScript Client",
                       AllowedGrantTypes = GrantTypes.Code,
                       RequirePkce = true,
                       RequireClientSecret = false,
                       RedirectUris =           { "http://localhost:7004/callback.html" },
                       PostLogoutRedirectUris = { "http://localhost:7004/index.html" },
                       AllowedCorsOrigins =     { "http://localhost:7004" },

                       AllowedScopes =
                       {
                         "OIDCClient",
                           IdentityServerConstants.StandardScopes.OpenId,
                           IdentityServerConstants.StandardScopes.Profile,
                           "mvcProfile"
                       }
                 },
                 new Client
                 {
                      ClientId = "spa",
                      ClientSecrets = { new Secret("secret".Sha256()) },
                      AllowedGrantTypes = GrantTypes.Hybrid,
                      AlwaysIncludeUserClaimsInIdToken = true, //IdToken包含Claims信息 必须为true
                      AllowAccessTokensViaBrowser = true,
                      RequireConsent = false,
                      RedirectUris = { "http://localhost:7003/signin-oidc" },
                      PostLogoutRedirectUris = { "http://localhost:7003/signout-callback-oidc" },
                      AllowedScopes = new List<string>
                      {
                          IdentityServerConstants.StandardScopes.OpenId,
                          IdentityServerConstants.StandardScopes.Profile,
                          "mvcProfile"
                      }
                 }
            };
    }
}
