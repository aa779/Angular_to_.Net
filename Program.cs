using DutchTreat.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DutchTreat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var run = BuildWebHost(args);

            RunSeeding(run);
            run.Run();
        }

        private static void RunSeeding(IWebHost run)
        {
            var scopeFactory = run.Services.GetService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetService<DutchSeeder>();
                seeder.Seed();
            };
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(SetupConfiguration)
                .UseStartup<Startup>()
                .Build();

        private static void SetupConfiguration(WebHostBuilderContext ctx, IConfigurationBuilder builder)
        {
            //Removind all default configurations
            builder.Sources.Clear();

            //Trying to connect
            builder.AddJsonFile("config.json", false, true)
            //       .AddXmlFile("config.xml", true, false)
                   .AddEnvironmentVariables();
        }
    }
}
