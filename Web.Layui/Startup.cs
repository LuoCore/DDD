using Infrastructure.CrossCutting.IoC;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Layui
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
            services.AddControllersWithViews();
            //解析上下文,HttpContext.Current会围绕这个业务逻辑
            services.AddHttpContextAccessor();
            //用于配置 HstsOptions 的委托。
            services.AddHsts(options =>
            {
                options.Preload = true;//设置严格传输安全标头的预载参数。
                options.IncludeSubDomains = true;//启用严格传输-安全标头的 includeSubDomain 参数。
                options.MaxAge = TimeSpan.FromDays(60);//设置严格传输安全标头的最大生存期参数。
                options.ExcludedHosts.Add("");//不会添加 HSTS 标头的主机名列表
            });
            services.AddSameSiteCookiePolicy();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddDistributedMemoryCache();//分布式内存缓存
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(1000 * 60 * 20);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

           



            // Adding MediatR for Domain Events
            // 领域命令、领域事件等注入
            // 引用包 MediatR.Extensions.Microsoft.DependencyInjection
            services.AddMediatR(typeof(Startup));

            // .NET Core 原生依赖注入
            // 单写一层用来添加依赖项，从展示层 Presentation 中隔离
            NativeInjectorBootStrapper.RegisterServices(services);

           
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
           
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Main}/{id?}"
                );
            });
       
        }
    }
}
