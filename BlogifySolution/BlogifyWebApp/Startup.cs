using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace BlogifyWebApp
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
            //2021-01-13 - Kadel D. Lacatt
            //.NET Core DI services registration
            services.AddScoped<BlogifyWebApp.Models.Interfaces.IBlogProvider, BlogifyWebApp.Models.Providers.BlogProvider>();
            services.AddScoped<BlogifyWebApp.Models.Interfaces.IAuthProvider, BlogifyWebApp.Models.Providers.AuthProvider>();
            services.AddScoped<BlogifyWebApp.Models.Interfaces.IBlog, BlogifyWebApp.Models.EF.Blog>();
            services.AddScoped<BlogifyWebApp.Models.Interfaces.ICategory, BlogifyWebApp.Models.EF.Category>();
            services.AddScoped<BlogifyWebApp.Models.Interfaces.IUser, BlogifyWebApp.Models.EF.User>();
            services.AddScoped<BlogifyWebApp.Models.Interfaces.IComment, BlogifyWebApp.Models.EF.Comment>();

            services.AddControllersWithViews();

            //Register and Create the authentication Middleware
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options => {});
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

            //Configuring the use of authentication and authorization for the app.
            app.UseAuthentication();
            app.UseAuthorization();
            
            //Setting up cookie policy for authentications
            var cookiePolicyOptions = new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
            };
            app.UseCookiePolicy(cookiePolicyOptions);


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
