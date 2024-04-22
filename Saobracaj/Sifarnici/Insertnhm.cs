using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.Sifarnici
{
    class Insertnhm
    {
        string connect = frmLogovanje.connectionString;
        public void InsNHM(string Broj, string Naziv, int RID, int ADRID, int Uvozni, int Interni)
        {
            SqlConnection myConnection = new SqlConnection(connect);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertNhm";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@Broj";
            parameter.SqlDbType = SqlDbType.Char;
            parameter.Size = 24;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = Broj;
            myCommand.Parameters.Add(parameter);

            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@Naziv";
            parameter2.SqlDbType = SqlDbType.Char;
            parameter.Size = 1000;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = Naziv;
            myCommand.Parameters.Add(parameter2);

            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@RID";
            parameter3.SqlDbType = SqlDbType.Int;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = RID;
            myCommand.Parameters.Add(parameter3);


            SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@ADRID";
            parameter4.SqlDbType = SqlDbType.Int;
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = ADRID;
            myCommand.Parameters.Add(parameter4);

            SqlParameter parameter5 = new SqlParameter();
            parameter5.ParameterName = "@Uvozni";
            parameter5.SqlDbType = SqlDbType.Int;
            parameter5.Direction = ParameterDirection.Input;
            parameter5.Value = Uvozni;
            myCommand.Parameters.Add(parameter5);

            SqlParameter parameter6 = new SqlParameter();
            parameter6.ParameterName = "@Interni";
            parameter6.SqlDbType = SqlDbType.Int;
            parameter6.Direction = ParameterDirection.Input;
            parameter6.Value = Interni;
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

            catch (SqlException)
            {
                throw new Exception("Neuspešan upis NHM brojeva");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos NHM broja je uspešno završena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }


        }


        public void UpdNHM(int ID, string Broj, string Naziv, int RID, int ADRID, int Uvozni, int Interni)
        {
            SqlConnection myConnection = new SqlConnection(connect);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdateNHM";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = ID;
            myCommand.Parameters.Add(parameter);


            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@Broj";
            parameter2.SqlDbType = SqlDbType.Char;
            parameter2.Size = 24;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = Broj;
            myCommand.Parameters.Add(parameter2);

            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@Naziv";
            parameter3.SqlDbType = SqlDbType.NVarChar;
            parameter3.Size = 1000;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = Naziv;
            myCommand.Parameters.Add(parameter3);

            SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@RID";
            parameter4.SqlDbType = SqlDbType.Int;
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = RID;
            myCommand.Parameters.Add(parameter4);

            SqlParameter parameter5 = new SqlParameter();
            parameter5.ParameterName = "@ADRID";
            parameter5.SqlDbType = SqlDbType.Int;
            parameter5.Direction = ParameterDirection.Input;
            parameter5.Value = ADRID;
            myCommand.Parameters.Add(parameter5);

            SqlParameter parameter6 = new SqlParameter();
            parameter6.ParameterName = "@Uvozni";
            parameter6.SqlDbType = SqlDbType.Int;
            parameter6.Direction = ParameterDirection.Input;
            parameter6.Value = Uvozni;
            myCommand.Parameters.Add(parameter6);

            SqlParameter parameter7 = new SqlParameter();
            parameter7.ParameterName = "@Interni";
            parameter7.SqlDbType = SqlDbType.Int;
            parameter7.Direction = ParameterDirection.Input;
            parameter7.Value = Interni;
            myCommand.Parameters.Add(parameter7);

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
                throw new Exception("Neuspešna promena NHM brojeva");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Promena NHM broja je uspešno završena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }


        }


        public void DeleteNHM(int ID)
        {

            SqlConnection myConnection = new SqlConnection(connect);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "DeleteNHM";
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
                    MessageBox.Show("Brisanje nije uspešno", "",
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
