using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Configuration;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using DataAccessLayer.SQL;
using Type = DataAccessLayer.Entities.Type;

namespace DataAccessLayer.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly IOptions<AppConfig> config;
        private string connectionString
        {
            get
            {
                return config.Value.DefaultConnection;
            }
        }
        public UserRepository(IOptions<AppConfig> options)
        {
            config = options;
        }

        public IEnumerable<User> GetAll()
        {
            List<User> users = new List<User>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataReader reader = SQLCall.ReadCall(con, "GetUsers"); 
                while (reader.Read())
                {
                    users.Add(new User
                    {
                        Id = Convert.ToInt32(reader["UserId"]),
                        Name = reader["UserName"].ToString(),
                        Surname = reader["Surname"].ToString(),
                        Password = reader["Password"].ToString(),
                        RoleId = Convert.ToInt32(reader["RoleId"]),
                        Role = new Role { Id = Convert.ToInt32(reader["RoleId"]), Name = reader["RoleName"].ToString()}
                    });
                }
            }
            return users;
        }

        public void Create(User user)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = SQLCall.WriteCall(con, "CreateUser");
                sqlCommand.Parameters.AddWithValue("@Id", user.Id);
                sqlCommand.Parameters.AddWithValue("@Password", user.Password);
                sqlCommand.Parameters.AddWithValue("@Name", user.Name);
                sqlCommand.Parameters.AddWithValue("@Surname", user.Surname);
                sqlCommand.Parameters.AddWithValue("@RoleId", user.RoleId);
                sqlCommand.ExecuteNonQuery();
            }
        }

        public User GetById(int id)
        {
            User user = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataReader reader = SQLCall.ReadCall(con, "GetUserById", id);
                while (reader.Read())
                {
                    user = new User
                    {
                        Id = Convert.ToInt32(reader["UserId"]),
                        Name = reader["UserName"].ToString(),
                        Surname = reader["Surname"].ToString(),
                        Password = reader["Password"].ToString(),
                        RoleId = Convert.ToInt32(reader["RoleId"]),
                        Role = new Role { Id = Convert.ToInt32(reader["RoleId"]), Name = reader["RoleName"].ToString() }
                    };
                }
            }
            return user;
        }

        public void Update(User user)
        {
           //do nothing
        }
        public void Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = SQLCall.WriteCall(con, "DeleteUserById");
                sqlCommand.Parameters.AddWithValue("@Id", id);
                sqlCommand.ExecuteNonQuery();
            }
        }

    }
}
