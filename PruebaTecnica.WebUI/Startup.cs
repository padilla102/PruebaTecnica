using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using PruebaTecnica.Data.Context;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Domain.Abstract;
using PruebaTecnica.Domain.Implements;
using AutoMapper;
using PruebaTecnica.Domain.DTOs;
using Newtonsoft.Json;

namespace PruebaTecnica.WebUI
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
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddControllersWithViews();

            services.AddDbContext<ApplicationDbContext>(
                 options => options.UseSqlServer(Configuration.GetConnectionString("AzureConnection")));

            services.AddTransient<IMunicipioRepository, MunicipioRepository>();
            services.AddTransient<IRegionRepository,RegionRepository>();
            services.AddTransient<IStatusRepository, StatusRepository>();
            services.AddTransient<IRegionMunicipioRepository, RegionMunicipioRepository>();


            services.AddMvc(options => options.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });


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
                app.UseExceptionHandler("/Municipio/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMvc(routes => {
                routes.MapRoute(
                name: "paginationMunicipio",
                template: "Municipio/Page{page}",
                defaults: new { Controller = "Municipio", action = "List" });

                routes.MapRoute(
                name: "paginationRegion",
                template: "Region/Page{page}",
                defaults: new { Controller = "Region", action = "List" });

                routes.MapRoute(
                name: "default",
                template: "{controller=Municipio}/{action=List}/{id?}");
            });

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Municipio}/{action=List}/{id?}");
            //});

            //SeedDatabase.Initialize(app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider);
        }
    }
}
