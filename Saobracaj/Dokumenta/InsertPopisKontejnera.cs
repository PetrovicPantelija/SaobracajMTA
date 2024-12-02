using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlTypes;

namespace Saobracaj.Dokumenta
{
    internal class InsertPopisKontejnera
    {
        public void InsPopisKontejnera(DateTime DatumPopisa, string Napomena, DateTime Datum, string Korisnik)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertPopisKontejnera";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;


            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@DatumPopisa";
            parameter1.SqlDbType = SqlDbType.DateTime;
        
            parameter1.Direction = ParameterDirection.Input;
            parameter1.Value = DatumPopisa;
            myCommand.Parameters.Add(parameter1);


            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@Napomena";
            parameter2.SqlDbType = SqlDbType.NVarChar;
            parameter2.Size = 120;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = Napomena;
            myCommand.Parameters.Add(parameter2);

            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@Datum";
            parameter3.SqlDbType = SqlDbType.DateTime;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = Datum;
            myCommand.Parameters.Add(parameter3);


            SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@Korisnik";
            parameter4.SqlDbType = SqlDbType.NVarChar;
            parameter4.Size = 20;
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = Korisnik;
            myCommand.Parameters.Add(parameter4);


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

        public void UpdPopisKontejnera(int ID, DateTime DatumPopisa, string Napomena,  DateTime Datum, string Korisnik)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdatePopisKontejnera";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = ID;
            myCommand.Parameters.Add(parameter);

            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@DatumPopisa";
            parameter1.SqlDbType = SqlDbType.DateTime;
            parameter1.Direction = ParameterDirection.Input;
            parameter1.Value = DatumPopisa;
            myCommand.Parameters.Add(parameter1);


            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@Napomena";
            parameter2.SqlDbType = SqlDbType.NVarChar;
            parameter2.Size = 120;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = Napomena;
            myCommand.Parameters.Add(parameter2);

            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@Datum";
            parameter3.SqlDbType = SqlDbType.DateTime;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = Datum;
            myCommand.Parameters.Add(parameter3);


            SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@Korisnik";
            parameter4.SqlDbType = SqlDbType.NVarChar;
            parameter4.Size = 20;
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = Korisnik;
            myCommand.Parameters.Add(parameter4);


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

        public void DelPopisKontejnera(int ID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "DeletePopisKontejnera";
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




        /////
        ///
        public void InsPopisKontejneraStavke(int IDNadredjenog, string BrojKontejnera, int SkladisteU, int LokacijaU, int PronadjenIspravan, int SkladisteNovo, int LokacijaNovo,  DateTime Datum, string Korisnik, int Brodar, int TipKontejnera, int StatusKontejnera)
        {
           

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertPopisKontejneraStavke";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;


            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@IDNadredjenog";
            parameter1.SqlDbType = SqlDbType.Int;
            parameter1.Direction = ParameterDirection.Input;
            parameter1.Value = IDNadredjenog;
            myCommand.Parameters.Add(parameter1);

    
            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@BrojKontejnera";
            parameter2.SqlDbType = SqlDbType.NVarChar;
            parameter2.Size = 20;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = BrojKontejnera;
            myCommand.Parameters.Add(parameter2);


            SqlParameter parameter21 = new SqlParameter();
            parameter21.ParameterName = "@SkladisteU";
            parameter21.SqlDbType = SqlDbType.Int;
            parameter21.Direction = ParameterDirection.Input;
            parameter21.Value = SkladisteU;
            myCommand.Parameters.Add(parameter21);

            SqlParameter parameter22 = new SqlParameter();
            parameter22.ParameterName = "@LokacijaU";
            parameter22.SqlDbType = SqlDbType.Int;
            parameter22.Direction = ParameterDirection.Input;
            parameter22.Value = LokacijaU;
            myCommand.Parameters.Add(parameter22);

            SqlParameter parameter23 = new SqlParameter();
            parameter23.ParameterName = "@PronadjenIspravan";
            parameter23.SqlDbType = SqlDbType.Int;
            parameter23.Direction = ParameterDirection.Input;
            parameter23.Value = PronadjenIspravan;
            myCommand.Parameters.Add(parameter23);


            SqlParameter parameter24 = new SqlParameter();
            parameter24.ParameterName = "@SkladisteNovo";
            parameter24.SqlDbType = SqlDbType.Int;
            parameter24.Direction = ParameterDirection.Input;
            parameter24.Value = SkladisteNovo;
            myCommand.Parameters.Add(parameter24);

            SqlParameter parameter25 = new SqlParameter();
            parameter25.ParameterName = "@LokacijaNovo";
            parameter25.SqlDbType = SqlDbType.Int;
            parameter25.Direction = ParameterDirection.Input;
            parameter25.Value = LokacijaNovo;
            myCommand.Parameters.Add(parameter25);

            //  nt Brodar, int TipKontejnera, int StatusKontejnera

            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@Datum";
            parameter3.SqlDbType = SqlDbType.DateTime;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = Datum;
            myCommand.Parameters.Add(parameter3);


            SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@Korisnik";
            parameter4.SqlDbType = SqlDbType.NVarChar;
            parameter4.Size = 20;
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = Korisnik;
            myCommand.Parameters.Add(parameter4);


            SqlParameter parameter41 = new SqlParameter();
            parameter41.ParameterName = "@Brodar";
            parameter41.SqlDbType = SqlDbType.Int;
            parameter41.Direction = ParameterDirection.Input;
            parameter41.Value = Brodar;
            myCommand.Parameters.Add(parameter41);


            SqlParameter parameter42 = new SqlParameter();
            parameter42.ParameterName = "@TipKontejnera";
            parameter42.SqlDbType = SqlDbType.Int;
            parameter42.Direction = ParameterDirection.Input;
            parameter42.Value = TipKontejnera;
            myCommand.Parameters.Add(parameter42);

            SqlParameter parameter43 = new SqlParameter();
            parameter43.ParameterName = "@StatusKontejnera";
            parameter43.SqlDbType = SqlDbType.Int;
            parameter43.Direction = ParameterDirection.Input;
            parameter43.Value = StatusKontejnera;
            myCommand.Parameters.Add(parameter43);


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


        public void IUpisiSveStavkeKontejnerTekuce(int IDNadredjenog)
        {


            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertPopisKontejneraKontTekuce";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;


            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@IDNadredjenog";
            parameter1.SqlDbType = SqlDbType.Int;
            parameter1.Direction = ParameterDirection.Input;
            parameter1.Value = IDNadredjenog;
            myCommand.Parameters.Add(parameter1);


           

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

        public void UpdPopisKontejneraStavke(int ID, int IDNadredjenog, string BrojKontejnera, int SkladisteU, int LokacijaU, int PronadjenIspravan, int SkladisteNovo, int LokacijaNovo, DateTime Datum, string Korisnik, int Brodar, int TipKontejnera, int StatusKontejnera)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdatePopisKontejneraStavke";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = ID;
            myCommand.Parameters.Add(parameter);

            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@IDNadredjenog";
            parameter1.SqlDbType = SqlDbType.Int;
            parameter1.Direction = ParameterDirection.Input;
            parameter1.Value = IDNadredjenog;
            myCommand.Parameters.Add(parameter1);


            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@BrojKontejnera";
            parameter2.SqlDbType = SqlDbType.NVarChar;
            parameter2.Size = 20;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = BrojKontejnera;
            myCommand.Parameters.Add(parameter2);


            SqlParameter parameter21 = new SqlParameter();
            parameter21.ParameterName = "@SkladisteU";
            parameter21.SqlDbType = SqlDbType.Int;
            parameter21.Direction = ParameterDirection.Input;
            parameter21.Value = SkladisteU;
            myCommand.Parameters.Add(parameter21);

            SqlParameter parameter22 = new SqlParameter();
            parameter22.ParameterName = "@LokacijaU";
            parameter22.SqlDbType = SqlDbType.Int;
            parameter22.Direction = ParameterDirection.Input;
            parameter22.Value = LokacijaU;
            myCommand.Parameters.Add(parameter22);

            SqlParameter parameter23 = new SqlParameter();
            parameter23.ParameterName = "@PronadjenIspravan";
            parameter23.SqlDbType = SqlDbType.Int;
            parameter23.Direction = ParameterDirection.Input;
            parameter23.Value = PronadjenIspravan;
            myCommand.Parameters.Add(parameter23);


            SqlParameter parameter24 = new SqlParameter();
            parameter24.ParameterName = "@SkladisteNovo";
            parameter24.SqlDbType = SqlDbType.Int;
            parameter24.Direction = ParameterDirection.Input;
            parameter24.Value = SkladisteNovo;
            myCommand.Parameters.Add(parameter24);

            SqlParameter parameter25 = new SqlParameter();
            parameter25.ParameterName = "@LokacijaNovo";
            parameter25.SqlDbType = SqlDbType.Int;
            parameter25.Direction = ParameterDirection.Input;
            parameter25.Value = LokacijaNovo;
            myCommand.Parameters.Add(parameter25);

            //  nt Brodar, int TipKontejnera, int StatusKontejnera

            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@Datum";
            parameter3.SqlDbType = SqlDbType.DateTime;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = Datum;
            myCommand.Parameters.Add(parameter3);


            SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@Korisnik";
            parameter4.SqlDbType = SqlDbType.NVarChar;
            parameter4.Size = 20;
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = Korisnik;
            myCommand.Parameters.Add(parameter4);


            SqlParameter parameter41 = new SqlParameter();
            parameter41.ParameterName = "@Brodar";
            parameter41.SqlDbType = SqlDbType.Int;
            parameter41.Direction = ParameterDirection.Input;
            parameter41.Value = Brodar;
            myCommand.Parameters.Add(parameter41);


            SqlParameter parameter42 = new SqlParameter();
            parameter42.ParameterName = "@TipKontejnera";
            parameter42.SqlDbType = SqlDbType.Int;
            parameter42.Direction = ParameterDirection.Input;
            parameter42.Value = TipKontejnera;
            myCommand.Parameters.Add(parameter42);

            SqlParameter parameter43 = new SqlParameter();
            parameter43.ParameterName = "@StatusKontejnera";
            parameter43.SqlDbType = SqlDbType.Int;
            parameter43.Direction = ParameterDirection.Input;
            parameter43.Value = StatusKontejnera;
            myCommand.Parameters.Add(parameter43);


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

        public void DelPopisKontejneraStavke(int ID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "DeletePopisKontejneraStavke";
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
