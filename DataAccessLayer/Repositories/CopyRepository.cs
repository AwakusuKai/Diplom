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
    public class CopyRepository : IRepository<Copy>
    {
        private readonly IOptions<AppConfig> config;
        private readonly BookRepository bookRepository;
        private string connectionString
        {
            get
            {
                return config.Value.DefaultConnection;
            }
        }
        public CopyRepository(IOptions<AppConfig> options)
        {
            config = options;
            bookRepository = new BookRepository(config);
        }

        public IEnumerable<Copy> GetAll()
        {
            List<Copy> copies = new List<Copy>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataReader reader = SQLCall.ReadCall(con, "GetCopies");
                while (reader.Read())
                {
                    copies.Add(new Copy
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        BookId = Convert.ToInt32(reader["BookId"]),
                        Book = bookRepository.GetById(Convert.ToInt32(reader["BookId"])),
                        Bookcase = Convert.ToInt32(reader["Bookcase"]),
                        Shelf = Convert.ToInt32(reader["Shelf"]),
                        Status = Convert.ToInt32(reader["Status"])
                    });
                }
            }
            return copies;
        }

        public void Create(Copy copy)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = SQLCall.WriteCall(con, "CreateCopy");
                sqlCommand.Parameters.AddWithValue("@BookId", copy.BookId);
                sqlCommand.Parameters.AddWithValue("@Bookcase", copy.Bookcase);
                sqlCommand.Parameters.AddWithValue("@Shelf", copy.Shelf);
                sqlCommand.Parameters.AddWithValue("@Status", copy.Status);
                sqlCommand.ExecuteNonQuery();
            }
        }

        public Copy GetById(int id)
        {
            Copy copy = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataReader reader = SQLCall.ReadCall(con, "GetCopyById", id);
                while (reader.Read())
                {
                    copy = new Copy
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        BookId = Convert.ToInt32(reader["BookId"]),
                        Book = bookRepository.GetById(Convert.ToInt32(reader["BookId"])),
                        Bookcase = Convert.ToInt32(reader["Bookcase"]),
                        Shelf = Convert.ToInt32(reader["Shelf"]),
                        Status = Convert.ToInt32(reader["Status"])
                    };
                }
            }
            return copy;
        }

        public void Update(Copy copy)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = SQLCall.WriteCall(con, "UpdateCopy");
                sqlCommand.Parameters.AddWithValue("@Id", copy.Id);
                sqlCommand.Parameters.AddWithValue("@BookId", copy.BookId);
                sqlCommand.Parameters.AddWithValue("@Bookcase", copy.Bookcase);
                sqlCommand.Parameters.AddWithValue("@Shelf", copy.Shelf);
                sqlCommand.Parameters.AddWithValue("@Status", copy.Status);
                sqlCommand.ExecuteNonQuery();
            }
        }
        public void Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = SQLCall.WriteCall(con, "DeleteCopyById");
                sqlCommand.Parameters.AddWithValue("@Id", id);
                sqlCommand.ExecuteNonQuery();
            }
        }
    }
}
