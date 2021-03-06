using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogifyWebApi
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
            services.AddScoped<BlogifyWebApi.Models.Interfaces.IBlogProvider, BlogifyWebApi.Models.Providers.BlogProvider>();
            services.AddScoped<BlogifyWebApi.Models.Interfaces.IBlog, BlogifyWebApi.Models.EF.Blog>();
            services.AddScoped<BlogifyWebApi.Models.Interfaces.ICategory, BlogifyWebApi.Models.EF.Category>();
            services.AddScoped<BlogifyWebApi.Models.Interfaces.IUser, BlogifyWebApi.Models.EF.User>();
            services.AddScoped<BlogifyWebApi.Models.Interfaces.IComment, BlogifyWebApi.Models.EF.Comment>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BlogifyWebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BlogifyWebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
