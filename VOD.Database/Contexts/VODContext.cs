using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
//using System.Reflection;
using System.Text;
using VOD.Common.Entities;
using System.Linq;
using VOD.Database.Migrations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace VOD.Database.Contexts
{
    public class VODContext : IdentityDbContext<VODUser>
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Download> Downloads { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
        public DbSet<Video> Videos { get; set; }


        public VODContext(DbContextOptions<VODContext> options)
 : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Composite key
            builder.Entity<UserCourse>().HasKey(uc =>
            new { uc.UserId, uc.CourseId });
            // Restrict cascading deletes
            foreach (var relationship in builder.Model.GetEntityTypes()
            .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
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
            //DbInitializer.RecreateDatabase(db);
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

