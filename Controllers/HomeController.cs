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
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace PathfinderToolkit.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            string connString = _configuration.GetConnectionString("CONNECTIONSTRING");
            try
            {
                // Table would be created ahead of time in production
                using var conn = new SqlConnection(connString);
                conn.Open();

                /*var command = new SqlCommand(
                    "CREATE TABLE Persons (ID int NOT NULL PRIMARY KEY IDENTITY, FirstName varchar(255), LastName varchar(255));",
                    conn);*/
                //using SqlDataReader reader = command.ExecuteReader();
            }
            catch (Exception e)
            {
                // Table may already exist
                Console.WriteLine(e.Message);

            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user1)
        {
            User User = new User();
            User.Username = user1.Username;
            User.Password = user1.Password;

            // Redirect to the home page or another appropriate page
            return RedirectToAction("Index");
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