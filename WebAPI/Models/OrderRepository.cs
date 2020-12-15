using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<OrderRepository> _logger;
        public OrderRepository(IConfiguration configuration, ILogger<OrderRepository> logger)
        {
            this.Configuration = configuration;
            connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            _logger = logger;
        }


        public IEnumerable<Order> GetAllOrders()
        {
            List<Order> orders = new List<Order>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
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
                catch (Exception ex)
                {
                    //ex.Message.ToString();
                    _logger.LogError(ex, "Error at GetAllOrders() :(");
                    orders = null;
                }
            }
            return orders;
        }

        public Order AddOrder(Order order)
        {
           
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    _logger.LogInformation("Could break here!!");
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
                catch(Exception ex)
                {
                    //ex.Message.ToString();
                    _logger.LogError(ex, "Error at AddOrder() :(");
                    order = null;
                }
            }
            return order;
        }

        public void DeleteOrder(int? id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("[dbo].[spDeleteOrder]", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    //ex.Message.ToString();
                    _logger.LogError(ex, "Error at DeleteOrder() :(");
                    
                }
            }

        }
        public Order UpdateOrder(Order order)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
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
                catch (Exception ex)
                {
                    //ex.Message.ToString();
                    _logger.LogError(ex, "Error at UpdateOrder() :(");
                    order = null;

                }
            }

            return order;
        }

        public Order GetOrderById(int id)
        {
            Order order = new Order();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
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
                catch (Exception ex)
                {
                    //ex.Message.ToString();
                    _logger.LogError(ex, "Error at GetOrderById() :(");

                }

            }
            return order;
        }
    }
}
