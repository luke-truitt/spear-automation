using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpearAutomation.Models.Logger.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpearAutomation.Models.Logger.Configuration
{
    public static class LoggerConfiguration
    {
        public static IServiceCollection ConfigureLoggerServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services.ConfigureLoggerData(configuration);
        }

        private static IServiceCollection ConfigureLoggerData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LoggerContext>(options => options.UseSqlite("Data Source = SpearLogging.db"));

            try
            {
                var context = services.GetService<LoggerContext>();
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
