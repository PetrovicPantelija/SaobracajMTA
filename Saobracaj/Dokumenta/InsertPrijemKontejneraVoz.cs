

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Saobracaj.Sifarnici;

namespace Saobracaj.Dokumeta
{
    class InsertPrijemKontejneraVoz
    {
        string connection = frmLogovanje.connectionString;
        public void InsertPrijemKontVoz( DateTime DatumPrijema,int StatusPrijema,int IdVoza, DateTime VremeDolaska,	DateTime Datum, string Korisnik,  string RegBrKamiona,   string ImeVozaca,   int Vozom, string Napomena, int PredefinisanePorukeID, int Operater, int VrstaKamiona, int Poreklo, int OperaterHR, int Modul)
        {
            /*
             @DatumPrijema [datetime] ,
  @StatusPrijema [int] ,
  @IdVoza [int] ,
  @VremeDolaska [datetime] ,
             */
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertPrijemKontejneraVoz";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;
            
            
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@DatumPrijema";
            parameter.SqlDbType = SqlDbType.DateTime;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = DatumPrijema;
            myCommand.Parameters.Add(parameter);

            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "StatusPrijema";
            parameter1.SqlDbType = SqlDbType.Int;
            parameter1.Direction = ParameterDirection.Input;
            parameter1.Value = StatusPrijema;
            myCommand.Parameters.Add(parameter1);

           

            SqlParameter parameter14 = new SqlParameter();
            parameter14.ParameterName = "@IdVoza";
            parameter14.SqlDbType = SqlDbType.Int;
            // parameter13.Size = 30;
            parameter14.Direction = ParameterDirection.Input;
            parameter14.Value = IdVoza;
            myCommand.Parameters.Add(parameter14);

            SqlParameter parameter15 = new SqlParameter();
            parameter15.ParameterName = "@VremeDolaska";
            parameter15.SqlDbType = SqlDbType.DateTime;
            // parameter13.Size = 30;
            parameter15.Direction = ParameterDirection.Input;
            parameter15.Value = VremeDolaska;
            myCommand.Parameters.Add(parameter15);

            
            SqlParameter parameter18 = new SqlParameter();
            parameter18.ParameterName = "@Datum";
            parameter18.SqlDbType = SqlDbType.DateTime;
          //  parameter18.Size = 20;
            parameter18.Direction = ParameterDirection.Input;
            parameter18.Value = Datum;
            myCommand.Parameters.Add(parameter18);
            
            SqlParameter parameter19 = new SqlParameter();
            parameter19.ParameterName = "@Korisnik";
            parameter19.SqlDbType = SqlDbType.NVarChar;
            parameter19.Size = 20;
            parameter19.Direction = ParameterDirection.Input;
            parameter19.Value = Korisnik;
            myCommand.Parameters.Add(parameter19);

            SqlParameter parameter20 = new SqlParameter();
            parameter20.ParameterName = "@RegBrKamiona";
            parameter20.SqlDbType = SqlDbType.NVarChar;
            parameter20.Size = 20;
            parameter20.Direction = ParameterDirection.Input;
            parameter20.Value = RegBrKamiona;
            myCommand.Parameters.Add(parameter20);

            SqlParameter parameter21 = new SqlParameter();
            parameter21.ParameterName = "@ImeVozaca";
            parameter21.SqlDbType = SqlDbType.NVarChar;
            parameter21.Size = 50;
            parameter21.Direction = ParameterDirection.Input;
            parameter21.Value = ImeVozaca;
            myCommand.Parameters.Add(parameter21);

            SqlParameter parameter22 = new SqlParameter();
            parameter22.ParameterName = "@Vozom";
            parameter22.SqlDbType = SqlDbType.Int;
            //parameter22.Size = 50;
            parameter22.Direction = ParameterDirection.Input;
            parameter22.Value = Vozom;
            myCommand.Parameters.Add(parameter22);

            SqlParameter parameter23 = new SqlParameter();
            parameter23.ParameterName = "@Napomena";
            parameter23.SqlDbType = SqlDbType.NVarChar;
            parameter23.Size = 300;
            parameter23.Direction = ParameterDirection.Input;
            parameter23.Value = Napomena;
            myCommand.Parameters.Add(parameter23);

            SqlParameter parameter24 = new SqlParameter();
            parameter24.ParameterName = "@PredefinisanePorukeID";
            parameter24.SqlDbType = SqlDbType.Int;
            parameter24.Direction = ParameterDirection.Input;
            parameter24.Value = PredefinisanePorukeID;
            myCommand.Parameters.Add(parameter24);

            // int Operater, int VrstaKamiona, int Poreklo
            SqlParameter parameter25 = new SqlParameter();
            parameter25.ParameterName = "@Operater";
            parameter25.SqlDbType = SqlDbType.Int;
            parameter25.Direction = ParameterDirection.Input;
            parameter25.Value = Operater;
            myCommand.Parameters.Add(parameter25);

            SqlParameter parameter26 = new SqlParameter();
            parameter26.ParameterName = "@VrstaKamiona";
            parameter26.SqlDbType = SqlDbType.Int;
            parameter26.Direction = ParameterDirection.Input;
            parameter26.Value = VrstaKamiona;
            myCommand.Parameters.Add(parameter26);

            SqlParameter parameter27 = new SqlParameter();
            parameter27.ParameterName = "@Poreklo";
            parameter27.SqlDbType = SqlDbType.Int;
            parameter27.Direction = ParameterDirection.Input;
            parameter27.Value = Poreklo;
            myCommand.Parameters.Add(parameter27);


            SqlParameter parameter28 = new SqlParameter();
            parameter28.ParameterName = "@OperaterHR";
            parameter28.SqlDbType = SqlDbType.Int;
            parameter28.Direction = ParameterDirection.Input;
            parameter28.Value = OperaterHR;
            myCommand.Parameters.Add(parameter28);

            SqlParameter parameter29 = new SqlParameter();
            parameter29.ParameterName = "@Modul";
            parameter29.SqlDbType = SqlDbType.Int;
            parameter29.Direction = ParameterDirection.Input;
            parameter29.Value = Modul;
            myCommand.Parameters.Add(parameter29);

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
                throw new Exception("Neuspešan upis u bazu");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Nije uspeo upis", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }


            }
        }

        public void UpdPrijemKontejneraVoz(int ID, DateTime DatumPrijema, int StatusPrijema, int IdVoza, DateTime VremeDolaska, DateTime Datum, string Korisnik, string RegBrKamiona, string ImeVozaca, int Vozom, string Napomena, int PredefinisanePorukeID, int Operater, int VrstaKamiona, int Poreklo, int OperaterHR, int Modul)
        {

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdatePrijemKontejneraVoz";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = ID;
            myCommand.Parameters.Add(parameter);


            SqlParameter parameter0 = new SqlParameter();
            parameter0.ParameterName = "@DatumPrijema";
            parameter0.SqlDbType = SqlDbType.DateTime;
            parameter0.Direction = ParameterDirection.Input;
            parameter0.Value = DatumPrijema;
            myCommand.Parameters.Add(parameter0);

            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "StatusPrijema";
            parameter1.SqlDbType = SqlDbType.Int;
            parameter1.Direction = ParameterDirection.Input;
            parameter1.Value = StatusPrijema;
            myCommand.Parameters.Add(parameter1);



            SqlParameter parameter14 = new SqlParameter();
            parameter14.ParameterName = "@IdVoza";
            parameter14.SqlDbType = SqlDbType.Int;
            // parameter13.Size = 30;
            parameter14.Direction = ParameterDirection.Input;
            parameter14.Value = IdVoza;
            myCommand.Parameters.Add(parameter14);

            SqlParameter parameter15 = new SqlParameter();
            parameter15.ParameterName = "@VremeDolaska";
            parameter15.SqlDbType = SqlDbType.DateTime;
            // parameter13.Size = 30;
            parameter15.Direction = ParameterDirection.Input;
            parameter15.Value = VremeDolaska;
            myCommand.Parameters.Add(parameter15);


            SqlParameter parameter18 = new SqlParameter();
            parameter18.ParameterName = "@Datum";
            parameter18.SqlDbType = SqlDbType.DateTime;
           // parameter18.Size = 20;
            parameter18.Direction = ParameterDirection.Input;
            parameter18.Value = Datum;
            myCommand.Parameters.Add(parameter18);

            SqlParameter parameter19 = new SqlParameter();
            parameter19.ParameterName = "@Korisnik";
            parameter19.SqlDbType = SqlDbType.NVarChar;
            parameter19.Size = 20;
            parameter19.Direction = ParameterDirection.Input;
            parameter19.Value = Korisnik;
            myCommand.Parameters.Add(parameter19);

            SqlParameter parameter20 = new SqlParameter();
            parameter20.ParameterName = "@RegBrKamiona";
            parameter20.SqlDbType = SqlDbType.NVarChar;
            parameter20.Size = 20;
            parameter20.Direction = ParameterDirection.Input;
            parameter20.Value = RegBrKamiona;
            myCommand.Parameters.Add(parameter20);

            SqlParameter parameter21 = new SqlParameter();
            parameter21.ParameterName = "@ImeVozaca";
            parameter21.SqlDbType = SqlDbType.NVarChar;
            parameter21.Size = 50;
            parameter21.Direction = ParameterDirection.Input;
            parameter21.Value = ImeVozaca;
            myCommand.Parameters.Add(parameter21);

            SqlParameter parameter22 = new SqlParameter();
            parameter22.ParameterName = "@Vozom";
            parameter22.SqlDbType = SqlDbType.Int;
            //parameter22.Size = 50;
            parameter22.Direction = ParameterDirection.Input;
            parameter22.Value = Vozom;
            myCommand.Parameters.Add(parameter22);

            SqlParameter parameter23 = new SqlParameter();
            parameter23.ParameterName = "@Napomena";
            parameter23.SqlDbType = SqlDbType.NVarChar;
            parameter23.Size = 300;
            parameter23.Direction = ParameterDirection.Input;
            parameter23.Value = Napomena;
            myCommand.Parameters.Add(parameter23);

            SqlParameter parameter24 = new SqlParameter();
            parameter24.ParameterName = "@PredefinisanePorukeID";
            parameter24.SqlDbType = SqlDbType.Int;
            parameter24.Direction = ParameterDirection.Input;
            parameter24.Value = PredefinisanePorukeID;
            myCommand.Parameters.Add(parameter24);

            SqlParameter parameter25 = new SqlParameter();
            parameter25.ParameterName = "@Operater";
            parameter25.SqlDbType = SqlDbType.Int;
            parameter25.Direction = ParameterDirection.Input;
            parameter25.Value = Operater;
            myCommand.Parameters.Add(parameter25);

            SqlParameter parameter26 = new SqlParameter();
            parameter26.ParameterName = "@VrstaKamiona";
            parameter26.SqlDbType = SqlDbType.Int;
            parameter26.Direction = ParameterDirection.Input;
            parameter26.Value = VrstaKamiona;
            myCommand.Parameters.Add(parameter26);

            SqlParameter parameter27 = new SqlParameter();
            parameter27.ParameterName = "@Poreklo";
            parameter27.SqlDbType = SqlDbType.Int;
            parameter27.Direction = ParameterDirection.Input;
            parameter27.Value = Poreklo;
            myCommand.Parameters.Add(parameter27);

            SqlParameter parameter28 = new SqlParameter();
            parameter28.ParameterName = "@OperaterHR";
            parameter28.SqlDbType = SqlDbType.Int;
            parameter28.Direction = ParameterDirection.Input;
            parameter28.Value = OperaterHR;
            myCommand.Parameters.Add(parameter28);

            SqlParameter parameter29 = new SqlParameter();
            parameter29.ParameterName = "@Modul";
            parameter29.SqlDbType = SqlDbType.Int;
            parameter29.Direction = ParameterDirection.Input;
            parameter29.Value = Modul;
            myCommand.Parameters.Add(parameter29);


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
                throw new Exception("Neuspešan upis u Bazu");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Nije uspeo upis ", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }


            }
        }

        public void DeletePrijemKontejneraVoz(int ID)
          {
              var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
              SqlConnection myConnection = new SqlConnection(s_connection);
              SqlCommand myCommand = myConnection.CreateCommand();
              myCommand.CommandText = "DeletePrijemKontejneraVoz";
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

        public void UpdateEmailPrijemNajava(int ID)
          {
              var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
              SqlConnection myConnection = new SqlConnection(s_connection);
              SqlCommand myCommand = myConnection.CreateCommand();
              myCommand.CommandText = "UpdateEmailPrijemNajava";
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

        public void UpdateEmailPrijemPrijem(int ID)
          {
              var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
              SqlConnection myConnection = new SqlConnection(s_connection);
              SqlCommand myCommand = myConnection.CreateCommand();
              myCommand.CommandText = "UpdateEmailPrijemPrijem";
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
        public void InsertPostupakStavke(int IdStavke,int IdPostupka)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertPostupakStavke";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter stavka = new SqlParameter();
            stavka.ParameterName = "@IDStavke";
            stavka.SqlDbType = SqlDbType.Int;
            stavka.Direction = ParameterDirection.Input;
            stavka.Value = IdStavke;
            cmd.Parameters.Add(stavka);

            SqlParameter postupak = new SqlParameter();
            postupak.ParameterName = "@IdPostupka";
            postupak.SqlDbType = SqlDbType.Int;
            postupak.Direction = ParameterDirection.Input;
            postupak.Value = IdPostupka;
            cmd.Parameters.Add(postupak);

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
                throw new Exception("Upis neuspešno");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Upis uspešno završen", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }
        public void DeletePostupakStavke(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeletePostupakStavke";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter stavka = new SqlParameter();
            stavka.ParameterName = "@ID";
            stavka.SqlDbType = SqlDbType.Int;
            stavka.Direction = ParameterDirection.Input;
            stavka.Value = ID;
            cmd.Parameters.Add(stavka);

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
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }
        public void InsertPostupakStavkeOtprema(int IdStavke, int IdPostupka)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertPostupakStavkeOtprema";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter stavka = new SqlParameter();
            stavka.ParameterName = "@IDStavke";
            stavka.SqlDbType = SqlDbType.Int;
            stavka.Direction = ParameterDirection.Input;
            stavka.Value = IdStavke;
            cmd.Parameters.Add(stavka);

            SqlParameter postupak = new SqlParameter();
            postupak.ParameterName = "@IdPostupka";
            postupak.SqlDbType = SqlDbType.Int;
            postupak.Direction = ParameterDirection.Input;
            postupak.Value = IdPostupka;
            cmd.Parameters.Add(postupak);

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
                throw new Exception("Upis neuspešno");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Upis uspešno završen", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }
        public void DeletePostupakStavkeOtprema(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeletePostupakStavkeOtprema";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter stavka = new SqlParameter();
            stavka.ParameterName = "@ID";
            stavka.SqlDbType = SqlDbType.Int;
            stavka.Direction = ParameterDirection.Input;
            stavka.Value = ID;
            cmd.Parameters.Add(stavka);

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
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }
    }
}



