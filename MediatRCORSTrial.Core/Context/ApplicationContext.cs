using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediatRCORSTrial.Core.Context
{
    public static class ApplicationContext
    {
        public static void Add<T>(T value, IHttpContextAccessor httpContextAccessor)
        {
            string key = typeof(T).Name;

            if (httpContextAccessor.HttpContext.Items[key] != null)
            {
                throw new Exception("Duplicate Context Items!");
            }

            httpContextAccessor.HttpContext.Items.Add(key, value);
        }

        public static void Add<T>(T value, string key, IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.HttpContext.Items[key] != null)
            {
                throw new Exception("Duplicate Context Items!");
            }

            httpContextAccessor.HttpContext.Items.Add(key, value);
        }

        public static T Get<T>(IHttpContextAccessor httpContextAccessor)
        {
            T value = default(T);

            string key = typeof(T).Name;

            if (httpContextAccessor.HttpContext.Items[key] != null)
            {
                value = (T)httpContextAccessor.HttpContext.Items[key];
            }

            return value;
        }

        public static T Get<T>(string key, IHttpContextAccessor httpContextAccessor)
        {
            T value = default(T);

            if (!String.IsNullOrWhiteSpace(key) && httpContextAccessor.HttpContext.Items[key] != null)
            {
                return (T)httpContextAccessor.HttpContext.Items[key];
            }

            return value;
        }
    }
}
