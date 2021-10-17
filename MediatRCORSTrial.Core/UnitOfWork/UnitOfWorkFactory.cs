using MediatRCORSTrial.Core.Common.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MediatRCORSTrial.Core.UnitOfWork
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IDbObjectFactory DbObjectFactory;
        private readonly IHttpContextAccessor HttpContextAccessor;

        public UnitOfWorkFactory(
            IDbObjectFactory dbObjectFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            this.DbObjectFactory = dbObjectFactory;
            this.HttpContextAccessor = httpContextAccessor;
        }

        public IUnitOfWork Create(string connectionString = "")
        {
            return new UnitOfWork(this.GetDbObjectInterface(null), connectionString, IsolationLevel.ReadCommitted, this.HttpContextAccessor);
        }

        public IUnitOfWork Create(string contextKey, string connectionString = "")
        {
            return new UnitOfWork(this.GetDbObjectInterface(contextKey), connectionString, IsolationLevel.ReadCommitted, this.HttpContextAccessor);
        }

        public IUnitOfWork Create(bool entityLazyLoad = false, string connectionString = "")
        {
            return new UnitOfWork(this.GetDbObjectInterface(null), connectionString, IsolationLevel.ReadCommitted, this.HttpContextAccessor, connectionString);
        }

        public IUnitOfWork Create(string contextKey, bool entityLazyLoad = false, string connectionString = "")
        {
            return new UnitOfWork(this.GetDbObjectInterface(contextKey), connectionString, IsolationLevel.ReadCommitted, this.HttpContextAccessor);
        }

        public IUnitOfWork Create(IsolationLevel isoLevel = IsolationLevel.ReadCommitted, string connectionString = "")
        {
            return new UnitOfWork(this.GetDbObjectInterface(null), connectionString, isoLevel, this.HttpContextAccessor);
        }

        public IUnitOfWork Create(string contextKey, IsolationLevel isoLevel = IsolationLevel.ReadCommitted, string connectionString = "")
        {
            return new UnitOfWork(this.GetDbObjectInterface(contextKey), connectionString, isoLevel, this.HttpContextAccessor);
        }

        public IUnitOfWork Create(IsolationLevel isoLevel = IsolationLevel.ReadCommitted, string uowKey = null, string connectionString = "")
        {
            return new UnitOfWork(this.GetDbObjectInterface(null), connectionString, isoLevel, this.HttpContextAccessor);
        }

        public IUnitOfWork Create(string contextKey, IsolationLevel isoLevel = IsolationLevel.ReadCommitted, string uowKey = null, string connectionString = "")
        {
            return new UnitOfWork(this.GetDbObjectInterface(contextKey), connectionString, isoLevel, this.HttpContextAccessor, factoryKey: uowKey);
        }

        public IUnitOfWork Create(IsolationLevel isoLevel = IsolationLevel.ReadCommitted, bool entityLazyLoad = false, string connectionString = "")
        {
            return new UnitOfWork(this.GetDbObjectInterface(null), connectionString, isoLevel, this.HttpContextAccessor, entityLazyLoad);
        }

        public IUnitOfWork Create(string contextKey, IsolationLevel isoLevel = IsolationLevel.ReadCommitted, bool entityLazyLoad = false, string connectionString = "")
        {
            return new UnitOfWork(this.GetDbObjectInterface(contextKey), connectionString, isoLevel, this.HttpContextAccessor, entityLazyLoad);
        }

        public IUnitOfWork Create(IsolationLevel isoLevel = IsolationLevel.ReadCommitted, string uowKey = null, bool entityLazyLoad = false, string connectionString = "")
        {
            return new UnitOfWork(this.GetDbObjectInterface(null), connectionString, isoLevel, this.HttpContextAccessor, factoryKey: uowKey, entityLazyLoad);
        }

        public IUnitOfWork Create(string contextKey, IsolationLevel isoLevel = IsolationLevel.ReadCommitted, string uowKey = null, bool entityLazyLoad = false, string connectionString = "")
        {
            return new UnitOfWork(this.GetDbObjectInterface(contextKey), connectionString, isoLevel, this.HttpContextAccessor, factoryKey: uowKey, entityLazyLoad);
        }

        public DbObject GetCurrentDbObject()
        {
            return UnitOfWork.GetDbObject(null, this.HttpContextAccessor);
        }

        public DbObject GetDbObjectByUowKey(string uowKey)
        {
            return UnitOfWork.GetDbObject(uowKey, this.HttpContextAccessor);
        }

        private IDbObject GetDbObjectInterface(string contextKey)
        {
            ICollection<IDbObject> dbObjects = this.DbObjectFactory.GetDbObjects();
            if (dbObjects == null || !dbObjects.Any())
            {
                throw new ArgumentNullException("DbObject NotFound!");
            }

            IDbObject dbObject = null;

            if (!String.IsNullOrWhiteSpace(contextKey))
            {
                dbObject = dbObjects.Where(x => x.Code.Equals(contextKey)).FirstOrDefault();
            }
            else
            {
                dbObject = dbObjects.Where(x => x.IsDefault).FirstOrDefault();
            }

            if (dbObject == null)
            {
                throw new ArgumentNullException("DbObject NotFound!");
            }

            return dbObject;
        }
    }
}
