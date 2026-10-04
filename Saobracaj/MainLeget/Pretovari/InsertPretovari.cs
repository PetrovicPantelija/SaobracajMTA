using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.ReportingServices.Diagnostics.Internal;

namespace Saobracaj.MainLeget.Pretovari
{
    internal class InsertPretovari
    {

        public void UpdateStatusUsluge(int IDNajave, string Korisnik)
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "[PromeniStatusPrveUsluge]";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = IDNajave;
            myCommand.Parameters.Add(parameter);

            SqlParameter kor = new SqlParameter();
            kor.ParameterName = "@Korisnik";
            kor.SqlDbType = SqlDbType.NVarChar;
            kor.Size = 50;
            kor.Direction = ParameterDirection.Input;
            kor.Value = Korisnik;
            myCommand.Parameters.Add(kor);

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

            catch (SqlException ex)
            {
                myTransaction.Rollback();
                MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show(ex.ToString());
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Nije uspeo upis dokumenta u bazu", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }
        

        public void UpdateStatusUslugeVaganje(int IDNajave)
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "[PromeniStatusUslugeVaganje]";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = IDNajave;
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

            catch (SqlException ex)
            {
                myTransaction.Rollback();
                MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show(ex.ToString());
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Nije uspeo upis dokumenta u bazu", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }
        public void UpdateRadniNalogInterniTipNaloga(int IDNajave, string TipNaloga, int BrojDokumenta)
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "[UpdateRadniNalogInterniTipNaloga]";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@IDNajave";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = IDNajave;
            myCommand.Parameters.Add(parameter);

            SqlParameter tipNaloga = new SqlParameter();
            tipNaloga.ParameterName = "@TipNaloga";
            tipNaloga.SqlDbType = SqlDbType.NVarChar;
            tipNaloga.Size = 4;
            tipNaloga.Direction = ParameterDirection.Input;
            tipNaloga.Value = TipNaloga;
            myCommand.Parameters.Add(tipNaloga);

            SqlParameter brojDokumenta = new SqlParameter();
            brojDokumenta.ParameterName = "@BrojDokumenta";
            brojDokumenta.SqlDbType = SqlDbType.Int;
            brojDokumenta.Direction = ParameterDirection.Input;
            brojDokumenta.Value = BrojDokumenta;
            myCommand.Parameters.Add(brojDokumenta);

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

            catch (SqlException ex)
            {
                myTransaction.Rollback();
                MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show(ex.ToString());
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Nije uspeo upis dokumenta u bazu", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }


        }

        public void PromeniStatusVaganje(int IDNajave)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "[PromeniStatusVaganje]";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = IDNajave;
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

            catch (SqlException ex)
            {
                myTransaction.Rollback();
                MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show(ex.ToString());
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Nije uspeo upis dokumenta u bazu", "",
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
