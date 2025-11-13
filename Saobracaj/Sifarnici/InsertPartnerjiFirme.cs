using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.Sifarnici
{
    class InsertPartnerjiFirme
    {
        public string connect = Sifarnici.frmLogovanje.connectionString;


        public int InsParnerjiFirme(int Parnerid, int SifraApp, string Firma)
        {
            int IDPom = 0;
            SqlConnection myConnection = new SqlConnection(connect);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertPartnerjiFirma";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parnerid = new SqlParameter();
            parnerid.ParameterName = "@Parnerid";
            parnerid.SqlDbType = SqlDbType.Int;
            parnerid.Direction = ParameterDirection.Input;
            parnerid.Value = Parnerid;
            myCommand.Parameters.Add(parnerid);

            SqlParameter sifraApp = new SqlParameter();
            sifraApp.ParameterName = "@SifraApp";
            sifraApp.SqlDbType = SqlDbType.Int;
            sifraApp.Direction = ParameterDirection.Input;
            sifraApp.Value = SifraApp;
            myCommand.Parameters.Add(sifraApp);

            SqlParameter firma = new SqlParameter();
            firma.ParameterName = "@Firma";
            firma.SqlDbType = SqlDbType.VarChar;
            firma.Size = 250;
            firma.Direction = ParameterDirection.Input;
            firma.Value = string.IsNullOrWhiteSpace(Firma) ? (object)DBNull.Value : (object)Firma;
            myCommand.Parameters.Add(firma);

            SqlParameter idParam = new SqlParameter("@IDPom", SqlDbType.Int);
            idParam.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(idParam);

            myConnection.Open();
            SqlTransaction myTransaction = myConnection.BeginTransaction();
            myCommand.Transaction = myTransaction;
           // IDPom = (int)myCommand.Parameters["@IDPom"].Value;
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
                MessageBox.Show(ex.ToString());

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
            return IDPom;
        }

        public void UpdPartneriFirme(int ID, int Parnerid, int SifraApp, string Firma)
        {
            SqlConnection myConnection = new SqlConnection(connect);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdatePartnerjiFirma";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = ID;
            myCommand.Parameters.Add(parameter);

            SqlParameter parnerid = new SqlParameter();
            parnerid.ParameterName = "@Parnerid";
            parnerid.SqlDbType = SqlDbType.Int;
            parnerid.Direction = ParameterDirection.Input;
            parnerid.Value = Parnerid;
            myCommand.Parameters.Add(parnerid);

            SqlParameter sifraApp = new SqlParameter();
            sifraApp.ParameterName = "@SifraApp";
            sifraApp.SqlDbType = SqlDbType.Int;
            sifraApp.Direction = ParameterDirection.Input;
            sifraApp.Value = SifraApp;
            myCommand.Parameters.Add(sifraApp);

            SqlParameter firma = new SqlParameter();
            firma.ParameterName = "@Firma";
            firma.SqlDbType = SqlDbType.VarChar;
            firma.Size = 250;
            firma.Direction = ParameterDirection.Input;
            firma.Value = string.IsNullOrWhiteSpace(Firma) ? (object)DBNull.Value : (object)Firma;
            myCommand.Parameters.Add(firma);
            /*
            
            */
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
                throw new Exception("Neuspešna promena podataka\n" + ex.ToString());
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

        public void DelPartneriFirme(int ID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "DeleteParnerjiFirma";
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

            catch (SqlException ex)
            {
                MessageBox.Show("Greška u SQL izvršavanju: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                myTransaction.Rollback(); // Ne zaboravi i rollback
                //throw new Exception("Neuspešna promena podataka");
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
