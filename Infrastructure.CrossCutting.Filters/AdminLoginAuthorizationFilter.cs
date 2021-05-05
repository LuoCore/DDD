using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CrossCutting.Filters
{
    /// <summary>
    /// 作者：(YJY)
    /// 时间：2021/5/5 13:19:42
    /// 版本：V1.0.1  
    /// 说明：校验是否登录
    /// </summary>
    public class AdminLoginAuthorizationFilter : Attribute, IAuthorizationFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public AdminLoginAuthorizationFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //也可以这样获取Session，就不需要注入了。
            var testData = context.HttpContext.Request.Cookies.TryGetValue("User", out string value);
            if (!testData || string.IsNullOrWhiteSpace(value))
            {
                //截断请求
                context.Result = new RedirectResult("/Admin/User/Login");
            }
        }
    }
}
