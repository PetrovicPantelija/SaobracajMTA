﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace Testiranje.Sifarnici
{
    class InsertVrstaManipulacije
    {

        public void InsVrstaManipulacije(string Naziv, DateTime Datum, string Korisnik, string JM, int UticeSkladisno, string JM2, int TipManipulacije, int OrgJed, string Oznaka, string Relacija, double Cena, int GrupaVrsteManipulacijeID, int administrativna, int drumski, int Dodatna, int PotvrdaUradio, string Apstrakt1, string Apstrakt2,int TipKontejnera, int RLTerminali, int RLTerminali2, int RLTerminali3)
        {
            // @Oznaka nvarchar(50), 
            // @Relacija nvarchar(100), 
            // @Cena numeric(18,2)
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertVrstaManipulacije";
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
            parameter6.ParameterName = "@JM";
            parameter6.SqlDbType = SqlDbType.NVarChar;
            parameter6.Size = 10;
            parameter6.Direction = ParameterDirection.Input;
            parameter6.Value = JM;
            myCommand.Parameters.Add(parameter6);

            SqlParameter parameter7 = new SqlParameter();
            parameter7.ParameterName = "@UticeSkladisno";
            parameter7.SqlDbType = SqlDbType.TinyInt;
            parameter7.Direction = ParameterDirection.Input;
            parameter7.Value = UticeSkladisno;
            myCommand.Parameters.Add(parameter7);


            SqlParameter parameter8 = new SqlParameter();
            parameter8.ParameterName = "@JM2";
            parameter8.SqlDbType = SqlDbType.NVarChar;
            parameter8.Size = 10;
            parameter8.Direction = ParameterDirection.Input;
            parameter8.Value = JM2;
            myCommand.Parameters.Add(parameter8);

            SqlParameter parameter9 = new SqlParameter();
            parameter9.ParameterName = "@TipManipulacije";
            parameter9.SqlDbType = SqlDbType.Int;
            parameter9.Direction = ParameterDirection.Input;
            parameter9.Value = TipManipulacije;
            myCommand.Parameters.Add(parameter9);

            SqlParameter parameter10 = new SqlParameter();
            parameter10.ParameterName = "@OrgJed";
            parameter10.SqlDbType = SqlDbType.Int;
            parameter10.Direction = ParameterDirection.Input;
            parameter10.Value = OrgJed;
            myCommand.Parameters.Add(parameter10);

            SqlParameter parameter11 = new SqlParameter();
            parameter11.ParameterName = "@Oznaka";
            parameter11.SqlDbType = SqlDbType.NVarChar;
            parameter11.Size = 50;
            parameter11.Direction = ParameterDirection.Input;
            parameter11.Value = Oznaka;
            myCommand.Parameters.Add(parameter11);

            SqlParameter parameter12 = new SqlParameter();
            parameter12.ParameterName = "@Relacija";
            parameter12.SqlDbType = SqlDbType.NVarChar;
            parameter12.Size = 100;
            parameter12.Direction = ParameterDirection.Input;
            parameter12.Value = Relacija;
            myCommand.Parameters.Add(parameter12);

            SqlParameter parameter13 = new SqlParameter();
            parameter13.ParameterName = "@Cena";
            parameter13.SqlDbType = SqlDbType.Decimal;
            parameter13.Direction = ParameterDirection.Input;
            parameter13.Value = Cena;
            myCommand.Parameters.Add(parameter13);

            SqlParameter parameter14 = new SqlParameter();
            parameter14.ParameterName = "@GrupaVrsteManipulacijeID";
            parameter14.SqlDbType = SqlDbType.Int;
            parameter14.Direction = ParameterDirection.Input;
            parameter14.Value = GrupaVrsteManipulacijeID;
            myCommand.Parameters.Add(parameter14);

            SqlParameter parameter15 = new SqlParameter();
            parameter15.ParameterName = "@Administrativna";
            parameter15.SqlDbType = SqlDbType.Int;
            parameter15.Direction = ParameterDirection.Input;
            parameter15.Value = administrativna;
            myCommand.Parameters.Add(parameter15);


            SqlParameter parameter16 = new SqlParameter();
            parameter16.ParameterName = "@Drumska";
            parameter16.SqlDbType = SqlDbType.Int;
            parameter16.Direction = ParameterDirection.Input;
            parameter16.Value = drumski;
            myCommand.Parameters.Add(parameter16);


            SqlParameter parameter17 = new SqlParameter();
            parameter17.ParameterName = "@Dodatna";
            parameter17.SqlDbType = SqlDbType.Int;
            parameter17.Direction = ParameterDirection.Input;
            parameter17.Value = Dodatna;
            myCommand.Parameters.Add(parameter17);


            SqlParameter parameter18 = new SqlParameter();
            parameter18.ParameterName = "@PotvrdaUradio";
            parameter18.SqlDbType = SqlDbType.Int;
            parameter18.Direction = ParameterDirection.Input;
            parameter18.Value = PotvrdaUradio;
            myCommand.Parameters.Add(parameter18);


            SqlParameter parameter19 = new SqlParameter();
            parameter19.ParameterName = "@Apstrakt1";
            parameter19.SqlDbType = SqlDbType.NVarChar;
            parameter19.Size = 30;
            parameter19.Direction = ParameterDirection.Input;
            parameter19.Value = Apstrakt1;
            myCommand.Parameters.Add(parameter19);

            SqlParameter parameter20 = new SqlParameter();
            parameter20.ParameterName = "@Apstrakt2";
            parameter20.SqlDbType = SqlDbType.NVarChar;
            parameter20.Size = 30;
            parameter20.Direction = ParameterDirection.Input;
            parameter20.Value = Apstrakt2;
            myCommand.Parameters.Add(parameter20);

            SqlParameter parameter21 = new SqlParameter();
            parameter21.ParameterName = "@TipKontejnera";
            parameter21.SqlDbType= SqlDbType.Int;
            parameter21.Direction = ParameterDirection.Input;
            parameter21.Value = TipKontejnera;
            myCommand.Parameters.Add(parameter21);


            SqlParameter parameter22 = new SqlParameter();
            parameter22.ParameterName = "@RLTerminali";
            parameter22.SqlDbType = SqlDbType.Int;
            parameter22.Direction = ParameterDirection.Input;
            parameter22.Value = RLTerminali;
            myCommand.Parameters.Add(parameter22);


            SqlParameter parameter23 = new SqlParameter();
            parameter23.ParameterName = "@RLTerminali2";
            parameter23.SqlDbType = SqlDbType.Int;
            parameter23.Direction = ParameterDirection.Input;
            parameter23.Value = RLTerminali2;
            myCommand.Parameters.Add(parameter23);

            SqlParameter parameter24 = new SqlParameter();
            parameter24.ParameterName = "@RLTerminali3";
            parameter24.SqlDbType = SqlDbType.Int;
            parameter24.Direction = ParameterDirection.Input;
            parameter24.Value = RLTerminali3;
            myCommand.Parameters.Add(parameter24);





            // @Oznaka nvarchar(50), 
            // @Relacija nvarchar(100), 
            // @Cena numeric(18,2)

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
                throw new Exception("Neuspešan upis manipulacije u bazu");
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

        public void UpdVrstaManipulacije(int ID, string Naziv, DateTime Datum, string Korisnik, string JM, int UticeSkladisno, string JM2, int TipManipulacije, int OrgJed, string Oznaka, string Relacija, double Cena, int GrupaVrsteManipulacijeID, int administrativna, int drumski, int Dodatna, int PotvrdaUradio, string Apstrakt1, string Apstrakt2,int TipKontejnera, int RLTerminali, int RLTerminali2, int RLTerminali3)
        {
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdateVrstaManipulacije";
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
            parameter6.ParameterName = "@JM";
            parameter6.SqlDbType = SqlDbType.NVarChar;
            parameter6.Size = 10;
            parameter6.Direction = ParameterDirection.Input;
            parameter6.Value = JM;
            myCommand.Parameters.Add(parameter6);

            SqlParameter parameter7 = new SqlParameter();
            parameter7.ParameterName = "@UticeSkladisno";
            parameter7.SqlDbType = SqlDbType.TinyInt;
            parameter7.Direction = ParameterDirection.Input;
            parameter7.Value = UticeSkladisno;
            myCommand.Parameters.Add(parameter7);


            SqlParameter parameter8 = new SqlParameter();
            parameter8.ParameterName = "@JM2";
            parameter8.SqlDbType = SqlDbType.NVarChar;
            parameter8.Size = 10;
            parameter8.Direction = ParameterDirection.Input;
            parameter8.Value = JM2;
            myCommand.Parameters.Add(parameter8);

            SqlParameter parameter9 = new SqlParameter();
            parameter9.ParameterName = "@TipManipulacije";
            parameter9.SqlDbType = SqlDbType.Int;
            parameter9.Direction = ParameterDirection.Input;
            parameter9.Value = TipManipulacije;
            myCommand.Parameters.Add(parameter9);

            SqlParameter parameter10 = new SqlParameter();
            parameter10.ParameterName = "@OrgJed";
            parameter10.SqlDbType = SqlDbType.Int;
            parameter10.Direction = ParameterDirection.Input;
            parameter10.Value = OrgJed;
            myCommand.Parameters.Add(parameter10);

            SqlParameter parameter11 = new SqlParameter();
            parameter11.ParameterName = "@Oznaka";
            parameter11.SqlDbType = SqlDbType.NVarChar;
            parameter11.Size = 50;
            parameter11.Direction = ParameterDirection.Input;
            parameter11.Value = Oznaka;
            myCommand.Parameters.Add(parameter11);

            SqlParameter parameter12 = new SqlParameter();
            parameter12.ParameterName = "@Relacija";
            parameter12.SqlDbType = SqlDbType.NVarChar;
            parameter12.Size = 100;
            parameter12.Direction = ParameterDirection.Input;
            parameter12.Value = Relacija;
            myCommand.Parameters.Add(parameter12);

            SqlParameter parameter13 = new SqlParameter();
            parameter13.ParameterName = "@Cena";
            parameter13.SqlDbType = SqlDbType.Decimal;
            parameter13.Direction = ParameterDirection.Input;
            parameter13.Value = Cena;
            myCommand.Parameters.Add(parameter13);

            SqlParameter parameter14 = new SqlParameter();
            parameter14.ParameterName = "@GrupaVrsteManipulacijeID";
            parameter14.SqlDbType = SqlDbType.Int;
            parameter14.Direction = ParameterDirection.Input;
            parameter14.Value = GrupaVrsteManipulacijeID;
            myCommand.Parameters.Add(parameter14);

            SqlParameter parameter15 = new SqlParameter();
            parameter15.ParameterName = "@Administrativna";
            parameter15.SqlDbType = SqlDbType.Int;
            parameter15.Direction = ParameterDirection.Input;
            parameter15.Value = administrativna;
            myCommand.Parameters.Add(parameter15);


            SqlParameter parameter16 = new SqlParameter();
            parameter16.ParameterName = "@Drumska";
            parameter16.SqlDbType = SqlDbType.Int;
            parameter16.Direction = ParameterDirection.Input;
            parameter16.Value = drumski;
            myCommand.Parameters.Add(parameter16);

            SqlParameter parameter17 = new SqlParameter();
            parameter17.ParameterName = "@Dodatna";
            parameter17.SqlDbType = SqlDbType.Int;
            parameter17.Direction = ParameterDirection.Input;
            parameter17.Value = Dodatna;
            myCommand.Parameters.Add(parameter17);


            SqlParameter parameter18 = new SqlParameter();
            parameter18.ParameterName = "@PotvrdaUradio";
            parameter18.SqlDbType = SqlDbType.Int;
            parameter18.Direction = ParameterDirection.Input;
            parameter18.Value = PotvrdaUradio;
            myCommand.Parameters.Add(parameter18);


            SqlParameter parameter19 = new SqlParameter();
            parameter19.ParameterName = "@Apstrakt1";
            parameter19.SqlDbType = SqlDbType.NVarChar;
            parameter19.Size = 30;
            parameter19.Direction = ParameterDirection.Input;
            parameter19.Value = Apstrakt1;
            myCommand.Parameters.Add(parameter19);

            SqlParameter parameter20 = new SqlParameter();
            parameter20.ParameterName = "@Apstrakt2";
            parameter20.SqlDbType = SqlDbType.NVarChar;
            parameter20.Size = 30;
            parameter20.Direction = ParameterDirection.Input;
            parameter20.Value = Apstrakt2;
            myCommand.Parameters.Add(parameter20);

            SqlParameter parameter21 = new SqlParameter();
            parameter21.ParameterName = "@TipKontejnera";
            parameter21.SqlDbType = SqlDbType.Int;
            parameter21.Direction = ParameterDirection.Input;
            parameter21.Value = TipKontejnera;
            myCommand.Parameters.Add(parameter21);

            SqlParameter parameter22 = new SqlParameter();
            parameter22.ParameterName = "@RLTerminali";
            parameter22.SqlDbType = SqlDbType.Int;
            parameter22.Direction = ParameterDirection.Input;
            parameter22.Value = RLTerminali;
            myCommand.Parameters.Add(parameter22);


            SqlParameter parameter23 = new SqlParameter();
            parameter23.ParameterName = "@RLTerminali2";
            parameter23.SqlDbType = SqlDbType.Int;
            parameter23.Direction = ParameterDirection.Input;
            parameter23.Value = RLTerminali2;
            myCommand.Parameters.Add(parameter23);

            SqlParameter parameter24 = new SqlParameter();
            parameter24.ParameterName = "@RLTerminali3";
            parameter24.SqlDbType = SqlDbType.Int;
            parameter24.Direction = ParameterDirection.Input;
            parameter24.Value = RLTerminali3;
            myCommand.Parameters.Add(parameter24);

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

        public void DeleteVrstaManipulacije(int ID)
        {
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "DeleteVrstaManipulacije";
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
                    MessageBox.Show("Brisanje Cena uspešno završeno", "",
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
