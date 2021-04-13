using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VOD.Common.DTOModels.UI;
using VOD.Common.Entities;
using VOD.Database.Contexts;
using VOD.Database.Migrations;
using VOD.Database.Services;

namespace VOD.UI
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<VODContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<VODUser>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<VODContext>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<IDbReadService, DbReadService>();
            services.AddScoped<IUIReadService, UIReadService>();

            //11장
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Video, VideoDTO>();
                cfg.CreateMap<Download, DownloadDTO>()
                   .ForMember(dest => dest.DownloadUrl, src => src.MapFrom(s => s.Url))
                   .ForMember(dest => dest.DownloadTitle, src => src.MapFrom(s => s.Title));

                cfg.CreateMap<Instructor, InstructorDTO>()
                   .ForMember(dest => dest.InstructorName, src => src.MapFrom(s => s.Name))
                   .ForMember(dest => dest.InstructorDescription, src => src.MapFrom(s => s.Description))
                   .ForMember(dest => dest.InstructorAvatar, src => src.MapFrom(s => s.Thumbnail));

                cfg.CreateMap<Course, CourseDTO>()
                   .ForMember(dest => dest.CourseId, src => src.MapFrom(s => s.Id))
                   .ForMember(dest => dest.CourseTitle, src => src.MapFrom(s => s.Title))
                   .ForMember(dest => dest.CourseDescription, src => src.MapFrom(s => s.Description))
                   .ForMember(dest => dest.MarqueeImageUrl, src => src.MapFrom(s => s.MarqueeImageUrl))
                   .ForMember(dest => dest.CourseImageUrl, src => src.MapFrom(s => s.ImageUrl));
                
                cfg.CreateMap<Module, ModuleDTO>()
                   .ForMember(dest => dest.ModuleTitle, src => src.MapFrom(s => s.Title));


            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);


        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env, VODContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            // Uncomment to recreate the database. ALL DATA WILL BE LOST !
            // DbInitializer.RecreateDatabase(db);
            //Uncomment to seed the database
            DbInitializer.Initialize(db);


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
