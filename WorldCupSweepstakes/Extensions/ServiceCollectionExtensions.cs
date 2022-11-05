using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldCupSweepstakes.Services;
using WorldCupSweepstakes.Settings;

namespace WorldCupSweepstakes.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            var oddsSettings = configuration.GetSection("OddsApi").Get<OddsApiSettings>();
            var playerSettings = configuration.GetSection("Players").Get<PlayersSettings>();

            services.AddTransient(oddsSettings);
            services.AddTransient(playerSettings);

            services.AddTransient<IOddsService, OddsService>();
            services.AddTransient<ISweepstakeService, SweepstakeService>();

            return services;
        }
    }
}
