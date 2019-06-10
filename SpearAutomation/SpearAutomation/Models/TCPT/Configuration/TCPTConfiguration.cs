using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpearAutomation.Models.TCPT.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpearAutomation.Models.TCPT.Configuration
{
    public static class TCPTConfiguration
    {
        public static IServiceCollection ConfigureTCPTServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITCPTRepository, TCPTRepository>();
            return services.ConfigureTCPTData(configuration);
        }

        private static IServiceCollection ConfigureTCPTData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SPEARTCPTContext>(options => options.UseSqlite(@"Data Source = SpearTcpt.db"));

            try
            {
                var context = services.GetService<SPEARTCPTContext>();
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
