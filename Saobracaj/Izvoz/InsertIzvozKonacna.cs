using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.Izvoz
{

    class InsertIzvozKonacna
    {
        string connection = Sifarnici.frmLogovanje.connectionString;


        public void PrenesiIzPlanUtovaraUPlanUtovara(int ID, int PlanIz, int PlanU)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "PrenesiIzPlanIzvozaUPlanIzvoza";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);


            SqlParameter idnadredjena = new SqlParameter();
            idnadredjena.ParameterName = "@PlanIz";
            idnadredjena.SqlDbType = SqlDbType.Int;
            idnadredjena.Direction = ParameterDirection.Input;
            idnadredjena.Value = PlanIz;
            cmd.Parameters.Add(idnadredjena);

            SqlParameter idnadredjena2 = new SqlParameter();
            idnadredjena2.ParameterName = "@PlanU";
            idnadredjena2.SqlDbType = SqlDbType.Int;
            idnadredjena2.Direction = ParameterDirection.Input;
            idnadredjena2.Value = PlanU;
            cmd.Parameters.Add(idnadredjena2);

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

        public void PrenesiUPlanUtovaraIzvoz(int ID, int IDNadredjena)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "PrenesiUPlanUtovaraSelektovanoIzvoz";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);


            SqlParameter idnadredjena = new SqlParameter();
            idnadredjena.ParameterName = "@IDNadredjeni";
            idnadredjena.SqlDbType = SqlDbType.Int;
            idnadredjena.Direction = ParameterDirection.Input;
            idnadredjena.Value = IDNadredjena;
            cmd.Parameters.Add(idnadredjena);

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


        public void OdrediKontejnerTerminal(int ID, string Kontejner, int TIzvoza)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "OdrediKontejnerTerminal";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter kontejner = new SqlParameter();
            kontejner.ParameterName = "@Kontejner";
            kontejner.SqlDbType = SqlDbType.NVarChar;
            kontejner.Size = 20;
            kontejner.Direction = ParameterDirection.Input;
            kontejner.Value = Kontejner;
            cmd.Parameters.Add(kontejner);



            SqlParameter tizvoza = new SqlParameter();
            tizvoza.ParameterName = "@TIzvoza";
            tizvoza.SqlDbType = SqlDbType.Int;
            tizvoza.Direction = ParameterDirection.Input;
            tizvoza.Value = TIzvoza;
            cmd.Parameters.Add(tizvoza);

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


        public void PrenesiIzPlanUtovaraUOtpremaVoz(int OtpremaID, int PlanID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "spPrenesiIzPlanUtovaraUOtpremaVoz";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter otpremaid = new SqlParameter();
            otpremaid.ParameterName = "@OtpremaID";
            otpremaid.SqlDbType = SqlDbType.Int;
            otpremaid.Direction = ParameterDirection.Input;
            otpremaid.Value = OtpremaID;
            cmd.Parameters.Add(otpremaid);


            SqlParameter planid = new SqlParameter();
            planid.ParameterName = "@PlanID";
            planid.SqlDbType = SqlDbType.Int;
            planid.Direction = ParameterDirection.Input;
            planid.Value = PlanID;
            cmd.Parameters.Add(planid);

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

        public void PrenesiKontejnerUOtpremuKamionomUvoz(int KontejnerID, int NalogID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "spPrenesiKontejnerUOtpremuKamionom";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter otpremaid = new SqlParameter();
            otpremaid.ParameterName = "@KontejnerID";
            otpremaid.SqlDbType = SqlDbType.Int;
            otpremaid.Direction = ParameterDirection.Input;
            otpremaid.Value = KontejnerID;
            cmd.Parameters.Add(otpremaid);

            SqlParameter nalogid = new SqlParameter();
            nalogid.ParameterName = "@NalogID";
            nalogid.SqlDbType = SqlDbType.Int;
            nalogid.Direction = ParameterDirection.Input;
            nalogid.Value = NalogID;
            cmd.Parameters.Add(nalogid);

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

        public void PrenesiKontejnerUOtpremuKamionomIzvoz(int OtpremnicaID, int KontejnerID, int NalogID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "spPrenesiKontejnerUOtpremuKamionomIzvoz";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter otpremnicaid = new SqlParameter();
            otpremnicaid.ParameterName = "@OtpremnicaID";
            otpremnicaid.SqlDbType = SqlDbType.Int;
            otpremnicaid.Direction = ParameterDirection.Input;
            otpremnicaid.Value = OtpremnicaID;
            cmd.Parameters.Add(otpremnicaid);


            SqlParameter otpremaid = new SqlParameter();
            otpremaid.ParameterName = "@KontejnerID";
            otpremaid.SqlDbType = SqlDbType.Int;
            otpremaid.Direction = ParameterDirection.Input;
            otpremaid.Value = KontejnerID;
            cmd.Parameters.Add(otpremaid);

            SqlParameter nalogid = new SqlParameter();
            nalogid.ParameterName = "@NalogID";
            nalogid.SqlDbType = SqlDbType.Int;
            nalogid.Direction = ParameterDirection.Input;
            nalogid.Value = NalogID;
            cmd.Parameters.Add(nalogid);

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


        public void VratiUNerasporedjene(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "VratiKontejnerNerasporedjenoSelektovanoIzvoz";
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

        public void InsIzvozNapomenePozicioniranja(int IDNadredjena, int IDNapomene, string Napomena)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertIzvozNapomenePozicioniranja";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter idnadredjena = new SqlParameter();
            idnadredjena.ParameterName = "@IDNadredjena";
            idnadredjena.SqlDbType = SqlDbType.Int;
            idnadredjena.Direction = ParameterDirection.Input;
            idnadredjena.Value = IDNadredjena;
            cmd.Parameters.Add(idnadredjena);

            SqlParameter idapomene = new SqlParameter();
            idapomene.ParameterName = "@IDNapomene";
            idapomene.SqlDbType = SqlDbType.Int;
            idapomene.Direction = ParameterDirection.Input;
            idapomene.Value = IDNapomene;
            cmd.Parameters.Add(idapomene);

            SqlParameter stapomene = new SqlParameter();
            stapomene.ParameterName = "@stNapomene";
            stapomene.SqlDbType = SqlDbType.NVarChar;
            stapomene.Size = 100;
            stapomene.Direction = ParameterDirection.Input;
            stapomene.Value = Napomena;
            cmd.Parameters.Add(stapomene);

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

        public void InsIzvozKonacnaNapomenePozicioniranja(int IDNadredjena, int IDNapomene)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertIzvozKonacnaNapomenePozicioniranja";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter idnadredjena = new SqlParameter();
            idnadredjena.ParameterName = "@IDNadredjena";
            idnadredjena.SqlDbType = SqlDbType.Int;
            idnadredjena.Direction = ParameterDirection.Input;
            idnadredjena.Value = IDNadredjena;
            cmd.Parameters.Add(idnadredjena);

            SqlParameter idapomene = new SqlParameter();
            idapomene.ParameterName = "@IDNapomene";
            idapomene.SqlDbType = SqlDbType.Int;
            idapomene.Direction = ParameterDirection.Input;
            idapomene.Value = IDNapomene;
            cmd.Parameters.Add(idapomene);

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

        public void DelIzvozKonacnaNapomenePozicioniranja(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteIzvozKonacnaNapomenePozicioniranja";
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

        public void DelIzvozNapomenePozicioniranja(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteIzvozNapomenePozicioniranja";
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

        public void PrenesiUPlanUtovaraPrazan(string Kontejner, int PlanID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "PrenesiUPlanUtovaraPrazan";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter kontejner = new SqlParameter();
            kontejner.ParameterName = "@Kontejner";
            kontejner.SqlDbType = SqlDbType.NVarChar;
            kontejner.Size = 20;
            kontejner.Direction = ParameterDirection.Input;
            kontejner.Value = Kontejner;
            cmd.Parameters.Add(kontejner);


            SqlParameter planid = new SqlParameter();
            planid.ParameterName = "@PlanID";
            planid.SqlDbType = SqlDbType.Int;
            planid.Direction = ParameterDirection.Input;
            planid.Value = PlanID;
            cmd.Parameters.Add(planid);

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
