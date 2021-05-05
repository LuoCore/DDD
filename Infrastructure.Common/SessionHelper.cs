using Microsoft.AspNetCore.Http;
using System;

namespace Infrastructure.Common
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/5 15:55:48
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public static class SessionHelper
    {
        /// <summary>
        /// Session写入
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static bool SetSession(this HttpContext httpContext, string key, string value)
        {
            try
            {
                httpContext.Session.SetString(key, value);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// Session写入
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static bool SetSession(this HttpContext httpContext, string key, int value)
        {
            try
            {
                httpContext.Session.SetInt32(key, value);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public static string GetSession(this HttpContext httpContext, string key)
        {
            try
            {
                var value = httpContext.Session.GetString(key);
                if (string.IsNullOrEmpty(value))
                    value = string.Empty;
                return value;
               
            }
            catch (System.Exception)
            {
                return string.Empty;
            }
        }
        public static int GetSessionInt32(this HttpContext httpContext, string key)
        {
            try
            {
                var value = httpContext.Session.GetInt32(key);
                if (value==null)
                    value = -1;
                return Convert.ToInt32(value);

            }
            catch (System.Exception)
            {
                return -1;
            }
        }

    }
}
