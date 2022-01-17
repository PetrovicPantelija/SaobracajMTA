using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Saobracaj.Sifarnici
{
    class InsertLokomotive
    {
        public void InsLokomotive(string SmSifra, string Password, double Masa, int Dizel, int StatusLokomotive)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdateLokomotivaPassword";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@SmSifra";
            parameter.SqlDbType = SqlDbType.Char;
            parameter.Size = 8;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = SmSifra;
            myCommand.Parameters.Add(parameter);

            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@Password";
            parameter2.SqlDbType = SqlDbType.NVarChar;
            parameter2.Size = 10;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = Password;
            myCommand.Parameters.Add(parameter2);

            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@Masa";
            parameter3.SqlDbType = SqlDbType.Decimal;
           // parameter3.Size = 10;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = Masa;
            myCommand.Parameters.Add(parameter3);

            SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@Dizel";
            parameter4.SqlDbType = SqlDbType.Int;
            // parameter3.Size = 10;
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = Dizel;
            myCommand.Parameters.Add(parameter4);

            SqlParameter parameter5 = new SqlParameter();
            parameter5.ParameterName = "@StatusLokomotive";
            parameter5.SqlDbType = SqlDbType.Int;
            // parameter3.Size = 10;
            parameter5.Direction = ParameterDirection.Input;
            parameter5.Value = StatusLokomotive;
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
                throw new Exception("Neuspešan update Password brojeva");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Promena password  je uspešno završena", "",
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
