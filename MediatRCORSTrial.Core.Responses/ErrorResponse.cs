using System;
using System.Collections.Generic;
using System.Text;

namespace MediatRCORSTrial.Core.Responses
{
    public class ErrorResponse
    {
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }

   
}
