using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Layui.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        //[TypeFilter(typeof(Infrastructure.CrossCutting.Filters.AdminLoginAuthorizationFilter))]
        public IActionResult Main()
        {
            return View();
        }

        public IActionResult Welcome() 
        {
            return View();
        }
    }
}
