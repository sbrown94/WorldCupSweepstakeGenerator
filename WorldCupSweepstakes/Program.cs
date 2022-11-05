using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;
using WorldCupSweepstakes.Extensions;
using WorldCupSweepstakes.Services;

namespace WorldCupSweepstakes
{
    internal class Program
    {
        private readonly IConfiguration _configuration;

        public Program(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        } 

        static async Task Main(string[] args)
        {
            var configuration = BuildConfiguration();
            await new Program(configuration).RunAsync(configuration);
        }

        private async Task RunAsync(IConfiguration configuration)
        {
            try
            {
                var services = new ServiceCollection()
                    .AddServices(configuration)
                    .BuildServiceProvider();

                await services.GetService<ISweepstakeService>().StartAsync();
            }
            catch(Exception e)
            {
                Console.WriteLine("Failed to start!");
            }
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
