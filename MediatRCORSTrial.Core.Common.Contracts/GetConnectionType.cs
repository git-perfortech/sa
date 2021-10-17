using Microsoft.Extensions.Configuration;
using System.IO;

namespace MediatRCORSTrial.Core.Common.Contracts
{
    public static class GetConnectionType
    {

        public static IConfiguration GetProductionConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public static IConfiguration GetDevelopmentConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();
        }

        public static ConnectionType Get(string contextName)
        {
            if (MediatRCORSTrialEnvironments.IsDevelopment == true)
            {
                IConfiguration configDevelopment = GetDevelopmentConfiguration();
                return configDevelopment.GetSection(contextName + ":0").Get<ConnectionType>();
            }
            else
            {
                IConfiguration configProduction = GetProductionConfiguration();
                return configProduction.GetSection(contextName + ":0").Get<ConnectionType>();
            }

        }
    }

}
