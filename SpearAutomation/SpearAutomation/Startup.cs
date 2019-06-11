using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SpearAutomation.BackgroundServices;
using SpearAutomation.Models.GCSS;
using SpearAutomation.Models.GCSS.Configuration;
using SpearAutomation.Models.Logger.Configuration;
using SpearAutomation.Models.Logger.Data;
using SpearAutomation.Models.Logger.Model;
using SpearAutomation.Models.MOL;
using SpearAutomation.Models.MOL.Configuration;
using SpearAutomation.Models.Services;
using SpearAutomation.Models.TCPT;
using SpearAutomation.Models.TCPT.Configuration;

namespace SpearAutomation
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
            services.ConfigureGCSSServices(Configuration);
            services.ConfigureMOLServices(Configuration);
            services.ConfigureTCPTServices(Configuration);
            services.ConfigureLoggerServices(Configuration);

            services.AddScoped<ISpearService, SpearService>();

            // services.AddHostedService<FiveSecondBackgroundService>();
            services.AddHostedService<ThirtySecondBackgroundService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
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
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
