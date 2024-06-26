﻿using Saobracaj.Sifarnici;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.Dokumenta
{
    class InsertPropratnica
    {
        string connect = frmLogovanje.connectionString;
        public void InsPropratnica(int IdNajave, string Napomena, int Zaduzen, int ZaduzenaFirma)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertPropratnica";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramIdNajave = new SqlParameter();
            paramIdNajave.ParameterName = "@IdNajave";
            paramIdNajave.SqlDbType = SqlDbType.Int;
            paramIdNajave.Direction = ParameterDirection.Input;
            paramIdNajave.Value = IdNajave;
            cmd.Parameters.Add(paramIdNajave);

            SqlParameter paramNapomena = new SqlParameter();
            paramNapomena.ParameterName = "@Napomena";
            paramNapomena.SqlDbType = SqlDbType.NVarChar;
            paramNapomena.Size = 150;
            paramNapomena.Direction = ParameterDirection.Input;
            paramNapomena.Value = Napomena;
            cmd.Parameters.Add(paramNapomena);

            SqlParameter zaduzen = new SqlParameter();
            zaduzen.ParameterName = "@Zaduzen";
            zaduzen.SqlDbType = SqlDbType.Int;
            zaduzen.Direction = ParameterDirection.Input;
            zaduzen.Value = Zaduzen;
            cmd.Parameters.Add(zaduzen);

            SqlParameter zaduzenaF = new SqlParameter();
            zaduzenaF.ParameterName = "@ZaduzenaFirma";
            zaduzenaF.SqlDbType = SqlDbType.Int;
            zaduzenaF.Direction = ParameterDirection.Input;
            zaduzenaF.Value = ZaduzenaFirma;
            cmd.Parameters.Add(zaduzenaF);

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
                throw new Exception("Neuspešan upis propratnice u Bazu");
            }
            finally
            {
                if (!error)
                {
                    tran.Commit();
                    MessageBox.Show("Nije uspeo upis propratnice", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
                if (error)
                {

                }
            }
        }
        public void UpdPropratnica(int ID, int IdNajave, string Napomena, int Zaduzen, int ZaduzenaFirma)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UpdatePropratnica";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramID = new SqlParameter();
            paramID.ParameterName = "@ID";
            paramID.SqlDbType = SqlDbType.Int;
            paramID.Direction = ParameterDirection.Input;
            paramID.Value = ID;
            cmd.Parameters.Add(paramID);

            SqlParameter paramIdNajave = new SqlParameter();
            paramIdNajave.ParameterName = "@IdNajave";
            paramIdNajave.SqlDbType = SqlDbType.Int;
            paramIdNajave.Direction = ParameterDirection.Input;
            paramIdNajave.Value = IdNajave;
            cmd.Parameters.Add(paramIdNajave);

            SqlParameter paramNapomena = new SqlParameter();
            paramNapomena.ParameterName = "@Napomena";
            paramNapomena.SqlDbType = SqlDbType.NVarChar;
            paramNapomena.Size = 150;
            paramNapomena.Direction = ParameterDirection.Input;
            paramNapomena.Value = Napomena;
            cmd.Parameters.Add(paramNapomena);

            SqlParameter zaduzen = new SqlParameter();
            zaduzen.ParameterName = "@Zaduzen";
            zaduzen.SqlDbType = SqlDbType.Int;
            zaduzen.Direction = ParameterDirection.Input;
            zaduzen.Value = Zaduzen;
            cmd.Parameters.Add(zaduzen);

            SqlParameter zaduzenaF = new SqlParameter();
            zaduzenaF.ParameterName = "@ZaduzenaFirma";
            zaduzenaF.SqlDbType = SqlDbType.Int;
            zaduzenaF.Direction = ParameterDirection.Input;
            zaduzenaF.Value = ZaduzenaFirma;
            cmd.Parameters.Add(zaduzenaF);

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
                throw new Exception("Neuspešana poromena Propratnice");
            }
            finally
            {
                if (!error)
                {
                    tran.Commit();
                    MessageBox.Show("Nije uspela promena propratnice", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
                if (error)
                {

                }
            }
        }
        public void DeletePropratnica(int ID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeletePropratnica";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramID = new SqlParameter();
            paramID.ParameterName = "@ID";
            paramID.SqlDbType = SqlDbType.Int;
            paramID.Direction = ParameterDirection.Input;
            paramID.Value = ID;
            cmd.Parameters.Add(paramID);
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
                throw new Exception("Brisanje nije uspelo");
            }
            finally
            {
                if (!error)
                {
                    tran.Commit();
                    MessageBox.Show("Brusanje propratnice uspešno završeno", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
                if (error)
                {

                }
            }
        }
    }
}
