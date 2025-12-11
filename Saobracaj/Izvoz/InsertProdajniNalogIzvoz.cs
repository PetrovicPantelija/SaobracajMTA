using Microsoft.IdentityModel.Tokens;
using Saobracaj.Uvoz;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.Izvoz
{
    class InsertProdajniNalogIzvoz
    {

        string connection = Sifarnici.frmLogovanje.connectionString;

        public void InsProdajniNalogIzvoz(string Korisnik, int Nalogodavac, string OpisPosla, int Brodar, int Izvoznik, string Link, DateTime CuttofPort, string BukingNumber)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertProdajniNalogIzvoz";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sifra = new SqlParameter();
            sifra.ParameterName = "@Korisnik";
            sifra.SqlDbType = SqlDbType.NVarChar;
            sifra.Size = 50;
            sifra.Direction = ParameterDirection.Input;
            sifra.Value = Korisnik;
            cmd.Parameters.Add(sifra);

            SqlParameter nalogodavac = new SqlParameter();
            nalogodavac.ParameterName = "@Nalogodavac";
            nalogodavac.SqlDbType = SqlDbType.Int;
            nalogodavac.Direction = ParameterDirection.Input;
            nalogodavac.Value = Nalogodavac;
            cmd.Parameters.Add(nalogodavac);

            SqlParameter opisposla = new SqlParameter();
            opisposla.ParameterName = "@OpisPosla";
            opisposla.SqlDbType = SqlDbType.NVarChar;
            opisposla.Size = 300;
            opisposla.Direction = ParameterDirection.Input;
            opisposla.Value = OpisPosla;
            cmd.Parameters.Add(opisposla);

            SqlParameter brodar = new SqlParameter();
            brodar.ParameterName = "@Brodar";
            brodar.SqlDbType = SqlDbType.Int;
            brodar.Direction = ParameterDirection.Input;
            brodar.Value = Brodar;
            cmd.Parameters.Add(brodar);

            SqlParameter izvoznik = new SqlParameter();
            izvoznik.ParameterName = "@Izvoznik";
            izvoznik.SqlDbType = SqlDbType.Int;
            izvoznik.Direction = ParameterDirection.Input;
            izvoznik.Value = Izvoznik;
            cmd.Parameters.Add(izvoznik);

            SqlParameter link = new SqlParameter();
            link.ParameterName = "@Link";
            link.SqlDbType = SqlDbType.NVarChar;
            link.Size = 300;
            link.Direction = ParameterDirection.Input;
            link.Value = Link;
            cmd.Parameters.Add(link);

            SqlParameter cuttofport = new SqlParameter();
            cuttofport.ParameterName = "@CuttOfPort";
            cuttofport.SqlDbType = SqlDbType.DateTime;
            cuttofport.Direction = ParameterDirection.Input;
            cuttofport.Value = CuttofPort;
            cmd.Parameters.Add(cuttofport);

            SqlParameter bukingnumber = new SqlParameter();
            bukingnumber.ParameterName = "@BukingNumber";
            bukingnumber.SqlDbType = SqlDbType.NVarChar;
            bukingnumber.Size = 300;
            bukingnumber.Direction = ParameterDirection.Input;
            bukingnumber.Value = BukingNumber;
            cmd.Parameters.Add(bukingnumber);




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


        public void InsProdajniNalogIzvozStavke( int IDNadredjena, double  Kolicina, string JM, int TipKontejnera, int KvalitetKontejnera,  string RefZaFakturisanje, string StatusStavke)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertProdajniNalogIzvozStavke";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter idnadredjena = new SqlParameter();
            idnadredjena.ParameterName = "@IDNAdredjenog";
            idnadredjena.SqlDbType = SqlDbType.Int;
            idnadredjena.Direction = ParameterDirection.Input;
            idnadredjena.Value = IDNadredjena;
            cmd.Parameters.Add(idnadredjena);

            SqlParameter kolicina = new SqlParameter();
            kolicina.ParameterName = "@Kolicina";
            kolicina.SqlDbType = SqlDbType.Decimal;
            kolicina.Direction = ParameterDirection.Input;
            kolicina.Value = Kolicina;
            cmd.Parameters.Add(kolicina);

            SqlParameter jm = new SqlParameter();
            jm.ParameterName = "@JM";
            jm.SqlDbType = SqlDbType.NVarChar;
            jm.Size = 3;
            jm.Direction = ParameterDirection.Input;
            jm.Value = JM;
            cmd.Parameters.Add(jm);

            SqlParameter tipKontejnera = new SqlParameter();
            tipKontejnera.ParameterName = "@TipKontejnera";
            tipKontejnera.SqlDbType = SqlDbType.Int;
            tipKontejnera.Direction = ParameterDirection.Input;
            tipKontejnera.Value = TipKontejnera;
            cmd.Parameters.Add(tipKontejnera);

            SqlParameter kvalitetkontejnera = new SqlParameter();
            kvalitetkontejnera.ParameterName = "@KvalitetKontejnera";
            kvalitetkontejnera.SqlDbType = SqlDbType.Int;
            kvalitetkontejnera.Direction = ParameterDirection.Input;
            kvalitetkontejnera.Value = KvalitetKontejnera;
            cmd.Parameters.Add(kvalitetkontejnera);

            SqlParameter refZaFakturisanje = new SqlParameter();
            refZaFakturisanje.ParameterName = "@ReferencaZaFakturisanje";
            refZaFakturisanje.SqlDbType = SqlDbType.NVarChar;
            refZaFakturisanje.Size = 300;
            refZaFakturisanje.Direction = ParameterDirection.Input;
            refZaFakturisanje.Value = RefZaFakturisanje;
           // refZaFakturisanje.Value = RefZaFakturisanje;
            cmd.Parameters.Add(refZaFakturisanje);

            SqlParameter statusstavke = new SqlParameter();
            statusstavke.ParameterName = "@StatusStavke";
            statusstavke.SqlDbType = SqlDbType.NVarChar;
            statusstavke.Size = 30;
            statusstavke.Direction = ParameterDirection.Input;
            statusstavke.Value = StatusStavke;
            // refZaFakturisanje.Value = RefZaFakturisanje;
            cmd.Parameters.Add(statusstavke);



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

        public void UpdKolicinaStavke(int ID, double Kolicina)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateProdajniNalogStavkeKolicina";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sifra = new SqlParameter();
            sifra.ParameterName = "@ID";
            sifra.SqlDbType = SqlDbType.Int;
            sifra.Direction = ParameterDirection.Input;
            sifra.Value = ID;
            cmd.Parameters.Add(sifra);

            SqlParameter naziv = new SqlParameter();
            naziv.ParameterName = "@Kolicina";
            naziv.SqlDbType = SqlDbType.Decimal;
            naziv.Size = 35;
            naziv.Direction = ParameterDirection.Input;
            naziv.Value = Kolicina;
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

        public void UpdStornirajStavku(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateProdajniNalogStavkeStorno";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sifra = new SqlParameter();
            sifra.ParameterName = "@ID";
            sifra.SqlDbType = SqlDbType.Int;
            sifra.Direction = ParameterDirection.Input;
            sifra.Value = ID;
            cmd.Parameters.Add(sifra);

           



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

        public void UpdDrumski(int ID, int Drumski)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateProdajniNalogStavkeDrumski";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sifra = new SqlParameter();
            sifra.ParameterName = "@ID";
            sifra.SqlDbType = SqlDbType.Int;
            sifra.Direction = ParameterDirection.Input;
            sifra.Value = ID;
            cmd.Parameters.Add(sifra);

            SqlParameter drumski = new SqlParameter();
            drumski.ParameterName = "@Drumski";
            drumski.SqlDbType = SqlDbType.Int;
            drumski.Direction = ParameterDirection.Input;
            drumski.Value = Drumski;
            cmd.Parameters.Add(drumski);





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

        public void UpdScenario(int ID, int Scenario)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateProdajniNalogStavkeScenario";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sifra = new SqlParameter();
            sifra.ParameterName = "@ID";
            sifra.SqlDbType = SqlDbType.Int;
            sifra.Direction = ParameterDirection.Input;
            sifra.Value = ID;
            cmd.Parameters.Add(sifra);

            SqlParameter scenario = new SqlParameter();
            scenario.ParameterName = "@Scenario";
            scenario.SqlDbType = SqlDbType.Int;
            scenario.Direction = ParameterDirection.Input;
            scenario.Value = Scenario;
            cmd.Parameters.Add(scenario);





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
        public void DelValute(string Sifra)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteValute";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sifra = new SqlParameter();
            sifra.ParameterName = "@Sifra";
            sifra.SqlDbType = SqlDbType.NVarChar;
            sifra.Size = 3;
            sifra.Direction = ParameterDirection.Input;
            sifra.Value = Sifra;
            cmd.Parameters.Add(sifra);

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
