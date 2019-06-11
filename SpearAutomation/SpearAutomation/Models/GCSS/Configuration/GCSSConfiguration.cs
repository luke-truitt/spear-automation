using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpearAutomation.Models.GCSS.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpearAutomation.Models.GCSS.Configuration
{
    public static class GCSSConfiguration
    {
        public static IServiceCollection ConfigureGCSSServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IGCSSRepository, GCSSRepository>();
            return services.ConfigureGCSSData(configuration);
        }

        private static IServiceCollection ConfigureGCSSData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SPEARGCSSContext>(options => options.UseSqlServer("Server=.;Database=GCSS;Trusted_Connection=True;MultipleActiveResultSets=true"));

            try
            {
                var context = services.GetService<SPEARGCSSContext>();
                context.Database.Migrate();
              
            } catch(Exception ex)
            {

            }

            return services;
        }

        private static TType GetService<TType>(this IServiceCollection services)
        {
            var temporaryProvider = services.BuildServiceProvider();
            var service = temporaryProvider.GetService<TType>();
            return service;
        }
    }
}
