using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationCenter.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AuthenticationCenter
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddControllers();
            services.AddControllersWithViews();
            #region 客户端
            //services.AddIdentityServer()//怎么处理
            //  .AddDeveloperSigningCredential()//默认的开发者证书--临时证书--生产环境为了保证token不失效，证书是不变的
            //  .AddInMemoryClients(ClientInitConfig.GetClients())//InMemory 内存模式
            //  .AddInMemoryApiResources(ClientInitConfig.GetApiResources());//能访问啥资源
            #endregion
            #region 账号密码
            //services.AddIdentityServer()//怎么处理
            //  .AddDeveloperSigningCredential()//默认的开发者证书--临时证书--生产环境为了保证token不失效，证书是不变的
            //  .AddInMemoryClients(PasswordInitConfig.GetClients())//InMemory 内存模式
            //  .AddInMemoryApiResources(PasswordInitConfig.GetApiResources())//能访问啥资源
            //  .AddTestUsers(PasswordInitConfig.GetUsers())
            //  ;
            #endregion
            #region 隐藏模式
            //services.AddIdentityServer()
            //    .AddDeveloperSigningCredential()//默认的开发者证书 
            //   .AddInMemoryApiResources(ImplicitInitConfig.GetApiResources()) //API访问授权资源
            //   .AddInMemoryClients(ImplicitInitConfig.GetClients())//客户端
            //   .AddTestUsers(ImplicitInitConfig.GetUsers()); //添加用户
            #endregion
            #region Code模式
            //services.AddIdentityServer()
            //    .AddDeveloperSigningCredential()//默认的开发者证书 
            //   .AddInMemoryApiResources(CodeInitConfig.GetApiResources()) //API访问授权资源
            //   .AddInMemoryClients(CodeInitConfig.GetClients())//客户端
            //   .AddTestUsers(CodeInitConfig.GetUsers()); //添加用户

            //mvc 客户端
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()//默认的开发者证书 
                 .AddInMemoryIdentityResources(MvcOidcConfig.GetIdentityResources())//身份信息授权资源
               .AddInMemoryApiResources(MvcOidcConfig.GetApiResources()) //API访问授权资源
               .AddInMemoryClients(MvcOidcConfig.GetClients())//客户端
               .AddTestUsers(MvcOidcConfig.GetUsers()) //测试用户
               //.AddResourceOwnerValidator<CustomResourceOwnerPasswordValidator>()
               ;
            #endregion
            #region Hybrid模式
            //services.AddIdentityServer()
            //    .AddDeveloperSigningCredential()//默认的开发者证书 
            //    .AddInMemoryIdentityResources(HybridInitConfig.GetIdentityResources())//身份信息授权资源
            //   .AddInMemoryApiResources(HybridInitConfig.GetApiResources()) //API访问授权资源
            //   .AddInMemoryClients(HybridInitConfig.GetClients())//客户端
            //   .AddTestUsers(HybridInitConfig.GetUsers()); //添加用户
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(
               new StaticFileOptions()
               {
                   FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot"))
               });
            app.UseIdentityServer();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
          name: "default",
          pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
