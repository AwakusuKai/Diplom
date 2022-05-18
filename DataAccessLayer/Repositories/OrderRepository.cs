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
    public class OrderRepository : IRepository<Order>
    {
        private readonly IOptions<AppConfig> config;
        private readonly CopyRepository copyRepository;
        private readonly UserRepository userRepository;

        private string connectionString
        {
            get
            {
                return config.Value.DefaultConnection;
            }
        }
        public OrderRepository(IOptions<AppConfig> options)
        {
            config = options;
            copyRepository = new CopyRepository(config);
            userRepository = new UserRepository(config);
        }

        public IEnumerable<Order> GetAll()
        {
            List<Order> orders = new List<Order>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataReader reader = SQLCall.ReadCall(con, "GetOrders"); 
                while (reader.Read())
                {
                    orders.Add(new Order
                    {
                        Id = Convert.ToInt32(reader["OrderId"]),
                        UserId = Convert.ToInt32(reader["OrderId"]),
                        User = userRepository.GetById(Convert.ToInt32(reader["OrderId"])),
                        CopyId = Convert.ToInt32(reader["CopyId"]),
                        Copy = copyRepository.GetById(Convert.ToInt32(reader["OrderId"]))
                    }); 
                }
            }
            return orders;
        }

        public void Create(Order order)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = SQLCall.WriteCall(con, "CreateOrder"); 
                sqlCommand.Parameters.AddWithValue("@CopyId", order.CopyId);
                sqlCommand.Parameters.AddWithValue("@UserId", order.UserId);
                sqlCommand.ExecuteNonQuery();
            }
        }

        public Order GetById(int id)
        {
            Order order = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataReader reader = SQLCall.ReadCall(con, "GetOrderById", id);
                while (reader.Read())
                {
                    order = new Order
                    {
                        Id = Convert.ToInt32(reader["OrderId"]),
                        UserId = Convert.ToInt32(reader["OrderId"]),
                        User = userRepository.GetById(Convert.ToInt32(reader["OrderId"])),
                        CopyId = Convert.ToInt32(reader["CopyId"]),
                        Copy = copyRepository.GetById(Convert.ToInt32(reader["OrderId"]))
                    };
                }
            }
            return order;
        }

        public void Update(Order order)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = SQLCall.WriteCall(con, "UpdateOrder");
                sqlCommand.Parameters.AddWithValue("@Id", order.Id);
                sqlCommand.Parameters.AddWithValue("@CopyId", order.CopyId);
                sqlCommand.Parameters.AddWithValue("@UserId", order.UserId);
                sqlCommand.ExecuteNonQuery();
            }
        }
        public void Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = SQLCall.WriteCall(con, "DeleteOrderById");
                sqlCommand.Parameters.AddWithValue("@Id", id);
                sqlCommand.ExecuteNonQuery();
            }
        }

    }
}
