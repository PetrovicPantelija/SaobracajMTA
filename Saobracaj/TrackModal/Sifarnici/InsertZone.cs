using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.TrackModal.Sifarnici
{
    internal class InsertZone
    {
        public void InsZone(string Oznaka, string Naziv,  int TipZoneID, int Aktivan,  DateTime Datum, string Korisnik)
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertZone";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@Oznaka";
            parameter2.SqlDbType = SqlDbType.NVarChar;
            parameter2.Size = 30;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = Oznaka;
            myCommand.Parameters.Add(parameter2);

            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@Naziv";
            parameter3.SqlDbType = SqlDbType.NVarChar;
            parameter3.Size = 100;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = Naziv;
            myCommand.Parameters.Add(parameter3);


            SqlParameter parameter31 = new SqlParameter();
            parameter31.ParameterName = "@TipZoneID";
            parameter31.SqlDbType = SqlDbType.Int;
            parameter31.Direction = ParameterDirection.Input;
            parameter31.Value = TipZoneID;
            myCommand.Parameters.Add(parameter31);

            SqlParameter parameter32 = new SqlParameter();
            parameter32.ParameterName = "@Aktivna";
            parameter32.SqlDbType = SqlDbType.Int;
            parameter32.Direction = ParameterDirection.Input;
            parameter32.Value = Aktivan;
            myCommand.Parameters.Add(parameter32);

            SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@Datum";
            parameter4.SqlDbType = SqlDbType.DateTime;
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = Datum;
            myCommand.Parameters.Add(parameter4);

            SqlParameter parameter5 = new SqlParameter();
            parameter5.ParameterName = "@Korisnik";
            parameter5.SqlDbType = SqlDbType.NVarChar;
            parameter5.Size = 20;
            parameter5.Direction = ParameterDirection.Input;
            parameter5.Value = Korisnik;
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
                throw new Exception("Neuspešan upis Tipa Cenovnika u bazu");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Nije uspeo upis cena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }


            }
        }

        public void UpdZone(int ID, string Oznaka,string Naziv,  int TipZoneID, int Aktivan, DateTime Datum, string Korisnik)
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdateZone";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = ID;
            myCommand.Parameters.Add(parameter);


            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@Oznaka";
            parameter2.SqlDbType = SqlDbType.NVarChar;
            parameter2.Size = 30;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = Oznaka;
            myCommand.Parameters.Add(parameter2);

            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@Naziv";
            parameter3.SqlDbType = SqlDbType.NVarChar;
            parameter3.Size = 100;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = Naziv;
            myCommand.Parameters.Add(parameter3);


            SqlParameter parameter31 = new SqlParameter();
            parameter31.ParameterName = "@TipZoneID";
            parameter31.SqlDbType = SqlDbType.Int;
            parameter31.Direction = ParameterDirection.Input;
            parameter31.Value = TipZoneID;
            myCommand.Parameters.Add(parameter31);

            SqlParameter parameter32 = new SqlParameter();
            parameter32.ParameterName = "@Aktivna";
            parameter32.SqlDbType = SqlDbType.Int;
            parameter32.Direction = ParameterDirection.Input;
            parameter32.Value = Aktivan;
            myCommand.Parameters.Add(parameter32);

            SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@Datum";
            parameter4.SqlDbType = SqlDbType.DateTime;
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = Datum;
            myCommand.Parameters.Add(parameter4);

            SqlParameter parameter5 = new SqlParameter();
            parameter5.ParameterName = "@Korisnik";
            parameter5.SqlDbType = SqlDbType.NVarChar;
            parameter5.Size = 20;
            parameter5.Direction = ParameterDirection.Input;
            parameter5.Value = Korisnik;
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
                throw new Exception("Neuspešan upis aktivnosti u Bazu");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Nije uspeo upis aktivnosti", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }


            }
        }

        public void DeleteZone(int ID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "DeleteZone";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = ID;
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
                    MessageBox.Show("Brisanje yona uspešno završeno", "",
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
