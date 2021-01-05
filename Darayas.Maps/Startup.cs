//https://ghorbani.dev
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Darayas.Maps.DAL.Repository;
using Darayas.Maps.DAL.Models;
using Darayas.Maps.DAL;
using Darayas.Maps.Ul.Data;
using Darayas.Maps.Models.ViewModels;

namespace Darayas.Maps
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
            services.AddDbContext<MainContext>(options =>
                options.UseSqlServer(new ConnStr().GetConn()));

            services.AddDefaultIdentity<tblUsers>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<MainContext>();

            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

            services
                .AddRazorPages()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AddPageRoute("/Home/Index", "");
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();

                AppSettings.Set(new vmAppSettings()
                {
                    SiteTitle = "DotnetLearn Maps",
                    SiteDescreption = "آموزش طراحی سایت مشابه Google Map با استفاده از Leaflet JS در سایت DotnetLearn",
                    SiteUrl = "https://localhost:5001"
                });
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

                AppSettings.Set(new vmAppSettings()
                {
                    SiteTitle = "DotnetLearn Maps",
                    SiteDescreption = "آموزش طراحی سایت مشابه Google Map با استفاده از Leaflet JS در سایت DotnetLearn",
                    SiteUrl = "http://maps.demos.dotnetlearn.com"
                });
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
