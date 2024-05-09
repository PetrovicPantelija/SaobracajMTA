using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj
{
    class InsertVoziloUsluga
    {
        string connection = Sifarnici.frmLogovanje.connectionString;
        public void InsVoziloUsluga(string RegBr, DateTime Datum, string Vozac, string BrojTelefona, string Napomena, int Modul, int IDUsluge)
        {
            /*  @ID int,
  @RegBr  nvarchar(50),
  @Datum datetime,
  @Vozac nvarchar(100),
  @BrojTelefona nvarchar(100),
  @Napomena nvarchar(500),
  @Modul int,
  @IDUsluge int
            */
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertVoziloUsluga";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter regbr = new SqlParameter();
            regbr.ParameterName = "@RegBr";
            regbr.SqlDbType = SqlDbType.NVarChar;
            regbr.Size = 50;
            regbr.Direction = ParameterDirection.Input;
            regbr.Value = RegBr;
            cmd.Parameters.Add(regbr);


            SqlParameter datum = new SqlParameter();
            datum.ParameterName = "@Datum";
            datum.SqlDbType = SqlDbType.DateTime;
            datum.Direction = ParameterDirection.Input;
            datum.Value = Datum;
            cmd.Parameters.Add(datum);

            SqlParameter vozac = new SqlParameter();
            vozac.ParameterName = "@Vozac";
            vozac.SqlDbType = SqlDbType.NVarChar;
            vozac.Size = 100;
            vozac.Direction = ParameterDirection.Input;
            vozac.Value = Vozac;
            cmd.Parameters.Add(vozac);


            SqlParameter brojtelefona = new SqlParameter();
            brojtelefona.ParameterName = "@BrojTelefona";
            brojtelefona.SqlDbType = SqlDbType.NVarChar;
            brojtelefona.Size = 100;
            brojtelefona.Direction = ParameterDirection.Input;
            brojtelefona.Value = BrojTelefona;
            cmd.Parameters.Add(brojtelefona);


            SqlParameter napomena = new SqlParameter();
            napomena.ParameterName = "@Napomena";
            napomena.SqlDbType = SqlDbType.NVarChar;
            napomena.Size = 500;
            napomena.Direction = ParameterDirection.Input;
            napomena.Value = Napomena;
            cmd.Parameters.Add(napomena);


            SqlParameter modul = new SqlParameter();
            modul.ParameterName = "@Modul";
            modul.SqlDbType = SqlDbType.Int;
            modul.Direction = ParameterDirection.Input;
            modul.Value = Modul;
            cmd.Parameters.Add(modul);

            SqlParameter idusluge = new SqlParameter();
            idusluge.ParameterName = "@IDUsluge";
            idusluge.SqlDbType = SqlDbType.Int;
            idusluge.Direction = ParameterDirection.Input;
            idusluge.Value = IDUsluge;
            cmd.Parameters.Add(idusluge);



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
        public void UpdVoziloUsliga(int ID, string RegBr, DateTime Datum, string Vozac, string BrojTelefona, string Napomena, int Modul, int IDUsluge)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateVoziloUsluga";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter regbr = new SqlParameter();
            regbr.ParameterName = "@RegBr";
            regbr.SqlDbType = SqlDbType.NVarChar;
            regbr.Size = 50;
            regbr.Direction = ParameterDirection.Input;
            regbr.Value = RegBr;
            cmd.Parameters.Add(regbr);


            SqlParameter datum = new SqlParameter();
            datum.ParameterName = "@Datum";
            datum.SqlDbType = SqlDbType.DateTime;
            datum.Direction = ParameterDirection.Input;
            datum.Value = Datum;
            cmd.Parameters.Add(datum);

            SqlParameter vozac = new SqlParameter();
            vozac.ParameterName = "@Vozac";
            vozac.SqlDbType = SqlDbType.NVarChar;
            vozac.Size = 100;
            vozac.Direction = ParameterDirection.Input;
            vozac.Value = Vozac;
            cmd.Parameters.Add(vozac);


            SqlParameter brojtelefona = new SqlParameter();
            brojtelefona.ParameterName = "@BrojTelefona";
            brojtelefona.SqlDbType = SqlDbType.NVarChar;
            brojtelefona.Size = 100;
            brojtelefona.Direction = ParameterDirection.Input;
            brojtelefona.Value = BrojTelefona;
            cmd.Parameters.Add(brojtelefona);


            SqlParameter napomena = new SqlParameter();
            napomena.ParameterName = "@Napomena";
            napomena.SqlDbType = SqlDbType.NVarChar;
            napomena.Size = 500;
            napomena.Direction = ParameterDirection.Input;
            napomena.Value = Napomena;
            cmd.Parameters.Add(napomena);


            SqlParameter modul = new SqlParameter();
            modul.ParameterName = "@Modul";
            modul.SqlDbType = SqlDbType.Int;
            modul.Direction = ParameterDirection.Input;
            modul.Value = Modul;
            cmd.Parameters.Add(modul);

            SqlParameter idusluge = new SqlParameter();
            idusluge.ParameterName = "@IDUsluge";
            idusluge.SqlDbType = SqlDbType.Int;
            idusluge.Direction = ParameterDirection.Input;
            idusluge.Value = IDUsluge;
            cmd.Parameters.Add(idusluge);

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
        public void DelVoziloUsluga(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteVoziloUsluga";
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
