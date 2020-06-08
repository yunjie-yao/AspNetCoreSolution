using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using System;
using System.Text;
using YangXuAPI.Data;
using YangXuAPI.DtoParameters;

namespace YangXuAPI.Helpers
{
    public static class ServiceExtensions
    {
        public static void ConfigureAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;

                    var token = configuration.GetSection("TokenParameter")
                        .Get<TokenDtoParameter>();
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token.Secret)),
                        ValidIssuer = token.Issuer,
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        // 不设置默认会有5min的缓冲时间，也就是真正的过期时间=自己设置的expired+5
                        ClockSkew = TimeSpan.Zero
                    };
                });
        }

        public static void ConfigureHttpCacheHeaders(this IServiceCollection services)
        {
            services.AddHttpCacheHeaders(expires =>
            {
                expires.MaxAge = 60;
                expires.CacheLocation = CacheLocation.Private;
            }, validation =>
            {
                // 缓存过期，必须重新验证
                validation.MustRevalidate = true;

            });
        }

        public static void ConfigureResponseCaching(this IServiceCollection services)
        {
            services.AddResponseCaching();
        }

        public static void ConfigureControllers(this IServiceCollection services)
        {
            services.AddControllers(setup =>
            {
                setup.ReturnHttpNotAcceptable = true;
                //setup.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());//1.添加xml输出,默认json
                //setup.OutputFormatters.Insert(0,new XmlDataContractSerializerOutputFormatter());//2.xml插入到第0位置，表示更改默认输出为xml

                //定义一个全局CacheProfile
                setup.CacheProfiles.Add("120sCacheProfiles", new CacheProfile()
                {
                    Duration = 120
                });
            })
                .AddNewtonsoftJson(setup =>
                {
                    setup.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                })
                .AddXmlDataContractSerializerFormatters() //3.core3.0之后最新的写法三种写法都可以
                .ConfigureApiBehaviorOptions(setup =>
                {
                    setup.InvalidModelStateResponseFactory = context =>
                    {
                        var problemDetails = new ValidationProblemDetails(context.ModelState)
                        {
                            Type = "http://www.baidu.com", //返回一个链接给用户，发生错误时点击查看
                            Title = "又错了！！",
                            Status = StatusCodes.Status422UnprocessableEntity,
                            Detail = "请看详细信息",
                            Instance = context.HttpContext.Request.Path
                        };
                        problemDetails.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);

                        return new UnprocessableEntityObjectResult(problemDetails)
                        {
                            ContentTypes = { "application/problem+json" }
                        };
                    };
                });
        }

        public static void ConfigureMySqlDbContext(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<RoutineDbContext>(options =>
                options.UseMySQL(configuration.GetConnectionString("dbconn")));
        }
    }
}
