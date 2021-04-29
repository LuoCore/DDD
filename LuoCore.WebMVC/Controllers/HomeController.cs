using LuoCore.WebMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LuoCore.WebMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly  Application.Interface.IUsersService _usersService;

        public HomeController(ILogger<HomeController> logger, Application.Interface.IUsersService usersService)
        {
            _logger = logger;
            _usersService = usersService;
        }

        public IActionResult Index()
        {
            _usersService.Register(new Application.Models.ViewModels.UserViewModel());
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
