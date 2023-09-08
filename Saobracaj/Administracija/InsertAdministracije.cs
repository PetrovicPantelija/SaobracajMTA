using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.Administracija
{
    internal class InsertAdministracije
    {
        public string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

        public void UpdateNull()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdateNullVrednosti";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

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

        public void InsertPravoGrupeNaForme(int IDGrupe, int IDForme, int Upis, int Izmena, int Brisanje)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertPravoGrupeNaForme";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramIDGrupe = new SqlParameter();
            paramIDGrupe.ParameterName = "@IDGrupe";
            paramIDGrupe.SqlDbType = SqlDbType.Int;
            paramIDGrupe.Direction = ParameterDirection.Input;
            paramIDGrupe.Value = IDGrupe;
            cmd.Parameters.Add(paramIDGrupe);

            SqlParameter paramIDForme = new SqlParameter();
            paramIDForme.ParameterName = "@IDForme";
            paramIDForme.SqlDbType = SqlDbType.Int;
            paramIDForme.Direction = ParameterDirection.Input;
            paramIDForme.Value = IDForme;
            cmd.Parameters.Add(paramIDForme);

            SqlParameter paramUpis = new SqlParameter();
            paramUpis.ParameterName = "@Upis";
            paramUpis.SqlDbType = SqlDbType.Int;
            paramUpis.Direction = ParameterDirection.Input;
            paramUpis.Value = Upis;
            cmd.Parameters.Add(paramUpis);

            SqlParameter paramIzmena = new SqlParameter();
            paramIzmena.ParameterName = "@Izmena";
            paramIzmena.SqlDbType = SqlDbType.Int;
            paramIzmena.Direction = ParameterDirection.Input;
            paramIzmena.Value = Izmena;
            cmd.Parameters.Add(paramIzmena);

            SqlParameter paramBrisanje = new SqlParameter();
            paramBrisanje.ParameterName = "@Brisanje";
            paramBrisanje.SqlDbType = SqlDbType.Int;
            paramBrisanje.Direction = ParameterDirection.Input;
            paramBrisanje.Value = Brisanje;
            cmd.Parameters.Add(paramBrisanje);

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
            catch (SqlException ex)
            {
                throw new Exception("Neuspešan upis");
            }
            finally
            {
                if (!error)
                {
                    tran.Commit();
                    MessageBox.Show("Unos NHM broja je uspešno završena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
            }
            if (error)
            {
            }
        }

        public void DeletePravoGrupeNaForme(int IDGrupe, int IDForme)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeletePravoGrupeNaForme";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramIDGrupe = new SqlParameter();
            paramIDGrupe.ParameterName = "@IDGrupe";
            paramIDGrupe.SqlDbType = SqlDbType.Int;
            paramIDGrupe.Direction = ParameterDirection.Input;
            paramIDGrupe.Value = IDGrupe;
            cmd.Parameters.Add(paramIDGrupe);

            SqlParameter paramIDForme = new SqlParameter();
            paramIDForme.ParameterName = "@IDForme";
            paramIDForme.SqlDbType = SqlDbType.Int;
            paramIDForme.Direction = ParameterDirection.Input;
            paramIDForme.Value = IDForme;
            cmd.Parameters.Add(paramIDForme);

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
            catch (SqlException ex)
            {
                throw new Exception("Neuspešan upis");
            }
            finally
            {
                if (!error)
                {
                    tran.Commit();
                    MessageBox.Show("Unos NHM broja je uspešno završena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
            }
            if (error)
            {
            }
        }
    }
}