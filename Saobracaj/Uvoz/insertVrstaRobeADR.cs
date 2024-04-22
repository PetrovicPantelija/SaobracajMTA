using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.Uvoz
{
    class insertVrstaRobeADR
    {
        string connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.TestiranjeConnectionString"].ConnectionString;
        public void InsVrstaRobeADR(string Naziv, string UNKod, string Klasa, string Grupa)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertVrstaRobeADR";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter naziv = new SqlParameter();
            naziv.ParameterName = "@Naziv";
            naziv.SqlDbType = SqlDbType.NVarChar;
            naziv.Size = 50;
            naziv.Direction = ParameterDirection.Input;
            naziv.Value = Naziv;
            cmd.Parameters.Add(naziv);

            SqlParameter unKod = new SqlParameter();
            unKod.ParameterName = "@UNKod";
            unKod.SqlDbType = SqlDbType.NVarChar;
            unKod.Size = 4;
            unKod.Direction = ParameterDirection.Input;
            unKod.Value = UNKod;
            cmd.Parameters.Add(unKod);


            SqlParameter klasa = new SqlParameter();
            klasa.ParameterName = "@Klasa";
            klasa.SqlDbType = SqlDbType.NVarChar;
            klasa.Size = 20;
            klasa.Direction = ParameterDirection.Input;
            klasa.Value = Klasa;
            cmd.Parameters.Add(klasa);

            SqlParameter grupa = new SqlParameter();
            grupa.ParameterName = "@Grupa";
            grupa.SqlDbType = SqlDbType.NVarChar;
            grupa.Size = 20;
            grupa.Direction = ParameterDirection.Input;
            grupa.Value = Grupa;
            cmd.Parameters.Add(grupa);

            conn.Open();
            SqlTransaction myTransaction = conn.BeginTransaction();
            cmd.Transaction = myTransaction;
            bool error = true;
            try
            {
                cmd.ExecuteNonQuery();
                myTransaction.Commit();
                myTransaction = conn.BeginTransaction();
                cmd.Transaction = myTransaction;
            }

            catch (SqlException)
            {
                throw new Exception("Neuspešan upis ");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos uspešno završen", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }
        public void UpdVrstaRobeADR(int ID, string Naziv, string UNKod, string Klasa, string Grupa)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateVrstaRobeADR";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter naziv = new SqlParameter();
            naziv.ParameterName = "@Naziv";
            naziv.SqlDbType = SqlDbType.NVarChar;
            naziv.Size = 50;
            naziv.Direction = ParameterDirection.Input;
            naziv.Value = Naziv;
            cmd.Parameters.Add(naziv);

            SqlParameter unKod = new SqlParameter();
            unKod.ParameterName = "@UNKod";
            unKod.SqlDbType = SqlDbType.NVarChar;
            unKod.Size = 4;
            unKod.Direction = ParameterDirection.Input;
            unKod.Value = UNKod;
            cmd.Parameters.Add(unKod);


            SqlParameter klasa = new SqlParameter();
            klasa.ParameterName = "@Klasa";
            klasa.SqlDbType = SqlDbType.NVarChar;
            klasa.Size = 20;
            klasa.Direction = ParameterDirection.Input;
            klasa.Value = Klasa;
            cmd.Parameters.Add(klasa);


            SqlParameter grupa = new SqlParameter();
            grupa.ParameterName = "@Grupa";
            grupa.SqlDbType = SqlDbType.NVarChar;
            grupa.Size = 20;
            grupa.Direction = ParameterDirection.Input;
            grupa.Value = Grupa;
            cmd.Parameters.Add(grupa);


            conn.Open();
            SqlTransaction myTransaction = conn.BeginTransaction();
            cmd.Transaction = myTransaction;
            bool error = true;
            try
            {
                cmd.ExecuteNonQuery();
                myTransaction.Commit();
                myTransaction = conn.BeginTransaction();
                cmd.Transaction = myTransaction;
            }

            catch (SqlException)
            {
                throw new Exception("Neuspešan upis ");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos uspešno završen", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }

        }
        public void DelVrstaRobeADR(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteVrstaRobeADR";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            conn.Open();
            SqlTransaction myTransaction = conn.BeginTransaction();
            cmd.Transaction = myTransaction;
            bool error = true;
            try
            {
                cmd.ExecuteNonQuery();
                myTransaction.Commit();
                myTransaction = conn.BeginTransaction();
                cmd.Transaction = myTransaction;
            }

            catch (SqlException)
            {
                throw new Exception("Neuspešan upis ");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos uspešno završen", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }

        }

    }
}
