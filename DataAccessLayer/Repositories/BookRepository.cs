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
    public class BookRepository : IRepository<Book>
    {
        private readonly IOptions<AppConfig> config;
        private string connectionString
        {
            get
            {
                return config.Value.DefaultConnection;
            }
        }
        public BookRepository(IOptions<AppConfig> options)
        {
            config = options;
        }

        public IEnumerable<Book> GetAll()
        {
            List<Book> books = new List<Book>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataReader reader = SQLCall.ReadCall(con, "GetBooks"); 
                while (reader.Read())
                {
                    books.Add(new Book
                    {
                        Id = Convert.ToInt32(reader["BookId"]),
                        Name = reader["BookName"].ToString(),
                        PublishingYear = Convert.ToInt32(reader["PublishingYear"]),
                        AutorId = Convert.ToInt32(reader["AutorId"]),
                        TypeId = Convert.ToInt32(reader["TypeId"]),
                        PublishingHouseId = Convert.ToInt32(reader["PublishingHouseId"]),
                        GenreId = Convert.ToInt32(reader["GenreId"]),
                        Autor = new Autor { Id = Convert.ToInt32(reader["AutorId"]), Name = reader["AutorName"].ToString()},
                        PublishingHouse = new PublishingHouse { Id = Convert.ToInt32(reader["PHId"]), Name = reader["PHName"].ToString() },
                        Genre = new Genre { Id = Convert.ToInt32(reader["GenreId"]), Name = reader["GenreName"].ToString() },
                        Type = new Type { Id = Convert.ToInt32(reader["TypeId"]), Name = reader["TypeName"].ToString() }
                    });
                }
            }
            return books;
        }

        public void Create(Book book)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = SQLCall.WriteCall(con, "CreateBook"); 
                sqlCommand.Parameters.AddWithValue("@Name", book.Name);
                sqlCommand.Parameters.AddWithValue("@AutorId", book.AutorId);
                sqlCommand.Parameters.AddWithValue("@GenreId", book.GenreId);
                sqlCommand.Parameters.AddWithValue("@TypeId", book.TypeId);
                sqlCommand.Parameters.AddWithValue("@PublishingYear", book.PublishingYear);
                sqlCommand.Parameters.AddWithValue("@PublishingHouseId", book.PublishingHouseId);
                sqlCommand.ExecuteNonQuery();
            }
        }

        public Book GetById(int id)
        {
            Book book = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataReader reader = SQLCall.ReadCall(con, "GetBookById", id);
                while (reader.Read())
                {
                    book = new Book
                    {
                        Id = Convert.ToInt32(reader["BookId"]),
                        Name = reader["BookName"].ToString(),
                        PublishingYear = Convert.ToInt32(reader["PublishingYear"]),
                        AutorId = Convert.ToInt32(reader["AutorId"]),
                        TypeId = Convert.ToInt32(reader["TypeId"]),
                        PublishingHouseId = Convert.ToInt32(reader["PublishingHouseId"]),
                        GenreId = Convert.ToInt32(reader["GenreId"]),
                        Autor = new Autor { Id = Convert.ToInt32(reader["AutorId"]), Name = reader["AutorName"].ToString() },
                        PublishingHouse = new PublishingHouse { Id = Convert.ToInt32(reader["PHId"]), Name = reader["PHName"].ToString() },
                        Genre = new Genre { Id = Convert.ToInt32(reader["GenreId"]), Name = reader["GenreName"].ToString() },
                        Type = new Type { Id = Convert.ToInt32(reader["TypeId"]), Name = reader["TypeName"].ToString() }
                    };
                }
            }
            return book;
        }

        public void Update(Book book)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = SQLCall.WriteCall(con, "spUpdateBook");
                sqlCommand.Parameters.AddWithValue("@Id", book.Id);
                sqlCommand.Parameters.AddWithValue("@Name", book.Name);
                sqlCommand.Parameters.AddWithValue("@AutorId", book.AutorId);
                sqlCommand.Parameters.AddWithValue("@Genre", book.Genre);
                sqlCommand.Parameters.AddWithValue("@TypeId", book.TypeId);
                sqlCommand.Parameters.AddWithValue("@PublishingYear", book.PublishingYear);
                sqlCommand.Parameters.AddWithValue("@PublishingHouseId", book.PublishingHouseId);
                sqlCommand.ExecuteNonQuery();
            }
        }
        public void Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = SQLCall.WriteCall(con, "DeleteBookById");
                sqlCommand.Parameters.AddWithValue("@Id", id);
                sqlCommand.ExecuteNonQuery();
            }
        }

    }
}
