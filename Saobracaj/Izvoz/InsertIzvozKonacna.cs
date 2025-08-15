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

        public void UpdejtujPodatkeIzUvoza(int IDIzvozni, int PlanID, int RadniNalogInterniID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "spPrenesiIzUvozaUIzvoz";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter otpremaid = new SqlParameter();
            otpremaid.ParameterName = "@IDIzvozni";
            otpremaid.SqlDbType = SqlDbType.Int;
            otpremaid.Direction = ParameterDirection.Input;
            otpremaid.Value = IDIzvozni;
            cmd.Parameters.Add(otpremaid);

            /*
            SqlParameter planid = new SqlParameter();
            planid.ParameterName = "@PlanID";
            planid.SqlDbType = SqlDbType.Int;
            planid.Direction = ParameterDirection.Input;
            planid.Value = PlanID;
            cmd.Parameters.Add(planid);
            */

            SqlParameter radninaloginterniid = new SqlParameter();
            radninaloginterniid.ParameterName = "@RadniNalogInterniID";
            radninaloginterniid.SqlDbType = SqlDbType.Int;
            radninaloginterniid.Direction = ParameterDirection.Input;
            radninaloginterniid.Value = RadniNalogInterniID;
            cmd.Parameters.Add(radninaloginterniid);

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




        public void DelIzvozKonacnaSve(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteIzvozKonacnaSve";
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
            //Prenesi u Plan Selektovano izvoz prenosi sa Skladista, fali drugi prenos obicno Iz Nerasporedjenih u rasporedjene
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

        public void PrenesiUPlanUtovaraIzvozBrodar(int ID, int IDNadredjena)
        {
            //Prenesi u Plan Selektovano izvoz prenosi sa Skladista, fali drugi prenos obicno Iz Nerasporedjenih u rasporedjene
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "PrenesiUPlanUtovaraSelektovanoIzvozBrodar";
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
        public void DelIzvozUslugaTerminalskih(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteIzvozVrstaManTerminalskih";
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
            //Ovde izgleda iz Kontejner tekuce se biraju prazni kontejneri 
            //PrenesiUPlanUtovaraPrazan
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "PrenesiUPlanUtovaraSelektovanoIzvozTErminal";
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
        public void InsIzvozUslugaDokumenta(int IDUsluga, string Putanja)
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertIzvozUslugaDokument";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@IDUsluga";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = IDUsluga;
            myCommand.Parameters.Add(parameter);


            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@Putanja";
            parameter2.SqlDbType = SqlDbType.NVarChar;
            parameter2.Size = 500;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = Putanja;
            myCommand.Parameters.Add(parameter2);

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
                throw new Exception("Neuspešan upis dokumenta u Bazu");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Nije uspeo upis dokumenta u bazu", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }


        }


        public void UpdateIzvozKonacnaVagon(int ID, string Vagon)
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdateIzvozKonacnaVagon";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = ID;
            myCommand.Parameters.Add(parameter);


            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@Vagon";
            parameter2.SqlDbType = SqlDbType.NVarChar;
            parameter2.Size = 30;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = Vagon;
            myCommand.Parameters.Add(parameter2);

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
                throw new Exception("Neuspešan upis dokumenta u Bazu");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Nije uspeo upis dokumenta u bazu", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }


        }

        public void DelIzvozUslugaDokumenta(int ID)
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "DeleteIzvozUslugaDokumenta";
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
                throw new Exception("Neuspešan upis dokumenta u Bazu");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Nije uspeo upis dokumenta u bazu", "",
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
