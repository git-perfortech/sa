using System;
using System.Collections.Generic;
using System.Text;

namespace MediatRCORSTrial.Core.Common.Contracts
{
    public class ConnectionType
    {
        public string DbType { get; set; }
        public string ConnectionString { get; set; }
    }
}
