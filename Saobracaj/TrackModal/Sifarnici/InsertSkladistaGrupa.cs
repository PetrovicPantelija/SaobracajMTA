using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Saobracaj.Administracija;
using System.Windows.Controls;

namespace Saobracaj.TrackModal.Sifarnici
{
    internal class InsertSkladistaGrupa
    {
        public void InsLokacija(string Naziv, string Oznaka, int ZonaID, string Drzava, string Grad, string Adresa, int Aktivan, int TipSkladista)
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertSkladistaGrupa";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;
           
            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@Naziv";
            parameter3.SqlDbType = SqlDbType.NVarChar;
            parameter3.Size = 100;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = Naziv;
            myCommand.Parameters.Add(parameter3);
           
            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@Oznaka";
            parameter2.SqlDbType = SqlDbType.NVarChar;
            parameter2.Size = 30;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = Oznaka;
            myCommand.Parameters.Add(parameter2);

            SqlParameter parameter31 = new SqlParameter();
            parameter31.ParameterName = "@ZonaID";
            parameter31.SqlDbType = SqlDbType.Int;
            parameter31.Direction = ParameterDirection.Input;
            parameter31.Value = ZonaID;
            myCommand.Parameters.Add(parameter31);

            SqlParameter parameter311 = new SqlParameter();
            parameter311.ParameterName = "@Drzava";
            parameter311.SqlDbType = SqlDbType.NVarChar;
            parameter311.Size = 30;
            parameter311.Direction = ParameterDirection.Input;
            parameter311.Value = Drzava;
            myCommand.Parameters.Add(parameter311);

            SqlParameter parameter312 = new SqlParameter();
            parameter312.ParameterName = "@Grad";
            parameter312.SqlDbType = SqlDbType.NVarChar;
            parameter312.Size = 30;
            parameter312.Direction = ParameterDirection.Input;
            parameter312.Value = Grad;
            myCommand.Parameters.Add(parameter312);

            SqlParameter parameter313 = new SqlParameter();
            parameter313.ParameterName = "@Adresa";
            parameter313.SqlDbType = SqlDbType.NVarChar;
            parameter313.Size = 300;
            parameter313.Direction = ParameterDirection.Input;
            parameter313.Value = Adresa;
            myCommand.Parameters.Add(parameter313);

            SqlParameter parameter32 = new SqlParameter();
            parameter32.ParameterName = "@Aktivna";
            parameter32.SqlDbType = SqlDbType.Int;
            parameter32.Direction = ParameterDirection.Input;
            parameter32.Value = Aktivan;
            myCommand.Parameters.Add(parameter32);


            SqlParameter parameter33 = new SqlParameter();
            parameter33.ParameterName = "@TipSkladista";
            parameter33.SqlDbType = SqlDbType.Int;
            parameter33.Direction = ParameterDirection.Input;
            parameter33.Value = TipSkladista;
            myCommand.Parameters.Add(parameter33);
        




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

        public void UpdLokacija(int ID, string Naziv, string Oznaka, int ZonaID, string Drzava, string Grad, string Adresa, int Aktivan, int TipSkladista)
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdateSkladistaGrupa";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = ID;
            myCommand.Parameters.Add(parameter);


            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@Naziv";
            parameter3.SqlDbType = SqlDbType.NVarChar;
            parameter3.Size = 100;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = Naziv;
            myCommand.Parameters.Add(parameter3);

            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@Oznaka";
            parameter2.SqlDbType = SqlDbType.NVarChar;
            parameter2.Size = 30;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = Oznaka;
            myCommand.Parameters.Add(parameter2);

            SqlParameter parameter31 = new SqlParameter();
            parameter31.ParameterName = "@ZonaID";
            parameter31.SqlDbType = SqlDbType.Int;
            parameter31.Direction = ParameterDirection.Input;
            parameter31.Value = ZonaID;
            myCommand.Parameters.Add(parameter31);

            SqlParameter parameter311 = new SqlParameter();
            parameter311.ParameterName = "@Drzava";
            parameter311.SqlDbType = SqlDbType.NVarChar;
            parameter311.Size = 30;
            parameter311.Direction = ParameterDirection.Input;
            parameter311.Value = Drzava;
            myCommand.Parameters.Add(parameter311);

            SqlParameter parameter312 = new SqlParameter();
            parameter312.ParameterName = "@Grad";
            parameter312.SqlDbType = SqlDbType.NVarChar;
            parameter312.Size = 30;
            parameter312.Direction = ParameterDirection.Input;
            parameter312.Value = Grad;
            myCommand.Parameters.Add(parameter312);

            SqlParameter parameter313 = new SqlParameter();
            parameter313.ParameterName = "@Adresa";
            parameter313.SqlDbType = SqlDbType.NVarChar;
            parameter313.Size = 300;
            parameter313.Direction = ParameterDirection.Input;
            parameter313.Value = Adresa;
            myCommand.Parameters.Add(parameter313);

            SqlParameter parameter32 = new SqlParameter();
            parameter32.ParameterName = "@Aktivna";
            parameter32.SqlDbType = SqlDbType.Int;
            parameter32.Direction = ParameterDirection.Input;
            parameter32.Value = Aktivan;
            myCommand.Parameters.Add(parameter32);

            SqlParameter parameter33 = new SqlParameter();
            parameter33.ParameterName = "@TipSkladista";
            parameter33.SqlDbType = SqlDbType.Int;
            parameter33.Direction = ParameterDirection.Input;
            parameter33.Value = TipSkladista;
            myCommand.Parameters.Add(parameter33);

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

        public void DeleteLokacija(int ID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "DeleteSkladistaGrupa";
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
                    MessageBox.Show("Brisanje Lokacija uspešno završeno", "",
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
