using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ScrapIt.DAL.Contracts;
using ScrapIt.DAL.Implementations;
using ScrapIt.Domain.Contracts;
using ScrapIt.Domain.Implementations.Services;
using System;

namespace ScrapIt
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
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ScrapIt API", Version = "v1" });
                c.IncludeXmlComments(GetXmlCommentsPath());
            });

            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("MSSQLConnection"),
                    assembly => assembly.MigrationsAssembly("ScrapIt.DAL.Migrations"));
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddTransient<IWebScraperService, WebScrapperService>();
            services.AddTransient<ITaskScrapperService, TaskScrapperService>();
            services.AddTransient<IDbRepository, DbRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("swagger/v1/swagger.json", "ScrupIt API v1");
                x.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static string GetXmlCommentsPath()
        {
            return String.Format(@"{0}\ScrapIt.Web.XML", AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
