using System;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.AspNetCore.SignalR;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Runtime.Caching.Redis;
using Abp.Zero.Configuration;
using JD.CRS.Authentication.JwtBearer;
using JD.CRS.Configuration;
using JD.CRS.EntityFrameworkCore;

namespace JD.CRS
{
    [DependsOn(
         typeof(CRSApplicationModule),
         typeof(CRSEntityFrameworkModule),
         typeof(AbpRedisCacheModule),
         typeof(AbpAspNetCoreModule)
        ,typeof(AbpAspNetCoreSignalRModule)
     )]
    public class CRSWebCoreModule : AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public CRSWebCoreModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                CRSConsts.ConnectionStringName
            );

            // Use database for language management
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            Configuration.Modules.AbpAspNetCore()
                 .CreateControllersForAppServices(
                     typeof(CRSApplicationModule).GetAssembly()
                 );
            //设置所有缓存的默认过期时间(必须放在使用redis缓存之前)
            Configuration.Caching.ConfigureAll(cache=>
            {
                cache.DefaultAbsoluteExpireTime=TimeSpan.FromMinutes(2);
            });
            //设置某个缓存的默认过期时间 根据 "CacheName" 来区分(必须放在使用redis缓存之前
            Configuration.Caching.Configure("Orgnazation",cache=>
            {
                cache.DefaultAbsoluteExpireTime=TimeSpan.FromMinutes(2);
            });

            Configuration.Caching.UseRedis(opt=>
            {
                opt.ConnectionString=_appConfiguration["App:RedisCache:ConnectionString"];
                opt.DatabaseId=_appConfiguration.GetValue<int>("app:RedisCache:DatabaseId");
            });

            ConfigureTokenAuth();
        }

        private void ConfigureTokenAuth()
        {
            IocManager.Register<TokenAuthConfiguration>();
            var tokenAuthConfig = IocManager.Resolve<TokenAuthConfiguration>();

            tokenAuthConfig.SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appConfiguration["Authentication:JwtBearer:SecurityKey"]));
            tokenAuthConfig.Issuer = _appConfiguration["Authentication:JwtBearer:Issuer"];
            tokenAuthConfig.Audience = _appConfiguration["Authentication:JwtBearer:Audience"];
            tokenAuthConfig.SigningCredentials = new SigningCredentials(tokenAuthConfig.SecurityKey, SecurityAlgorithms.HmacSha256);
            tokenAuthConfig.Expiration = TimeSpan.FromDays(1);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CRSWebCoreModule).GetAssembly());
        }
    }
}
