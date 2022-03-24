using Ficha14.Models;
using Ficha14.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Ficha14.Controllers
{

    public class HomeController : Controller
    {
        private readonly IConfiguration config;
        private readonly IJWTService tokenService;
        private readonly IUserService userService;        

        public HomeController(IConfiguration config, IJWTService tokenService, IUserService userService)
        {
            this.config = config;
            this.tokenService = tokenService;
            this.userService = userService;
        }     

        public IActionResult Index()
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