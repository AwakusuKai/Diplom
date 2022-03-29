using DataAccessLayer.Configuration;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.SQL;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Type = DataAccessLayer.Entities.Type;

namespace DataAccessLayer.Repositories
{
    public class TypeRepository: IRepository<Type>
    {
        private readonly IOptions<AppConfig> config;
        private string connectionString
        {
            get
            {
                return config.Value.DefaultConnection;
            }
        }
        public TypeRepository(IOptions<AppConfig> options)
        {
            config = options;
        }

        public IEnumerable<Type> GetAll()
        {
            List<Type> types = new List<Type>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataReader reader = SQLCall.ReadCall(con, "GetTypes");
                while (reader.Read())
                {
                    types.Add(new Type
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString()
                    });
                }
            }
            return types;
        }

        public void Create(Type type)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = SQLCall.WriteCall(con, "CreateType");
                sqlCommand.Parameters.AddWithValue("@Name", type.Name);
                sqlCommand.ExecuteNonQuery();
            }
        }

        public Type GetById(int id)
        {
            Type type = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataReader reader = SQLCall.ReadCall(con, "GetTypeById", id);
                while (reader.Read())
                {
                    type = new Type
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),                    
                    };
                }
            }
            return type;
        }

        public void Update(Type type)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = SQLCall.WriteCall(con, "UpdateGenre");
                sqlCommand.Parameters.AddWithValue("@Id", type.Id);
                sqlCommand.Parameters.AddWithValue("@Name", type.Name);
                sqlCommand.ExecuteNonQuery();
            }
        }
        public void Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = SQLCall.WriteCall(con, "DeleteGenreById");
                sqlCommand.Parameters.AddWithValue("@Id", id);
                sqlCommand.ExecuteNonQuery();
            }
        }
    }
}
