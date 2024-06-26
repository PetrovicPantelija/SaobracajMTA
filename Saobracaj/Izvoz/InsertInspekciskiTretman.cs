﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.Izvoz
{
    class InsertInspekciskiTretman
    {
        string connection = Sifarnici.frmLogovanje.connectionString;

        public void InsInspekciskiTretman(string Naziv)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertInspekciskiTretman";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter naziv = new SqlParameter();
            naziv.ParameterName = "@Naziv";
            naziv.SqlDbType = SqlDbType.NVarChar;
            naziv.Size = 50;
            naziv.Direction = ParameterDirection.Input;
            naziv.Value = Naziv;
            cmd.Parameters.Add(naziv);


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
                throw new Exception("Neuspešan upis ");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos uspešno završen", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }
        public void UpdInspekciskiTretman(int ID, string Naziv)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateInspekciskiTretman";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter naziv = new SqlParameter();
            naziv.ParameterName = "@Naziv";
            naziv.SqlDbType = SqlDbType.NVarChar;
            naziv.Size = 50;
            naziv.Direction = ParameterDirection.Input;
            naziv.Value = Naziv;
            cmd.Parameters.Add(naziv);



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
                throw new Exception("Neuspešan upis ");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos uspešno završen", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }

        }
        public void DelInspekciskiTretman(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteInspekciskiTretman";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

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
                throw new Exception("Neuspešan upis ");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos uspešno završen", "",
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
