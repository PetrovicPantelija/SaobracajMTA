using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Saobracaj.Pantheon_Export;
using Saobracaj.Sifarnici;

namespace Saobracaj.Drumski
{
    class InsertKurs
    {
        public string connect = frmLogovanje.connectionString;
        public void InsKurs(string Valuta, decimal? Kurs, DateTime? Datum)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertKurs";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter valuta = new SqlParameter();
            valuta.ParameterName = "@Valuta";
            valuta.SqlDbType = SqlDbType.NVarChar;
            valuta.Size = 3;
            valuta.Direction = ParameterDirection.Input;
            valuta.Value = (object)Valuta ?? DBNull.Value;
            cmd.Parameters.Add(valuta);

            SqlParameter kurs = new SqlParameter();
            kurs.ParameterName = "@Kurs";
            kurs.SqlDbType = SqlDbType.Decimal;
            kurs.Precision = 18;
            kurs.Scale = 4;
            kurs.Direction = ParameterDirection.Input;
            kurs.Value = Kurs.HasValue ? (object)Kurs.Value : DBNull.Value;
            cmd.Parameters.Add(kurs);

            SqlParameter datum = new SqlParameter();
            datum.ParameterName = "@Datum";
            datum.SqlDbType = SqlDbType.Date;
            datum.Direction = ParameterDirection.Input;
            datum.Value = Datum.HasValue ? (object)Datum.Value : DBNull.Value;
            cmd.Parameters.Add(datum);

            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            cmd.Transaction = tran;
            bool error = true;
            try
            {
                cmd.ExecuteNonQuery();
                tran.Commit();
                error = false;
                tran = conn.BeginTransaction();
                cmd.Transaction = tran;

            }
            catch (SqlException ex)
            {
                //throw new Exception("Neuspešan upis");
                MessageBox.Show("Greška u SQL izvršavanju: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tran.Rollback(); // Ne zaboravi i rollback
            }
            finally
            {
                if (!error)
                {
                    tran.Commit();
                    MessageBox.Show("Kreiranje je uspešno završeno", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
            }
            if (error)
            {
            }
        }

        public void UpdateKurs(int ID, string Valuta, decimal? Kurs, DateTime? Datum)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateKurs";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter iD = new SqlParameter();
            iD.ParameterName = "@ID";
            iD.SqlDbType = SqlDbType.Int;
            iD.Direction = ParameterDirection.Input;
            iD.Value = ID;
            cmd.Parameters.Add(iD);

            SqlParameter valuta = new SqlParameter();
            valuta.ParameterName = "@Valuta";
            valuta.SqlDbType = SqlDbType.NVarChar;
            valuta.Size = 3;
            valuta.Direction = ParameterDirection.Input;
            valuta.Value = (object)Valuta ?? DBNull.Value;
            cmd.Parameters.Add(valuta);

            SqlParameter kurs = new SqlParameter();
            kurs.ParameterName = "@Kurs";
            kurs.SqlDbType = SqlDbType.Decimal;
            kurs.Precision = 18;
            kurs.Scale = 4;
            kurs.Direction = ParameterDirection.Input;
            kurs.Value = Kurs.HasValue ? (object)Kurs.Value : DBNull.Value;
            cmd.Parameters.Add(kurs);

            SqlParameter datum = new SqlParameter();
            datum.ParameterName = "@Datum";
            datum.SqlDbType = SqlDbType.Date;
            datum.Direction = ParameterDirection.Input;
            datum.Value = Datum.HasValue ? (object)Datum.Value : DBNull.Value;
            cmd.Parameters.Add(datum);

            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            cmd.Transaction = tran;
            bool error = true;
            try
            {
                cmd.ExecuteNonQuery();
                tran.Commit();
                error = false;
                tran = conn.BeginTransaction();
                cmd.Transaction = tran;
            }
            catch (SqlException ex)
            {
                //throw new Exception("Neuspešan upis");
                MessageBox.Show("Greška u SQL izvršavanju: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tran.Rollback(); // Ne zaboravi i rollback
            }
            finally
            {
                if (!error)
                {
                    tran.Commit();
                    MessageBox.Show("Ažuriranje je uspešno završeno", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
            }
            if (error)
            {
            }
        }

        public void DelKurs(int ID)
        {
           
            SqlConnection myConnection = new SqlConnection(connect);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "DeleteKurs";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            myCommand.Parameters.Add(id);

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
