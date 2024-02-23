using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


namespace Saobracaj.Sifarnici
{
    class InsertScenario
    {

        public void InsScenario(int PotpunoNovi, string Naziv, int Usluga, string Pokret, int StatusKontejnera, string Forma)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertScenario";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@PotpunoNovi";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = PotpunoNovi;
            myCommand.Parameters.Add(parameter);

            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@Naziv";
            parameter1.SqlDbType = SqlDbType.Char;
            parameter1.Size = 100;
            parameter1.Direction = ParameterDirection.Input;
            parameter1.Value = Naziv;
            myCommand.Parameters.Add(parameter1);

            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@Usluga";
            parameter2.SqlDbType = SqlDbType.Int;
            //parameter2.Size = 100;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = Usluga;
            myCommand.Parameters.Add(parameter2);


            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@Pokret";
            parameter3.SqlDbType = SqlDbType.NVarChar;
            parameter3.Size = 50;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = Pokret;
            myCommand.Parameters.Add(parameter3);

            SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@StatusKontejnera";
            parameter4.SqlDbType = SqlDbType.Int;
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = StatusKontejnera;
            myCommand.Parameters.Add(parameter4);

            SqlParameter parameter5 = new SqlParameter();
            parameter5.ParameterName = "@Forma";
            parameter5.SqlDbType = SqlDbType.NVarChar;
            parameter5.Size = 50;
            parameter5.Direction = ParameterDirection.Input;
            parameter5.Value = Forma;
            myCommand.Parameters.Add(parameter5);


            myConnection.Open();
            SqlTransaction myTransaction = myConnection.BeginTransaction();
            myCommand.Transaction = myTransaction;
            bool error = true;
            try
            {
                myCommand.ExecuteNonQuery();
                myTransaction.Commit();
                myTransaction = myConnection.BeginTransaction();
                myCommand.Transaction = myTransaction;
            }

            catch (SqlException)
            {
                throw new Exception("Neuspešna promena podataka");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Neuspešna promena podataka", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }

        public void UpdScenario(int ScenarioID, int RB, string Naziv, int Usluga, string Pokret, int StatusKontejnera, string Forma)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdateScenario";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ScenarioID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = ScenarioID;
            myCommand.Parameters.Add(parameter);

            SqlParameter parameter0 = new SqlParameter();
            parameter0.ParameterName = "@RB";
            parameter0.SqlDbType = SqlDbType.Int;
            parameter0.Direction = ParameterDirection.Input;
            parameter0.Value = RB;
            myCommand.Parameters.Add(parameter0);

            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@Naziv";
            parameter1.SqlDbType = SqlDbType.Char;
            parameter1.Size = 100;
            parameter1.Direction = ParameterDirection.Input;
            parameter1.Value = Naziv;
            myCommand.Parameters.Add(parameter1);

            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@Usluga";
            parameter2.SqlDbType = SqlDbType.Int;
            parameter2.Size = 100;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = Usluga;
            myCommand.Parameters.Add(parameter2);


            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@Pokret";
            parameter3.SqlDbType = SqlDbType.NVarChar;
            parameter3.Size = 50;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = Pokret;
            myCommand.Parameters.Add(parameter3);

            SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@StatusKontejnera";
            parameter4.SqlDbType = SqlDbType.Int;
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = StatusKontejnera;
            myCommand.Parameters.Add(parameter4);

            SqlParameter parameter5 = new SqlParameter();
            parameter5.ParameterName = "@Forma";
            parameter5.SqlDbType = SqlDbType.NVarChar;
            parameter5.Size = 50;
            parameter5.Direction = ParameterDirection.Input;
            parameter5.Value = Forma;
            myCommand.Parameters.Add(parameter5);

            myConnection.Open();
            SqlTransaction myTransaction = myConnection.BeginTransaction();
            myCommand.Transaction = myTransaction;
            bool error = true;
            try
            {
                myCommand.ExecuteNonQuery();
                myTransaction.Commit();
                myTransaction = myConnection.BeginTransaction();
                myCommand.Transaction = myTransaction;
            }

            catch (SqlException)
            {
                throw new Exception("Neuspešna promena podataka");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Neuspešna promena podataka", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }


        }

        public void DelScenario(int ScenarioID, int RB)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "DeleteScenario";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ScenarioID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = ScenarioID;
            myCommand.Parameters.Add(parameter);

            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@RB";
            parameter1.SqlDbType = SqlDbType.Int;
            parameter1.Direction = ParameterDirection.Input;
            parameter1.Value = RB;
            myCommand.Parameters.Add(parameter1);




            myConnection.Open();
            SqlTransaction myTransaction = myConnection.BeginTransaction();
            myCommand.Transaction = myTransaction;
            bool error = true;
            try
            {
                myCommand.ExecuteNonQuery();
                myTransaction.Commit();
                myTransaction = myConnection.BeginTransaction();
                myCommand.Transaction = myTransaction;
            }

            catch (SqlException)
            {
                throw new Exception("Neuspešna promena podataka");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Neuspešna promena podataka", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }


        }
    }
}
