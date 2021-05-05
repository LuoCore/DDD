using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Common
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/5 15:45:38
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public static class CookieHelper
    {
        /// <summary>
        /// 设置本地cookie
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值  </param>
        /// <param name="minutes">过期时长，单位：分钟   </param>
        public static bool SetCookies(this HttpContext httpContext, string key, string value, int minutes = 30)
        {
            try 
            {
                httpContext.Response.Cookies.Append(key, value, new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(minutes)
                });
                return true;
            }
            catch { return false; }
        }




        /// <summary>
        ///  删除指定的cookie
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static bool DeleteCookies(this HttpContext httpContext,string key)
        {
            try
            {
                httpContext.Response.Cookies.Delete(key);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
        }
        /// <summary>
        /// 获取cookies
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static string GetCookies(this HttpContext httpContext, string key)
        {
            httpContext.Request.Cookies.TryGetValue(key, out string value);
            if (string.IsNullOrEmpty(value))
                value = string.Empty;
            return value;
        }

    }
}
