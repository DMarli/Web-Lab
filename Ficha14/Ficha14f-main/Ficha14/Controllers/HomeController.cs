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
        private readonly IWebHostEnvironment hostEnvironment; // Provides information about the web hosting environment an application is running in

        public HomeController(IConfiguration config, IJWTService tokenService, IUserService userService, IWebHostEnvironment hostEnvironment)
        {
            this.config = config;
            this.tokenService = tokenService;
            this.userService = userService;
            this.hostEnvironment = hostEnvironment;
        }



        public IActionResult Upload()
        {
            return View();
        }

        public IActionResult Image(ImageUploaded image)
        {
            return View(image);
        }

        [HttpPost("FileUpload")]
        public IActionResult UploadImage(IFormFile file, User _user)
        {
            string path = Path.Combine(this.hostEnvironment.WebRootPath, "images");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fileName = Path.GetFileName(file.FileName);
            //_user.Avatar = fileName;

            userService.ImageUpdate(fileName, _user);
            using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                file.CopyTo(stream);


                return RedirectToAction("Image", new ImageUploaded { Path = file.FileName });
            }

            return RedirectToAction("Error");
        }

        /*
        public IActionResult ChangeAvatar(string avatar, User user)
        {
            userService.ImageUpdate(avatar, user);

            return (RedirectToAction("UserDetails"));
        }*/



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult Index() {
            return View();
        }


        [AllowAnonymous] //Temos de invocar sem estarmos autenticados
        [Route("Index")] //Nome da rota
        [HttpPost]
        public IActionResult Index(User userModel)
        {
            if (string.IsNullOrEmpty(userModel.UserName) || string.IsNullOrEmpty(userModel.Password))
            {
                return (RedirectToAction("Error"));
            }

            var user = userService.Get(userModel.UserName, userModel.Password); //usamos Get para buscar User e Pass
            var validUser = new UserViewModel { UserName = user.UserName, ID = user.ID, Email = user.Email, Role = user.Role, Avatar = user.Avatar};

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
                    return RedirectToAction("Upload", user); // validUser - temos de criar view UserDetails 
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
        public async Task<IActionResult> SignUp (User user, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var userExists = userService.FindByName(user.UserName);
                if (userExists != null)
                    return StatusCode(StatusCodes.Status500InternalServerError, "User already exists!");

                ////criar pasta images, guardar imagens local
              
                //string path = Path.Combine(this.hostEnvironment.WebRootPath, "images");
                //if (!Directory.Exists(path))
                //{
                //    Directory.CreateDirectory(path);
                //}

                //string fileName = Path.GetFileName(file.FileName);
                //using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                //{
                //    file.CopyTo(stream);
                //    //return RedirectToAction("Image", new ImageUploaded { Path = file.FileName });
                //}

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