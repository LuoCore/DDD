using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Layui.Areas.Admin.ViewComponents
{

    
    public class HeaderViewComponent : ViewComponent
    {

        /// <summary>
        ///  如果需要构造函数的话，你就创建 构造函数
        /// </summary>
        public HeaderViewComponent()
        {

        }
        /// <summary>
        /// 异步调用
        /// </summary>
        /// <returns></returns>
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
