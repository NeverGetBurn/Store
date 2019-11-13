using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Store.DAL.Syncronizer
{
    public class Worker
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
    
        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration ??throw new ArgumentNullException(nameof(configuration));
        }
        public void Run(){
            _logger.LogInformation("Worker start");
        
            _logger.LogInformation("Worker stop");
        }
    }
}