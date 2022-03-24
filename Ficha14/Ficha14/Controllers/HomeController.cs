using Ficha14.Models;
using Ficha14.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Ficha14.Controllers
{

    public class HomeController : Controller
    {
        private readonly IConfiguration config; //ler o app settings, ficheiro segredo token
        private readonly IJWTService tokenService; //validar o token
        private readonly IUserService userService;  //o que nós criámos

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

        [AllowAnonymous] //Temos de invocar sem estarmos autenticados
        [Route("Login")] //Nome da rota
        [HttpPost]
        public IActionResult Login(User userModel)
        {
            if (string.IsNullOrEmpty(userModel.UserName) || string.IsNullOrEmpty(userModel.Password))
            {
                return (RedirectToAction("Error"));
            }

            var user = userService.Get(userModel.UserName, userModel.Password); //usamos Get para buscar User e Pass
            var validUser = new UserViewModel { UserName = user.UserName, ID = user.ID, Email = user.Email, Role = user.Role };

            if (validUser != null)
            {
                string generatedToken = tokenService.GenerateToken(
                    config["Jwt:Key"].ToString(),
                    config["Jwt:Issuer"].ToString(),
                    config["Jwt:Audience"].ToString(),
                validUser);

                if (generatedToken != null)
                {
                    HttpContext.Session.SetString("Token", generatedToken);
                    return RedirectToAction("UserDetails", validUser); //temos de criar view UserDetails
                }
                else
                {
                    return (RedirectToAction("Error"));
                }
            }
            else
            {
                return (RedirectToAction("Error"));
            }
        }

        public IActionResult UserDetails(UserViewModel user)
        {
            string token = HttpContext.Session.GetString("Token");

            if (token == null)
            {
                return (RedirectToAction("Index"));
            }

            if (!tokenService.IsTokenValid(
                config["Jwt:Key"].ToString(),
                config["Jwt:Issuer"].ToString(),
                config["Jwt:Audience"].ToString(),
                token))
            {
                return (RedirectToAction("Index"));
            }

            ViewBag.Token = token;
            return View(user);
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [AllowAnonymous] //Temos de invocar sem estarmos autenticados
        [HttpPost]
        public async Task<IActionResult> SignUp (User user)
        {
            if (ModelState.IsValid)
            {
                var userExists = userService.FindByName(user.UserName);
                if (userExists != null)
                    return StatusCode(StatusCodes.Status500InternalServerError, "User already exists!");

                var newUser = userService.Create(user);
                if (newUser is not null)
                    return RedirectToAction(nameof(Index));
                else
                    return RedirectToAction(nameof(Error));
            }
            else
            {
                return RedirectToAction(nameof(Error));
            }
        }

    }
}