using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.Izvoz
{

    class InsertDogovorenoIzvoz
    {
        string connection = Sifarnici.frmLogovanje.connectionString;
        public void InsDogovorenoIzvoz(int Partner, DateTime PeriodOd, DateTime PeriodDo, int BrojUgovorenih, int BrojUradjenih, string Napomena, int Zatvoren)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertDogovorenoIzvoz";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter partner = new SqlParameter();
            partner.ParameterName = "@Partner";
            partner.SqlDbType = SqlDbType.Int;
            //  naziv.Size = 50;
            partner.Direction = ParameterDirection.Input;
            partner.Value = Partner;
            cmd.Parameters.Add(partner);

            SqlParameter periodOd = new SqlParameter();
            periodOd.ParameterName = "@PeriodOd";
            periodOd.SqlDbType = SqlDbType.DateTime;
            //  naziv.Size = 50;
            periodOd.Direction = ParameterDirection.Input;
            periodOd.Value = PeriodOd;
            cmd.Parameters.Add(periodOd);

            SqlParameter perioddo = new SqlParameter();
            perioddo.ParameterName = "@PeriodDo";
            perioddo.SqlDbType = SqlDbType.DateTime;
            //  naziv.Size = 50;
            perioddo.Direction = ParameterDirection.Input;
            perioddo.Value = PeriodDo;
            cmd.Parameters.Add(perioddo);

            SqlParameter brojuUgovorenih = new SqlParameter();
            brojuUgovorenih.ParameterName = "@BrojUgovorenih";
            brojuUgovorenih.SqlDbType = SqlDbType.Int;
            brojuUgovorenih.Direction = ParameterDirection.Input;
            brojuUgovorenih.Value = BrojUgovorenih;
            cmd.Parameters.Add(brojuUgovorenih);

            SqlParameter brojUradjenih = new SqlParameter();
            brojUradjenih.ParameterName = "@BrojUradjenih";
            brojUradjenih.SqlDbType = SqlDbType.Int;
            brojUradjenih.Direction = ParameterDirection.Input;
            brojUradjenih.Value = BrojUradjenih;
            cmd.Parameters.Add(brojUradjenih);


            SqlParameter napomena = new SqlParameter();
            napomena.ParameterName = "@Napomena";
            napomena.SqlDbType = SqlDbType.NVarChar;
            napomena.Size = 500;
            napomena.Direction = ParameterDirection.Input;
            napomena.Value = Napomena;
            cmd.Parameters.Add(napomena);

            SqlParameter zatvoren = new SqlParameter();
            zatvoren.ParameterName = "@Zatvoren";
            zatvoren.SqlDbType = SqlDbType.Int;
            zatvoren.Direction = ParameterDirection.Input;
            zatvoren.Value = Zatvoren;
            cmd.Parameters.Add(zatvoren);


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
        public void UpdDogovorenoIzvoz(int ID, int Partner, DateTime PeriodOd, DateTime PeriodDo, int BrojUgovorenih, int BrojUradjenih, string Napomena, int Zatvoren)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateDogovorenoIzvoz";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter partner = new SqlParameter();
            partner.ParameterName = "@Partner";
            partner.SqlDbType = SqlDbType.Int;
            //  naziv.Size = 50;
            partner.Direction = ParameterDirection.Input;
            partner.Value = Partner;
            cmd.Parameters.Add(partner);

            SqlParameter periodOd = new SqlParameter();
            periodOd.ParameterName = "@PeriodOd";
            periodOd.SqlDbType = SqlDbType.DateTime;
            //  naziv.Size = 50;
            periodOd.Direction = ParameterDirection.Input;
            periodOd.Value = PeriodOd;
            cmd.Parameters.Add(periodOd);

            SqlParameter perioddo = new SqlParameter();
            perioddo.ParameterName = "@PeriodDo";
            perioddo.SqlDbType = SqlDbType.DateTime;
            //  naziv.Size = 50;
            perioddo.Direction = ParameterDirection.Input;
            perioddo.Value = PeriodDo;
            cmd.Parameters.Add(perioddo);

            SqlParameter brojuUgovorenih = new SqlParameter();
            brojuUgovorenih.ParameterName = "@BrojUgovorenih";
            brojuUgovorenih.SqlDbType = SqlDbType.Int;
            brojuUgovorenih.Direction = ParameterDirection.Input;
            brojuUgovorenih.Value = BrojUgovorenih;
            cmd.Parameters.Add(brojuUgovorenih);

            SqlParameter brojUradjenih = new SqlParameter();
            brojUradjenih.ParameterName = "@BrojUradjenih";
            brojUradjenih.SqlDbType = SqlDbType.Int;
            brojUradjenih.Direction = ParameterDirection.Input;
            brojUradjenih.Value = BrojUradjenih;
            cmd.Parameters.Add(brojUradjenih);


            SqlParameter napomena = new SqlParameter();
            napomena.ParameterName = "@Napomena";
            napomena.SqlDbType = SqlDbType.NVarChar;
            napomena.Size = 500;
            napomena.Direction = ParameterDirection.Input;
            napomena.Value = Napomena;
            cmd.Parameters.Add(napomena);

            SqlParameter zatvoren = new SqlParameter();
            zatvoren.ParameterName = "@Zatvoren";
            zatvoren.SqlDbType = SqlDbType.Int;
            zatvoren.Direction = ParameterDirection.Input;
            zatvoren.Value = Zatvoren;
            cmd.Parameters.Add(zatvoren);



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
        public void DelDogovorenoIzvoz(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteDogovorenoIzvoz";
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
