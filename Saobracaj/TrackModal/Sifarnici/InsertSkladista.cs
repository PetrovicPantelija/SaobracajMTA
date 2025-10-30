using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace Testiranje.Sifarnici
{
    class InsertSkladista
    {

        public void InsSkladista(string Naziv, DateTime Datum, string Korisnik, string Kapacitet, int Grupa, string Oznaka, string SkNaziv, int Brodar, int TipKontejnera, int KvalitetKontejnera, int TipPalete, int Aktivan)
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertSkladista";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@Naziv";
            parameter3.SqlDbType = SqlDbType.NVarChar;
            parameter3.Size = 100;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = Naziv;
            myCommand.Parameters.Add(parameter3);

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

            SqlParameter parameter6 = new SqlParameter();
            parameter6.ParameterName = "@Kapacitet";
            parameter6.SqlDbType = SqlDbType.NVarChar;
            parameter6.Size = 40;
            parameter6.Direction = ParameterDirection.Input;
            parameter6.Value = Kapacitet;
            myCommand.Parameters.Add(parameter6);


            SqlParameter parameter7 = new SqlParameter();
            parameter7.ParameterName = "@Grupa";
            parameter7.SqlDbType = SqlDbType.Int;
            parameter7.Direction = ParameterDirection.Input;
            parameter7.Value = Grupa;
            myCommand.Parameters.Add(parameter7);

            SqlParameter parameter8 = new SqlParameter();
            parameter8.ParameterName = "@Oznaka";
            parameter8.SqlDbType = SqlDbType.NVarChar;
            parameter8.Size = 30;
            parameter8.Direction = ParameterDirection.Input;
            parameter8.Value = Oznaka;
            myCommand.Parameters.Add(parameter8);

            SqlParameter parameter9 = new SqlParameter();
            parameter9.ParameterName = "@SkNaziv";
            parameter9.SqlDbType = SqlDbType.NVarChar;
            parameter9.Size = 10;
            parameter9.Direction = ParameterDirection.Input;
            parameter9.Value = SkNaziv;
            myCommand.Parameters.Add(parameter9);

            SqlParameter parameter10 = new SqlParameter();
            parameter10.ParameterName = "@Brodar";
            parameter10.SqlDbType = SqlDbType.Int;
            parameter10.Direction = ParameterDirection.Input;
            parameter10.Value = Brodar;
            myCommand.Parameters.Add(parameter10);

            SqlParameter parameter11 = new SqlParameter();
            parameter11.ParameterName = "@TipKontejnera";
            parameter11.SqlDbType = SqlDbType.Int;
            parameter11.Direction = ParameterDirection.Input;
            parameter11.Value = TipKontejnera;
            myCommand.Parameters.Add(parameter11);

            SqlParameter parameter111 = new SqlParameter();
            parameter111.ParameterName = "@KvalitetKontejnera";
            parameter111.SqlDbType = SqlDbType.Int;
            parameter111.Direction = ParameterDirection.Input;
            parameter111.Value = KvalitetKontejnera;
            myCommand.Parameters.Add(parameter111);

            SqlParameter parameter12 = new SqlParameter();
            parameter12.ParameterName = "@TipPalete";
            parameter12.SqlDbType = SqlDbType.Int;
            parameter12.Direction = ParameterDirection.Input;
            parameter12.Value = TipPalete;
            myCommand.Parameters.Add(parameter12);

            SqlParameter parameter13 = new SqlParameter();
            parameter13.ParameterName = "@Aktivan";
            parameter13.SqlDbType = SqlDbType.Int;
            parameter13.Direction = ParameterDirection.Input;
            parameter13.Value = Aktivan;
            myCommand.Parameters.Add(parameter13);



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
                throw new Exception("Neuspešan upis cena u bazu");
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

        public void UpdSkladista(int ID, string Naziv, DateTime Datum, string Korisnik, string Kapacitet, int Grupa, string Oznaka ,string	SkNaziv ,	int Brodar, int   TipKontejnera, int   KvalitetKontejnera ,  int  TipPalete , int Aktivan)
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdateSkladista";
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

            SqlParameter parameter6 = new SqlParameter();
            parameter6.ParameterName = "@Kapacitet";
            parameter6.SqlDbType = SqlDbType.NVarChar;
            parameter6.Size = 40;
            parameter6.Direction = ParameterDirection.Input;
            parameter6.Value = Kapacitet;
            myCommand.Parameters.Add(parameter6);

            SqlParameter parameter7 = new SqlParameter();
            parameter7.ParameterName = "@Grupa";
            parameter7.SqlDbType = SqlDbType.Int;
            parameter7.Direction = ParameterDirection.Input;
            parameter7.Value = Grupa;
            myCommand.Parameters.Add(parameter7);

            SqlParameter parameter8 = new SqlParameter();
            parameter8.ParameterName = "@Oznaka";
            parameter8.SqlDbType = SqlDbType.NVarChar;
            parameter8.Size = 30;
            parameter8.Direction = ParameterDirection.Input;
            parameter8.Value = Oznaka;
            myCommand.Parameters.Add(parameter8);

            SqlParameter parameter9 = new SqlParameter();
            parameter9.ParameterName = "@SkNaziv";
            parameter9.SqlDbType = SqlDbType.NVarChar;
            parameter9.Size = 10;
            parameter9.Direction = ParameterDirection.Input;
            parameter9.Value = SkNaziv;
            myCommand.Parameters.Add(parameter9);

            SqlParameter parameter10 = new SqlParameter();
            parameter10.ParameterName = "@Brodar";
            parameter10.SqlDbType = SqlDbType.Int;
            parameter10.Direction = ParameterDirection.Input;
            parameter10.Value = Brodar;
            myCommand.Parameters.Add(parameter10);

            SqlParameter parameter11 = new SqlParameter();
            parameter11.ParameterName = "@TipKontejnera";
            parameter11.SqlDbType = SqlDbType.Int;
            parameter11.Direction = ParameterDirection.Input;
            parameter11.Value = TipKontejnera;
            myCommand.Parameters.Add(parameter11);

            SqlParameter parameter111 = new SqlParameter();
            parameter111.ParameterName = "@KvalitetKontejnera";
            parameter111.SqlDbType = SqlDbType.Int;
            parameter111.Direction = ParameterDirection.Input;
            parameter111.Value = KvalitetKontejnera;
            myCommand.Parameters.Add(parameter111);




            SqlParameter parameter12 = new SqlParameter();
            parameter12.ParameterName = "@TipPalete";
            parameter12.SqlDbType = SqlDbType.Int;
            parameter12.Direction = ParameterDirection.Input;
            parameter12.Value = TipPalete;
            myCommand.Parameters.Add(parameter12);

            SqlParameter parameter13 = new SqlParameter();
            parameter13.ParameterName = "@Aktivan";
            parameter13.SqlDbType = SqlDbType.Int;
            parameter13.Direction = ParameterDirection.Input;
            parameter13.Value = Aktivan;
            myCommand.Parameters.Add(parameter13);

    

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

        public void DeleteSkladista(int ID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "DeleteSkladista";
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
                    MessageBox.Show("Brisanje uspešno završeno", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }

        public void ProglasiAktivnim(int ID, int IzForme)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "AktivirajSkladista";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = ID;
            myCommand.Parameters.Add(parameter);


            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@IzForme";
            parameter2.SqlDbType = SqlDbType.Int;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = IzForme;
            myCommand.Parameters.Add(parameter2);

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
                    MessageBox.Show("Brisanje uspešno završeno", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }

        public void ProglasiNeAktivnim(int ID, int IzForme)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "DeAktivirajSkladista";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = ID;
            myCommand.Parameters.Add(parameter);


            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@IzForme";
            parameter2.SqlDbType = SqlDbType.Int;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = IzForme;
            myCommand.Parameters.Add(parameter2);

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
                    MessageBox.Show("Brisanje uspešno završeno", "",
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


