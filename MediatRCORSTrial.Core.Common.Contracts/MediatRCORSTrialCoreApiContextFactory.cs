using MediatRCORSTrial.Core.Common.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace MediatRCORSTrial.Core.Common.Contracts
{
    public class MediatRCORSTrialCoreApiContextFactory : IDbObject
    {
        private readonly string ContextCode;
        private readonly bool IsDefaultContext;
        private readonly string ContextName;
        private readonly IMediatRCORSTrialCoreApiConfiguration _mediatRCORSTrialCoreApiConfiguration;
        private readonly IConfiguration Configuration;

        public MediatRCORSTrialCoreApiContextFactory(
            IMediatRCORSTrialCoreApiConfiguration mediatRCORSTrialCoreApiConfiguration, IConfiguration configuration)
        {
            _mediatRCORSTrialCoreApiConfiguration = mediatRCORSTrialCoreApiConfiguration;
            this.ContextCode = _mediatRCORSTrialCoreApiConfiguration.GetContextKey();
            this.ContextName = _mediatRCORSTrialCoreApiConfiguration.GetContextName();
            this.IsDefaultContext = false;
            this.Configuration = configuration;
        }
        public string Code
        {
            get { return this.ContextCode; }
        }

        public string Name
        {
            get { return this.ContextName; }
        }

        public DbObject DbObject()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in assemblies)
                foreach (var type in assembly.GetTypes())
                    if (type.IsSubclassOf(typeof(DbContext)))
                        if (type.Name == Name)
                            return new DbObject
                            {
                                DbProvider = DbProvider.EntityFramework,
                                DbObjectContext = type.GetConstructor(Array.Empty<Type>()).Invoke(Array.Empty<object>())
                            };

            return null;
        }

        public bool IsDefault
        {
            get { return this.IsDefaultContext; }
        }

        public void Migrate()
        {
            throw new NotImplementedException();
        }

        public DbObject DbObject(bool entityLazyLoad = false)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in assemblies)
                foreach (var type in assembly.GetTypes())
                    if (type.IsSubclassOf(typeof(DbContext)))
                        if (type.Name == Name)
                            //Type[] types = new Type[1];
                            //types[0] = typeof(IConfiguration);
                            //var constructor = type.GetConstructor(types);
                            return new DbObject
                            {
                                DbProvider = DbProvider.EntityFramework,
                                //DbObjectContext = constructor.Invoke(new object[] { this.Configuration })
                                DbObjectContext = type.GetConstructor(Array.Empty<Type>()).Invoke(Array.Empty<object>())
                            };

            return null;
        }
    }
}
