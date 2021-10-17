using MediatRCORSTrial.Core.Common.Contracts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace MediatRCORSTrial.Core.DBObject
{
    public class DbObjectFactory : IDbObjectFactory
    {
        private IConfiguration Config;

        public DbObjectFactory(IConfiguration config)
        {
            this.Config = config;
        }

        public ICollection<IDbObject> GetDbObjects()
        {
            List<IDbObject> response = new List<IDbObject>();

            string[] castleConfigAllFiles = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ""), "Castle.*.config", SearchOption.AllDirectories);
            string[] castleConfigFiles = castleConfigAllFiles.Where(x => !x.Contains("Deployment")).ToArray();
            //XmlDocument doc = new XmlDocument();

            foreach (var item in castleConfigFiles)
            {
                XDocument configFile = XDocument.Load(item);
                List<MediatRCORSTrialCoreApiConfiguration> items = (from _dbObject in configFile.Element("components").Elements("component")
                                                                select new MediatRCORSTrialCoreApiConfiguration
                                                                {
                                                                    ContextKey = _dbObject.Element("ContextKey").Value,
                                                                    ContextName = _dbObject.Element("ContextName").Value,
                                                                    MockIntegration = bool.Parse(_dbObject.Element("MockIntegration").Value)
                                                                }).ToList();

                foreach (var config in items)
                {
                    MediatRCORSTrialCoreApiContextFactory factory = new MediatRCORSTrialCoreApiContextFactory(
                        new MediatRCORSTrialCoreApiConfiguration
                        {
                            ContextKey = config.ContextKey,
                            ContextName = config.ContextName,
                            MockIntegration = config.MockIntegration
                        },
                        this.Config);

                    response.Add(factory);
                }
            }

            return response;
        }
    }
}
