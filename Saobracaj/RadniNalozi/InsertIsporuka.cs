using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.RadniNalozi
{
    internal class InsertIsporuka
    {
        public string connect = Sifarnici.frmLogovanje.connectionString;
        public void InsertDobavnica(int Partner, string MestoTroska, int Referent, string Vozac, string Vozilo, DateTime Datum)
        {
            SqlConnection myConnection = new SqlConnection(connect);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertDobavnica";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@Partner";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = Partner;
            myCommand.Parameters.Add(parameter);

            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@MestoTroska";
            parameter2.SqlDbType = SqlDbType.Char;
            parameter2.Size = 8;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = MestoTroska;
            myCommand.Parameters.Add(parameter2);
            // PrepareCommand(myCommand);

            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@Referent";
            parameter3.SqlDbType = SqlDbType.Int;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = Referent;
            myCommand.Parameters.Add(parameter3);

            SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@Vozac";
            parameter4.SqlDbType = SqlDbType.NVarChar;
            parameter4.Size = 50;
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = Vozac;
            myCommand.Parameters.Add(parameter4);

            SqlParameter parameter5 = new SqlParameter();
            parameter5.ParameterName = "@Vozilo";
            parameter5.SqlDbType = SqlDbType.NVarChar;
            parameter5.Size = 20;
            parameter5.Direction = ParameterDirection.Input;
            parameter5.Value = Vozilo;
            myCommand.Parameters.Add(parameter5);

            SqlParameter parameter6 = new SqlParameter();
            parameter6.ParameterName = "@Datum";
            parameter6.SqlDbType = SqlDbType.DateTime;
            //   parameter6.Size = 150;
            parameter6.Direction = ParameterDirection.Input;
            parameter6.Value = Datum;
            myCommand.Parameters.Add(parameter6);

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
                throw new Exception("Neuspešan upis zaglavlja otpremnice\n"+ex.ToString());
            }
            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos kombinacije je uspesno zavrsen", "Rezultat generisanja",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }



        }

        public void InsertDobavnicaPostav(int Artikal, decimal Kolicina, int Sklad, string Lokac, string MestoTroska)
        {
            SqlConnection myConnection = new SqlConnection(connect);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertDobavnicaPostav";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@Artikal";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = Artikal;
            myCommand.Parameters.Add(parameter);
            // PrepareCommand(myCommand);
            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@Kolicina";
            parameter2.SqlDbType = SqlDbType.Decimal;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = Kolicina;
            myCommand.Parameters.Add(parameter2);

            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@Sklad";
            parameter3.SqlDbType = SqlDbType.Int;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = Sklad;
            myCommand.Parameters.Add(parameter3);

            SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@Lokac";
            parameter4.SqlDbType = SqlDbType.Char;
            parameter4.Size = 12;
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = Lokac;
            myCommand.Parameters.Add(parameter4);

            SqlParameter parameter5 = new SqlParameter();
            parameter5.ParameterName = "@MestoTroska";
            parameter5.SqlDbType = SqlDbType.Char;
            parameter5.Size = 12;
            parameter5.Direction = ParameterDirection.Input;
            parameter5.Value = MestoTroska;
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
                throw new Exception("Neuspešan upis zaglavlja otpremnice");
            }
            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos kombinacije je uspesno zavrsen", "Rezultat generisanja",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }



        }
        public void InsertPrijemnica(int Partner, string MestoTroska, int Referent, int Primio, DateTime Datum)
        {
            SqlConnection myConnection = new SqlConnection(connect);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertPrijemnica";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@Partner";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = Partner;
            myCommand.Parameters.Add(parameter);

            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@MestoTroska";
            parameter2.SqlDbType = SqlDbType.Char;
            parameter2.Size = 8;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = MestoTroska;
            myCommand.Parameters.Add(parameter2);

            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@Referent";
            parameter3.SqlDbType = SqlDbType.Int;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = Referent;
            myCommand.Parameters.Add(parameter3);

            SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@Primio";
            parameter4.SqlDbType = SqlDbType.Int;
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = Primio;
            myCommand.Parameters.Add(parameter4);

            SqlParameter parameter6 = new SqlParameter();
            parameter6.ParameterName = "@Datum";
            parameter6.SqlDbType = SqlDbType.DateTime;
            //   parameter6.Size = 150;
            parameter6.Direction = ParameterDirection.Input;
            parameter6.Value = Datum;
            myCommand.Parameters.Add(parameter6);

            // PrepareCommand(myCommand);


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
                throw new Exception("Neuspešan upis zaglavlja prijemnice\n"+ex.ToString());
            }
            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos kombinacije je uspesno zavrsen", "Rezultat generisanja",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }



        }

        public void InsertPrijemnicaPostav(int Artikal, decimal Kolicina, int Sklad, string Lokac, string MestoTroska)
        {
            SqlConnection myConnection = new SqlConnection(connect);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertPrijemnicaPostav";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@Artikal";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = Artikal;
            myCommand.Parameters.Add(parameter);
            // PrepareCommand(myCommand);
            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@Kolicina";
            parameter2.SqlDbType = SqlDbType.Decimal;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = Kolicina;
            myCommand.Parameters.Add(parameter2);

            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@Sklad";
            parameter3.SqlDbType = SqlDbType.Int;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = Sklad;
            myCommand.Parameters.Add(parameter3);

            SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@Lokac";
            parameter4.SqlDbType = SqlDbType.Char;
            parameter4.Size = 12;
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = Lokac;
            myCommand.Parameters.Add(parameter4);

            SqlParameter parameter5 = new SqlParameter();
            parameter5.ParameterName = "@MestoTroska";
            parameter5.SqlDbType = SqlDbType.Char;
            parameter5.Size = 12;
            parameter5.Direction = ParameterDirection.Input;
            parameter5.Value = MestoTroska;
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
                throw new Exception("Neuspešan upis stavki prijemnice");
            }
            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos kombinacije je uspesno zavrsen", "Rezultat generisanja",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }

        }
        public void UpdPorudzbenica(int BrojPorudzbenice, int Sifra, decimal Kolicina)
        {
            SqlConnection myConnection = new SqlConnection(connect);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdatePorudzbenica";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter br = new SqlParameter();
            br.ParameterName = "@BrojPorudzbenice";
            br.SqlDbType = SqlDbType.Int;
            br.Direction = ParameterDirection.Input;
            br.Value = BrojPorudzbenice;
            myCommand.Parameters.Add(br);

            SqlParameter sifra = new SqlParameter();
            sifra.ParameterName = "@Sifra";
            sifra.SqlDbType = SqlDbType.Int;
            sifra.Direction = ParameterDirection.Input;
            sifra.Value = Sifra;
            myCommand.Parameters.Add(sifra);

            SqlParameter kolicina = new SqlParameter();
            kolicina.ParameterName = "@Kolicina";
            kolicina.SqlDbType = SqlDbType.Decimal;
            kolicina.Direction = ParameterDirection.Input;
            kolicina.Value = Kolicina;
            myCommand.Parameters.Add(kolicina);

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
                throw new Exception("Neuspešan upis prijemnice");
            }
            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos kombinacije je uspesno zavrsen", "Rezultat generisanja",
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
