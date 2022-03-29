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
    public class GenreRepository: IRepository<Genre>
    {
        private readonly IOptions<AppConfig> config;
        private string connectionString
        {
            get
            {
                return config.Value.DefaultConnection;
            }
        }
        public GenreRepository(IOptions<AppConfig> options)
        {
            config = options;
        }

        public IEnumerable<Genre> GetAll()
        {
            List<Genre> genres = new List<Genre>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataReader reader = SQLCall.ReadCall(con, "GetGenres");
                while (reader.Read())
                {
                    genres.Add(new Genre
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString()
                    });
                }
            }
            return genres;
        }

        public void Create(Genre genre)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = SQLCall.WriteCall(con, "CreateGenre");
                sqlCommand.Parameters.AddWithValue("@Name", genre.Name);
                sqlCommand.ExecuteNonQuery();
            }
        }

        public Genre GetById(int id)
        {
            Genre genre = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataReader reader = SQLCall.ReadCall(con, "GetGenreById", id);
                while (reader.Read())
                {
                    genre = new Genre
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),                    
                    };
                }
            }
            return genre;
        }

        public void Update(Genre genre)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = SQLCall.WriteCall(con, "UpdateGenre");
                sqlCommand.Parameters.AddWithValue("@Id", genre.Id);
                sqlCommand.Parameters.AddWithValue("@Name", genre.Name);
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
