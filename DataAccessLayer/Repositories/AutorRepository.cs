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
    public class AutorRepository: IRepository<Autor>
    {
        private readonly IOptions<AppConfig> config;
        private string connectionString
        {
            get
            {
                return config.Value.DefaultConnection;
            }
        }
        public AutorRepository(IOptions<AppConfig> options)
        {
            config = options;
        }

        public IEnumerable<Autor> GetAll()
        {
            List<Autor> autors = new List<Autor>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataReader reader = SQLCall.ReadCall(con, "GetAutors");
                while (reader.Read())
                {
                    autors.Add(new Autor
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString()
                    });
                }
            }
            return autors;
        }

        public void Create(Autor autor)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = SQLCall.WriteCall(con, "CreateAutor");
                sqlCommand.Parameters.AddWithValue("@Name", autor.Name);
                sqlCommand.ExecuteNonQuery();
            }
        }

        public Autor GetById(int id)
        {
            Autor autor = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataReader reader = SQLCall.ReadCall(con, "GetAutorById", id);
                while (reader.Read())
                {
                    autor = new Autor
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),                    };
                }
            }
            return autor;
        }

        public void Update(Autor autor)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = SQLCall.WriteCall(con, "UpdateAutor");
                sqlCommand.Parameters.AddWithValue("@Id", autor.Id);
                sqlCommand.Parameters.AddWithValue("@Name", autor.Name);
                sqlCommand.ExecuteNonQuery();
            }
        }
        public void Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = SQLCall.WriteCall(con, "DeleteAutorById");
                sqlCommand.Parameters.AddWithValue("@Id", id);
                sqlCommand.ExecuteNonQuery();
            }
        }
    }
}
