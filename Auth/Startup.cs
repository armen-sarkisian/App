using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.Controllers;
using Auth.DAO;
using Auth.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auth
{
    public class Startup
    {
        AccountController acc;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            acc = new AccountController();
            acc.Logout();

            using (var client = new ContextAuth())
            {
                client.Database.EnsureCreated();
            }
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            // установка конфигурации подключения
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => //CookieAuthenticationOptions
                {
                    options.LoginPath = new PathString("/Account/Login");
                    /*options.Cookie.Name = "CookieName";
                    options.Cookie.Expiration = TimeSpan.FromSeconds(5);
                    options.ExpireTimeSpan = TimeSpan.FromSeconds(5);
                    options.SlidingExpiration = true;*/
                }
                );


           // services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddMvc();
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseSession();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

