using MediatRCORSTrial.Core.Common.Contracts;
using MediatRCORSTrial.Core.DBObject;
using MediatRCORSTrial.Core.Enums;
using MediatRCORSTrial.Core.UnitOfWork;
using MediatRCORSTrial.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MediatRCORSTrial.Core.Common.Extensions.Extensions
{
    public static class DependencyInjection
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

        public static void ConfigureContext(this IServiceCollection services, IConfiguration config)
        {
            ConnectionType domainContext = GetConnectionType.Get("MediatRCORSTrialContext");
            //ConnectionType domainContext = config.GetSection("DomainContext:0").Get<ConnectionType>();

            //Log.Write(LogEventLevel.Information, $"Context DbType: {domainContext.DbType} Context ConnectionString {domainContext.ConnectionString}");

            switch (domainContext.DbType)
            {
                case DatabaseProviders.SqlServer:
                    services.AddDbContext<MediatRCORSTrialContext>(o => o.UseSqlServer(domainContext.ConnectionString));
                    break;

                case DatabaseProviders.Oracle:
                    services.AddDbContext<MediatRCORSTrialContext>(o => o.UseOracle(domainContext.ConnectionString));
                    break;

                case DatabaseProviders.PostgreSql:
                    services.AddDbContext<MediatRCORSTrialContext>(o => o.UseNpgsql(domainContext.ConnectionString));
                    break;

                case DatabaseProviders.MySql:
                    services.AddDbContext<MediatRCORSTrialContext>(o => o.UseMySQL(domainContext.ConnectionString));
                    break;

                case DatabaseProviders.Sqlite:
                    services.AddDbContext<MediatRCORSTrialContext>(o => o.UseSqlite(domainContext.ConnectionString));
                    break;

                default:
                    break;
            }

        }

        public static void ConfigureUnitOfWork(this IServiceCollection services)
        {
            services.AddSingleton<IMediatRCORSTrialCoreApiConfiguration, MediatRCORSTrialCoreApiConfiguration>();
            services.AddScoped<IDbObjectFactory, DbObjectFactory>();
            services.AddScoped<IDbObject, MediatRCORSTrialCoreApiContextFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();

        }

        //public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration config)
        //{
        //    services.Configure<JWTConfiguration>(config.GetSection("JWTConfiguration"));
        //}

        //public static void ConfigureSystemParams(this IServiceCollection services, IConfiguration config)
        //{
        //    services.Configure<SystemParams>(config.GetSection("SystemParams"));
        //    services.Configure<NotificationMessageText>(config.GetSection("NotificationMessageText"));
        //    services.Configure<PostUrls>(config.GetSection("PostUrls"));
        //    services.Configure<CardPersonelization>(config.GetSection("CardPersonelization"));
        //}

    }
}
