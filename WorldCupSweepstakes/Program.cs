using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using WorldCupSweepstakes.Services;

namespace WorldCupSweepstakes
{
    internal class Program
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public Program(ILogger logger, IConfiguration configuration)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        } 

        static async void Main(string[] args)
        {
            var configuration = BuildConfiguration();
            var logger = CreateLogger(configuration);
            await new Program(configuration, logger).RunAsync();
        }

        private async Task RunAsync()
        {
            try
            {
                var services = new ServiceCollection();
                await services.GetService<ISweepstakeService>().StartAsync();
            }
            catch(Exception e)
            {
                _logger.Error(e, "Failed to start!");
            }
        }

        private static ILogger CreateLogger(IConfiguration configuration)
        {
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
            logger.Logger = logger;
            return logger.ForContext<Program>();
        }

        private static IConfiguration BuildConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}
