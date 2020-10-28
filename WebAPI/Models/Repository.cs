using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace APIControllers.Models
{
    public class Repository: IRepository
    {
        public IConfiguration Configuration { get; }
        public string connectionString;
        public Repository(IConfiguration configuration)
        {
            this.Configuration = configuration;
            connectionString = Configuration["ConnectionStrings:DefaultConnection"];
        }

        
        public IEnumerable<Reservation> GetAllReservations()
        {
            List<Reservation> reservations = new List<Reservation>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[spSelectReservation]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while(rdr.Read())
                {
                    Reservation reservation = new Reservation();
                    reservation.Id = Convert.ToInt32(rdr["Id"]);
                    reservation.Name = rdr["Name"].ToString();
                    reservation.StartLocation = rdr["Start Location"].ToString();
                    reservation.EndLocation = rdr["End Location"].ToString();
                    reservations.Add(reservation);

                }
                con.Close();
            }
            return reservations;
        }
        
        public Reservation AddReservation(Reservation reservation)
        {
            Reservation receivedReservation = new Reservation();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[spInsertIntoReservation]", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                cmd.Parameters.AddWithValue("@Name", reservation.Name);
                cmd.Parameters.AddWithValue("@StartLocation", reservation.StartLocation);
                cmd.Parameters.AddWithValue("@EndLocation", reservation.EndLocation);
                // cmd.Parameters.AddWithValue("@ret", ParameterDirection.Output);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            return reservation;


        }
        
        public void DeleteReservation(int? id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[spDeleteReservation]", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
                connection.Close();
            }

        }
        
       

       

        public Reservation UpdateReservation(Reservation reservation)
        {
            Reservation receivedReservation = new Reservation();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[spUpdateReservation]", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                cmd.Parameters.AddWithValue("@Id", reservation.Id);
                cmd.Parameters.AddWithValue("@Name", reservation.Name);
                cmd.Parameters.AddWithValue("@StartLocation", reservation.StartLocation);
                cmd.Parameters.AddWithValue("@EndLocation", reservation.EndLocation);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            
            return reservation;
        }

        public Reservation GetReservationById(int id)
        {
            Reservation reservation = new Reservation();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[spSelectReservationById]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    reservation.Id = id;
                    reservation.Name = rdr["Name"].ToString();
                    reservation.StartLocation = rdr["Start Location"].ToString();
                    reservation.EndLocation = rdr["End Location"].ToString();
                    

                }
                con.Close();
            }
            return reservation;
        }
    }
}
