﻿using Saobracaj.Sifarnici;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.Administracija
{
    class InsertStavkePrimopredaje
    {
        public string connect = frmLogovanje.connectionString;
        public void InsStavkePrimopredaje(string Naziv)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertSifStavkePrimopredaje";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramNaziv = new SqlParameter();
            paramNaziv.ParameterName = "@Naziv";
            paramNaziv.SqlDbType = SqlDbType.NChar;
            paramNaziv.Size = 50;
            paramNaziv.Direction = ParameterDirection.Input;
            paramNaziv.Value = Naziv;
            cmd.Parameters.Add(paramNaziv);

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
                throw new Exception("Neuspešan upis" + ex.ToString());
            }
            finally
            {
                if (!error)
                {
                    tran.Commit();
                    MessageBox.Show("Unos grupe uspešan", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
            }
            if (error)
            {

            }
        }
        public void UpdateStavkePrimopredaje(int ID, string Naziv)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateSifStavkePrimopredaje";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramId = new SqlParameter();
            paramId.ParameterName = "@ID";
            paramId.SqlDbType = SqlDbType.Int;
            paramId.Direction = ParameterDirection.Input;
            paramId.Value = ID;
            cmd.Parameters.Add(paramId);

            SqlParameter paramNaziv = new SqlParameter();
            paramNaziv.ParameterName = "@Naziv";
            paramNaziv.SqlDbType = SqlDbType.NChar;
            paramNaziv.Size = 50;
            paramNaziv.Direction = ParameterDirection.Input;
            paramNaziv.Value = Naziv;
            cmd.Parameters.Add(paramNaziv);

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
                throw new Exception("Neuspešan upis" + ex.ToString());
            }
            finally
            {
                if (!error)
                {
                    tran.Commit();
                    MessageBox.Show("Unos grupe uspešan", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
            }
            if (error)
            {

            }
        }
        public void DeleteStavkePrimopredaje(int ID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteSifStavkePrimopredaje";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramId = new SqlParameter();
            paramId.ParameterName = "@ID";
            paramId.SqlDbType = SqlDbType.Int;
            paramId.Direction = ParameterDirection.Input;
            paramId.Value = ID;
            cmd.Parameters.Add(paramId);

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
                throw new Exception("Neuspešan upis" + ex.ToString());
            }
            finally
            {
                if (!error)
                {
                    tran.Commit();
                    MessageBox.Show("Unos grupe uspešan", "",
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
