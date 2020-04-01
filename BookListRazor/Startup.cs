using BookListRazor.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BookListRazor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Called by dependency injection 
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Database connections - added to the pipeline and usable in the application.
            services.AddDbContext<ApplicationDbContext>(option => 
                option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Because we selected razor pages when creating the project.
            services.AddRazorPages().AddRazorRuntimeCompilation();

            // Need a service for API if we are going to use APIs
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // Some middlewares in a pipeline: Authentication, MVC, Static Files
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // If the environment is development we call this exception code for errors.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            // Middlewares in the Pipeline
            //
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();

                // Need to add to API to middleware as well, not sure why yet.
                endpoints.MapControllers();
            });
        }
    }
}
