using Saobracaj.Sifarnici;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.Administracija
{
    internal class InsertKorisnici
    {
        public void InsKorisnici(string Korisnik, string Password, int Zakljucavanje, int DeSifra)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertKorisnici";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@Korisnik";
            parameter.SqlDbType = SqlDbType.NChar;
            parameter.Size = 50;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = Korisnik;
            myCommand.Parameters.Add(parameter);

            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@Password";
            parameter.SqlDbType = SqlDbType.NChar;
            parameter.Size = 20;
            parameter1.Direction = ParameterDirection.Input;
            parameter1.Value = Password;
            myCommand.Parameters.Add(parameter1);

            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@Zakljucavanje";
            parameter2.SqlDbType = SqlDbType.Int;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = Zakljucavanje;
            myCommand.Parameters.Add(parameter2);

            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@DeSifra";
            parameter3.SqlDbType = SqlDbType.Int;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = DeSifra;
            myCommand.Parameters.Add(parameter3);

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
                throw new Exception("Neuspešan upis NHM brojeva");
            }
            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos NHM broja je uspešno završena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }

        public void DeleteKorisnici(string Korisnik)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "DeleteKorisnici";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@Korisnik";
            parameter.SqlDbType = SqlDbType.NChar;
            parameter.Size = 50;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = Korisnik;
            myCommand.Parameters.Add(parameter);

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
                throw new Exception("Brisanje neuspešno");
            }
            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Brisanje nije uspešno", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }
        public void InsertPravo(string Korisnik, int Main, int Kartica, int Forma)
        {
            var s_connection = frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertPravo";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@Korisnik";
            parameter.SqlDbType = SqlDbType.NChar;
            parameter.Size = 50;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = Korisnik;
            myCommand.Parameters.Add(parameter);

            SqlParameter par = new SqlParameter();
            par.ParameterName = "@Main";
            par.SqlDbType = SqlDbType.Int;
            par.Direction = ParameterDirection.Input;
            par.Value = Main;
            myCommand.Parameters.Add(par);

            SqlParameter par2 = new SqlParameter();
            par2.ParameterName = "@Kartica";
            par2.SqlDbType = SqlDbType.Int;
            par2.Direction = ParameterDirection.Input;
            par2.Value = Kartica;
            myCommand.Parameters.Add(par2);


            SqlParameter par3 = new SqlParameter();
            par3.ParameterName = "@Forma";
            par3.SqlDbType = SqlDbType.Int;
            par3.Direction = ParameterDirection.Input;
            par3.Value = Forma;
            myCommand.Parameters.Add(par3);

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
                throw new Exception("Brisanje neuspešno");
            }
            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Brisanje nije uspešno", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }
        public void DeletePravo(string Korisnik)
        {
            var s_connection = frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "DeletePravo";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@Korisnik";
            parameter.SqlDbType = SqlDbType.NChar;
            parameter.Size = 50;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = Korisnik;
            myCommand.Parameters.Add(parameter);

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
                throw new Exception("Brisanje neuspešno");
            }
            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Brisanje nije uspešno", "",
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