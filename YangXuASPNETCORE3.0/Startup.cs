using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YangXuASPNETCORE3._0.Services;

namespace YangXuASPNETCORE3._0
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            //var therr = _configuration["Three:BoldDepartmentEmployeeCountThreshold"];
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            // services.AddControllers();//Web Api 使用这个就够了
            // 注册IClock服务
            services.AddSingleton<IClock, ChinaClock>();
            services.AddSingleton<IEmployeeService, EmployeeService>();
            services.AddSingleton<IDepartmentService, DepartmentService>();
            services.Configure<ThreeOptions>(_configuration.GetSection("Three"));
        }

        /// <summary>
        /// 约定，如果当前环境是Development，则默认会走ConfigureDevelopment这个配置方法
        /// 如果当前环境是Production，由于没找到ConfigureProduction这个方法，则会走Configure这个方法
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        //public void ConfigureDevelopment(IApplicationBuilder app, IWebHostEnvironment env)
        //{
        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //    }

        //    app.UseStaticFiles();//没有这个静态文件没法展示

        //    app.UseHttpsRedirection();

        //    app.UseAuthentication();

        //    app.UseRouting();

        //    app.UseEndpoints(endpoints =>
        //    {
        //        endpoints.MapGet("/", async context =>
        //        {
        //            await context.Response.WriteAsync("Hello World!");
        //        });
        //    });

        //}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //env.IsProduction();
            //env.IsEnvironment("Custome");

            app.UseStaticFiles();//静态文件，js，css之类

            app.UseHttpsRedirection();//可以强制用户使用https请求

            app.UseAuthentication();//身份认证要放在端点之前           

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Department}/{action=index}/{id?}");
            });
        }
    }
}
