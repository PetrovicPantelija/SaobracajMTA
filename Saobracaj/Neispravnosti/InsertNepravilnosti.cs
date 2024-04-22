using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.Nepravilnosti
{
    class InsertNepravilnosti
    {
        string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        public void InsSifGrupaNepravilnosti(string Naziv, string Opis)
        {

            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertSifGrupaNepravilnosti";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@Naziv";
            parameter.SqlDbType = SqlDbType.NVarChar;
            parameter.Size = 100;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = Naziv;
            cmd.Parameters.Add(parameter);

            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@Opis";
            parameter2.SqlDbType = SqlDbType.NVarChar;
            parameter2.Size = 1000;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = Opis;
            cmd.Parameters.Add(parameter2);

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
                throw new Exception("Neuspešan upis tehnologije");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos tehnologije je uspešno završena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }
        public void UpdSifGrupaNepravilnosti(int ID, string Naziv, string Opis)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateSifGrupaNepravilnosti";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@Naziv";
            parameter.SqlDbType = SqlDbType.NVarChar;
            parameter.Size = 100;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = Naziv;
            cmd.Parameters.Add(parameter);

            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@Opis";
            parameter2.SqlDbType = SqlDbType.NVarChar;
            parameter2.Size = 1000;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = Opis;
            cmd.Parameters.Add(parameter2);

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
                throw new Exception("Neuspešan upis tehnologije");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos tehnologije je uspešno završena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }
        public void DelSifGrupaNepravilnosti(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteSifGrupaNepravilnosti";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ID";
            parameter.SqlDbType = SqlDbType.NVarChar;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = ID;
            cmd.Parameters.Add(parameter);

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
                throw new Exception("Neuspešan upis tehnologije");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos tehnologije je uspešno završena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void InsSifNeispravnostPostupak(string Naziv, string Opis)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertSifNeispravnostPostupak";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@Naziv";
            parameter.SqlDbType = SqlDbType.NVarChar;
            parameter.Size = 100;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = Naziv;
            cmd.Parameters.Add(parameter);


            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@Opis";
            parameter2.SqlDbType = SqlDbType.NVarChar;
            parameter2.Size = 1000;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = Opis;
            cmd.Parameters.Add(parameter2);

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
                throw new Exception("Neuspešan upis tehnologije");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos tehnologije je uspešno završena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }
        public void UpdSifNeispravnostPostupak(int ID, string Naziv, string Opis)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateSifNeispravnostPostupak";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@Naziv";
            parameter.SqlDbType = SqlDbType.NVarChar;
            parameter.Size = 100;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = Naziv;
            cmd.Parameters.Add(parameter);


            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@Opis";
            parameter2.SqlDbType = SqlDbType.NVarChar;
            parameter2.Size = 1000;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = Opis;
            cmd.Parameters.Add(parameter2);

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
                throw new Exception("Neuspešan upis tehnologije");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos tehnologije je uspešno završena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }
        public void DelSifNeispravnostPostupak(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteSifNeispravnostPostupak";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ID";
            parameter.SqlDbType = SqlDbType.NVarChar;

            parameter.Direction = ParameterDirection.Input;
            parameter.Value = ID;
            cmd.Parameters.Add(parameter);

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
                throw new Exception("Neuspešan upis tehnologije");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos tehnologije je uspešno završena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void InsSifOpisNeispravnosti(string Naziv, string Opis)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertSifOpisNeispravnosti";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@Naziv";
            parameter.SqlDbType = SqlDbType.NVarChar;
            parameter.Size = 100;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = Naziv;
            cmd.Parameters.Add(parameter);


            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@Opis";
            parameter2.SqlDbType = SqlDbType.NVarChar;
            parameter2.Size = 1000;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = Opis;
            cmd.Parameters.Add(parameter2);

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
                throw new Exception("Neuspešan upis tehnologije");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos tehnologije je uspešno završena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }
        public void UpdSifOpisNeispravnosti(int ID, string Naziv, string Opis)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateSifOpisNeispravnosti";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@Naziv";
            parameter.SqlDbType = SqlDbType.NVarChar;
            parameter.Size = 100;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = Naziv;
            cmd.Parameters.Add(parameter);


            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@Opis";
            parameter2.SqlDbType = SqlDbType.NVarChar;
            parameter2.Size = 1000;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = Opis;
            cmd.Parameters.Add(parameter2);

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
                throw new Exception("Neuspešan upis tehnologije");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos tehnologije je uspešno završena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }
        public void DelSifOpisNeispravnosti(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteSifOpisNeispravnosti";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ID";
            parameter.SqlDbType = SqlDbType.NVarChar;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = ID;
            cmd.Parameters.Add(parameter);

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
                throw new Exception("Neuspešan upis tehnologije");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos tehnologije je uspešno završena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void InsSifRazredNepravilnosti(string Naziv, string Opis)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertSifRazredNepravilnosti";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@Naziv";
            parameter.SqlDbType = SqlDbType.NVarChar;
            parameter.Size = 100;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = Naziv;
            cmd.Parameters.Add(parameter);


            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@Opis";
            parameter2.SqlDbType = SqlDbType.NVarChar;
            parameter2.Size = 1000;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = Opis;
            cmd.Parameters.Add(parameter2);

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
                throw new Exception("Neuspešan upis tehnologije");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos tehnologije je uspešno završena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }
        public void UpdSifRazredNepravilnosti(int ID, string Naziv, string Opis)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateSifRazredNepravilnosti";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@Naziv";
            parameter.SqlDbType = SqlDbType.NVarChar;
            parameter.Size = 100;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = Naziv;
            cmd.Parameters.Add(parameter);


            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@Opis";
            parameter2.SqlDbType = SqlDbType.NVarChar;
            parameter2.Size = 1000;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = Opis;
            cmd.Parameters.Add(parameter2);

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
                throw new Exception("Neuspešan upis tehnologije");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos tehnologije je uspešno završena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }
        public void DelSifRazredNepravilnosti(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteSifRazredNepravilnosti";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ID";
            parameter.SqlDbType = SqlDbType.NVarChar;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = ID;
            cmd.Parameters.Add(parameter);

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
                throw new Exception("Neuspešan upis tehnologije");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos tehnologije je uspešno završena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }
        /////////////////////////////////////////////////////////////////////////////////////
        public void InsSifVrstaNepravilnosti(string Naziv, string Opis)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertSifVrstaNepravilnosti";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@Naziv";
            parameter.SqlDbType = SqlDbType.NVarChar;
            parameter.Size = 100;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = Naziv;
            cmd.Parameters.Add(parameter);


            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@Opis";
            parameter2.SqlDbType = SqlDbType.NVarChar;
            parameter2.Size = 1000;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = Opis;
            cmd.Parameters.Add(parameter2);

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
                throw new Exception("Neuspešan upis tehnologije");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos tehnologije je uspešno završena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }
        public void UpdSifVrstaNepravilnosti(int ID, string Naziv, string Opis)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateSifVrstaNepravilnosti";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@Naziv";
            parameter.SqlDbType = SqlDbType.NVarChar;
            parameter.Size = 100;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = Naziv;
            cmd.Parameters.Add(parameter);


            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@Opis";
            parameter2.SqlDbType = SqlDbType.NVarChar;
            parameter2.Size = 1000;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = Opis;
            cmd.Parameters.Add(parameter2);

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
                throw new Exception("Neuspešan upis tehnologije");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos tehnologije je uspešno završena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }
        public void DelSifVrstaNepravilnosti(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteSifVrstaNepravilnosti";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ID";
            parameter.SqlDbType = SqlDbType.NVarChar;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = ID;
            cmd.Parameters.Add(parameter);

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
                throw new Exception("Neuspešan upis tehnologije");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos tehnologije je uspešno završena", "",
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
