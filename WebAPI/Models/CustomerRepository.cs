using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;

namespace WebAPI.Models
{
    public class CustomerRepository:ICustomerRepository
    {
        public IConfiguration Configuration { get; }
        public string connectionString;
        public CustomerRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
            connectionString = Configuration["ConnectionStrings:DefaultConnection"];
        }


        public IEnumerable<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[spSelectCustomer]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Customer customer = new Customer();
                    customer.Id = Convert.ToInt32(rdr["Id"]);
                    customer.Name = rdr["Name"].ToString();
                    customer.Address = rdr["Address"].ToString();
                    customer.Telephone = rdr["Telephone"].ToString();
                    customer.Email = rdr["Email"].ToString();
                    customers.Add(customer);

                }
                con.Close();
            }
            return customers;
        }

        public Customer AddCustomer(Customer customer)
        {
          
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[spInsertIntoCustomer]", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                cmd.Parameters.AddWithValue("@Name", customer.Name);
                cmd.Parameters.AddWithValue("@Address", customer.Address);
                cmd.Parameters.AddWithValue("@Telephone", customer.Telephone);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                // cmd.Parameters.AddWithValue("@ret", ParameterDirection.Output);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            return customer;
        }

        public void DeleteCustomer(int? id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[spDeleteCustomer]", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
                connection.Close();
            }

        }
        public Customer UpdateCustomer(Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[spUpdateCustomer]", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                cmd.Parameters.AddWithValue("@Id", customer.Id);
                cmd.Parameters.AddWithValue("@Name", customer.Name);
                cmd.Parameters.AddWithValue("@Address", customer.Address);
                cmd.Parameters.AddWithValue("@Telephone", customer.Telephone);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.ExecuteNonQuery();
                connection.Close();
            }

            return customer;
        }

        public Customer GetCustomerById(int id)
        {
            Customer customer = new Customer();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[spSelectCustomerById]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    customer.Id = id;
                    customer.Name = rdr["Name"].ToString();
                    customer.Address = rdr["Address"].ToString();
                    customer.Telephone = rdr["Telephone"].ToString();
                    customer.Email = rdr["Email"].ToString();
                }
                con.Close();
            }
            return customer;
        }
    }
}
