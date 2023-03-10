using Microsoft.AspNetCore.Mvc;
using PathfinderToolkit.Models;
using System.Diagnostics;
using System.Text.Json;
using PathfinderToolkit.Models;
using Newtonsoft.Json;
using System.ComponentModel;
using static PathfinderToolkit.Models.Resources.Bestiary;

namespace PathfinderToolkit.Controllers
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
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Resources()
        {
            string jsonFilePath = "C:/Users/kerds/Documents/GitHub/PathfinderToolkit/wwwroot/Data/Json PF/test.json";
            string jsonString = System.IO.File.ReadAllText(jsonFilePath);

            var json = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, List<Resources.Test>>>(jsonString);
            var testList = json["test"].ToArray();
            return View(testList);
        }


    }
}