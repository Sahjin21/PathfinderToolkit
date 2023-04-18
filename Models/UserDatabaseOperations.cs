using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace PathfinderToolkit.Models
{
    public class UserDatabaseOperations : User
    {
        private readonly IConfiguration _configuration;
        private string connString;

        public UserDatabaseOperations(IConfiguration configuration)
        {
            _configuration = configuration;
            connString = _configuration.GetConnectionString("CONNECTIONSTRING");
        }

        public User GetUserByUsernameAndPassword(string username, string password)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE user=@Username AND password=@Password", conn);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        User userFromDb = new User();
                        userFromDb.Id = Convert.ToInt32(reader["Id"]);
                        userFromDb.Username = Convert.ToString(reader["Username"]);
                        userFromDb.Password = Convert.ToString(reader["Password"]);
                        userFromDb.IsAdmin = Convert.ToBoolean(reader["Admin"]);
                        return userFromDb;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        // Create a new user
        public void CreateUser(string username, string password, bool admin)
        {
            using (var conn = new SqlConnection(connString))
            {
                var sql = @"INSERT INTO Users (user, password, admin) VALUES (@user, @password, @admin)";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@user", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@admin", admin);
                    conn.Open();
                    cmd.ExecuteNonQuery();
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
            using (var conn = new SqlConnection(connString))
            {
                var sql = @"SELECT id, user, password, admin FROM Users WHERE username = @Username";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", name);
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