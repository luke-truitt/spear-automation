using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpearAutomation.Models.MOL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpearAutomation.Models.MOL.Configuration
{
    public static class MOLConfiguration
    {
        public static IServiceCollection ConfigureMOLServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMOLRepository, MOLRepository>();
            return services.ConfigureMOLData(configuration);
        }

        private static IServiceCollection ConfigureMOLData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SPEARMOLContext>(options => options.UseSqlServer("Server=.;Database=MOL;Trusted_Connection=True;MultipleActiveResultSets=true"));

            try
            {
                var context = services.GetService<SPEARMOLContext>();
                context.Database.Migrate();

            }
            catch (Exception ex)
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
