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
            //2020-01-13 - Kadel D. Lacatt
            //.NET Core DI services registration
            services.AddScoped<BlogifyWebApp.Models.Interfaces.IBlogProvider, BlogifyWebApp.Models.Providers.BlogProvider>();
            services.AddScoped<BlogifyWebApp.Models.Interfaces.IBlog, BlogifyWebApp.Models.EF.Blog>();
            services.AddScoped<BlogifyWebApp.Models.Interfaces.ICategory, BlogifyWebApp.Models.EF.Category>();
            services.AddScoped<BlogifyWebApp.Models.Interfaces.IUser, BlogifyWebApp.Models.EF.User>();
            services.AddScoped<BlogifyWebApp.Models.Interfaces.IComment, BlogifyWebApp.Models.EF.Comment>();

            services.AddControllersWithViews();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
