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
            // services.AddControllers();//Web Api ʹ������͹���
            // ע��IClock����
            services.AddSingleton<IClock, ChinaClock>();
            services.AddSingleton<IEmployeeService, EmployeeService>();
            services.AddSingleton<IDepartmentService, DepartmentService>();
            services.Configure<ThreeOptions>(_configuration.GetSection("Three"));
        }

        /// <summary>
        /// Լ���������ǰ������Development����Ĭ�ϻ���ConfigureDevelopment������÷���
        /// �����ǰ������Production������û�ҵ�ConfigureProduction��������������Configure�������
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        //public void ConfigureDevelopment(IApplicationBuilder app, IWebHostEnvironment env)
        //{
        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //    }

        //    app.UseStaticFiles();//û�������̬�ļ�û��չʾ

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

            app.UseStaticFiles();//��̬�ļ���js��css֮��

            app.UseHttpsRedirection();//����ǿ���û�ʹ��https����

            app.UseAuthentication();//�����֤Ҫ���ڶ˵�֮ǰ           

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
