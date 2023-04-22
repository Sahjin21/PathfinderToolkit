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
        string? connectionString = Environment.GetEnvironmentVariable("CONNECTIONSTRING");
        private readonly string _connString;


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public UserDatabaseOperations(IConfiguration configuration)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            _configuration = configuration;
            connString = _configuration.GetConnectionString("CONNECTIONSTRING");
        }

        public User? GetUserByUsernameAndPassword(string username, string password)
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
                        userFromDb.Username = Convert.ToString(reader["user"]);
                        userFromDb.Password = Convert.ToString(reader["password"]);
                        userFromDb.IsAdmin = Convert.ToBoolean(reader["admin"]);
                        userFromDb.FirstName = Convert.ToString(reader["first_name"]);
                        userFromDb.LastName = Convert.ToString(reader["last_name"]);
                        userFromDb.Email = Convert.ToString(reader["email"]);
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

        public void UpdateUser(User user)
        {
            string connString = _configuration.GetConnectionString("CONNECTIONSTRING");
            using (var conn = new SqlConnection(connString))
            {
                var sql = @"UPDATE [dbo].[Users] SET password = @Password, first_name = @FirstName, last_name = @LastName, email = @Email WHERE id = @Id";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", user.Id);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@FirstName", string.IsNullOrEmpty(user.FirstName) ? "" : user.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", string.IsNullOrEmpty(user.LastName) ? "" : user.LastName);
                    cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(user.Email) ? "" : user.Email);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Needs to be fixed
        // Get a user by ID
        public User? GetUserById(int id)
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

