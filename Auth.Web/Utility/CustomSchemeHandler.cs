using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Web.Utility
{
    public class CustomSchemeHandler : IAuthenticationHandler
    {
        public AuthenticationScheme Scheme { get; private set; }
        protected HttpContext Context { get; private set; }
        readonly ILogger<CustomSchemeHandler> _logger;
        public CustomSchemeHandler(ILogger<CustomSchemeHandler> logger)
        {
            _logger = logger;
        }
        public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
        {
            Scheme = scheme;
            Context = context;
            return Task.CompletedTask;
        }
        public Task<AuthenticateResult> AuthenticateAsync()
        {
            throw new NotImplementedException();
        }

        public  Task ChallengeAsync(AuthenticationProperties properties)
        {
            _logger.LogInformation("ChallengeAsync");

            //隐藏模式跳转地址
            //Context.Response.Redirect( "http://localhost:7000/connect/authorize?client_id=client&redirect_uri=http://localhost:7002/AuthenticationSuccess&response_type=token&scope=AuthApi");
            //授权码跳转地址
            //Context.Response.Redirect("http://localhost:7000/connect/authorize?client_id=client&redirect_uri=http://localhost:7002/AuthenticationSuccess&response_type=code&scope=AuthApi");
            //混合模式
            Context.Response.Redirect("http://localhost:7000/connect/authorize?client_id=client&redirect_uri=http://localhost:7002/AuthenticationSuccess&response_type=code%20token%20id_token&scope=AuthApi%20openid%20CustomIdentityResource&response_model=fragment&nonce=12345");

            return Task.CompletedTask;
        }

        public Task ForbidAsync(AuthenticationProperties properties)
        {
            //_logger.LogInformation("ForbidAsync");
            throw new NotImplementedException();
        }

      
    }
}
