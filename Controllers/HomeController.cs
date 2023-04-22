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
using Microsoft.Extensions.Hosting;
//using Google.Apis.Drive.v3.Data;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core.DAG;
using Microsoft.VisualBasic;
using static Microsoft.Azure.Management.ResourceManager.Fluent.ResourceManager;

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


          /*Polymorphism: Polymorphism refers to the ability of objects to take on many forms.
          In your code, you are using method polymorphism by defining the Login method in the 
          AccountController class with different method signatures
          (i.e., one with an empty parameter list for the HTTP GET request and one with a User parameter for 
          the HTTP POST request). You are also using method overriding by defining the ToString method in 
          the User class to return a string representation of the object.*/

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
        /*Abstraction: Abstraction is the process of hiding implementation details while showing only 
          the necessary information to the user.In your code, you are abstracting the details of 
          the database connection and SQL queries behind the SqlConnection and SqlCommand classes. 
          The user of your code (i.e., the web application) only needs to know that it 
          can use the Login method to authenticate a user, and it does not need to know the details 
          of how the database connection is made or how the SQL query is executed.*/

        [HttpPost]
        public IActionResult AccountUpdate(User model)
        {
            // Perform validation on the model and update the user information in the database
            if (ModelState.IsValid)
            {
                UserDatabaseOperations userDb = new UserDatabaseOperations(_configuration);
                var user = new User
                {
                    Id = model.Id,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email
                };
                userDb.UpdateUser(user);
                return RedirectToAction("Account");
            }
            else
            {
                // Model validation failed, return the model with error messages
                return View("Account", model);
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