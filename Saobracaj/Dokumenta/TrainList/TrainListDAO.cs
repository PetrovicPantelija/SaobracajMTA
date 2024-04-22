using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Saobracaj.Dokumenta.TrainList
{
    public class TrainListDAO
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

        public int Insert(TrainListModel obj)
        {
            int success = -1;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("TrainList_Insert", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@VremeDolaska", obj.VremeDolaska);
                command.Parameters.AddWithValue("@KomOznaka", obj.KomOznaka);
                command.Parameters.AddWithValue("@Napomena", obj.Napomena);
                try
                {
                    success = command.ExecuteNonQuery();
                }
                catch (SqlException)
                {
                    // throw new Exception("ERROR!");
                }
            }
            return success;
        }

        public int Update(TrainListModel obj)
        {
            int success = -1;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("TrainList_Update", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", obj.Id);
                command.Parameters.AddWithValue("@VremeDolaska", obj.VremeDolaska);
                command.Parameters.AddWithValue("@KomOznaka", obj.KomOznaka);
                command.Parameters.AddWithValue("@Napomena", obj.Napomena);
                try
                {
                    success = command.ExecuteNonQuery();
                }
                catch (SqlException)
                {
                    throw new Exception("ERROR!");
                }
            }
            return success;
        }

        public int Delete(int Id)
        {
            int success = -1;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("TrainList_Delete", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", Id);

                try
                {
                    success = command.ExecuteNonQuery();
                }
                catch (SqlException)
                {
                    throw new Exception("ERROR!");
                }
            }
            return success;
        }

        public List<TrainListModel> GetAll()
        {
            List<TrainListModel> returnThese = new List<TrainListModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("TrainList_GetAll", con);
                command.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.Add(new SqlParameter("@CustomerID", custId));
                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            returnThese.Add(new TrainListModel { Id = (int)reader[0], VremeDolaska = (DateTime)reader[1], KomOznaka = (string)reader[2], Napomena = (string)reader[3] });
                        }
                    }
                }
                catch (SqlException)
                {
                    // throw new Exception("ERROR!");
                }
            }
            return returnThese;
        }

        public List<TrainListModel> GetAllActive()
        {
            List<TrainListModel> returnThese = new List<TrainListModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("TrainList_GetAllActive", con);
                command.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.Add(new SqlParameter("@CustomerID", custId));
                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            returnThese.Add(new TrainListModel { Id = (int)reader[0], VremeDolaska = (DateTime)reader[1], KomOznaka = (string)reader[2], Napomena = (string)reader[3] });
                        }
                    }
                }
                catch (SqlException)
                {
                    // throw new Exception("ERROR!");
                }
            }
            return returnThese;
        }

        public List<TrainListModel> Search(string term)
        {
            List<TrainListModel> returnThese = new List<TrainListModel>();
            string termUPD = "%" + term + "%";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("TrainList_Search", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@term", termUPD);
                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            returnThese.Add(new TrainListModel { Id = (int)reader[0], VremeDolaska = (DateTime)reader[1], KomOznaka = (string)reader[2], Napomena = (string)reader[3] });
                        }
                    }
                }
                catch (SqlException)
                {
                    throw new Exception("ERROR!");
                }
            }
            return returnThese;
        }

    }
}
