using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MediatRCORSTrial.Core.Common.Contracts
{
    public interface IUnitOfWorkFactory
    {
        DbObject GetCurrentDbObject();
        DbObject GetDbObjectByUowKey(string uowKey);
        IUnitOfWork Create(string connectionString = "");
        IUnitOfWork Create(string contextKey, string connectionString = "");
        IUnitOfWork Create(bool entityLazyLoad = false, string connectionString = "");
        IUnitOfWork Create(string contextKey, bool entityLazyLoad = false, string connectionString = "");
        IUnitOfWork Create(IsolationLevel isoLevel = IsolationLevel.ReadCommitted, string connectionString = "");
        IUnitOfWork Create(string contextKey, IsolationLevel isoLevel = IsolationLevel.ReadCommitted, string connectionString = "");
        IUnitOfWork Create(IsolationLevel isoLevel = IsolationLevel.ReadCommitted, string uowKey = null, string connectionString = "");
        IUnitOfWork Create(string contextKey, IsolationLevel isoLevel = IsolationLevel.ReadCommitted, string uowKey = null, string connectionString = "");
        IUnitOfWork Create(IsolationLevel isoLevel = IsolationLevel.ReadCommitted, bool entityLazyLoad = false, string connectionString = "");
        IUnitOfWork Create(string contextKey, IsolationLevel isoLevel = IsolationLevel.ReadCommitted, bool entityLazyLoad = false, string connectionString = "");
        IUnitOfWork Create(IsolationLevel isoLevel = IsolationLevel.ReadCommitted, string uowKey = null, bool entityLazyLoad = false, string connectionString = "");
        IUnitOfWork Create(string contextKey, IsolationLevel isoLevel = IsolationLevel.ReadCommitted, string uowKey = null, bool entityLazyLoad = false, string connectionString = "");
    }
}
