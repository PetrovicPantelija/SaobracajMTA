using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Drumski
{
    class InsertStatus
    {
        public int InsStatusVozila(string Naziv, bool StatusZaCerade, bool StatusZaPlatforme, bool StatusVangabaritni)
        {
            int IDPom = 0;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertStatusVozila";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter name = new SqlParameter();
            name.ParameterName = "@Naziv";
            name.SqlDbType = SqlDbType.Char;
            name.Size = 35;
            name.Direction = ParameterDirection.Input;
            name.Value = Naziv;
            myCommand.Parameters.Add(name);

            // StatusZaCerade parametar
            SqlParameter statusCerade = new SqlParameter("@StatusZaCerade", SqlDbType.Bit);
            statusCerade.Direction = ParameterDirection.Input;
            statusCerade.Value = StatusZaCerade;
            myCommand.Parameters.Add(statusCerade);

            // StatusZaPlatforme parametar
            SqlParameter statusPlatforme = new SqlParameter("@StatusZaPlatforme", SqlDbType.Bit);
            statusPlatforme.Direction = ParameterDirection.Input;
            statusPlatforme.Value = StatusZaPlatforme;
            myCommand.Parameters.Add(statusPlatforme);

            // StatusVangabaritni parametar
            SqlParameter statusVangabaritni = new SqlParameter("@StatusVangabaritni", SqlDbType.Bit);
            statusVangabaritni.Direction = ParameterDirection.Input;
            statusVangabaritni.Value = StatusVangabaritni;
            myCommand.Parameters.Add(statusVangabaritni);

            SqlParameter idParam = new SqlParameter("@IDPom", SqlDbType.Int);
            idParam.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(idParam);


            myConnection.Open();
            SqlTransaction myTransaction = myConnection.BeginTransaction();
            myCommand.Transaction = myTransaction;
            bool error = true;
            try
            {
                myCommand.ExecuteNonQuery();
                myTransaction.Commit();
                IDPom = (int)myCommand.Parameters["@IDPom"].Value;
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
            return IDPom;
        }

        public void UpdStatusVozila(int ID, string Naziv, bool StatusZaCerade, bool StatusZaPlatforme, bool StatusVangabaritni)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdateStatusVozila";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            myCommand.Parameters.Add(id);

            SqlParameter name = new SqlParameter();
            name.ParameterName = "@Naziv";
            name.SqlDbType = SqlDbType.Char;
            name.Size = 35;
            name.Direction = ParameterDirection.Input;
            name.Value = Naziv;
            myCommand.Parameters.Add(name);

            // StatusZaCerade parametar
            SqlParameter statusCerade = new SqlParameter("@StatusZaCerade", SqlDbType.Bit);
            statusCerade.Direction = ParameterDirection.Input;
            statusCerade.Value = StatusZaCerade;
            myCommand.Parameters.Add(statusCerade);

            // StatusZaPlatforme parametar
            SqlParameter statusPlatforme = new SqlParameter("@StatusZaPlatforme", SqlDbType.Bit);
            statusPlatforme.Direction = ParameterDirection.Input;
            statusPlatforme.Value = StatusZaPlatforme;
            myCommand.Parameters.Add(statusPlatforme);

            // StatusVangabaritni parametar
            SqlParameter statusVangabaritni = new SqlParameter("@StatusVangabaritni", SqlDbType.Bit);
            statusVangabaritni.Direction = ParameterDirection.Input;
            statusVangabaritni.Value = StatusVangabaritni;
            myCommand.Parameters.Add(statusVangabaritni);

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

        public void DelStatusUsluga(int ID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "DeleteStatusVozila";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

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
