using MediatRCORSTrial.Core.Common.Contracts;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace MediatRCORSTrial.Core.Responses
{
    public static class GetResponseCode
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
        public static string GetDescription(string RC)
        {

            if (MediatRCORSTrialEnvironments.IsDevelopment == true)
            {
                IConfiguration config = GetDevelopmentConfiguration();
                return config.GetSection("ResponseCodes:" + RC).Get<string>();
            }
            else
            {
                IConfiguration config = GetProductionConfiguration();
                return config.GetSection("ResponseCodes:" + RC).Get<string>();
            }


        }
    }
}
