using DataAccessLayer.Configuration;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.SQL;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class PHRepository: IRepository<PublishingHouse>
    {
        private readonly IOptions<AppConfig> config;
        private string connectionString
        {
            get
            {
                return config.Value.DefaultConnection;
            }
        }
        public PHRepository(IOptions<AppConfig> options)
        {
            config = options;
        }

        public IEnumerable<PublishingHouse> GetAll()
        {
            List<PublishingHouse> publishingHouses = new List<PublishingHouse>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataReader reader = SQLCall.ReadCall(con, "GetPHs");
                while (reader.Read())
                {
                    publishingHouses.Add(new PublishingHouse
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString()
                    });
                }
            }
            return publishingHouses;
        }

        public void Create(PublishingHouse publishingHouse)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = SQLCall.WriteCall(con, "CreatePH");
                sqlCommand.Parameters.AddWithValue("@Name", publishingHouse.Name);
                sqlCommand.ExecuteNonQuery();
            }
        }

        public PublishingHouse GetById(int id)
        {
            PublishingHouse publishingHouse = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataReader reader = SQLCall.ReadCall(con, "GetPHById", id);
                while (reader.Read())
                {
                    publishingHouse = new PublishingHouse
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),                    
                    };
                }
            }
            return publishingHouse;
        }

        public void Update(PublishingHouse publishingHouse)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = SQLCall.WriteCall(con, "UpdatePH");
                sqlCommand.Parameters.AddWithValue("@Id", publishingHouse.Id);
                sqlCommand.Parameters.AddWithValue("@Name", publishingHouse.Name);
                sqlCommand.ExecuteNonQuery();
            }
        }
        public void Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = SQLCall.WriteCall(con, "DeletePHById");
                sqlCommand.Parameters.AddWithValue("@Id", id);
                sqlCommand.ExecuteNonQuery();
            }
        }
    }
}
