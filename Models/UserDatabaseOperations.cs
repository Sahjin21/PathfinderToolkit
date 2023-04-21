using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using Google.Apis.Drive.v3.Data;
using System.Diagnostics;
using PathfinderToolkit.Models;

namespace PathfinderToolkit.Models
{
    public class UserDatabaseOperations : User
    {
        private readonly IConfiguration _configuration;
        private string connString;
        string connectionString = Environment.GetEnvironmentVariable("CONNECTIONSTRING");
        private readonly string _connString;


        public UserDatabaseOperations(IConfiguration configuration)
        {
            _configuration = configuration;
            connString = _configuration.GetConnectionString("CONNECTIONSTRING");
        }

        public User GetUserByUsernameAndPassword(string username, string password)
        {
            string connString = _configuration.GetConnectionString("CONNECTIONSTRING");
                using (var conn = new SqlConnection(connString))
                {
                    conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Users] WHERE [user] = @Username AND [password]=@Password", conn);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        User userFromDb = new User();
                        userFromDb.Id = Convert.ToInt32(reader["Id"]);
                        //System.Diagnostics.Debug.WriteLine("Selected User Id: " + userFromDb.Id);
                        userFromDb.Username = Convert.ToString(reader["user"]);
                        //System.Diagnostics.Debug.WriteLine("Selected User name: " + userFromDb.Username);
                        userFromDb.Password = Convert.ToString(reader["password"]);
                        //System.Diagnostics.Debug.WriteLine("Selected User name: " + userFromDb.Password);
                        userFromDb.IsAdmin = Convert.ToBoolean(reader["admin"]);
                        return userFromDb;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public void CreateUser(User user)
        {
            string connString = _configuration.GetConnectionString("CONNECTIONSTRING");
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [dbo].[Users] WHERE [user] = @Username", conn);
                cmd.Parameters.AddWithValue("@Username", user.Username);
                int count = (int)cmd.ExecuteScalar();
                if (count == 0)
                {
                    cmd = new SqlCommand("INSERT INTO [dbo].[Users] ([user], password, admin) VALUES (@Username, @Password, @IsAdmin)", conn);
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@IsAdmin", user.IsAdmin);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    throw new Exception("Username already exists");
                }
            }
        }

        // Update an existing user
        public void UpdateUser(int id, string username, string password, bool admin)
        {
            using (var conn = new SqlConnection(connString))
            {
                var sql = @"UPDATE Users SET user = @user, password = @password, admin = @admin WHERE id = @id";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@user", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@admin", admin);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Get a user by username
        public User GetUserByUsername(string name)
        {
            using (var conn = new SqlConnection(_connString))
            {
                //var sql = "SELECT * FROM Users WHERE user = '" + name + "'";
                var sql = "SELECT * FROM Users WHERE user = @Username";
                Debug.WriteLine(@Username);
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@Username", name);
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        var user = new User();
                        if (reader.Read())
                        {
                            
                            user.Id = reader.GetInt32(0);
                            user.Username = reader.GetString(1);
                            user.Password = reader.GetString(2);
                            user.IsAdmin = reader.GetBoolean(3);
                            Debug.WriteLine("User: {0}, Password: {1}, IsAdmin: {2}", user.Username, user.Password, user.IsAdmin);
                            Console.WriteLine("User: {0}, Password: {1}, IsAdmin: {2}", user.Username, user.Password, user.IsAdmin);
                            return user;
                        }
                        Debug.WriteLine(user.Username + user.Password);
                    }
                }
            }
            return null;
        }


        // Get a user by ID
        public User GetUserById(int id)
        {
            using (var conn = new SqlConnection(connString))
            {
                var sql = @"SELECT id, user, password, admin FROM Users WHERE id = @id";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var user = new User();
                            user.Id = reader.GetInt32(0);
                            user.Username = reader.GetString(1);
                            user.Password = reader.GetString(2);
                            user.IsAdmin = reader.GetBoolean(3);
                            return user;
                        }
                    }
                }
            }
            return null;
        }

        // Delete a user by ID
        public void DeleteUserById(int id)
        {
            using (var conn = new SqlConnection(connString))
            {
                var sql = @"DELETE FROM Users WHERE id = @id";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}





/*[HttpPost]
public IActionResult Login(User user)
{
    string connString = Environment.GetEnvironmentVariable("CONNECTIONSTRING");
    UserDatabaseOperations userDb = new UserDatabaseOperations(connString);
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
*/


// This works to query
/*
[HttpPost]
public IActionResult Login(User user)
{
    string connString = _configuration.GetConnectionString("CONNECTIONSTRING");
    var model = new User();
    try
    {
        using (var conn = new SqlConnection(connString))
        {
            conn.Open();

            using (var command = new SqlCommand("SELECT * FROM [dbo].[Users] WHERE [user] = 'shawnembry';", conn))

            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["Id"]);
                        //string username = reader.GetString(1);
                        string username = reader["user"].ToString();
                        string password = reader["password"].ToString();
                        bool isAdmin = reader.GetBoolean(3);

                        model.Id = Convert.ToInt32(reader["Id"]);
                        model.Username = reader["user"].ToString();
                        model.Password = reader["password"].ToString();
                        model.IsAdmin = isAdmin;

                        System.Diagnostics.Debug.WriteLine($"id={id}, username={username}, password={password}, isAdmin={isAdmin}");
                    }
                }
            }
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }*/