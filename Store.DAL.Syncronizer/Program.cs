using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Store.DAL.Syncronizer
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = GetServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                var logger = serviceProvider.GetService<ILogger<Program>>();
                logger.LogInformation("Starting Application");
                
                var worker = serviceProvider.GetService<Worker>();
                
                worker.Run();
                logger.LogInformation("Closing Application");
            }  
        }

        private static IServiceProvider GetServiceProvider(){
            

            var services = new ServiceCollection();
                services.AddLogging(provider => provider.AddConsole());
                
            ConfigureIOC(services);

            // build the service provider
            return services.BuildServiceProvider();
        }

        private static IConfiguration GetConfiguration(){
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.local.json", optional: true)
                //.AddUserSecrets<Settings>()
                .Build();
            return config;
        }

        private static void ConfigureIOC(IServiceCollection services){
            var configuration = GetConfiguration();
            // register `Worker` in the service collection
            services.AddTransient<Worker>();
            services.AddScoped<IConfiguration>((_)=>configuration);
        }
    }
}
