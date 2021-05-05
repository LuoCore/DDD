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
    /// 时间：2021/5/5 13:16:08
    /// 版本：V1.0.1  
    /// 说明：
    /// </summary>
    public class AajxAuthorizeFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //.获取区域、控制器、Action的名称
            //必须在区域里的控制器上加个特性[Area("")]才能获取
            var areaName = context.ActionDescriptor.RouteValues["area"] == null ? "" : context.ActionDescriptor.RouteValues["area"].ToString();
            var controllerName = context.ActionDescriptor.RouteValues["controller"] == null ? "" : context.ActionDescriptor.RouteValues["controller"].ToString();
            var actionName = context.ActionDescriptor.RouteValues["action"] == null ? "" : context.ActionDescriptor.RouteValues["action"].ToString();

            //下面的方式也能获取控制器和action的名称
            //var controllerName = context.RouteData.Values["controller"].ToString();
            //var actionName = context.RouteData.Values["action"].ToString();

            //2.判断是什么请求，进行响应的页面跳转
            if (IsAjaxRequest(context.HttpContext.Request))
            {
                //2.1 是ajax请求
                context.Result = new JsonResult(new
                {
                    status = "error",
                    message = "您没有权限"
                });
            }
            else
            {
                //2.2 不是ajax请求
                var result = new ViewResult { ViewName = "~/Views/Shared/Error.cshtml" };
                context.Result = result;
            }
        }
        /// <summary>
        /// 判断该请求是否是ajax请求
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private bool IsAjaxRequest(HttpRequest request)
        {
            string header = request.Headers["X-Requested-With"];
            return "XMLHttpRequest".Equals(header);
        }
    }
}
