using MediatRCORSTrial.Core.Common.Contracts;
using MediatRCORSTrial.Core.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;

namespace MediatRCORSTrial.Core.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _dbContext;
        private DbTransaction _dbTransaction;
        private readonly DbObject _dbObject;
        private string _factoryKey;
        private readonly IsolationLevel _isoLevel;
        private readonly bool _entityLazyLoad;
        //private readonly ILog _logService;
        private bool _isTransactionRequired = true;
        private IHttpContextAccessor _httpContextAccessor;
        private string _connectionString;

        public UnitOfWork() { }

        public UnitOfWork(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        public UnitOfWork(
            IDbObject dbObject,
            string connectionString,
            IsolationLevel isoLevel,
            IHttpContextAccessor httpContextAccessor
            )
        {
            this._dbObject = dbObject.DbObject();
            this._isoLevel = isoLevel;
            this._httpContextAccessor = httpContextAccessor;
            this._connectionString = connectionString;
            this.SetProviderMember();
        }

        public UnitOfWork(
            IDbObject dbObject,
            string connectionString,
            IsolationLevel isoLevel,
            IHttpContextAccessor httpContextAccessor,
            string factoryKey = null)
        {
            this._dbObject = dbObject.DbObject();
            this._isoLevel = isoLevel;
            this._factoryKey = factoryKey;
            this._httpContextAccessor = httpContextAccessor;
            this._connectionString = connectionString;
            this.SetProviderMember();
        }

        public UnitOfWork(
            IDbObject dbObject,
            string connectionString,
            IsolationLevel isoLevel,
            IHttpContextAccessor httpContextAccessor,
            bool entityLazyLoad = false)
        {
            this._dbObject = dbObject.DbObject(entityLazyLoad);
            this._isoLevel = isoLevel;
            this._entityLazyLoad = entityLazyLoad;
            this._httpContextAccessor = httpContextAccessor;
            this._connectionString = connectionString;
            this.SetProviderMember();
        }

        public UnitOfWork(
            IDbObject dbObject,
            string connectionString,
            IsolationLevel isoLevel,
            IHttpContextAccessor httpContextAccessor,
            string factoryKey = null,
            bool entityLazyLoad = false)
        {
            //this._logService = logService;
            this._dbObject = dbObject.DbObject(entityLazyLoad);
            this._isoLevel = isoLevel;
            this._factoryKey = factoryKey;
            this._entityLazyLoad = entityLazyLoad;
            this._httpContextAccessor = httpContextAccessor;
            this._connectionString = connectionString;
            this.SetProviderMember();
        }

        private void SetProviderMember()
        {
            if (String.IsNullOrWhiteSpace(this._factoryKey))
            {
                this._factoryKey = Guid.NewGuid().ToString();
            }

            switch (this._dbObject.DbProvider)
            {
                case DbProvider.EntityFramework:
                    ConfigureEntityFrameworkConnection();
                    break;
                case DbProvider.NHibernate:
                    break;
            }
        }

        public void Begin(bool isTransactionRequired = true)
        {
            try
            {
                _isTransactionRequired = isTransactionRequired;
                switch (this._dbObject.DbProvider)
                {
                    case DbProvider.EntityFramework:
                        this._dbContext.Database.OpenConnection();
                        if (_isTransactionRequired)
                        {
                            this._dbTransaction = (DbTransaction)this._dbContext.Database.BeginTransaction(this._isoLevel);
                            this._dbContext.Database.UseTransaction(this._dbTransaction);
                        }
                        break;
                    case DbProvider.NHibernate:
                        break;
                    default:
                        throw new Exception("Db Connection Problem");
                        //default:
                        //    throw new ArgumentNullException(ResponseCode.DBConnectionProblem);
                }

                Stack<UnitOfWork> unitOfWorkStack = ApplicationContext.Get<Stack<UnitOfWork>>(Thread.CurrentThread.ManagedThreadId.ToString(), this._httpContextAccessor);
                if (unitOfWorkStack == null)
                {
                    unitOfWorkStack = new Stack<UnitOfWork>();
                    ApplicationContext.Add<Stack<UnitOfWork>>(unitOfWorkStack, Thread.CurrentThread.ManagedThreadId.ToString(), this._httpContextAccessor);
                }

                unitOfWorkStack.Push(this);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Commit()
        {
            try
            {
                switch (this._dbObject.DbProvider)
                {
                    case DbProvider.EntityFramework:
                        this._dbContext.SaveChanges();
                        if (_isTransactionRequired)
                        {
                            this._dbTransaction.Commit();
                        }
                        break;
                    case DbProvider.NHibernate:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Dispose()
        {
            try
            {
                this._dbContext.Dispose();
                if (_isTransactionRequired)
                {
                    this._dbTransaction.Dispose();
                }
                GC.SuppressFinalize(this);
                GetLastUnitOfWork(Thread.CurrentThread.ManagedThreadId.ToString(), true, this._httpContextAccessor);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        internal static UnitOfWork GetLastUnitOfWork(
            string uowKey,
            bool disposed,
            IHttpContextAccessor httpContextAccessor)
        {
            UnitOfWork unitOfWork = null;

            if (String.IsNullOrWhiteSpace(uowKey))
            {
                uowKey = Thread.CurrentThread.ManagedThreadId.ToString();
            }

            Stack<UnitOfWork> unitOfWorkStack = ApplicationContext.Get<Stack<UnitOfWork>>(uowKey, httpContextAccessor);

            unitOfWork = disposed ? unitOfWorkStack.Pop() : unitOfWorkStack.Peek();

            return unitOfWork;
        }

        internal static DbObject GetDbObject(
            string uowKey,
            IHttpContextAccessor httpContextAccessor)
        {
            DbObject dbObject = null;

            UnitOfWork unitOfWork = GetLastUnitOfWork(uowKey, false, httpContextAccessor);

            dbObject = unitOfWork._dbObject;

            return dbObject;
        }

        public void Rollback()
        {
            try
            {
                switch (this._dbObject.DbProvider)
                {
                    case DbProvider.EntityFramework:
                        this._dbTransaction.Rollback();
                        break;
                    case DbProvider.NHibernate:
                        this._dbTransaction.Rollback();
                        break;
                }
            }
            catch (Exception ex)
            {
                this._dbTransaction.Rollback();
            }
        }


        private void ConfigureEntityFrameworkConnection()
        {
            this._dbContext = this._dbObject.DbObjectContext as DbContext;
            if (!string.IsNullOrEmpty(_connectionString))
            {
                this._dbContext.Database.GetDbConnection().ConnectionString = _connectionString;
            }
        }
    }
}
