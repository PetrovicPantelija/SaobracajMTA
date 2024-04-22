
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Testiranje
{
    class InsertCene
    {

        public void InsCene(int TipCenovnika, int Komitent, double Cena, int VrstaManipulacije, DateTime Datum, string Korisnik, double Cena2, int Uvoznik, int PostupakSaRobom,
            string NazivCenovnika, string Razred, string OznakaManipulacije, int OrgJed)
        {

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertCene";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //  @NazivCenovnika nvarchar(100),
            //@Razred nvarchar(3),
            //@OznakaManipulacije nvarchar(50),
            //@OrgJed int



            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@TipCenovnika";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = TipCenovnika;
            myCommand.Parameters.Add(parameter);

            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@Komitent";
            parameter1.SqlDbType = SqlDbType.Int;
            parameter1.Direction = ParameterDirection.Input;
            parameter1.Value = Komitent;
            myCommand.Parameters.Add(parameter1);

            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@Cena";
            parameter2.SqlDbType = SqlDbType.Decimal;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = Cena;
            myCommand.Parameters.Add(parameter2);

            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@VrstaManipulacije";
            parameter3.SqlDbType = SqlDbType.Int;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = VrstaManipulacije;
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
            parameter6.ParameterName = "@Cena2";
            parameter6.SqlDbType = SqlDbType.Decimal;
            parameter6.Direction = ParameterDirection.Input;
            parameter6.Value = Cena2;
            myCommand.Parameters.Add(parameter6);

            SqlParameter parameter7 = new SqlParameter();
            parameter7.ParameterName = "@Uvoznik";
            parameter7.SqlDbType = SqlDbType.Int;
            parameter7.Direction = ParameterDirection.Input;
            parameter7.Value = Uvoznik;
            myCommand.Parameters.Add(parameter7);

            SqlParameter parameter8 = new SqlParameter();
            parameter8.ParameterName = "@PostupakSaRobom";
            parameter8.SqlDbType = SqlDbType.Int;
            parameter8.Direction = ParameterDirection.Input;
            parameter8.Value = PostupakSaRobom;
            myCommand.Parameters.Add(parameter8);

            SqlParameter parameter9 = new SqlParameter();
            parameter9.ParameterName = "@NazivCenovnika";
            parameter9.SqlDbType = SqlDbType.NVarChar;
            parameter9.Size = 100;
            parameter9.Direction = ParameterDirection.Input;
            parameter9.Value = NazivCenovnika;
            myCommand.Parameters.Add(parameter9);

            SqlParameter parameter10 = new SqlParameter();
            parameter10.ParameterName = "@Razred";
            parameter10.SqlDbType = SqlDbType.NVarChar;
            parameter10.Size = 3;
            parameter10.Direction = ParameterDirection.Input;
            parameter10.Value = Razred;
            myCommand.Parameters.Add(parameter10);

            SqlParameter parameter11 = new SqlParameter();
            parameter11.ParameterName = "@OznakaManipulacije";
            parameter11.SqlDbType = SqlDbType.NVarChar;
            parameter11.Size = 50;
            parameter11.Direction = ParameterDirection.Input;
            parameter11.Value = OznakaManipulacije;
            myCommand.Parameters.Add(parameter11);

            SqlParameter parameter12 = new SqlParameter();
            parameter12.ParameterName = "@OrgJed";
            parameter12.SqlDbType = SqlDbType.Int;
            parameter12.Direction = ParameterDirection.Input;
            parameter12.Value = OrgJed;
            myCommand.Parameters.Add(parameter12);


            //  @NazivCenovnika nvarchar(100),
            //@Razred nvarchar(3),
            //@OznakaManipulacije nvarchar(50),
            //@OrgJed int



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

        public void UpdCene(int ID, int TipCenovnika, int Komitent, double Cena, int VrstaManipulacije, DateTime Datum, string Korisnik, double Cena2, int Uvoznik, int PostupakSaRobom,
            string NazivCenovnika, string Razred, string OznakaManipulacije, int OrgJed)
        {

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdateCene";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = ID;
            myCommand.Parameters.Add(parameter);




            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@TipCenovnika";
            parameter1.SqlDbType = SqlDbType.Int;
            parameter1.Direction = ParameterDirection.Input;
            parameter1.Value = TipCenovnika;
            myCommand.Parameters.Add(parameter1);

            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@Komitent";
            parameter2.SqlDbType = SqlDbType.Int;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = Komitent;
            myCommand.Parameters.Add(parameter2);

            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@Cena";
            parameter3.SqlDbType = SqlDbType.Decimal;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = Cena;
            myCommand.Parameters.Add(parameter3);

            SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@VrstaManipulacije";
            parameter4.SqlDbType = SqlDbType.Int;
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = VrstaManipulacije;
            myCommand.Parameters.Add(parameter4);

            SqlParameter parameter5 = new SqlParameter();
            parameter5.ParameterName = "@Datum";
            parameter5.SqlDbType = SqlDbType.DateTime;
            parameter5.Direction = ParameterDirection.Input;
            parameter5.Value = Datum;
            myCommand.Parameters.Add(parameter5);

            SqlParameter parameter6 = new SqlParameter();
            parameter6.ParameterName = "@Korisnik";
            parameter6.SqlDbType = SqlDbType.NVarChar;
            parameter6.Size = 20;
            parameter6.Direction = ParameterDirection.Input;
            parameter6.Value = Korisnik;
            myCommand.Parameters.Add(parameter6);










            SqlParameter parameter7 = new SqlParameter();
            parameter7.ParameterName = "@Cena2";
            parameter7.SqlDbType = SqlDbType.Decimal;
            parameter7.Direction = ParameterDirection.Input;
            parameter7.Value = Cena2;
            myCommand.Parameters.Add(parameter7);

            SqlParameter parameter8 = new SqlParameter();
            parameter8.ParameterName = "@Uvoznik";
            parameter8.SqlDbType = SqlDbType.Int;
            parameter8.Direction = ParameterDirection.Input;
            parameter8.Value = Uvoznik;
            myCommand.Parameters.Add(parameter8);

            SqlParameter parameter9 = new SqlParameter();
            parameter9.ParameterName = "@PostupakSaRobom";
            parameter9.SqlDbType = SqlDbType.Int;
            parameter9.Direction = ParameterDirection.Input;
            parameter9.Value = PostupakSaRobom;
            myCommand.Parameters.Add(parameter9);

            SqlParameter parameter10 = new SqlParameter();
            parameter10.ParameterName = "@NazivCenovnika";
            parameter10.SqlDbType = SqlDbType.NVarChar;
            parameter10.Size = 100;
            parameter10.Direction = ParameterDirection.Input;
            parameter10.Value = NazivCenovnika;
            myCommand.Parameters.Add(parameter10);

            SqlParameter parameter11 = new SqlParameter();
            parameter11.ParameterName = "@Razred";
            parameter11.SqlDbType = SqlDbType.NVarChar;
            parameter11.Size = 3;
            parameter11.Direction = ParameterDirection.Input;
            parameter11.Value = Razred;
            myCommand.Parameters.Add(parameter11);

            SqlParameter parameter12 = new SqlParameter();
            parameter12.ParameterName = "@OznakaManipulacije";
            parameter12.SqlDbType = SqlDbType.NVarChar;
            parameter12.Size = 50;
            parameter12.Direction = ParameterDirection.Input;
            parameter12.Value = OznakaManipulacije;
            myCommand.Parameters.Add(parameter12);

            SqlParameter parameter13 = new SqlParameter();
            parameter13.ParameterName = "@OrgJed";
            parameter13.SqlDbType = SqlDbType.Int;
            parameter13.Direction = ParameterDirection.Input;
            parameter13.Value = OrgJed;
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
                throw new Exception("Neuspešan upis cena u Bazu");
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

        public void DeleteCene(int ID)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "DeleteCene";
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

        public void KopirajCene(int ID, int Komitent)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "KopirajCene";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = ID;
            myCommand.Parameters.Add(parameter);

            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@Partner";
            parameter1.SqlDbType = SqlDbType.Int;
            parameter1.Direction = ParameterDirection.Input;
            parameter1.Value = Komitent;
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

        public void KopirajCeneTip(int TipCenovnika1, int TipCenovnika2)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "KopirajCeneTip";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@TipCenovnika1";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = TipCenovnika1;
            myCommand.Parameters.Add(parameter);

            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@TipCenovnika2";
            parameter1.SqlDbType = SqlDbType.Int;
            parameter1.Direction = ParameterDirection.Input;
            parameter1.Value = TipCenovnika2;
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
