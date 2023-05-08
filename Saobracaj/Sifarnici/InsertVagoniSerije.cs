using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Sifarnici
{
    class InsertVagoniSerije
    {
        public void InsVagoniSerije(string Serija, string BrojcanaSerija, int BrojOsovina)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(s_connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertVagoniSerije";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter serija = new SqlParameter();
            serija.ParameterName = "@Serija";
            serija.SqlDbType = SqlDbType.NVarChar;
            serija.Size = 50;
            serija.Direction = ParameterDirection.Input;
            serija.Value = Serija;
            cmd.Parameters.Add(serija);


            SqlParameter brojcanaserija = new SqlParameter();
            brojcanaserija.ParameterName = "@BrojcanaSerija";
            brojcanaserija.SqlDbType = SqlDbType.NVarChar;
            brojcanaserija.Size = 20;
            brojcanaserija.Direction = ParameterDirection.Input;
            brojcanaserija.Value = BrojcanaSerija;
            cmd.Parameters.Add(brojcanaserija);


            SqlParameter brojosovina = new SqlParameter();
            brojosovina.ParameterName = "@BrojOsovina";
            brojosovina.SqlDbType = SqlDbType.Int;
            // brojcanaserija.Size = 20;
            brojosovina.Direction = ParameterDirection.Input;
            brojosovina.Value = BrojOsovina;
            cmd.Parameters.Add(brojosovina);

            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            cmd.Transaction = tran;
            bool error = true;
            try
            {
                cmd.ExecuteNonQuery();
                tran.Commit();
                tran = conn.BeginTransaction();
                cmd.Transaction = tran;
            }
            catch (SqlException)
            {
                throw new Exception("Neuspešan upis serije");
            }
            finally
            {
                if (!error)
                {
                    tran.Commit();
                    MessageBox.Show("Uspešan upis serije", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
                if (error) { }
            }
        }
        public void UpdVagoniSerije(int ID, string Serija, string BrojcanaSerija, int BrojOsovina)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdateVagoniSerije";
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            myCommand.Parameters.Add(id);

            SqlParameter serija = new SqlParameter();
            serija.ParameterName = "@Serija";
            serija.SqlDbType = SqlDbType.NVarChar;
            serija.Size = 50;
            serija.Direction = ParameterDirection.Input;
            serija.Value = Serija;
            myCommand.Parameters.Add(serija);

            SqlParameter brojcanaserija = new SqlParameter();
            brojcanaserija.ParameterName = "@BrojcanaSerija";
            brojcanaserija.SqlDbType = SqlDbType.NVarChar;
            brojcanaserija.Size = 20;
            brojcanaserija.Direction = ParameterDirection.Input;
            brojcanaserija.Value = BrojcanaSerija;
            myCommand.Parameters.Add(brojcanaserija);

            SqlParameter brojosovina = new SqlParameter();
            brojosovina.ParameterName = "@BrojOsovina";
            brojosovina.SqlDbType = SqlDbType.Int;
            // brojcanaserija.Size = 20;
            brojosovina.Direction = ParameterDirection.Input;
            brojosovina.Value = BrojOsovina;
            myCommand.Parameters.Add(brojosovina);


            myConnection.Open();
            SqlTransaction myTransaction = myConnection.BeginTransaction();
            myCommand.Transaction = myTransaction;
            bool error = true;

            myCommand.Transaction = myTransaction;
            
            try
            {
                myCommand.ExecuteNonQuery();
                myTransaction.Commit();
                myTransaction = myConnection.BeginTransaction();
                myCommand.Transaction = myTransaction;
            }
            catch (SqlException)
            {
                throw new Exception("Neuspešana promena");
            }
            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Neuspesna promena", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                myConnection.Close();
                if (error) { }
            }
        }
        public void DelVagoniSerije(int ID)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "DeleteVagoniSerije";
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            myCommand.Parameters.Add(id);

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
    }
}
