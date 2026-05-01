using Microsoft.ReportingServices.Diagnostics.Internal;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.Uvoz
{
    internal class InsertZapisnikONepravilnosti
    {
        string connection = Sifarnici.frmLogovanje.connectionString;

        public void InsZapisnikONepravilnosti(int NalogID, string Napomena, string BrojKontejnera, int VrstaKontejnera, string BrojPlombe, string OstalePlombe)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertZapisnikONepravilnosti";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter nalogid = new SqlParameter();
            nalogid.ParameterName = "@NalogID";
            nalogid.SqlDbType = SqlDbType.Int;
            nalogid.Direction = ParameterDirection.Input;
            nalogid.Value = NalogID;
            cmd.Parameters.Add(nalogid);

            SqlParameter napomena = new SqlParameter();
            napomena.ParameterName = "@Napomena";
            napomena.SqlDbType = SqlDbType.NVarChar;
            napomena.Size = 500;
            napomena.Direction = ParameterDirection.Input;
            napomena.Value = Napomena;
            cmd.Parameters.Add(napomena);


            SqlParameter brojkontejnera = new SqlParameter();
            brojkontejnera.ParameterName = "@BrojKontejnera";
            brojkontejnera.SqlDbType = SqlDbType.NVarChar;
            brojkontejnera.Size = 50;
            brojkontejnera.Direction = ParameterDirection.Input;
            brojkontejnera.Value = BrojKontejnera;
            cmd.Parameters.Add(brojkontejnera);

            SqlParameter vrstakontejnera = new SqlParameter();
            vrstakontejnera.ParameterName = "@VrstaKontejnera";
            vrstakontejnera.SqlDbType = SqlDbType.Int;
            vrstakontejnera.Direction = ParameterDirection.Input;
            vrstakontejnera.Value = VrstaKontejnera;
            cmd.Parameters.Add(vrstakontejnera);

            SqlParameter brojplombe = new SqlParameter();
            brojplombe.ParameterName = "@BrojPlombe";
            brojplombe.SqlDbType = SqlDbType.NVarChar;
            brojplombe.Size = 30;
            brojplombe.Direction = ParameterDirection.Input;
            brojplombe.Value = BrojPlombe;
            cmd.Parameters.Add(brojplombe);

            SqlParameter ostaleplombe = new SqlParameter();
            ostaleplombe.ParameterName = "@OstalePlombe";
            ostaleplombe.SqlDbType = SqlDbType.NVarChar;
            ostaleplombe.Size = 50;
            ostaleplombe.Direction = ParameterDirection.Input;
            ostaleplombe.Value = OstalePlombe;
            cmd.Parameters.Add(ostaleplombe);


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

        public void DelZapisnikONepravilnosti(int NalogID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteZapisnikONepravilnosti";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter nalogid = new SqlParameter();
            nalogid.ParameterName = "@NalogID";
            nalogid.SqlDbType = SqlDbType.Int;
            nalogid.Direction = ParameterDirection.Input;
            nalogid.Value = NalogID;
            cmd.Parameters.Add(nalogid);


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
