using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.Uvoz
{
    class InsertVrstaRobeHS
    {
        string connection = Sifarnici.frmLogovanje.connectionString;
        public void InsVrstaRobeHS(string Naziv, string HSKod, int ADRID, int Izvozni)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertVrstaRobeHS";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter naziv = new SqlParameter();
            naziv.ParameterName = "@Naziv";
            naziv.SqlDbType = SqlDbType.NVarChar;
            naziv.Size = 50;
            naziv.Direction = ParameterDirection.Input;
            naziv.Value = Naziv;
            cmd.Parameters.Add(naziv);

            SqlParameter hsKod = new SqlParameter();
            hsKod.ParameterName = "@HSKod";
            hsKod.SqlDbType = SqlDbType.NVarChar;
            hsKod.Size = 8;
            hsKod.Direction = ParameterDirection.Input;
            hsKod.Value = HSKod;
            cmd.Parameters.Add(hsKod);

            SqlParameter adrid = new SqlParameter();
            adrid.ParameterName = "@ADRID";
            adrid.SqlDbType = SqlDbType.Int;
            adrid.Direction = ParameterDirection.Input;
            adrid.Value = ADRID;
            cmd.Parameters.Add(adrid);

            SqlParameter izvozni = new SqlParameter();
            izvozni.ParameterName = "@Izvozni";
            izvozni.SqlDbType = SqlDbType.Int;
            izvozni.Direction = ParameterDirection.Input;
            izvozni.Value = Izvozni;
            cmd.Parameters.Add(izvozni);

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
        public void UpdVrstaRobeHS(int ID, string Naziv, string HSKod, int ADRID, int Izvozni)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateVrstaRobeHS";
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

            SqlParameter hsKod = new SqlParameter();
            hsKod.ParameterName = "@HSKod";
            hsKod.SqlDbType = SqlDbType.NVarChar;
            hsKod.Size = 8;
            hsKod.Direction = ParameterDirection.Input;
            hsKod.Value = HSKod;
            cmd.Parameters.Add(hsKod);

            SqlParameter adrid = new SqlParameter();
            adrid.ParameterName = "@ADRID";
            adrid.SqlDbType = SqlDbType.Int;
            adrid.Direction = ParameterDirection.Input;
            adrid.Value = ADRID;
            cmd.Parameters.Add(adrid);

            SqlParameter izvozni = new SqlParameter();
            izvozni.ParameterName = "@Izvozni";
            izvozni.SqlDbType = SqlDbType.Int;
            izvozni.Direction = ParameterDirection.Input;
            izvozni.Value = Izvozni;
            cmd.Parameters.Add(izvozni);

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
        public void DelVrstaRobeHS(int ID)
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
