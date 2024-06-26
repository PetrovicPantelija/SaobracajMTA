﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.Dokumenta
{
    class insertBolovanja
    {
        public void InsBolovanja(DateTime DatumOd, DateTime DatumDo, double Ukupno, int ZaposleniID, string Napomena, int TipBolovanja)
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertBolovanje";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;


            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@DatumOd";
            parameter.SqlDbType = SqlDbType.DateTime;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = DatumOd;
            myCommand.Parameters.Add(parameter);

            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@DatumDo";
            parameter1.SqlDbType = SqlDbType.DateTime;
            parameter1.Direction = ParameterDirection.Input;
            parameter1.Value = DatumDo;
            myCommand.Parameters.Add(parameter1);

            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@Ukupno";
            parameter2.SqlDbType = SqlDbType.Decimal;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = Ukupno;
            myCommand.Parameters.Add(parameter2);

            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@ZaposleniID";
            parameter3.SqlDbType = SqlDbType.Int;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = ZaposleniID;
            myCommand.Parameters.Add(parameter3);

            SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@Napomena";
            parameter4.SqlDbType = SqlDbType.NVarChar;
            parameter4.Size = 500;
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = Napomena;
            myCommand.Parameters.Add(parameter4);


            SqlParameter parameter5 = new SqlParameter();
            parameter5.ParameterName = "@TipBolovanja";
            parameter5.SqlDbType = SqlDbType.Int;
            parameter5.Direction = ParameterDirection.Input;
            parameter5.Value = TipBolovanja;
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

        public void UpdBolovanje(int ID, DateTime DatumOd, DateTime DatumDo, double Ukupno, int ZaposleniID, string Napomena, int TipBolovanja)
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdateBolovanje";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter0 = new SqlParameter();
            parameter0.ParameterName = "@ID";
            parameter0.SqlDbType = SqlDbType.Int;
            parameter0.Direction = ParameterDirection.Input;
            parameter0.Value = ID;
            myCommand.Parameters.Add(parameter0);



            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@DatumOd";
            parameter.SqlDbType = SqlDbType.DateTime;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = DatumOd;
            myCommand.Parameters.Add(parameter);

            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@DatumDo";
            parameter1.SqlDbType = SqlDbType.DateTime;
            parameter1.Direction = ParameterDirection.Input;
            parameter1.Value = DatumDo;
            myCommand.Parameters.Add(parameter1);

            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@Ukupno";
            parameter2.SqlDbType = SqlDbType.Decimal;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = Ukupno;
            myCommand.Parameters.Add(parameter2);

            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@ZaposleniID";
            parameter3.SqlDbType = SqlDbType.Int;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = ZaposleniID;
            myCommand.Parameters.Add(parameter3);

            SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@Napomena";
            parameter4.SqlDbType = SqlDbType.NVarChar;
            parameter4.Size = 500;
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = Napomena;
            myCommand.Parameters.Add(parameter4);

            SqlParameter parameter5 = new SqlParameter();
            parameter5.ParameterName = "@TipBolovanja";
            parameter5.SqlDbType = SqlDbType.Int;
            parameter5.Direction = ParameterDirection.Input;
            parameter5.Value = TipBolovanja;
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
                throw new Exception("Neuspešan upis aktivnosti u Bazu");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Nije uspeo upis aktivnosti", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }


            }
        }

        public void DeleteBolovanje(int ID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "DeleteBolovanje";
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


    }
}
