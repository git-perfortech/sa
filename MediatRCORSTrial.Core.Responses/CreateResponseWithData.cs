using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRCORSTrial.Core.Responses
{
    public static class CreateResponseWithData<T> where T : class
    {
        /// <summary>
        /// Returns the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <returns></returns>
        public static async Task<ResponseDTO<T>> Return(T entity)
        {
            var message = GetResponseCode.GetDescription(entity != null ? ResponseCodes.Success : ResponseCodes.NotFound);
            ResponseDTO<T> response = new ResponseDTO<T>
            {
                Data = entity,
                Message = message,
                Information = new Information
                {
                    ResponseDate = DateTime.Now,
                    TrackId = Guid.NewGuid().ToString()
                },
                RC = entity == null ? ResponseCodes.Failed : ResponseCodes.Success
            };
            //Log.Write(LogEventLevel.Information, message, response);
            return response;
        }

        /// <summary>
        /// Returns the specified entity.
        /// </summary>
        /// <param name="entity">if set to <c>true</c> [entity].</param>
        /// <param name="methodName">Name of the method.</param>
        /// <returns></returns>
        public static async Task<ResponseDTO> Return(bool entity)
        {
            var message = GetResponseCode.GetDescription(ResponseCodes.Success);
            ResponseDTO response = new ResponseDTO
            {
                Data = entity,
                Message = message,
                Information = new Information
                {
                    ResponseDate = DateTime.Now,
                    TrackId = Guid.NewGuid().ToString()
                },
                RC = ResponseCodes.Success
            };
            //Log.Write(LogEventLevel.Information, message, response);
            return response;
        }

        /// <summary>
        /// Returns the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <returns></returns>
        public static async Task<ResponseListDTO<T>> Return(IEnumerable<T> entity)
        {
            var message = GetResponseCode.GetDescription(entity.Any() ? ResponseCodes.Success : ResponseCodes.NotFound);

            ResponseListDTO<T> response = new ResponseListDTO<T>
            {
                Data = entity,
                Message = message,
                Information = new Information
                {
                    ResponseDate = DateTime.Now,
                    TrackId = Guid.NewGuid().ToString()
                },
                RC = ResponseCodes.Success
            };
            //Log.Write(LogEventLevel.Information, message, response);
            return response;
        }

    }
}
