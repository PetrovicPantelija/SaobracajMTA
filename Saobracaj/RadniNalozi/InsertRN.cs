using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.RadniNalozi
{
    internal class InsertRN
    {
        public string connect = Sifarnici.frmLogovanje.connectionString;

        public void PotvrdiUradjenRN9(int RN, string Korisnik)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateRN9Uradjen";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter rn = new SqlParameter();
            rn.ParameterName = "@RN";
            rn.SqlDbType = SqlDbType.Int;
            rn.Direction = ParameterDirection.Input;
            rn.Value = RN;
            cmd.Parameters.Add(rn);

            SqlParameter kor = new SqlParameter();
            kor.ParameterName = "@Korisnik";
            kor.SqlDbType = SqlDbType.NVarChar;
            kor.Size = 50;
            kor.Direction = ParameterDirection.Input;
            kor.Value = Korisnik;
            cmd.Parameters.Add(kor);

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
                conn.Close();

                if (error)
                { }
            }
        }

        public void PokreniSkladisninuKontejnera(int NalogID, string Korisnik)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "PokreniSkladisninuKontejnera";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter nalogid = new SqlParameter();
            nalogid.ParameterName = "@NalogID";
            nalogid.SqlDbType = SqlDbType.Int;
            nalogid.Direction = ParameterDirection.Input;
            nalogid.Value = NalogID;
            cmd.Parameters.Add(nalogid);

            SqlParameter kor = new SqlParameter();
            kor.ParameterName = "@Korisnik";
            kor.SqlDbType = SqlDbType.NVarChar;
            kor.Size = 50;
            kor.Direction = ParameterDirection.Input;
            kor.Value = Korisnik;
            cmd.Parameters.Add(kor);

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
                conn.Close();

                if (error)
                { }
            }
        }
        public void ZatvoriSkladisninuKontejnera(int NalogID, string Korisnik)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "ZatvoriSkladisninuKontejnera";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter nalogid = new SqlParameter();
            nalogid.ParameterName = "@NalogID";
            nalogid.SqlDbType = SqlDbType.Int;
            nalogid.Direction = ParameterDirection.Input;
            nalogid.Value = NalogID;
            cmd.Parameters.Add(nalogid);

            SqlParameter kor = new SqlParameter();
            kor.ParameterName = "@Korisnik";
            kor.SqlDbType = SqlDbType.NVarChar;
            kor.Size = 50;
            kor.Direction = ParameterDirection.Input;
            kor.Value = Korisnik;
            cmd.Parameters.Add(kor);

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
                conn.Close();

                if (error)
                { }
            }
        }

        public void PotvrdiUradjenPretovarCirade(int NalogID, string Korisnik)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateRN9PretovarCIRADE";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter nalogid = new SqlParameter();
            nalogid.ParameterName = "@NalogID";
            nalogid.SqlDbType = SqlDbType.Int;
            nalogid.Direction = ParameterDirection.Input;
            nalogid.Value = NalogID;
            cmd.Parameters.Add(nalogid);

            SqlParameter kor = new SqlParameter();
            kor.ParameterName = "@Korisnik";
            kor.SqlDbType = SqlDbType.NVarChar;
            kor.Size = 50;
            kor.Direction = ParameterDirection.Input;
            kor.Value = Korisnik;
            cmd.Parameters.Add(kor);

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
                conn.Close();

                if (error)
                { }
            }
        }

        public void UpdateRN1Skladiste(int Skladiste, int RN)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateRNPrijemVozaSkladiste";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter skladiste = new SqlParameter();
            skladiste.ParameterName = "@Skladiste";
            skladiste.SqlDbType = SqlDbType.Int;
            skladiste.Direction = ParameterDirection.Input;
            skladiste.Value = Skladiste;
            cmd.Parameters.Add(skladiste);

            SqlParameter rn = new SqlParameter();
            rn.ParameterName = "@RN";
            rn.SqlDbType = SqlDbType.Int;
            rn.Direction = ParameterDirection.Input;
            rn.Value = RN;
            cmd.Parameters.Add(rn);

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
                conn.Close();

                if (error)
                { }
            }
        }

        public void UpdateRN4PrivremenoSkladiste(int Skladiste, int RN)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateRN4PrivremenoSkladiste";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter skladiste = new SqlParameter();
            skladiste.ParameterName = "@Skladiste";
            skladiste.SqlDbType = SqlDbType.Int;
            skladiste.Direction = ParameterDirection.Input;
            skladiste.Value = Skladiste;
            cmd.Parameters.Add(skladiste);

            SqlParameter rn = new SqlParameter();
            rn.ParameterName = "@RN";
            rn.SqlDbType = SqlDbType.Int;
            rn.Direction = ParameterDirection.Input;
            rn.Value = RN;
            cmd.Parameters.Add(rn);

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
                conn.Close();

                if (error)
                { }
            }
        }

        public void UpdateRN4SkladisteIzCIRA(int Skladiste, int RN)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateRN4SkladisteIzCira";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter skladiste = new SqlParameter();
            skladiste.ParameterName = "@Skladiste";
            skladiste.SqlDbType = SqlDbType.Int;
            skladiste.Direction = ParameterDirection.Input;
            skladiste.Value = Skladiste;
            cmd.Parameters.Add(skladiste);

            SqlParameter rn = new SqlParameter();
            rn.ParameterName = "@RN";
            rn.SqlDbType = SqlDbType.Int;
            rn.Direction = ParameterDirection.Input;
            rn.Value = RN;
            cmd.Parameters.Add(rn);

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
                conn.Close();

                if (error)
                { }
            }
        }

        public void UpdateRN6Skladiste(int Skladiste, int RN)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateRN6OtpremaSkladiste";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter skladiste = new SqlParameter();
            skladiste.ParameterName = "@Skladiste";
            skladiste.SqlDbType = SqlDbType.Int;
            skladiste.Direction = ParameterDirection.Input;
            skladiste.Value = Skladiste;
            cmd.Parameters.Add(skladiste);

            SqlParameter rn = new SqlParameter();
            rn.ParameterName = "@RN";
            rn.SqlDbType = SqlDbType.Int;
            rn.Direction = ParameterDirection.Input;
            rn.Value = RN;
            cmd.Parameters.Add(rn);

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
                conn.Close();

                if (error)
                { }
            }
        }

        public void PotvrdiUradjenRN6(int RN, string Korisnik)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateRN6Uradjene";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter rn = new SqlParameter();
            rn.ParameterName = "@RN";
            rn.SqlDbType = SqlDbType.Int;
            rn.Direction = ParameterDirection.Input;
            rn.Value = RN;
            cmd.Parameters.Add(rn);

            SqlParameter korisnik = new SqlParameter();
            korisnik.ParameterName = "@Korisnik";
            korisnik.SqlDbType = SqlDbType.NVarChar;
            korisnik.Size = 20;
            korisnik.Direction = ParameterDirection.Input;
            korisnik.Value = Korisnik;
            cmd.Parameters.Add(korisnik);

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
                conn.Close();

                if (error)
                { }
            }
        }

        public void PotvrdiUradjenRN7(int RN, string Korisnik)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateRN7Uradjene";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter rn = new SqlParameter();
            rn.ParameterName = "@RN";
            rn.SqlDbType = SqlDbType.Int;
            rn.Direction = ParameterDirection.Input;
            rn.Value = RN;
            cmd.Parameters.Add(rn);

            SqlParameter korisnik = new SqlParameter();
            korisnik.ParameterName = "@Korisnik";
            korisnik.SqlDbType = SqlDbType.NVarChar;
            korisnik.Size = 20;
            korisnik.Direction = ParameterDirection.Input;
            korisnik.Value = Korisnik;
            cmd.Parameters.Add(korisnik);

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
                conn.Close();

                if (error)
                { }
            }
        }

        public void PotvrdiUradjenRN8(int RN, string Korisnik)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateRN8Uradjene";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter rn = new SqlParameter();
            rn.ParameterName = "@RN";
            rn.SqlDbType = SqlDbType.Int;
            rn.Direction = ParameterDirection.Input;
            rn.Value = RN;
            cmd.Parameters.Add(rn);


            SqlParameter korisnik = new SqlParameter();
            korisnik.ParameterName = "@Korisnik";
            korisnik.SqlDbType = SqlDbType.NVarChar;
            korisnik.Size = 20;
            korisnik.Direction = ParameterDirection.Input;
            korisnik.Value = Korisnik;
            cmd.Parameters.Add(korisnik);

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
                conn.Close();

                if (error)
                { }
            }
        }

        public void PotvrdiUradjenRN3(int RN)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateRN3Uradjene";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter rn = new SqlParameter();
            rn.ParameterName = "@RN";
            rn.SqlDbType = SqlDbType.Int;
            rn.Direction = ParameterDirection.Input;
            rn.Value = RN;
            cmd.Parameters.Add(rn);

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
                conn.Close();

                if (error)
                { }
            }
        }

        public void PotvrdiUradjenRN1(int RN, string Korisnik)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateRN1Uradjene";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter rn = new SqlParameter();
            rn.ParameterName = "@RN";
            rn.SqlDbType = SqlDbType.Int;
            rn.Direction = ParameterDirection.Input;
            rn.Value = RN;
            cmd.Parameters.Add(rn);

            SqlParameter korisnik = new SqlParameter();
            korisnik.ParameterName = "@Korisnik";
            korisnik.SqlDbType = SqlDbType.NVarChar;
            korisnik.Size = 20;
            korisnik.Direction = ParameterDirection.Input;
            korisnik.Value = Korisnik;
            cmd.Parameters.Add(korisnik);

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
                conn.Close();

                if (error)
                { }
            }
        }
        public void ArhivirajKontejner(string BrojKontejnera)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "PrenesiKontejnerTekuceUArhiv";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter brojkontejnera = new SqlParameter();
            brojkontejnera.ParameterName = "@BrojKontejnera";
            brojkontejnera.SqlDbType = SqlDbType.NVarChar;
            brojkontejnera.Size = 50;
            brojkontejnera.Direction = ParameterDirection.Input;
            brojkontejnera.Value = BrojKontejnera;
            cmd.Parameters.Add(brojkontejnera);

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
                conn.Close();

                if (error)
                { }
            }
        }



        public void PotvrdiUradjenRN2(int RN, string Korisnik)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateRN2Uradjene";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter rn = new SqlParameter();
            rn.ParameterName = "@RN";
            rn.SqlDbType = SqlDbType.Int;
            rn.Direction = ParameterDirection.Input;
            rn.Value = RN;
            cmd.Parameters.Add(rn);

            SqlParameter korisnik = new SqlParameter();
            korisnik.ParameterName = "@Korisnik";
            korisnik.SqlDbType = SqlDbType.NVarChar;
            korisnik.Size = 20;
            korisnik.Direction = ParameterDirection.Input;
            korisnik.Value = Korisnik;
            cmd.Parameters.Add(korisnik);

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
                conn.Close();

                if (error)
                { }
            }
        }

        public void PotvrdiUradjenRN1VP(int RN, string Korisnik, string NapomenaVP, string NapomenaPlombe1, string NapomenaPlombe2)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateRN1UradjeneVP";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter rn = new SqlParameter();
            rn.ParameterName = "@RN";
            rn.SqlDbType = SqlDbType.Int;
            rn.Direction = ParameterDirection.Input;
            rn.Value = RN;
            cmd.Parameters.Add(rn);

            SqlParameter korisnik = new SqlParameter();
            korisnik.ParameterName = "@Korisnik";
            korisnik.SqlDbType = SqlDbType.NVarChar;
            korisnik.Size = 20;
            korisnik.Direction = ParameterDirection.Input;
            korisnik.Value = Korisnik;
            cmd.Parameters.Add(korisnik);

            SqlParameter napomenavp = new SqlParameter();
            napomenavp.ParameterName = "@NapomenaVP";
            napomenavp.SqlDbType = SqlDbType.NVarChar;
            napomenavp.Size = 100;
            napomenavp.Direction = ParameterDirection.Input;
            napomenavp.Value = NapomenaVP;
            cmd.Parameters.Add(napomenavp);

            SqlParameter napomenaplombe1 = new SqlParameter();
            napomenaplombe1.ParameterName = "@NapomenaPlombe1";
            napomenaplombe1.SqlDbType = SqlDbType.NVarChar;
            napomenaplombe1.Size = 100;
            napomenaplombe1.Direction = ParameterDirection.Input;
            napomenaplombe1.Value = NapomenaPlombe1;
            cmd.Parameters.Add(napomenaplombe1);

            SqlParameter napomenaplombe2 = new SqlParameter();
            napomenaplombe2.ParameterName = "@NapomenaPlombe2";
            napomenaplombe2.SqlDbType = SqlDbType.NVarChar;
            napomenaplombe2.Size = 100;
            napomenaplombe2.Direction = ParameterDirection.Input;
            napomenaplombe2.Value = NapomenaPlombe2;
            cmd.Parameters.Add(napomenaplombe2);

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
                conn.Close();

                if (error)
                { }
            }
        }

        public void PotvrdiUradjenRN1CIr(int RN, string Korisnik)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateRN1UradjeneCIR";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter rn = new SqlParameter();
            rn.ParameterName = "@RN";
            rn.SqlDbType = SqlDbType.Int;
            rn.Direction = ParameterDirection.Input;
            rn.Value = RN;
            cmd.Parameters.Add(rn);

            SqlParameter korisnik = new SqlParameter();
            korisnik.ParameterName = "@Korisnik";
            korisnik.SqlDbType = SqlDbType.NVarChar;
            korisnik.Size = 20;
            korisnik.Direction = ParameterDirection.Input;
            korisnik.Value = Korisnik;
            cmd.Parameters.Add(korisnik);

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
                conn.Close();

                if (error)
                { }
            }
        }

        public void PotvrdiUradjenRN1S(int RN, string Korisnik)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateRN1UradjeneS";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter rn = new SqlParameter();
            rn.ParameterName = "@RN";
            rn.SqlDbType = SqlDbType.Int;
            rn.Direction = ParameterDirection.Input;
            rn.Value = RN;
            cmd.Parameters.Add(rn);

            SqlParameter korisnik = new SqlParameter();
            korisnik.ParameterName = "@Korisnik";
            korisnik.SqlDbType = SqlDbType.NVarChar;
            korisnik.Size = 20;
            korisnik.Direction = ParameterDirection.Input;
            korisnik.Value = Korisnik;
            cmd.Parameters.Add(korisnik);

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
                conn.Close();

                if (error)
                { }
            }
        }

        public void PotvrdiUradjenRN5S(int RN)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateRN5UradjeneS";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter rn = new SqlParameter();
            rn.ParameterName = "@RN";
            rn.SqlDbType = SqlDbType.Int;
            rn.Direction = ParameterDirection.Input;
            rn.Value = RN;
            cmd.Parameters.Add(rn);

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
                conn.Close();

                if (error)
                { }
            }
        }


        public void PotvrdiUradjenRN4(int RN, string Korisnik)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateRN4Uradjene";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter rn = new SqlParameter();
            rn.ParameterName = "@RN";
            rn.SqlDbType = SqlDbType.Int;
            rn.Direction = ParameterDirection.Input;
            rn.Value = RN;
            cmd.Parameters.Add(rn);

            SqlParameter korisnik = new SqlParameter();
            korisnik.ParameterName = "@Korisnik";
            korisnik.SqlDbType = SqlDbType.NVarChar;
            korisnik.Size = 20;
            korisnik.Direction = ParameterDirection.Input;
            korisnik.Value = Korisnik;
            cmd.Parameters.Add(korisnik);

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
                conn.Close();

                if (error)
                { }
            }
        }

        public void PotvrdiUradjenRN4CIR(int RN, string Korisnik)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateRN4UradjeneCIR";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter rn = new SqlParameter();
            rn.ParameterName = "@RN";
            rn.SqlDbType = SqlDbType.Int;
            rn.Direction = ParameterDirection.Input;
            rn.Value = RN;
            cmd.Parameters.Add(rn);

            SqlParameter korisnik = new SqlParameter();
            korisnik.ParameterName = "@Korisnik";
            korisnik.SqlDbType = SqlDbType.NVarChar;
            korisnik.Size = 20;
            korisnik.Direction = ParameterDirection.Input;
            korisnik.Value = Korisnik;
            cmd.Parameters.Add(korisnik);

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
                conn.Close();

                if (error)
                { }
            }
        }

        public void PotvrdiUradjenRN4Premesten(int RN,string Korisnik)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateRN4UradjenePostavljen";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter rn = new SqlParameter();
            rn.ParameterName = "@RN";
            rn.SqlDbType = SqlDbType.Int;
            rn.Direction = ParameterDirection.Input;
            rn.Value = RN;
            cmd.Parameters.Add(rn);

            SqlParameter korisnik = new SqlParameter();
            korisnik.ParameterName = "@Korisnik";
            korisnik.SqlDbType = SqlDbType.NVarChar;
            korisnik.Size = 20;
            korisnik.Direction = ParameterDirection.Input;
            korisnik.Value = Korisnik;
            cmd.Parameters.Add(korisnik);

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
                conn.Close();

                if (error)
                { }
            }
        }

        public void PotvrdiUradjenRN5(int RN, string Korisnik)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateRN5Uradjene";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter rn = new SqlParameter();
            rn.ParameterName = "@RN";
            rn.SqlDbType = SqlDbType.Int;
            rn.Direction = ParameterDirection.Input;
            rn.Value = RN;
            cmd.Parameters.Add(rn);


            SqlParameter korisnik = new SqlParameter();
            korisnik.ParameterName = "@Korisnik";
            korisnik.SqlDbType = SqlDbType.NVarChar;
            korisnik.Size = 20;
            korisnik.Direction = ParameterDirection.Input;
            korisnik.Value = Korisnik;
            cmd.Parameters.Add(korisnik);

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
                conn.Close();

                if (error)
                { }
            }
        }
       

        public void PotvrdiUradjenRN5CIR(int RN, string Korisnik)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateRN5UradjeneCIR";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter rn = new SqlParameter();
            rn.ParameterName = "@RN";
            rn.SqlDbType = SqlDbType.Int;
            rn.Direction = ParameterDirection.Input;
            rn.Value = RN;
            cmd.Parameters.Add(rn);


            SqlParameter korisnik = new SqlParameter();
            korisnik.ParameterName = "@Korisnik";
            korisnik.SqlDbType = SqlDbType.NVarChar;
            korisnik.Size = 20;
            korisnik.Direction = ParameterDirection.Input;
            korisnik.Value = Korisnik;
            cmd.Parameters.Add(korisnik);

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
                conn.Close();

                if (error)
                { }
            }
        }

        public void UpdateKontejnerIzCira(string BrojKontejnera, string Stanje, string Ostecenje, int Kvalitet, int CIR)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateIZCIRA";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter brojkontejnera = new SqlParameter();
            brojkontejnera.ParameterName = "@BrojKontejnera";
            brojkontejnera.SqlDbType = SqlDbType.NVarChar;
            brojkontejnera.Size = 50;
            brojkontejnera.Direction = ParameterDirection.Input;
            brojkontejnera.Value = BrojKontejnera;
            cmd.Parameters.Add(brojkontejnera);


            SqlParameter stanje = new SqlParameter();
            stanje.ParameterName = "@Stanje";
            stanje.SqlDbType = SqlDbType.NVarChar;
            stanje.Size = 50;
            stanje.Direction = ParameterDirection.Input;
            stanje.Value = Stanje;
            cmd.Parameters.Add(stanje);

            SqlParameter ostecenja = new SqlParameter();
            ostecenja.ParameterName = "@Ostecenja";
            ostecenja.SqlDbType = SqlDbType.NVarChar;
            ostecenja.Size = 50;
            ostecenja.Direction = ParameterDirection.Input;
            ostecenja.Value = Ostecenje;
            cmd.Parameters.Add(ostecenja);

            SqlParameter kvalitet = new SqlParameter();
            kvalitet.ParameterName = "@Kvalitet";
            kvalitet.SqlDbType = SqlDbType.Int;
            kvalitet.Direction = ParameterDirection.Input;
            kvalitet.Value = Kvalitet;
            cmd.Parameters.Add(kvalitet);

            SqlParameter cir = new SqlParameter();
            cir.ParameterName = "@CIR";
            cir.SqlDbType = SqlDbType.Int;
            cir.Direction = ParameterDirection.Input;
            cir.Value = CIR;
            cmd.Parameters.Add(cir);

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
                conn.Close();

                if (error)
                { }
            }
        }

        public void InsRNPPrijemVoza(DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva, string BrojPlombe,
            int Uvoznik, int NazivBrodara, int VrstaRobe, int NaSkladiste, int NaPozicijuSklad, int IdUsluge, string NalogRealizovao, string Napomena)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertRNPrijemVoza";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter dat1 = new SqlParameter();
            dat1.ParameterName = "@DatumRasporeda";
            dat1.SqlDbType = SqlDbType.DateTime;
            dat1.Direction = ParameterDirection.Input;
            dat1.Value = DatumRasporeda;
            cmd.Parameters.Add(dat1);

            SqlParameter br = new SqlParameter();
            br.ParameterName = "@BrojKontejnera";
            br.SqlDbType = SqlDbType.NVarChar;
            br.Size = 50;
            br.Direction = ParameterDirection.Input;
            br.Value = BrojKontejnera;
            cmd.Parameters.Add(br);

            SqlParameter vrsta = new SqlParameter();
            vrsta.ParameterName = "@VrstaKontejnera";
            vrsta.SqlDbType = SqlDbType.Int;
            vrsta.Direction = ParameterDirection.Input;
            vrsta.Value = VrstaKontejnera;
            cmd.Parameters.Add(vrsta);

            SqlParameter nalogIzdao = new SqlParameter();
            nalogIzdao.ParameterName = "@NalogIzdao";
            nalogIzdao.SqlDbType = SqlDbType.NVarChar;
            nalogIzdao.Size = 50;
            nalogIzdao.Direction = ParameterDirection.Input;
            nalogIzdao.Value = NalogIzdao;
            cmd.Parameters.Add(nalogIzdao);

            SqlParameter dat2 = new SqlParameter();
            dat2.ParameterName = "@DatumRealizacije";
            dat2.SqlDbType = SqlDbType.DateTime;
            dat2.Direction = ParameterDirection.Input;
            dat2.Value = DatumRealizacije;
            cmd.Parameters.Add(dat2);

            SqlParameter saSredstva = new SqlParameter();
            saSredstva.ParameterName = "@SaVoznogSredstva";
            saSredstva.SqlDbType = SqlDbType.Int;
            saSredstva.Direction = ParameterDirection.Input;
            saSredstva.Value = SaVoznogSredstva;
            cmd.Parameters.Add(saSredstva);

            SqlParameter plomba = new SqlParameter();
            plomba.ParameterName = "@BrojPlombe";
            plomba.SqlDbType = SqlDbType.NVarChar;
            plomba.Size = 50;
            plomba.Direction = ParameterDirection.Input;
            plomba.Value = BrojPlombe;
            cmd.Parameters.Add(plomba);

            SqlParameter uvoznik = new SqlParameter();
            uvoznik.ParameterName = "@Uvoznik";
            uvoznik.SqlDbType = SqlDbType.Int;
            uvoznik.Direction = ParameterDirection.Input;
            uvoznik.Value = Uvoznik;
            cmd.Parameters.Add(uvoznik);

            SqlParameter brodar = new SqlParameter();
            brodar.ParameterName = "@NazivBrodara";
            brodar.SqlDbType = SqlDbType.Int;
            brodar.Direction = ParameterDirection.Input;
            brodar.Value = NazivBrodara;
            cmd.Parameters.Add(brodar);

            SqlParameter roba = new SqlParameter();
            roba.ParameterName = "@VrstaRobe";
            roba.SqlDbType = SqlDbType.Int;
            roba.Direction = ParameterDirection.Input;
            roba.Value = VrstaRobe;
            cmd.Parameters.Add(roba);

            SqlParameter naSklad = new SqlParameter();
            naSklad.ParameterName = "@NaSkladiste";
            naSklad.SqlDbType = SqlDbType.Int;
            naSklad.Direction = ParameterDirection.Input;
            naSklad.Value = NaSkladiste;
            cmd.Parameters.Add(naSklad);

            SqlParameter naPoz = new SqlParameter();
            naPoz.ParameterName = "@NaPozicijuSklad";
            naPoz.SqlDbType = SqlDbType.Int;
            naPoz.Direction = ParameterDirection.Input;
            naPoz.Value = NaPozicijuSklad;
            cmd.Parameters.Add(naPoz);

            SqlParameter usluga = new SqlParameter();
            usluga.ParameterName = "@IdUsluge";
            usluga.SqlDbType = SqlDbType.Int;
            usluga.Direction = ParameterDirection.Input;
            usluga.Value = IdUsluge;
            cmd.Parameters.Add(usluga);

            SqlParameter real = new SqlParameter();
            real.ParameterName = "@NalogRealizovao";
            real.SqlDbType = SqlDbType.NVarChar;
            real.Size = 50;
            real.Direction = ParameterDirection.Input;
            real.Value = NalogRealizovao;
            cmd.Parameters.Add(real);

            SqlParameter napomena = new SqlParameter();
            napomena.ParameterName = "@Napomena";
            napomena.SqlDbType = SqlDbType.NVarChar;
            napomena.Size = 500;
            napomena.Direction = ParameterDirection.Input;
            napomena.Value = Napomena;
            cmd.Parameters.Add(napomena);

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
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }

        public void InsRNPPrijemVozaCeoVoz(DateTime DatumRasporeda, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva, int NaSkladiste, int NaPozicijuSklad, int IdUsluge, string NalogRealizovao, string Napomena, int PrijemID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertRNPrijemVozaCeoVoz";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter dat1 = new SqlParameter();
            dat1.ParameterName = "@DatumRasporeda";
            dat1.SqlDbType = SqlDbType.DateTime;
            dat1.Direction = ParameterDirection.Input;
            dat1.Value = DatumRasporeda;
            cmd.Parameters.Add(dat1);



            SqlParameter nalogIzdao = new SqlParameter();
            nalogIzdao.ParameterName = "@NalogIzdao";
            nalogIzdao.SqlDbType = SqlDbType.NVarChar;
            nalogIzdao.Size = 50;
            nalogIzdao.Direction = ParameterDirection.Input;
            nalogIzdao.Value = NalogIzdao;
            cmd.Parameters.Add(nalogIzdao);

            SqlParameter dat2 = new SqlParameter();
            dat2.ParameterName = "@DatumRealizacije";
            dat2.SqlDbType = SqlDbType.DateTime;
            dat2.Direction = ParameterDirection.Input;
            dat2.Value = DatumRealizacije;
            cmd.Parameters.Add(dat2);

            SqlParameter saSredstva = new SqlParameter();
            saSredstva.ParameterName = "@SaVoznogSredstva";
            saSredstva.SqlDbType = SqlDbType.Int;
            saSredstva.Direction = ParameterDirection.Input;
            saSredstva.Value = SaVoznogSredstva;
            cmd.Parameters.Add(saSredstva);



            SqlParameter naSklad = new SqlParameter();
            naSklad.ParameterName = "@NaSkladiste";
            naSklad.SqlDbType = SqlDbType.Int;
            naSklad.Direction = ParameterDirection.Input;
            naSklad.Value = NaSkladiste;
            cmd.Parameters.Add(naSklad);

            SqlParameter naPoz = new SqlParameter();
            naPoz.ParameterName = "@NaPozicijuSklad";
            naPoz.SqlDbType = SqlDbType.Int;
            naPoz.Direction = ParameterDirection.Input;
            naPoz.Value = NaPozicijuSklad;
            cmd.Parameters.Add(naPoz);

            SqlParameter usluga = new SqlParameter();
            usluga.ParameterName = "@IdUsluge";
            usluga.SqlDbType = SqlDbType.Int;
            usluga.Direction = ParameterDirection.Input;
            usluga.Value = IdUsluge;
            cmd.Parameters.Add(usluga);

            SqlParameter real = new SqlParameter();
            real.ParameterName = "@NalogRealizovao";
            real.SqlDbType = SqlDbType.NVarChar;
            real.Size = 50;
            real.Direction = ParameterDirection.Input;
            real.Value = NalogRealizovao;
            cmd.Parameters.Add(real);

            SqlParameter napomena = new SqlParameter();
            napomena.ParameterName = "@Napomena";
            napomena.SqlDbType = SqlDbType.NVarChar;
            napomena.Size = 500;
            napomena.Direction = ParameterDirection.Input;
            napomena.Value = Napomena;
            cmd.Parameters.Add(napomena);

            SqlParameter prijemid = new SqlParameter();
            prijemid.ParameterName = "@PrijemID";
            prijemid.SqlDbType = SqlDbType.Int;

            prijemid.Direction = ParameterDirection.Input;
            prijemid.Value = PrijemID;
            cmd.Parameters.Add(prijemid);





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
                throw new Exception("Neuspešan upis  u bazu");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Nije uspeo upis cena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }

        public void InsRNPrijemPlatformeKamIzvoz(DateTime DatumRasporeda, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva, int NaSkladiste, int NaPozicijuSklad, int IdUsluge, string NalogRealizovao, string Napomena, int PrijemID, string Kamion, int NalogID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertRN4PrPlatformeKamIzvoz";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter dat1 = new SqlParameter();
            dat1.ParameterName = "@DatumRasporeda";
            dat1.SqlDbType = SqlDbType.DateTime;
            dat1.Direction = ParameterDirection.Input;
            dat1.Value = DatumRasporeda;
            cmd.Parameters.Add(dat1);



            SqlParameter nalogIzdao = new SqlParameter();
            nalogIzdao.ParameterName = "@NalogIzdao";
            nalogIzdao.SqlDbType = SqlDbType.NVarChar;
            nalogIzdao.Size = 50;
            nalogIzdao.Direction = ParameterDirection.Input;
            nalogIzdao.Value = NalogIzdao;
            cmd.Parameters.Add(nalogIzdao);

            SqlParameter dat2 = new SqlParameter();
            dat2.ParameterName = "@DatumRealizacije";
            dat2.SqlDbType = SqlDbType.DateTime;
            dat2.Direction = ParameterDirection.Input;
            dat2.Value = DatumRealizacije;
            cmd.Parameters.Add(dat2);

            SqlParameter saSredstva = new SqlParameter();
            saSredstva.ParameterName = "@SaVoznogSredstva";
            saSredstva.SqlDbType = SqlDbType.Int;
            saSredstva.Direction = ParameterDirection.Input;
            saSredstva.Value = SaVoznogSredstva;
            cmd.Parameters.Add(saSredstva);



            SqlParameter naSklad = new SqlParameter();
            naSklad.ParameterName = "@NaSkladiste";
            naSklad.SqlDbType = SqlDbType.Int;
            naSklad.Direction = ParameterDirection.Input;
            naSklad.Value = NaSkladiste;
            cmd.Parameters.Add(naSklad);

            SqlParameter naPoz = new SqlParameter();
            naPoz.ParameterName = "@NaPozicijuSklad";
            naPoz.SqlDbType = SqlDbType.Int;
            naPoz.Direction = ParameterDirection.Input;
            naPoz.Value = NaPozicijuSklad;
            cmd.Parameters.Add(naPoz);

            SqlParameter usluga = new SqlParameter();
            usluga.ParameterName = "@IdUsluge";
            usluga.SqlDbType = SqlDbType.Int;
            usluga.Direction = ParameterDirection.Input;
            usluga.Value = IdUsluge;
            cmd.Parameters.Add(usluga);

            SqlParameter real = new SqlParameter();
            real.ParameterName = "@NalogRealizovao";
            real.SqlDbType = SqlDbType.NVarChar;
            real.Size = 50;
            real.Direction = ParameterDirection.Input;
            real.Value = NalogRealizovao;
            cmd.Parameters.Add(real);

            SqlParameter napomena = new SqlParameter();
            napomena.ParameterName = "@Napomena";
            napomena.SqlDbType = SqlDbType.NVarChar;
            napomena.Size = 500;
            napomena.Direction = ParameterDirection.Input;
            napomena.Value = Napomena;
            cmd.Parameters.Add(napomena);

            SqlParameter prijemid = new SqlParameter();
            prijemid.ParameterName = "@PrijemID";
            prijemid.SqlDbType = SqlDbType.Int;

            prijemid.Direction = ParameterDirection.Input;
            prijemid.Value = PrijemID;
            cmd.Parameters.Add(prijemid);

            SqlParameter kamion = new SqlParameter();
            kamion.ParameterName = "@Kamion";
            kamion.SqlDbType = SqlDbType.NVarChar;
            kamion.Size = 50;
            kamion.Direction = ParameterDirection.Input;
            kamion.Value = Kamion;
            cmd.Parameters.Add(kamion);


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
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }

        public void InsRNPrijemPlatformeKamUvoz(DateTime DatumRasporeda, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva, int NaSkladiste, int NaPozicijuSklad, int IdUsluge, string NalogRealizovao, string Napomena, int PrijemID, string Kamion, int NalogID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertRN4PrPlatformeKamUvoz";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter dat1 = new SqlParameter();
            dat1.ParameterName = "@DatumRasporeda";
            dat1.SqlDbType = SqlDbType.DateTime;
            dat1.Direction = ParameterDirection.Input;
            dat1.Value = DatumRasporeda;
            cmd.Parameters.Add(dat1);



            SqlParameter nalogIzdao = new SqlParameter();
            nalogIzdao.ParameterName = "@NalogIzdao";
            nalogIzdao.SqlDbType = SqlDbType.NVarChar;
            nalogIzdao.Size = 50;
            nalogIzdao.Direction = ParameterDirection.Input;
            nalogIzdao.Value = NalogIzdao;
            cmd.Parameters.Add(nalogIzdao);

            SqlParameter dat2 = new SqlParameter();
            dat2.ParameterName = "@DatumRealizacije";
            dat2.SqlDbType = SqlDbType.DateTime;
            dat2.Direction = ParameterDirection.Input;
            dat2.Value = DatumRealizacije;
            cmd.Parameters.Add(dat2);

            SqlParameter saSredstva = new SqlParameter();
            saSredstva.ParameterName = "@SaVoznogSredstva";
            saSredstva.SqlDbType = SqlDbType.Int;
            saSredstva.Direction = ParameterDirection.Input;
            saSredstva.Value = SaVoznogSredstva;
            cmd.Parameters.Add(saSredstva);



            SqlParameter naSklad = new SqlParameter();
            naSklad.ParameterName = "@NaSkladiste";
            naSklad.SqlDbType = SqlDbType.Int;
            naSklad.Direction = ParameterDirection.Input;
            naSklad.Value = NaSkladiste;
            cmd.Parameters.Add(naSklad);

            SqlParameter naPoz = new SqlParameter();
            naPoz.ParameterName = "@NaPozicijuSklad";
            naPoz.SqlDbType = SqlDbType.Int;
            naPoz.Direction = ParameterDirection.Input;
            naPoz.Value = NaPozicijuSklad;
            cmd.Parameters.Add(naPoz);

            SqlParameter usluga = new SqlParameter();
            usluga.ParameterName = "@IdUsluge";
            usluga.SqlDbType = SqlDbType.Int;
            usluga.Direction = ParameterDirection.Input;
            usluga.Value = IdUsluge;
            cmd.Parameters.Add(usluga);

            SqlParameter real = new SqlParameter();
            real.ParameterName = "@NalogRealizovao";
            real.SqlDbType = SqlDbType.NVarChar;
            real.Size = 50;
            real.Direction = ParameterDirection.Input;
            real.Value = NalogRealizovao;
            cmd.Parameters.Add(real);

            SqlParameter napomena = new SqlParameter();
            napomena.ParameterName = "@Napomena";
            napomena.SqlDbType = SqlDbType.NVarChar;
            napomena.Size = 500;
            napomena.Direction = ParameterDirection.Input;
            napomena.Value = Napomena;
            cmd.Parameters.Add(napomena);

            SqlParameter prijemid = new SqlParameter();
            prijemid.ParameterName = "@PrijemID";
            prijemid.SqlDbType = SqlDbType.Int;

            prijemid.Direction = ParameterDirection.Input;
            prijemid.Value = PrijemID;
            cmd.Parameters.Add(prijemid);

            SqlParameter kamion = new SqlParameter();
            kamion.ParameterName = "@Kamion";
            kamion.SqlDbType = SqlDbType.NVarChar;
            kamion.Size = 50;
            kamion.Direction = ParameterDirection.Input;
            kamion.Value = Kamion;
            cmd.Parameters.Add(kamion);

            SqlParameter nalogid = new SqlParameter();
            nalogid.ParameterName = "@NalogID";
            nalogid.SqlDbType = SqlDbType.NVarChar;
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
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }

        public void InsRN5PrijemPlatformeKam(DateTime DatumRasporeda, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva, int NaSkladiste, int NaPozicijuSklad, int IdUsluge, string NalogRealizovao, string Napomena, int PrijemID, string Kamion, int NalogID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertRN5PrPlatFormeKam";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter dat1 = new SqlParameter();
            dat1.ParameterName = "@DatumRasporeda";
            dat1.SqlDbType = SqlDbType.DateTime;
            dat1.Direction = ParameterDirection.Input;
            dat1.Value = DatumRasporeda;
            cmd.Parameters.Add(dat1);



            SqlParameter nalogIzdao = new SqlParameter();
            nalogIzdao.ParameterName = "@NalogIzdao";
            nalogIzdao.SqlDbType = SqlDbType.NVarChar;
            nalogIzdao.Size = 50;
            nalogIzdao.Direction = ParameterDirection.Input;
            nalogIzdao.Value = NalogIzdao;
            cmd.Parameters.Add(nalogIzdao);

            SqlParameter dat2 = new SqlParameter();
            dat2.ParameterName = "@DatumRealizacije";
            dat2.SqlDbType = SqlDbType.DateTime;
            dat2.Direction = ParameterDirection.Input;
            dat2.Value = DatumRealizacije;
            cmd.Parameters.Add(dat2);

            SqlParameter saSredstva = new SqlParameter();
            saSredstva.ParameterName = "@SaVoznogSredstva";
            saSredstva.SqlDbType = SqlDbType.Int;
            saSredstva.Direction = ParameterDirection.Input;
            saSredstva.Value = SaVoznogSredstva;
            cmd.Parameters.Add(saSredstva);



            SqlParameter naSklad = new SqlParameter();
            naSklad.ParameterName = "@NaSkladiste";
            naSklad.SqlDbType = SqlDbType.Int;
            naSklad.Direction = ParameterDirection.Input;
            naSklad.Value = NaSkladiste;
            cmd.Parameters.Add(naSklad);

            SqlParameter naPoz = new SqlParameter();
            naPoz.ParameterName = "@NaPozicijuSklad";
            naPoz.SqlDbType = SqlDbType.Int;
            naPoz.Direction = ParameterDirection.Input;
            naPoz.Value = NaPozicijuSklad;
            cmd.Parameters.Add(naPoz);

            SqlParameter usluga = new SqlParameter();
            usluga.ParameterName = "@IdUsluge";
            usluga.SqlDbType = SqlDbType.Int;
            usluga.Direction = ParameterDirection.Input;
            usluga.Value = IdUsluge;
            cmd.Parameters.Add(usluga);

            SqlParameter real = new SqlParameter();
            real.ParameterName = "@NalogRealizovao";
            real.SqlDbType = SqlDbType.NVarChar;
            real.Size = 50;
            real.Direction = ParameterDirection.Input;
            real.Value = NalogRealizovao;
            cmd.Parameters.Add(real);

            SqlParameter napomena = new SqlParameter();
            napomena.ParameterName = "@Napomena";
            napomena.SqlDbType = SqlDbType.NVarChar;
            napomena.Size = 500;
            napomena.Direction = ParameterDirection.Input;
            napomena.Value = Napomena;
            cmd.Parameters.Add(napomena);

            SqlParameter prijemid = new SqlParameter();
            prijemid.ParameterName = "@PrijemID";
            prijemid.SqlDbType = SqlDbType.Int;

            prijemid.Direction = ParameterDirection.Input;
            prijemid.Value = PrijemID;
            cmd.Parameters.Add(prijemid);

            SqlParameter kamion = new SqlParameter();
            kamion.ParameterName = "@Kamion";
            kamion.SqlDbType = SqlDbType.NVarChar;
            kamion.Size = 50;
            kamion.Direction = ParameterDirection.Input;
            kamion.Value = Kamion;
            cmd.Parameters.Add(kamion);

            SqlParameter nalogid = new SqlParameter();
            nalogid.ParameterName = "@NalogID";
            nalogid.SqlDbType = SqlDbType.NVarChar;
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
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }

        public void InsRN6OtpremaPlatformeKam(DateTime DatumRasporeda, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva, int NaSkladiste, int NaPozicijuSklad, int IdUsluge, string NalogRealizovao, string Napomena, int PrijemID, string Kamion, int NalogID, int Uvoz, int Izvoznik)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertRN6OtPlatFormeKam";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter dat1 = new SqlParameter();
            dat1.ParameterName = "@DatumRasporeda";
            dat1.SqlDbType = SqlDbType.DateTime;
            dat1.Direction = ParameterDirection.Input;
            dat1.Value = DatumRasporeda;
            cmd.Parameters.Add(dat1);

            SqlParameter nalogIzdao = new SqlParameter();
            nalogIzdao.ParameterName = "@NalogIzdao";
            nalogIzdao.SqlDbType = SqlDbType.NVarChar;
            nalogIzdao.Size = 50;
            nalogIzdao.Direction = ParameterDirection.Input;
            nalogIzdao.Value = NalogIzdao;
            cmd.Parameters.Add(nalogIzdao);

            SqlParameter dat2 = new SqlParameter();
            dat2.ParameterName = "@DatumRealizacije";
            dat2.SqlDbType = SqlDbType.DateTime;
            dat2.Direction = ParameterDirection.Input;
            dat2.Value = DatumRealizacije;
            cmd.Parameters.Add(dat2);

            SqlParameter saSredstva = new SqlParameter();
            saSredstva.ParameterName = "@SaVoznogSredstva";
            saSredstva.SqlDbType = SqlDbType.Int;
            saSredstva.Direction = ParameterDirection.Input;
            saSredstva.Value = SaVoznogSredstva;
            cmd.Parameters.Add(saSredstva);



            SqlParameter naSklad = new SqlParameter();
            naSklad.ParameterName = "@NaSkladiste";
            naSklad.SqlDbType = SqlDbType.Int;
            naSklad.Direction = ParameterDirection.Input;
            naSklad.Value = NaSkladiste;
            cmd.Parameters.Add(naSklad);

            SqlParameter naPoz = new SqlParameter();
            naPoz.ParameterName = "@NaPozicijuSklad";
            naPoz.SqlDbType = SqlDbType.Int;
            naPoz.Direction = ParameterDirection.Input;
            naPoz.Value = NaPozicijuSklad;
            cmd.Parameters.Add(naPoz);

            SqlParameter usluga = new SqlParameter();
            usluga.ParameterName = "@IdUsluge";
            usluga.SqlDbType = SqlDbType.Int;
            usluga.Direction = ParameterDirection.Input;
            usluga.Value = IdUsluge;
            cmd.Parameters.Add(usluga);

            SqlParameter real = new SqlParameter();
            real.ParameterName = "@NalogRealizovao";
            real.SqlDbType = SqlDbType.NVarChar;
            real.Size = 50;
            real.Direction = ParameterDirection.Input;
            real.Value = NalogRealizovao;
            cmd.Parameters.Add(real);

            SqlParameter napomena = new SqlParameter();
            napomena.ParameterName = "@Napomena";
            napomena.SqlDbType = SqlDbType.NVarChar;
            napomena.Size = 500;
            napomena.Direction = ParameterDirection.Input;
            napomena.Value = Napomena;
            cmd.Parameters.Add(napomena);

            SqlParameter prijemid = new SqlParameter();
            prijemid.ParameterName = "@OtpremaID";
            prijemid.SqlDbType = SqlDbType.Int;

            prijemid.Direction = ParameterDirection.Input;
            prijemid.Value = PrijemID;
            cmd.Parameters.Add(prijemid);

            SqlParameter kamion = new SqlParameter();
            kamion.ParameterName = "@Kamion";
            kamion.SqlDbType = SqlDbType.NVarChar;
            kamion.Size = 50;
            kamion.Direction = ParameterDirection.Input;
            kamion.Value = Kamion;
            cmd.Parameters.Add(kamion);


            SqlParameter nalogid = new SqlParameter();
            nalogid.ParameterName = "@NalogID";
            nalogid.SqlDbType = SqlDbType.Int;
            nalogid.Direction = ParameterDirection.Input;
            nalogid.Value = NalogID;
            cmd.Parameters.Add(nalogid);


            SqlParameter uvoz = new SqlParameter();
            uvoz.ParameterName = "@Uvoz";
            uvoz.SqlDbType = SqlDbType.Int;
            uvoz.Direction = ParameterDirection.Input;
            uvoz.Value = Uvoz;
            cmd.Parameters.Add(uvoz);



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
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }
        public void InsRN10PregledCeoVoz(DateTime DatumRasporeda, string NalogIzdao, DateTime DatumRealizacije, int SaSkladiste, int SaPozicijuSklad, int NaSkladiste, int NaPozicijuSklad, int IdUsluge, string NalogRealizovao, string Napomena, int PrijemID, string Kamion)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertRN10PregledCeoVoz";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter dat1 = new SqlParameter();
            dat1.ParameterName = "@DatumRasporeda";
            dat1.SqlDbType = SqlDbType.DateTime;
            dat1.Direction = ParameterDirection.Input;
            dat1.Value = DatumRasporeda;
            cmd.Parameters.Add(dat1);



            SqlParameter nalogIzdao = new SqlParameter();
            nalogIzdao.ParameterName = "@NalogIzdao";
            nalogIzdao.SqlDbType = SqlDbType.NVarChar;
            nalogIzdao.Size = 50;
            nalogIzdao.Direction = ParameterDirection.Input;
            nalogIzdao.Value = NalogIzdao;
            cmd.Parameters.Add(nalogIzdao);

            SqlParameter dat2 = new SqlParameter();
            dat2.ParameterName = "@DatumRealizacije";
            dat2.SqlDbType = SqlDbType.DateTime;
            dat2.Direction = ParameterDirection.Input;
            dat2.Value = DatumRealizacije;
            cmd.Parameters.Add(dat2);

            SqlParameter saSklad = new SqlParameter();
            saSklad.ParameterName = "@SaSkladiste";
            saSklad.SqlDbType = SqlDbType.Int;
            saSklad.Direction = ParameterDirection.Input;
            saSklad.Value = SaSkladiste;
            cmd.Parameters.Add(saSklad);

            SqlParameter saPoz = new SqlParameter();
            saPoz.ParameterName = "@SaPozicijuSklad";
            saPoz.SqlDbType = SqlDbType.Int;
            saPoz.Direction = ParameterDirection.Input;
            saPoz.Value = SaPozicijuSklad;
            cmd.Parameters.Add(saPoz);



            SqlParameter naSklad = new SqlParameter();
            naSklad.ParameterName = "@NaSkladiste";
            naSklad.SqlDbType = SqlDbType.Int;
            naSklad.Direction = ParameterDirection.Input;
            naSklad.Value = NaSkladiste;
            cmd.Parameters.Add(naSklad);

            SqlParameter naPoz = new SqlParameter();
            naPoz.ParameterName = "@NaPozicijuSklad";
            naPoz.SqlDbType = SqlDbType.Int;
            naPoz.Direction = ParameterDirection.Input;
            naPoz.Value = NaPozicijuSklad;
            cmd.Parameters.Add(naPoz);

            SqlParameter usluga = new SqlParameter();
            usluga.ParameterName = "@IdUsluge";
            usluga.SqlDbType = SqlDbType.Int;
            usluga.Direction = ParameterDirection.Input;
            usluga.Value = IdUsluge;
            cmd.Parameters.Add(usluga);

            SqlParameter real = new SqlParameter();
            real.ParameterName = "@NalogRealizovao";
            real.SqlDbType = SqlDbType.NVarChar;
            real.Size = 50;
            real.Direction = ParameterDirection.Input;
            real.Value = NalogRealizovao;
            cmd.Parameters.Add(real);

            SqlParameter napomena = new SqlParameter();
            napomena.ParameterName = "@Napomena";
            napomena.SqlDbType = SqlDbType.NVarChar;
            napomena.Size = 500;
            napomena.Direction = ParameterDirection.Input;
            napomena.Value = Napomena;
            cmd.Parameters.Add(napomena);

            SqlParameter prijemid = new SqlParameter();
            prijemid.ParameterName = "@PrijemID";
            prijemid.SqlDbType = SqlDbType.Int;
            prijemid.Direction = ParameterDirection.Input;
            prijemid.Value = PrijemID;
            cmd.Parameters.Add(prijemid);

            SqlParameter kamion = new SqlParameter();
            kamion.ParameterName = "@Kamion";
            kamion.SqlDbType = SqlDbType.NVarChar;
            kamion.Size = 500;
            kamion.Direction = ParameterDirection.Input;
            kamion.Value = Kamion;
            cmd.Parameters.Add(kamion);



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
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }

        public void InsRN12Medjuskladisni(DateTime DatumRasporeda, string NalogIzdao, DateTime DatumRealizacije, int SaSkladiste, int SaPozicijuSklad, int NaSkladiste, int NaPozicijuSklad, int IdUsluge, string NalogRealizovao, string Napomena, string BrojKontejnera, int VrstaKontejnera, int Brodar)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertRN12Medjuskladisni";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter dat1 = new SqlParameter();
            dat1.ParameterName = "@DatumRasporeda";
            dat1.SqlDbType = SqlDbType.DateTime;
            dat1.Direction = ParameterDirection.Input;
            dat1.Value = DatumRasporeda;
            cmd.Parameters.Add(dat1);



            SqlParameter nalogIzdao = new SqlParameter();
            nalogIzdao.ParameterName = "@NalogIzdao";
            nalogIzdao.SqlDbType = SqlDbType.NVarChar;
            nalogIzdao.Size = 50;
            nalogIzdao.Direction = ParameterDirection.Input;
            nalogIzdao.Value = NalogIzdao;
            cmd.Parameters.Add(nalogIzdao);

            SqlParameter dat2 = new SqlParameter();
            dat2.ParameterName = "@DatumRealizacije";
            dat2.SqlDbType = SqlDbType.DateTime;
            dat2.Direction = ParameterDirection.Input;
            dat2.Value = DatumRealizacije;
            cmd.Parameters.Add(dat2);

            SqlParameter saSklad = new SqlParameter();
            saSklad.ParameterName = "@SaSkladiste";
            saSklad.SqlDbType = SqlDbType.Int;
            saSklad.Direction = ParameterDirection.Input;
            saSklad.Value = SaSkladiste;
            cmd.Parameters.Add(saSklad);

            SqlParameter saPoz = new SqlParameter();
            saPoz.ParameterName = "@SaPozicijuSklad";
            saPoz.SqlDbType = SqlDbType.Int;
            saPoz.Direction = ParameterDirection.Input;
            saPoz.Value = SaPozicijuSklad;
            cmd.Parameters.Add(saPoz);



            SqlParameter naSklad = new SqlParameter();
            naSklad.ParameterName = "@NaSkladiste";
            naSklad.SqlDbType = SqlDbType.Int;
            naSklad.Direction = ParameterDirection.Input;
            naSklad.Value = NaSkladiste;
            cmd.Parameters.Add(naSklad);

            SqlParameter naPoz = new SqlParameter();
            naPoz.ParameterName = "@NaPozicijuSklad";
            naPoz.SqlDbType = SqlDbType.Int;
            naPoz.Direction = ParameterDirection.Input;
            naPoz.Value = NaPozicijuSklad;
            cmd.Parameters.Add(naPoz);

            SqlParameter usluga = new SqlParameter();
            usluga.ParameterName = "@IdUsluge";
            usluga.SqlDbType = SqlDbType.Int;
            usluga.Direction = ParameterDirection.Input;
            usluga.Value = IdUsluge;
            cmd.Parameters.Add(usluga);

            SqlParameter real = new SqlParameter();
            real.ParameterName = "@NalogRealizovao";
            real.SqlDbType = SqlDbType.NVarChar;
            real.Size = 50;
            real.Direction = ParameterDirection.Input;
            real.Value = NalogRealizovao;
            cmd.Parameters.Add(real);

            SqlParameter napomena = new SqlParameter();
            napomena.ParameterName = "@Napomena";
            napomena.SqlDbType = SqlDbType.NVarChar;
            napomena.Size = 500;
            napomena.Direction = ParameterDirection.Input;
            napomena.Value = Napomena;
            cmd.Parameters.Add(napomena);

            SqlParameter brojkontejnera = new SqlParameter();
            brojkontejnera.ParameterName = "@BrojKontejnera";
            brojkontejnera.SqlDbType = SqlDbType.NVarChar;
            brojkontejnera.Size = 50;
            brojkontejnera.Direction = ParameterDirection.Input;
            brojkontejnera.Value = BrojKontejnera;
            cmd.Parameters.Add(brojkontejnera);

            SqlParameter vrstakontejnera = new SqlParameter();
            vrstakontejnera.ParameterName = "@VrstaKontejnera";
            vrstakontejnera.SqlDbType = SqlDbType.Int;
            vrstakontejnera.Direction = ParameterDirection.Input;
            vrstakontejnera.Value = VrstaKontejnera;
            cmd.Parameters.Add(vrstakontejnera);

            SqlParameter brodar = new SqlParameter();
            brodar.ParameterName = "@Brodar";
            brodar.SqlDbType = SqlDbType.Int;
            brodar.Direction = ParameterDirection.Input;
            brodar.Value = Brodar;
            cmd.Parameters.Add(brodar);


            // @BrojKontejnera nvarchar(50), @VrstaKontejnera int, @Brodar int


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
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }

        public void InsRN11PregledCeoVoz(DateTime DatumRasporeda, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva, int SaSkladiste, int SaPozicijuSklad, int NaSkladiste, int NaPozicijuSklad, int IdUsluge, string NalogRealizovao, string Napomena, int OtpremaID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertRN11PregledCeoVozOtprema";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter dat1 = new SqlParameter();
            dat1.ParameterName = "@DatumRasporeda";
            dat1.SqlDbType = SqlDbType.DateTime;
            dat1.Direction = ParameterDirection.Input;
            dat1.Value = DatumRasporeda;
            cmd.Parameters.Add(dat1);



            SqlParameter nalogIzdao = new SqlParameter();
            nalogIzdao.ParameterName = "@NalogIzdao";
            nalogIzdao.SqlDbType = SqlDbType.NVarChar;
            nalogIzdao.Size = 50;
            nalogIzdao.Direction = ParameterDirection.Input;
            nalogIzdao.Value = NalogIzdao;
            cmd.Parameters.Add(nalogIzdao);

            SqlParameter dat2 = new SqlParameter();
            dat2.ParameterName = "@DatumRealizacije";
            dat2.SqlDbType = SqlDbType.DateTime;
            dat2.Direction = ParameterDirection.Input;
            dat2.Value = DatumRealizacije;
            cmd.Parameters.Add(dat2);

            SqlParameter saSklad = new SqlParameter();
            saSklad.ParameterName = "@SaVoznogSredstva";
            saSklad.SqlDbType = SqlDbType.Int;
            saSklad.Direction = ParameterDirection.Input;
            saSklad.Value = SaSkladiste;
            cmd.Parameters.Add(saSklad);

            SqlParameter saPoz = new SqlParameter();
            saPoz.ParameterName = "@SaPozicijuSklad";
            saPoz.SqlDbType = SqlDbType.Int;
            saPoz.Direction = ParameterDirection.Input;
            saPoz.Value = SaPozicijuSklad;
            cmd.Parameters.Add(saPoz);



            SqlParameter naSklad = new SqlParameter();
            naSklad.ParameterName = "@NaSkladiste";
            naSklad.SqlDbType = SqlDbType.Int;
            naSklad.Direction = ParameterDirection.Input;
            naSklad.Value = NaSkladiste;
            cmd.Parameters.Add(naSklad);

            SqlParameter naPoz = new SqlParameter();
            naPoz.ParameterName = "@NaPozicijuSklad";
            naPoz.SqlDbType = SqlDbType.Int;
            naPoz.Direction = ParameterDirection.Input;
            naPoz.Value = NaPozicijuSklad;
            cmd.Parameters.Add(naPoz);

            SqlParameter usluga = new SqlParameter();
            usluga.ParameterName = "@IdUsluge";
            usluga.SqlDbType = SqlDbType.Int;
            usluga.Direction = ParameterDirection.Input;
            usluga.Value = IdUsluge;
            cmd.Parameters.Add(usluga);

            SqlParameter real = new SqlParameter();
            real.ParameterName = "@NalogRealizovao";
            real.SqlDbType = SqlDbType.NVarChar;
            real.Size = 50;
            real.Direction = ParameterDirection.Input;
            real.Value = NalogRealizovao;
            cmd.Parameters.Add(real);

            SqlParameter napomena = new SqlParameter();
            napomena.ParameterName = "@Napomena";
            napomena.SqlDbType = SqlDbType.NVarChar;
            napomena.Size = 500;
            napomena.Direction = ParameterDirection.Input;
            napomena.Value = Napomena;
            cmd.Parameters.Add(napomena);

            SqlParameter prijemid = new SqlParameter();
            prijemid.ParameterName = "@OtpremaID";
            prijemid.SqlDbType = SqlDbType.Int;
            prijemid.Direction = ParameterDirection.Input;
            prijemid.Value = OtpremaID;
            cmd.Parameters.Add(prijemid);


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
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }

        public void InsRN7OtpremaPlatformeKam(DateTime DatumRasporeda, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva, int NaSkladiste, int NaPozicijuSklad, int IdUsluge, string NalogRealizovao, string Napomena, int OtpremaID, string Kamion, int NalogID)
        {

            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertRN7OtpremaOtpremaPlatKam";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter dat1 = new SqlParameter();
            dat1.ParameterName = "@DatumRasporeda";
            dat1.SqlDbType = SqlDbType.DateTime;
            dat1.Direction = ParameterDirection.Input;
            dat1.Value = DatumRasporeda;
            cmd.Parameters.Add(dat1);

            SqlParameter nalogIzdao = new SqlParameter();
            nalogIzdao.ParameterName = "@NalogIzdao";
            nalogIzdao.SqlDbType = SqlDbType.NVarChar;
            nalogIzdao.Size = 50;
            nalogIzdao.Direction = ParameterDirection.Input;
            nalogIzdao.Value = NalogIzdao;
            cmd.Parameters.Add(nalogIzdao);

            SqlParameter dat2 = new SqlParameter();
            dat2.ParameterName = "@DatumRealizacije";
            dat2.SqlDbType = SqlDbType.DateTime;
            dat2.Direction = ParameterDirection.Input;
            dat2.Value = DatumRealizacije;
            cmd.Parameters.Add(dat2);

            SqlParameter saSredstva = new SqlParameter();
            saSredstva.ParameterName = "@SaVoznogSredstva";
            saSredstva.SqlDbType = SqlDbType.Int;
            saSredstva.Direction = ParameterDirection.Input;
            saSredstva.Value = SaVoznogSredstva;
            cmd.Parameters.Add(saSredstva);



            SqlParameter naSklad = new SqlParameter();
            naSklad.ParameterName = "@NaSkladiste";
            naSklad.SqlDbType = SqlDbType.Int;
            naSklad.Direction = ParameterDirection.Input;
            naSklad.Value = NaSkladiste;
            cmd.Parameters.Add(naSklad);

            SqlParameter naPoz = new SqlParameter();
            naPoz.ParameterName = "@NaPozicijuSklad";
            naPoz.SqlDbType = SqlDbType.Int;
            naPoz.Direction = ParameterDirection.Input;
            naPoz.Value = NaPozicijuSklad;
            cmd.Parameters.Add(naPoz);

            SqlParameter usluga = new SqlParameter();
            usluga.ParameterName = "@IdUsluge";
            usluga.SqlDbType = SqlDbType.Int;
            usluga.Direction = ParameterDirection.Input;
            usluga.Value = IdUsluge;
            cmd.Parameters.Add(usluga);

            SqlParameter real = new SqlParameter();
            real.ParameterName = "@NalogRealizovao";
            real.SqlDbType = SqlDbType.NVarChar;
            real.Size = 50;
            real.Direction = ParameterDirection.Input;
            real.Value = NalogRealizovao;
            cmd.Parameters.Add(real);

            SqlParameter napomena = new SqlParameter();
            napomena.ParameterName = "@Napomena";
            napomena.SqlDbType = SqlDbType.NVarChar;
            napomena.Size = 500;
            napomena.Direction = ParameterDirection.Input;
            napomena.Value = Napomena;
            cmd.Parameters.Add(napomena);

            SqlParameter prijemid = new SqlParameter();
            prijemid.ParameterName = "@OtpremaID";
            prijemid.SqlDbType = SqlDbType.Int;

            prijemid.Direction = ParameterDirection.Input;
            prijemid.Value = OtpremaID;
            cmd.Parameters.Add(prijemid);

            SqlParameter kamion = new SqlParameter();
            kamion.ParameterName = "@Kamion";
            kamion.SqlDbType = SqlDbType.NVarChar;
            kamion.Size = 50;
            kamion.Direction = ParameterDirection.Input;
            kamion.Value = Kamion;
            cmd.Parameters.Add(kamion);

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
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }

        public void InsRN8OtpremaCiradeKam(DateTime DatumRasporeda, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva, int NaSkladiste, int NaPozicijuSklad, int IdUsluge, string NalogRealizovao, string Napomena, int OtpremaID, string Kamion, int CarinskiPostupak, string NalogID)
        {

            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertRN8OtpremaCiradeKam";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter dat1 = new SqlParameter();
            dat1.ParameterName = "@DatumRasporeda";
            dat1.SqlDbType = SqlDbType.DateTime;
            dat1.Direction = ParameterDirection.Input;
            dat1.Value = DatumRasporeda;
            cmd.Parameters.Add(dat1);

            SqlParameter nalogIzdao = new SqlParameter();
            nalogIzdao.ParameterName = "@NalogIzdao";
            nalogIzdao.SqlDbType = SqlDbType.NVarChar;
            nalogIzdao.Size = 50;
            nalogIzdao.Direction = ParameterDirection.Input;
            nalogIzdao.Value = NalogIzdao;
            cmd.Parameters.Add(nalogIzdao);

            SqlParameter dat2 = new SqlParameter();
            dat2.ParameterName = "@DatumRealizacije";
            dat2.SqlDbType = SqlDbType.DateTime;
            dat2.Direction = ParameterDirection.Input;
            dat2.Value = DatumRealizacije;
            cmd.Parameters.Add(dat2);

            SqlParameter saSredstva = new SqlParameter();
            saSredstva.ParameterName = "@SaVoznogSredstva";
            saSredstva.SqlDbType = SqlDbType.Int;
            saSredstva.Direction = ParameterDirection.Input;
            saSredstva.Value = SaVoznogSredstva;
            cmd.Parameters.Add(saSredstva);



            SqlParameter naSklad = new SqlParameter();
            naSklad.ParameterName = "@NaSkladiste";
            naSklad.SqlDbType = SqlDbType.Int;
            naSklad.Direction = ParameterDirection.Input;
            naSklad.Value = NaSkladiste;
            cmd.Parameters.Add(naSklad);

            SqlParameter naPoz = new SqlParameter();
            naPoz.ParameterName = "@NaPozicijuSklad";
            naPoz.SqlDbType = SqlDbType.Int;
            naPoz.Direction = ParameterDirection.Input;
            naPoz.Value = NaPozicijuSklad;
            cmd.Parameters.Add(naPoz);

            SqlParameter usluga = new SqlParameter();
            usluga.ParameterName = "@IdUsluge";
            usluga.SqlDbType = SqlDbType.Int;
            usluga.Direction = ParameterDirection.Input;
            usluga.Value = IdUsluge;
            cmd.Parameters.Add(usluga);

            SqlParameter real = new SqlParameter();
            real.ParameterName = "@NalogRealizovao";
            real.SqlDbType = SqlDbType.NVarChar;
            real.Size = 50;
            real.Direction = ParameterDirection.Input;
            real.Value = NalogRealizovao;
            cmd.Parameters.Add(real);

            SqlParameter napomena = new SqlParameter();
            napomena.ParameterName = "@Napomena";
            napomena.SqlDbType = SqlDbType.NVarChar;
            napomena.Size = 500;
            napomena.Direction = ParameterDirection.Input;
            napomena.Value = Napomena;
            cmd.Parameters.Add(napomena);

            SqlParameter prijemid = new SqlParameter();
            prijemid.ParameterName = "@OtpremaID";
            prijemid.SqlDbType = SqlDbType.Int;

            prijemid.Direction = ParameterDirection.Input;
            prijemid.Value = OtpremaID;
            cmd.Parameters.Add(prijemid);

            SqlParameter kamion = new SqlParameter();
            kamion.ParameterName = "@Kamion";
            kamion.SqlDbType = SqlDbType.NVarChar;
            kamion.Size = 50;
            kamion.Direction = ParameterDirection.Input;
            kamion.Value = Kamion;
            cmd.Parameters.Add(kamion);


            SqlParameter carinskipostupak = new SqlParameter();
            carinskipostupak.ParameterName = "@CarinskiPostupak";
            carinskipostupak.SqlDbType = SqlDbType.Int;
            carinskipostupak.Direction = ParameterDirection.Input;
            carinskipostupak.Value = CarinskiPostupak;
            cmd.Parameters.Add(carinskipostupak);


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
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }

        public void InsRN9PrijmCiradeKam(DateTime DatumRasporeda, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva, int NaSkladiste, int NaPozicijuSklad, int IdUsluge, string NalogRealizovao, string Napomena, int OtpremaID, string Kamion, int CarinskiPostupak
            , int InspekciskiPregled, int SpedicijaRTC, int Brodar, string BrojPlombe,int NalogID)
        {

            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertRN9PrijemCiradeKam";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter dat1 = new SqlParameter();
            dat1.ParameterName = "@DatumRasporeda";
            dat1.SqlDbType = SqlDbType.DateTime;
            dat1.Direction = ParameterDirection.Input;
            dat1.Value = DatumRasporeda;
            cmd.Parameters.Add(dat1);

            SqlParameter nalogIzdao = new SqlParameter();
            nalogIzdao.ParameterName = "@NalogIzdao";
            nalogIzdao.SqlDbType = SqlDbType.NVarChar;
            nalogIzdao.Size = 50;
            nalogIzdao.Direction = ParameterDirection.Input;
            nalogIzdao.Value = NalogIzdao;
            cmd.Parameters.Add(nalogIzdao);

            SqlParameter dat2 = new SqlParameter();
            dat2.ParameterName = "@DatumRealizacije";
            dat2.SqlDbType = SqlDbType.DateTime;
            dat2.Direction = ParameterDirection.Input;
            dat2.Value = DatumRealizacije;
            cmd.Parameters.Add(dat2);

            SqlParameter saSredstva = new SqlParameter();
            saSredstva.ParameterName = "@SaVoznogSredstva";
            saSredstva.SqlDbType = SqlDbType.Int;
            saSredstva.Direction = ParameterDirection.Input;
            saSredstva.Value = SaVoznogSredstva;
            cmd.Parameters.Add(saSredstva);



            SqlParameter naSklad = new SqlParameter();
            naSklad.ParameterName = "@NaSkladiste";
            naSklad.SqlDbType = SqlDbType.Int;
            naSklad.Direction = ParameterDirection.Input;
            naSklad.Value = NaSkladiste;
            cmd.Parameters.Add(naSklad);

            SqlParameter naPoz = new SqlParameter();
            naPoz.ParameterName = "@NaPozicijuSklad";
            naPoz.SqlDbType = SqlDbType.Int;
            naPoz.Direction = ParameterDirection.Input;
            naPoz.Value = NaPozicijuSklad;
            cmd.Parameters.Add(naPoz);

            SqlParameter usluga = new SqlParameter();
            usluga.ParameterName = "@IdUsluge";
            usluga.SqlDbType = SqlDbType.Int;
            usluga.Direction = ParameterDirection.Input;
            usluga.Value = IdUsluge;
            cmd.Parameters.Add(usluga);

            SqlParameter real = new SqlParameter();
            real.ParameterName = "@NalogRealizovao";
            real.SqlDbType = SqlDbType.NVarChar;
            real.Size = 50;
            real.Direction = ParameterDirection.Input;
            real.Value = NalogRealizovao;
            cmd.Parameters.Add(real);

            SqlParameter napomena = new SqlParameter();
            napomena.ParameterName = "@Napomena";
            napomena.SqlDbType = SqlDbType.NVarChar;
            napomena.Size = 500;
            napomena.Direction = ParameterDirection.Input;
            napomena.Value = Napomena;
            cmd.Parameters.Add(napomena);

            SqlParameter prijemid = new SqlParameter();
            prijemid.ParameterName = "@PrijemID";
            prijemid.SqlDbType = SqlDbType.Int;

            prijemid.Direction = ParameterDirection.Input;
            prijemid.Value = OtpremaID;
            cmd.Parameters.Add(prijemid);

            SqlParameter kamion = new SqlParameter();
            kamion.ParameterName = "@Kamion";
            kamion.SqlDbType = SqlDbType.NVarChar;
            kamion.Size = 50;
            kamion.Direction = ParameterDirection.Input;
            kamion.Value = Kamion;
            cmd.Parameters.Add(kamion);


            SqlParameter carinskipostupak = new SqlParameter();
            carinskipostupak.ParameterName = "@CarinskiPostupak";
            carinskipostupak.SqlDbType = SqlDbType.Int;
            carinskipostupak.Direction = ParameterDirection.Input;
            carinskipostupak.Value = CarinskiPostupak;
            cmd.Parameters.Add(carinskipostupak);

            SqlParameter inspekciskipregled = new SqlParameter();
            inspekciskipregled.ParameterName = "@InspekciskiPregled";
            inspekciskipregled.SqlDbType = SqlDbType.Int;
            inspekciskipregled.Direction = ParameterDirection.Input;
            inspekciskipregled.Value = InspekciskiPregled;
            cmd.Parameters.Add(inspekciskipregled);

            SqlParameter spedicijaRTC = new SqlParameter();
            spedicijaRTC.ParameterName = "@SpedicijaRTC";
            spedicijaRTC.SqlDbType = SqlDbType.Int;
            spedicijaRTC.Direction = ParameterDirection.Input;
            spedicijaRTC.Value = SpedicijaRTC;
            cmd.Parameters.Add(spedicijaRTC);

            SqlParameter brodar = new SqlParameter();
            brodar.ParameterName = "@Brodar";
            brodar.SqlDbType = SqlDbType.Int;
            brodar.Direction = ParameterDirection.Input;
            brodar.Value = Brodar;
            cmd.Parameters.Add(brodar);

            SqlParameter brojplombe = new SqlParameter();
            brojplombe.ParameterName = "@BrojPlombe";
            brojplombe.SqlDbType = SqlDbType.NVarChar;
            brojplombe.Size = 50;
            brojplombe.Direction = ParameterDirection.Input;
            brojplombe.Value = BrojPlombe;
            cmd.Parameters.Add(brojplombe);

            SqlParameter nalogID = new SqlParameter();
            nalogID.ParameterName = "@NalogID";
            nalogID.SqlDbType= SqlDbType.Int;
            nalogID.Direction = ParameterDirection.Input;
            nalogID.Value = NalogID;
            cmd.Parameters.Add(nalogID);

            // , @InspekciskiPregled int, @SpedicijaRTC int, @Brodar int, @BrojPlombe nvarchar(50)

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
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }

        public void InsRNOtpremaVozaCeoVoz(DateTime DatumRasporeda, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva, int NaSkladiste, int NaPozicijuSklad, int IdUsluge, string NalogRealizovao, string Napomena, int OtpremaID)
        {

            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertRNOtpremaVozaCeoVoz";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter dat1 = new SqlParameter();
            dat1.ParameterName = "@DatumRasporeda";
            dat1.SqlDbType = SqlDbType.DateTime;
            dat1.Direction = ParameterDirection.Input;
            dat1.Value = DatumRasporeda;
            cmd.Parameters.Add(dat1);

            SqlParameter nalogIzdao = new SqlParameter();
            nalogIzdao.ParameterName = "@NalogIzdao";
            nalogIzdao.SqlDbType = SqlDbType.NVarChar;
            nalogIzdao.Size = 50;
            nalogIzdao.Direction = ParameterDirection.Input;
            nalogIzdao.Value = NalogIzdao;
            cmd.Parameters.Add(nalogIzdao);

            SqlParameter dat2 = new SqlParameter();
            dat2.ParameterName = "@DatumRealizacije";
            dat2.SqlDbType = SqlDbType.DateTime;
            dat2.Direction = ParameterDirection.Input;
            dat2.Value = DatumRealizacije;
            cmd.Parameters.Add(dat2);

            SqlParameter saSredstva = new SqlParameter();
            saSredstva.ParameterName = "@SaVoznogSredstva";
            saSredstva.SqlDbType = SqlDbType.Int;
            saSredstva.Direction = ParameterDirection.Input;
            saSredstva.Value = SaVoznogSredstva;
            cmd.Parameters.Add(saSredstva);



            SqlParameter naSklad = new SqlParameter();
            naSklad.ParameterName = "@NaSkladiste";
            naSklad.SqlDbType = SqlDbType.Int;
            naSklad.Direction = ParameterDirection.Input;
            naSklad.Value = NaSkladiste;
            cmd.Parameters.Add(naSklad);

            SqlParameter naPoz = new SqlParameter();
            naPoz.ParameterName = "@NaPozicijuSklad";
            naPoz.SqlDbType = SqlDbType.Int;
            naPoz.Direction = ParameterDirection.Input;
            naPoz.Value = NaPozicijuSklad;
            cmd.Parameters.Add(naPoz);

            SqlParameter usluga = new SqlParameter();
            usluga.ParameterName = "@IdUsluge";
            usluga.SqlDbType = SqlDbType.Int;
            usluga.Direction = ParameterDirection.Input;
            usluga.Value = IdUsluge;
            cmd.Parameters.Add(usluga);

            SqlParameter real = new SqlParameter();
            real.ParameterName = "@NalogRealizovao";
            real.SqlDbType = SqlDbType.NVarChar;
            real.Size = 50;
            real.Direction = ParameterDirection.Input;
            real.Value = NalogRealizovao;
            cmd.Parameters.Add(real);

            SqlParameter napomena = new SqlParameter();
            napomena.ParameterName = "@Napomena";
            napomena.SqlDbType = SqlDbType.NVarChar;
            napomena.Size = 500;
            napomena.Direction = ParameterDirection.Input;
            napomena.Value = Napomena;
            cmd.Parameters.Add(napomena);

            SqlParameter prijemid = new SqlParameter();
            prijemid.ParameterName = "@OtpremaID";
            prijemid.SqlDbType = SqlDbType.Int;

            prijemid.Direction = ParameterDirection.Input;
            prijemid.Value = OtpremaID;
            cmd.Parameters.Add(prijemid);



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
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }

        public void InsRNPPrijemVozaCeoVozPretovar(DateTime DatumRasporeda, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva, int NaSkladiste, int NaPozicijuSklad, int IdUsluge, string NalogRealizovao, string Napomena, int PrijemID, int NalogID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertRNPrijemVozaCeoVozPretovar";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter dat1 = new SqlParameter();
            dat1.ParameterName = "@DatumRasporeda";
            dat1.SqlDbType = SqlDbType.DateTime;
            dat1.Direction = ParameterDirection.Input;
            dat1.Value = DatumRasporeda;
            cmd.Parameters.Add(dat1);



            SqlParameter nalogIzdao = new SqlParameter();
            nalogIzdao.ParameterName = "@NalogIzdao";
            nalogIzdao.SqlDbType = SqlDbType.NVarChar;
            nalogIzdao.Size = 50;
            nalogIzdao.Direction = ParameterDirection.Input;
            nalogIzdao.Value = NalogIzdao;
            cmd.Parameters.Add(nalogIzdao);

            SqlParameter dat2 = new SqlParameter();
            dat2.ParameterName = "@DatumRealizacije";
            dat2.SqlDbType = SqlDbType.DateTime;
            dat2.Direction = ParameterDirection.Input;
            dat2.Value = DatumRealizacije;
            cmd.Parameters.Add(dat2);

            SqlParameter saSredstva = new SqlParameter();
            saSredstva.ParameterName = "@SaVoznogSredstva";
            saSredstva.SqlDbType = SqlDbType.Int;
            saSredstva.Direction = ParameterDirection.Input;
            saSredstva.Value = SaVoznogSredstva;
            cmd.Parameters.Add(saSredstva);



            SqlParameter naSklad = new SqlParameter();
            naSklad.ParameterName = "@NaSkladiste";
            naSklad.SqlDbType = SqlDbType.Int;
            naSklad.Direction = ParameterDirection.Input;
            naSklad.Value = NaSkladiste;
            cmd.Parameters.Add(naSklad);

            SqlParameter naPoz = new SqlParameter();
            naPoz.ParameterName = "@NaPozicijuSklad";
            naPoz.SqlDbType = SqlDbType.Int;
            naPoz.Direction = ParameterDirection.Input;
            naPoz.Value = NaPozicijuSklad;
            cmd.Parameters.Add(naPoz);

            SqlParameter usluga = new SqlParameter();
            usluga.ParameterName = "@IdUsluge";
            usluga.SqlDbType = SqlDbType.Int;
            usluga.Direction = ParameterDirection.Input;
            usluga.Value = IdUsluge;
            cmd.Parameters.Add(usluga);

            SqlParameter real = new SqlParameter();
            real.ParameterName = "@NalogRealizovao";
            real.SqlDbType = SqlDbType.NVarChar;
            real.Size = 50;
            real.Direction = ParameterDirection.Input;
            real.Value = NalogRealizovao;
            cmd.Parameters.Add(real);

            SqlParameter napomena = new SqlParameter();
            napomena.ParameterName = "@Napomena";
            napomena.SqlDbType = SqlDbType.NVarChar;
            napomena.Size = 500;
            napomena.Direction = ParameterDirection.Input;
            napomena.Value = Napomena;
            cmd.Parameters.Add(napomena);

            SqlParameter prijemid = new SqlParameter();
            prijemid.ParameterName = "@PrijemID";
            prijemid.SqlDbType = SqlDbType.Int;
            prijemid.Direction = ParameterDirection.Input;
            prijemid.Value = PrijemID;
            cmd.Parameters.Add(prijemid);


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
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }

        public void UpdRNPPrijemVoza(int ID, DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva, string BrojPlombe,
            int Uvoznik, int NazivBrodara, int VrstaRobe, int NaSkladiste, int NaPozicijuSklad, int IdUsluge, string NalogRealizovao, string Napomena)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateRNPrijemVoza";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter dat1 = new SqlParameter();
            dat1.ParameterName = "@DatumRasporeda";
            dat1.SqlDbType = SqlDbType.DateTime;
            dat1.Direction = ParameterDirection.Input;
            dat1.Value = DatumRasporeda;
            cmd.Parameters.Add(dat1);

            SqlParameter br = new SqlParameter();
            br.ParameterName = "@BrojKontejnera";
            br.SqlDbType = SqlDbType.NVarChar;
            br.Size = 50;
            br.Direction = ParameterDirection.Input;
            br.Value = BrojKontejnera;
            cmd.Parameters.Add(br);

            SqlParameter vrsta = new SqlParameter();
            vrsta.ParameterName = "@VrstaKontejnera";
            vrsta.SqlDbType = SqlDbType.Int;
            vrsta.Direction = ParameterDirection.Input;
            vrsta.Value = VrstaKontejnera;
            cmd.Parameters.Add(vrsta);

            SqlParameter nalogIzdao = new SqlParameter();
            nalogIzdao.ParameterName = "@NalogIzdao";
            nalogIzdao.SqlDbType = SqlDbType.NVarChar;
            nalogIzdao.Size = 50;
            nalogIzdao.Direction = ParameterDirection.Input;
            nalogIzdao.Value = NalogIzdao;
            cmd.Parameters.Add(nalogIzdao);

            SqlParameter dat2 = new SqlParameter();
            dat2.ParameterName = "@DatumRealizacije";
            dat2.SqlDbType = SqlDbType.DateTime;
            dat2.Direction = ParameterDirection.Input;
            dat2.Value = DatumRealizacije;
            cmd.Parameters.Add(dat2);

            SqlParameter saSredstva = new SqlParameter();
            saSredstva.ParameterName = "@SaVoznogSredstva";
            saSredstva.SqlDbType = SqlDbType.Int;
            saSredstva.Direction = ParameterDirection.Input;
            saSredstva.Value = SaVoznogSredstva;
            cmd.Parameters.Add(saSredstva);

            SqlParameter plomba = new SqlParameter();
            plomba.ParameterName = "@BrojPlombe";
            plomba.SqlDbType = SqlDbType.NVarChar;
            plomba.Size = 50;
            plomba.Direction = ParameterDirection.Input;
            plomba.Value = BrojPlombe;
            cmd.Parameters.Add(plomba);

            SqlParameter uvoznik = new SqlParameter();
            uvoznik.ParameterName = "@Uvoznik";
            uvoznik.SqlDbType = SqlDbType.Int;
            uvoznik.Direction = ParameterDirection.Input;
            uvoznik.Value = Uvoznik;
            cmd.Parameters.Add(uvoznik);

            SqlParameter brodar = new SqlParameter();
            brodar.ParameterName = "@NazivBrodara";
            brodar.SqlDbType = SqlDbType.Int;
            brodar.Direction = ParameterDirection.Input;
            brodar.Value = NazivBrodara;
            cmd.Parameters.Add(brodar);

            SqlParameter roba = new SqlParameter();
            roba.ParameterName = "@VrstaRobe";
            roba.SqlDbType = SqlDbType.Int;
            roba.Direction = ParameterDirection.Input;
            roba.Value = VrstaRobe;
            cmd.Parameters.Add(roba);

            SqlParameter naSklad = new SqlParameter();
            naSklad.ParameterName = "@NaSkladiste";
            naSklad.SqlDbType = SqlDbType.Int;
            naSklad.Direction = ParameterDirection.Input;
            naSklad.Value = NaSkladiste;
            cmd.Parameters.Add(naSklad);

            SqlParameter naPoz = new SqlParameter();
            naPoz.ParameterName = "@NaPozicijuSklad";
            naPoz.SqlDbType = SqlDbType.Int;
            naPoz.Direction = ParameterDirection.Input;
            naPoz.Value = NaPozicijuSklad;
            cmd.Parameters.Add(naPoz);

            SqlParameter usluga = new SqlParameter();
            usluga.ParameterName = "@IdUsluge";
            usluga.SqlDbType = SqlDbType.Int;
            usluga.Direction = ParameterDirection.Input;
            usluga.Value = IdUsluge;
            cmd.Parameters.Add(usluga);

            SqlParameter real = new SqlParameter();
            real.ParameterName = "@NalogRealizovao";
            real.SqlDbType = SqlDbType.NVarChar;
            real.Size = 50;
            real.Direction = ParameterDirection.Input;
            real.Value = NalogRealizovao;
            cmd.Parameters.Add(real);

            SqlParameter napomena = new SqlParameter();
            napomena.ParameterName = "@Napomena";
            napomena.SqlDbType = SqlDbType.NVarChar;
            napomena.Size = 500;
            napomena.Direction = ParameterDirection.Input;
            napomena.Value = Napomena;
            cmd.Parameters.Add(napomena);

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
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }
        public void DelRNPPrijemVoza(int ID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteRNPrijemVoza";
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
                conn.Close();

                if (error)
                { }
            }
        }
        public void InsRnMedjuskladisni(DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije,
    int NazivBrodara, int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int NaSkladiste, int NaPozicijuSklad, int IdUsluge, string NalogRealizovao, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = "InsertRNMedjuskladisni";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@NaSkladiste", SqlDbType.Int) { Value = NaSkladiste });
                            cmd.Parameters.Add(new SqlParameter("@NaPoziciju", SqlDbType.Int) { Value = NaPozicijuSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter(@"NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });

                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (SqlException)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                conn.Close();
            }
        }

        public void UpdRnMedjuskladisni(int ID, DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije,
            int NazivBrodara, int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int NaSkladiste, int NaPozicijuSklad, int IdUsluge, string NalogRealizovao, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = "UpdateRNMedjuskladisni";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@NaSkladiste", SqlDbType.Int) { Value = NaSkladiste });
                            cmd.Parameters.Add(new SqlParameter("@NaPoziciju", SqlDbType.Int) { Value = NaPozicijuSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter(@"NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });

                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (SqlException)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                conn.Close();
            }
        }

        public void DelRnMedjuskladisni(int ID)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = "DeleteRNMedjuskladisni";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (SqlException)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void InsRnOtpremaCirade(DateTime DatumRasporeda, string BrojKontejnera, int NazivBrodara, string NalogIzdao, DateTime DatumRealizacije, int NaVoznoSredstvo, int CarinskiPostupak,
            int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int IdUsluge, string NalogRealizovao, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "InsertRNOtpremaCirade";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NaVoznoSredstvo", SqlDbType.Int) { Value = NaVoznoSredstvo });
                            cmd.Parameters.Add(new SqlParameter("@CarinskiPostupak", SqlDbType.Int) { Value = CarinskiPostupak });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex)
                    {
                        tran.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MessageBox.Show(ex.ToString());
                    }
                }
                conn.Close();
            }
        }

        public void UpdRnOtpremaCirade(int ID, DateTime DatumRasporeda, string BrojKontejnera, int NazivBrodara, string NalogIzdao, DateTime DatumRealizacije, int NaVoznoSredstvo, int CarinskiPostupak,
            int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int IdUsluge, string NalogRealizovao, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "UpdateRNOtpremaCirade";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NaVoznoSredstvo", SqlDbType.Int) { Value = NaVoznoSredstvo });
                            cmd.Parameters.Add(new SqlParameter("@CarinskiPostupak", SqlDbType.Int) { Value = CarinskiPostupak });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex)
                    {
                        tran.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MessageBox.Show(ex.ToString());
                    }
                }
                conn.Close();
            }
        }

        public void DelRnOtpremaCirade(int ID)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "DeleteRNOtpremaCirade";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void InsRNOtpremaPlatforme(DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int NaVoznoSredstvo,
            int Uvoznik, int CarinskiPostupak, int SpedicijaRTC, int NazivBrodara, int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int IdUsluge, string NalogRealizovao)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "InsertRNOtpremaPlatforme";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NaVoznoSredstvo", SqlDbType.Int) { Value = NaVoznoSredstvo });
                            cmd.Parameters.Add(new SqlParameter("@CarinskiPostupak", SqlDbType.Int) { Value = CarinskiPostupak });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@Uvoznik", SqlDbType.Int) { Value = Uvoznik });
                            cmd.Parameters.Add(new SqlParameter("@SpedicijaRTC", SqlDbType.Int) { Value = SpedicijaRTC });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex)
                    {
                        tran.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MessageBox.Show(ex.ToString());

                    }
                }
                conn.Close();
            }
        }

        public void UpdRNOtpremaPlatforme(int ID, DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int NaVoznoSredstvo,
            int Uvoznik, int CarinskiPostupak, int SpedicijaRTC, int NazivBrodara, int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int IdUsluge, string NalogRealizovao)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "UpdateRNOtpremaPlatforme";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NaVoznoSredstvo", SqlDbType.Int) { Value = NaVoznoSredstvo });
                            cmd.Parameters.Add(new SqlParameter("@CarinskiPostupak", SqlDbType.Int) { Value = CarinskiPostupak });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@Uvoznik", SqlDbType.Int) { Value = Uvoznik });
                            cmd.Parameters.Add(new SqlParameter("@SpedicijaRTC", SqlDbType.Int) { Value = SpedicijaRTC });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void DelRNOtpremaPlatforme(int ID)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "DeleteRNOtpremaPlatforme";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void InsRNOtpremaPlatforme2(DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int NaVoznoSredstvo,
            int NazivBrodara, int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int IdUsluge, string NalogRealizovao, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "InsertRNOtpremaPlatforme2";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NaVoznoSredstvo", SqlDbType.Int) { Value = NaVoznoSredstvo });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void UpdRNOtpremaPlatforme2(int ID, DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int NaVoznoSredstvo,
            int NazivBrodara, int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int IdUsluge, string NalogRealizovao, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "UpdateRNOtpremaPlatforme2";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NaVoznoSredstvo", SqlDbType.Int) { Value = NaVoznoSredstvo });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void DelRNOtpremaPlatforme2(int ID)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "DeleteRNOtpremaPlatforme2";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void InsRNOtpremaVoza(DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int NaVoznoSredstvo,
            string BrojPlombe, string BrojVagona,
            int NazivBrodara, int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int IdUsluge, string NalogRealizovao, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "InsertRNOtpremaVoza";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NaVoznoSredstvo", SqlDbType.Int) { Value = NaVoznoSredstvo });
                            cmd.Parameters.Add(new SqlParameter("@BrojPlombe", SqlDbType.NVarChar, 50) { Value = BrojPlombe });
                            cmd.Parameters.Add(new SqlParameter("@BrojVagona", SqlDbType.NVarChar, 50) { Value = BrojVagona });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void UpdRNOtpremaVoza(int ID, DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int NaVoznoSredstvo,
            string BrojPlombe, string BrojVagona,
            int NazivBrodara, int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int IdUsluge, string NalogRealizovao, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "UpdateRNOtpremaVoza";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NaVoznoSredstvo", SqlDbType.Int) { Value = NaVoznoSredstvo });
                            cmd.Parameters.Add(new SqlParameter("@BrojPlombe", SqlDbType.NVarChar, 50) { Value = BrojPlombe });
                            cmd.Parameters.Add(new SqlParameter("@BrojVagona", SqlDbType.NVarChar, 50) { Value = BrojVagona });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void DelRNOtpremaVoza(int ID)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "DeleteRNOtpremaVoza";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void InsRNPregledIpostavkaKontejnera(DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije,
            int NazivBrodara, int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int NaSkladiste, int NaPoziciju, int IdUsluge, string NalogRealizovao, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "InsertRNPregledIpostavkaKontejnera";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@NaSkladiste", SqlDbType.Int) { Value = NaSkladiste });
                            cmd.Parameters.Add(new SqlParameter("@NaPoziciju", SqlDbType.Int) { Value = NaPoziciju });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void UpdRNPregledIpostavkaKontejnera(int ID, DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije,
            int NazivBrodara, int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int NaSkladiste, int NaPoziciju, int IdUsluge, string NalogRealizovao, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "UpdateRNPregledIpostavkaKontejnera";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@NaSkladiste", SqlDbType.Int) { Value = NaSkladiste });
                            cmd.Parameters.Add(new SqlParameter("@NaPoziciju", SqlDbType.Int) { Value = NaPoziciju });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void DelRNPregledIpostavkaKontejnera(int ID)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "DeleteRNPregledIpostavkaKontejnera";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void InsRNPregledIspraznjenogKontejnera(DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int NazivBrodara,
            int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int NaSkladiste, int NaPoziciju, int IdUsluge, string NalogRealizovao, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "InsertRNPregledIspraznjenogKontejnera";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@NaSkladiste", SqlDbType.Int) { Value = NaSkladiste });
                            cmd.Parameters.Add(new SqlParameter("@NaPoziciju", SqlDbType.Int) { Value = NaPoziciju });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void UpdRNPregledIspraznjenogKontejnera(int ID, DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int NazivBrodara,
            int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int NaSkladiste, int NaPoziciju, int IdUsluge, string NalogRealizovao, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "UpdateRNPregledIspraznjenogKontejnera";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@NaSkladiste", SqlDbType.Int) { Value = NaSkladiste });
                            cmd.Parameters.Add(new SqlParameter("@NaPoziciju", SqlDbType.Int) { Value = NaPoziciju });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void DelRNPregledIspraznjenogKontejnera(int ID)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "DeleteRNPregledIspraznjenogKontejnera";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void InsRNPPrijemCirade(DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva,
            string BrojPlombe, int CarinskiPostupak, int InspekcijskiPregled, int SpedicijaRTC, int NazivBrodara, int VrstaRobe, int SaSkladista, int SaPozicijeSklad,
            int IdUsluge, string NalogRealizovao, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "InsertRNPrijemCirade";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@SaVoznogSredstva", SqlDbType.Int) { Value = SaVoznogSredstva });
                            cmd.Parameters.Add(new SqlParameter("@BrojPlombe", SqlDbType.NVarChar, 50) { Value = BrojPlombe });
                            cmd.Parameters.Add(new SqlParameter("@CarinskiPostupak", SqlDbType.Int) { Value = CarinskiPostupak });
                            cmd.Parameters.Add(new SqlParameter("@InspekcijskiPregled", SqlDbType.Int) { Value = InspekcijskiPregled });
                            cmd.Parameters.Add(new SqlParameter("@SpedicijaRTC", SqlDbType.Int) { Value = SpedicijaRTC });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void UpdRNPPrijemCirade(int ID, DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva,
            string BrojPlombe, int CarinskiPostupak, int InspekcijskiPregled, int SpedicijaRTC, int NazivBrodara, int VrstaRobe, int SaSkladista, int SaPozicijeSklad,
            int IdUsluge, string NalogRealizovao, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "UpdateRNPrijemCirade";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@SaVoznogSredstva", SqlDbType.Int) { Value = SaVoznogSredstva });
                            cmd.Parameters.Add(new SqlParameter("@BrojPlombe", SqlDbType.NVarChar, 50) { Value = BrojPlombe });
                            cmd.Parameters.Add(new SqlParameter("@CarinskiPostupak", SqlDbType.Int) { Value = CarinskiPostupak });
                            cmd.Parameters.Add(new SqlParameter("@InspekcijskiPregled", SqlDbType.Int) { Value = InspekcijskiPregled });
                            cmd.Parameters.Add(new SqlParameter("@SpedicijaRTC", SqlDbType.Int) { Value = SpedicijaRTC });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void DelRNPPrijemCirade(int ID)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "DeleteRNPrijemCirade";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void InsRNPPrijemPlatforme(DateTime DatumRasporeda, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva, string BrojKontejnera, int VrstaKontejnera,
            string BrojPlombe, int Izvoznik, int NazivBrodara, int CarinskiPostupak, int InspekcijskiPregled, int VrstaRobe, int NaSkladiste, int NaPozicijuSklad, int IdUsluge,
            string NalogRealizovao, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "InsertRNPrijemPlatforme";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@SaVoznogSredstva", SqlDbType.Int) { Value = SaVoznogSredstva });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@BrojPlombe", SqlDbType.NVarChar, 50) { Value = BrojPlombe });
                            cmd.Parameters.Add(new SqlParameter("@Izvoznik", SqlDbType.Int) { Value = Izvoznik });
                            cmd.Parameters.Add(new SqlParameter("@CarinskiPostupak", SqlDbType.Int) { Value = CarinskiPostupak });
                            cmd.Parameters.Add(new SqlParameter("@InspekcijskiPregled", SqlDbType.Int) { Value = InspekcijskiPregled });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@USkladiste", SqlDbType.Int) { Value = NaSkladiste });
                            cmd.Parameters.Add(new SqlParameter("@UPozicijaSklad", SqlDbType.Int) { Value = NaPozicijuSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void UpdRNPPrijemPlatforme(int ID, DateTime DatumRasporeda, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva, string BrojKontejnera, int VrstaKontejnera,
            string BrojPlombe, int Izvoznik, int NazivBrodara, int CarinskiPostupak, int InspekcijskiPregled, int VrstaRobe, int NaSkladiste, int NaPozicijuSklad, int IdUsluge,
            string NalogRealizovao, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "UpdateRNPrijemPlatforme";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@SaVoznogSredstva", SqlDbType.Int) { Value = SaVoznogSredstva });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@BrojPlombe", SqlDbType.NVarChar, 50) { Value = BrojPlombe });
                            cmd.Parameters.Add(new SqlParameter("@Izvoznik", SqlDbType.Int) { Value = Izvoznik });
                            cmd.Parameters.Add(new SqlParameter("@CarinskiPostupak", SqlDbType.Int) { Value = CarinskiPostupak });
                            cmd.Parameters.Add(new SqlParameter("@InspekcijskiPregled", SqlDbType.Int) { Value = InspekcijskiPregled });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@USkladiste", SqlDbType.Int) { Value = NaSkladiste });
                            cmd.Parameters.Add(new SqlParameter("@UPozicijaSklad", SqlDbType.Int) { Value = NaPozicijuSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void DelRNPPrijemPlatforme(int ID)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "DeleteRNPrijemPlatforme";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void InsRNPPrijemPlatforme2(DateTime DatumRasporeda, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva, string BrojKontejnera, int VrstaKontejnera,
            int NazivBrodara, int VrstaRobe, int NaSkladiste, int NaPozicijuSklad, int IdUsluge, string NalogRealizovao, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "InsertRNPrijemPlatforme2";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@SaVoznogSredstva", SqlDbType.Int) { Value = SaVoznogSredstva });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@USkladiste", SqlDbType.Int) { Value = NaSkladiste });
                            cmd.Parameters.Add(new SqlParameter("@UPozicijaSklad", SqlDbType.Int) { Value = NaPozicijuSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void UpdRNPPrijemPlatforme2(int ID, DateTime DatumRasporeda, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva, string BrojKontejnera, int VrstaKontejnera,
            int NazivBrodara, int VrstaRobe, int NaSkladiste, int NaPozicijuSklad, int IdUsluge, string NalogRealizovao, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "UpdateRNPrijemPlatforme2";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@SaVoznogSredstva", SqlDbType.Int) { Value = SaVoznogSredstva });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@USkladiste", SqlDbType.Int) { Value = NaSkladiste });
                            cmd.Parameters.Add(new SqlParameter("@UPozicijaSklad", SqlDbType.Int) { Value = NaPozicijuSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void DelRNPPrijemPlatforme2(int ID)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "DeleteRNPrijemPlatforme2";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }
        public void InsRNPPrijemVoza2(DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva, string BrojPlombe,
            int Uvoznik, int CarinskiPostupak, int InspekcijskiPregled, int NazivBrodara, int VrstaRobe, int NaSkladiste, int NaPozicijuSklad, int IdUsluge, string NalogRealizovao, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "InsertRNPrijemVoza2";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@SaVoznogSredstva", SqlDbType.Int) { Value = SaVoznogSredstva });
                            cmd.Parameters.Add(new SqlParameter("@BrojPlombe", SqlDbType.NVarChar, 50) { Value = BrojPlombe });
                            cmd.Parameters.Add(new SqlParameter("@Uvoznik", SqlDbType.Int) { Value = Uvoznik });
                            cmd.Parameters.Add(new SqlParameter("@CarinskiPostupak", SqlDbType.Int) { Value = CarinskiPostupak });
                            cmd.Parameters.Add(new SqlParameter("@InspekcijskiPregled", SqlDbType.Int) { Value = InspekcijskiPregled });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@USkladiste", SqlDbType.Int) { Value = NaSkladiste });
                            cmd.Parameters.Add(new SqlParameter("@UPozicijuSklad", SqlDbType.Int) { Value = NaPozicijuSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void UpdRNPPrijemVoza2(int ID, DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva, string BrojPlombe,
            int Uvoznik, int CarinskiPostupak, int InspekcijskiPregled, int NazivBrodara, int VrstaRobe, int NaSkladiste, int NaPozicijuSklad, int IdUsluge, string NalogRealizovao, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "UpdateRNPrijemVoza2";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@SaVoznogSredstva", SqlDbType.Int) { Value = SaVoznogSredstva });
                            cmd.Parameters.Add(new SqlParameter("@BrojPlombe", SqlDbType.NVarChar, 50) { Value = BrojPlombe });
                            cmd.Parameters.Add(new SqlParameter("@Uvoznik", SqlDbType.Int) { Value = Uvoznik });
                            cmd.Parameters.Add(new SqlParameter("@CarinskiPostupak", SqlDbType.Int) { Value = CarinskiPostupak });
                            cmd.Parameters.Add(new SqlParameter("@InspekcijskiPregled", SqlDbType.Int) { Value = InspekcijskiPregled });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@USkladiste", SqlDbType.Int) { Value = NaSkladiste });
                            cmd.Parameters.Add(new SqlParameter("@UPozicijuSklad", SqlDbType.Int) { Value = NaPozicijuSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void DelRNPPrijemVoza2(int ID)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "DeleteRNPrijemVoza2";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }
    }
}