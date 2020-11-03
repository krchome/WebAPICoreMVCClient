using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;

namespace WebAPI.Models
{
    public class OrderRepository:IOrderRepository
    {
        public IConfiguration Configuration { get; }
        public string connectionString;
        public OrderRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
            connectionString = Configuration["ConnectionStrings:DefaultConnection"];
        }


        public IEnumerable<Order> GetAllOrders()
        {
            List<Order> orders = new List<Order>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[spSelectOrder]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Order order = new Order();
                    order.Id = Convert.ToInt32(rdr["Id"]);
                    order.CustomerId = Convert.ToInt32(rdr["CustomerId"]);
                    order.Description = rdr["Description"].ToString();
                    order.OrderCost = Convert.ToDecimal(rdr["OrderCost"]);
                    orders.Add(order);

                }
                con.Close();
            }
            return orders;
        }

        public Order AddOrder(Order order)
        {
          
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[spInsertIntoOrder]", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                cmd.Parameters.AddWithValue("@CustomerId", order.CustomerId);
                cmd.Parameters.AddWithValue("@Description", order.Description);
                cmd.Parameters.AddWithValue("@OrderCost", order.OrderCost);
                
                // cmd.Parameters.AddWithValue("@ret", ParameterDirection.Output);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            return order;
        }

        public void DeleteOrder(int? id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[spDeleteOrder]", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
                connection.Close();
            }

        }
        public Order UpdateOrder(Order order)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[spUpdateOrder]", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                cmd.Parameters.AddWithValue("@Id", order.Id);
                cmd.Parameters.AddWithValue("@CustomerId", order.CustomerId);
                cmd.Parameters.AddWithValue("@Description", order.Description);
                cmd.Parameters.AddWithValue("@OrderCost", order.OrderCost);
                cmd.ExecuteNonQuery();
                connection.Close();
            }

            return order;
        }

        public Order GetOrderById(int id)
        {
            Order order = new Order();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("dbo.spSelectOrderById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    order.Id = Convert.ToInt32(rdr["Id"]);
                    order.CustomerId = Convert.ToInt32(rdr["CustomerId"]);
                    order.Description = rdr["Description"].ToString();
                    order.OrderCost = Convert.ToDecimal(rdr["OrderCost"]);
                }
                con.Close();
            }
            return order;
        }
    }
}
