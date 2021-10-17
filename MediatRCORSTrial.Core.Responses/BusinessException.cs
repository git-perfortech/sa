using System;
using System.Collections.Generic;
using System.Text;

namespace MediatRCORSTrial.Core.Responses
{
    public class BusinessException : Exception
    {
        public BusinessException()
        {
        }

        public BusinessException(string rc, string detail)
        {
            this.RC = rc;
            this.Message = GetResponseCode.GetDescription(RC);
            this.Details = detail;
        }

        public string Message { get; set; }
        public string RC { get; set; }
        public string Details { get; set; }

        public static string GetDescription(string RC)
        {
            return GetResponseCode.GetDescription(RC);
        }
    }
}
