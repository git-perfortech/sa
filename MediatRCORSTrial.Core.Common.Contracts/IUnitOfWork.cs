using System;
using System.Collections.Generic;
using System.Text;

namespace MediatRCORSTrial.Core.Common.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        void Begin(bool isTransactionRequired = true);

        void Commit();

        void Rollback();
    }
}
