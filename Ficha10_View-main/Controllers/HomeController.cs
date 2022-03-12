using Ficha10_View.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;

namespace Ficha10_View.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //Primeira forma
            //Characters ca = JsonLoader.LoadCharactersJSON();  
            //return View(ca);   

            //Segunda forma
            //CharactersController chr = new CharactersController();
            //return View(new Characters (chr.Get().ToList()));

            //Terceira forma
            //var webCliente = new WebClient();           // C:\Users\Net02\Desktop\Ficha10_View\JSON
            //var json = webCliente.DownloadString(@"./JSON/Employees.json");
            //var characters = JsonConvert.DeserializeObject<Characters>(json);

            //Quarta forma
            Characters characters = JsonLoader.LoadCharactersJSON();

            return View(characters);
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