using Microsoft.AspNetCore.Mvc;
using PathfinderToolkit.Models;
using System.Diagnostics;
using System.Text.Json;
using Newtonsoft.Json;
using System.ComponentModel;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace PathfinderToolkit.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string username, string password)
        {
            return View();
        }
        public IActionResult GM()
        {
            return View();
        }
        public IActionResult Player()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Resources()
        {
            return View();
        }

    }
}