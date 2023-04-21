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
using static PathfinderToolkit.Models.Resources;
using System;

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
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user, string action)
        {
            UserDatabaseOperations userDb = new UserDatabaseOperations(_configuration);
            if (action == "Login")
            {
                User userFromDb = userDb.GetUserByUsernameAndPassword(user.Username, user.Password);

                if (userFromDb != null)
                {
                    // User exists and credentials are valid
                    // Store the user info in a model and pass it forward
                    var model = new User();
                    model.Id = userFromDb.Id;
                    model.Username = userFromDb.Username;
                    model.IsAdmin = userFromDb.IsAdmin;
                    return View("Account", model);
                }
                else
                {
                    // User does not exist or credentials are invalid
                    // Show an error message
                    ViewBag.Message = "Invalid username or password.";
                    return View("Login");
                }
            }
            else if (action == "Register")
            {
                try
                {
                    //UserDatabaseOperations userDb = new UserDatabaseOperations(_configuration);
                    
                    userDb.CreateUser(user);
                    var model = user;
                    //model.Id = user.Id;
                   // model.Username = user.Username;
                   // model.IsAdmin = user.IsAdmin;
                    return RedirectToAction("Account", model);
                }
                catch (Exception e)
                {
                    ViewBag.Message = e.Message;
                    return View("Account");
                }
            }
            else
            {
                return View("Error");
                // Invalid action
            }
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