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

/*        [HttpPost]
        public IActionResult LoginOrRegister(User user)
        {
            var db = new UserDatabaseOperations(_configuration);

            // Check if the user exists in the database
            var userFromDb = db.GetUserByUsernameAndPassword(user.Username, user.Password);

            if (userFromDb != null)
            {
                // Redirect to the home page or another appropriate page
                return RedirectToAction("Index");
            }
            else
            {
                // Check if the username already exists in the database
                var existingUser = db.GetUserByUsername(user.Username);
                if (existingUser != null)
                {
                    ModelState.AddModelError("", "Username already exists");
                    return View();
                }

                // Create a new user and save it to the database
                var newUser = new User
                {
                    Username = user.Username,
                    IsAdmin = false,
                    Password = user.Password
                };
                db.CreateUser(user.Username, user.Password, newUser.IsAdmin);

                // Get the newly created user from the database
                var createdUser = db.GetUserByUsername(newUser.Username);

                // Log in the user and redirect to the home page or another appropriate page
                return RedirectToAction("Index");
            }
        }*/

        [HttpPost]
        public IActionResult Login(User user)
        {
            var db = new UserDatabaseOperations(_configuration);
            var userFromDb = db.GetUserByUsernameAndPassword(user.Username, user.Password);
            
            if (userFromDb != null)
            {
                // Redirect to the home page or another appropriate page
                return RedirectToAction("Index");
            }
            else
            {
                // Show an error message to the user
                ModelState.AddModelError("", "Invalid username or password");
                return View();
            }
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
            var db = new UserDatabaseOperations(_configuration);

            // Check if the username already exists in the database
            var existingUser = db.GetUserByUsername(user.Username);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "Username already exists");
                return View();
            }

            // Create a new user and save it to the database
            var newUser = new User
            {
                Username = user.Username,
                IsAdmin = false,
                Password = user.Password            
            };
            db.CreateUser(user.Username, user.Password, newUser.IsAdmin);

            // Get the newly created user from the database
            var createdUser = db.GetUserByUsername(newUser.Username);

            // Create a new instance of the User class and set its properties
            var model = new User();
            model.Username=createdUser.Username;
            model.IsAdmin = false;
            model.Password=createdUser.Password;

            // Log in the user and redirect to the home page or another appropriate page
            //HttpContext.Session.SetString("Username", createdUser.Username);
            return RedirectToAction("Login", model);
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