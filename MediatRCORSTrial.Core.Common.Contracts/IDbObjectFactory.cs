using System;
using System.Collections.Generic;
using System.Text;

namespace MediatRCORSTrial.Core.Common.Contracts
{
    public interface IDbObjectFactory
    {
        ICollection<IDbObject> GetDbObjects();
    }
}
