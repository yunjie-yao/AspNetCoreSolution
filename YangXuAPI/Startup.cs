using System;
using System.Text;
using AutoMapper;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using YangXuAPI.Data;
using YangXuAPI.DtoParameters;
using YangXuAPI.Services;

namespace YangXuAPI
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;

                    var token = Configuration.GetSection("TokenParameter")
                        .Get<TokenDtoParameter>();
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token.Secret)),
                        ValidIssuer = token.Issuer,
                        ValidateIssuer = true,
                        ValidateAudience = false,
                    };
                });
            services.AddHttpCacheHeaders(expires =>
            {
                expires.MaxAge = 60;
                expires.CacheLocation = CacheLocation.Private;
            }, validation =>
            {
                // ������ڣ�����������֤
                validation.MustRevalidate = true; 

            });

            services.AddResponseCaching();
            services.AddControllers(setup =>
                {
                    setup.ReturnHttpNotAcceptable = true;
                    //setup.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());//1.���xml���,Ĭ��json
                    //setup.OutputFormatters.Insert(0,new XmlDataContractSerializerOutputFormatter());//2.xml���뵽��0λ�ã���ʾ����Ĭ�����Ϊxml

                    //����һ��ȫ��CacheProfile
                    setup.CacheProfiles.Add("120sCacheProfiles",new CacheProfile()
                    {
                        Duration = 120
                    });
                })
                .AddNewtonsoftJson(setup =>
                {
                    setup.SerializerSettings.ContractResolver=new CamelCasePropertyNamesContractResolver();
                })
                .AddXmlDataContractSerializerFormatters() //3.core3.0֮�����µ�д������д��������
                .ConfigureApiBehaviorOptions(setup =>
                {
                    setup.InvalidModelStateResponseFactory = context =>
                    {
                        var problemDetails = new ValidationProblemDetails(context.ModelState)
                        {
                            Type = "http://www.baidu.com", //����һ�����Ӹ��û�����������ʱ����鿴
                            Title = "�ִ��ˣ���",
                            Status = StatusCodes.Status422UnprocessableEntity,
                            Detail = "�뿴��ϸ��Ϣ",
                            Instance = context.HttpContext.Request.Path
                        };
                        problemDetails.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);

                        return new UnprocessableEntityObjectResult(problemDetails)
                        {
                            ContentTypes = {"application/problem+json"}
                        };
                    };
                });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<ICompanyRepository, CompanyRepository>();

            services.AddDbContext<RoutineDbContext>(options =>
                options.UseMySQL(Configuration.GetConnectionString("dbconn")));

            services.AddTransient<IPropertyMappingService, PropertyMappingService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //˼����ô��¼��־
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("Unexpected Error");
                    });
                });
            }

            // ΢�������м��û��ʵ����֤ģ��
            // app.UseResponseCaching();

            app.UseHttpCacheHeaders();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
