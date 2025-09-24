using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Saobracaj.Sifarnici;
using Saobracaj.eDokumenta;
using Saobracaj.Pantheon_Export;
using Syncfusion.Windows.Forms.Diagram;

namespace Saobracaj.Drumski
{
    class InsertFakture
    {
        public string connect = frmLogovanje.connectionString;


        //      @Tip int,                                -- 0 = Izlazna, 1 = Ulazna
        //@ID int,                                     -- ID reda u FakturaDrumskiStavka
        //  @Faktura nvarchar(50) = NULL,                -- IzlaznaFaktura ili UlaznaFaktura(zavisi od @Tip)
        //  @DatumSlanja datetime = NULL,                -- koristi se samo za @Tip = 0
        //  @BeleskaUlazneFakture nvarchar(500) = NULL    -- koristi se samo za @Tip = 1
        public void UpdateFakturaDrumskiStavka(int Tip, int? ID, string Faktura, DateTime? DatumSlanja, string BeleskaUlazneFakture)

        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateFakturaDrumskiStavka";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter tip = new SqlParameter();
            tip.ParameterName = "@Tip";
            tip.SqlDbType = SqlDbType.Int;
            tip.Direction = ParameterDirection.Input;
            tip.Value = Tip;
            cmd.Parameters.Add(tip);

            SqlParameter iD = new SqlParameter();
            iD.ParameterName = "@ID";
            iD.SqlDbType = SqlDbType.Int;
            iD.Direction = ParameterDirection.Input;
            iD.Value = ID.HasValue ? (object)ID.Value : DBNull.Value;
            cmd.Parameters.Add(iD);

            SqlParameter faktura = new SqlParameter();
            faktura.ParameterName = "@Faktura";
            faktura.SqlDbType = SqlDbType.NVarChar;
            faktura.Size = 50;
            faktura.Direction = ParameterDirection.Input;
            faktura.Value = (object)Faktura ?? DBNull.Value;
            cmd.Parameters.Add(faktura);

            SqlParameter datumSlanja = new SqlParameter();
            datumSlanja.ParameterName = "@DatumSlanja";
            datumSlanja.SqlDbType = SqlDbType.DateTime;
            datumSlanja.Direction = ParameterDirection.Input;
            datumSlanja.Value = DatumSlanja.HasValue ? (object)DatumSlanja.Value : DBNull.Value;
            cmd.Parameters.Add(datumSlanja);


            SqlParameter beleskaUlazneFakture = new SqlParameter();
            beleskaUlazneFakture.ParameterName = "@BeleskaUlazneFakture";
            beleskaUlazneFakture.SqlDbType = SqlDbType.NVarChar;
            beleskaUlazneFakture.Size = 500;
            beleskaUlazneFakture.Direction = ParameterDirection.Input;
            beleskaUlazneFakture.Value = (object)BeleskaUlazneFakture ?? DBNull.Value;
            cmd.Parameters.Add(beleskaUlazneFakture);

            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            cmd.Transaction = tran;
            bool error = true;
            try
            {
                cmd.ExecuteNonQuery();
                tran.Commit();
                error = false;
                tran = conn.BeginTransaction();
                cmd.Transaction = tran;
            }
            catch (SqlException ex)
            {
                // throw new Exception("Neuspešan upis");
                MessageBox.Show("Greška u SQL izvršavanju: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tran.Rollback(); // Ne zaboravi i rollback
            }
            finally
            {
                if (!error)
                {
                    tran.Commit();
                    //MessageBox.Show("Ažuriranje je uspešno završeno", "",
                    //MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
            }
            if (error)
            {
            }
        }


        public void UpdateUlazne(int? ID, string UlaznaFaktura, string BeleskeFakture)

        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateUlaznaFakturaStavka";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter iD = new SqlParameter();
            iD.ParameterName = "@ID";
            iD.SqlDbType = SqlDbType.Int;
            iD.Direction = ParameterDirection.Input;
            iD.Value = ID.HasValue ? (object)ID.Value : DBNull.Value;
            cmd.Parameters.Add(iD);

            SqlParameter ulaznaFaktura = new SqlParameter();
            ulaznaFaktura.ParameterName = "@UlaznaFaktura";
            ulaznaFaktura.SqlDbType = SqlDbType.NVarChar;
            ulaznaFaktura.Size = 50;
            ulaznaFaktura.Direction = ParameterDirection.Input;
            ulaznaFaktura.Value = (object)UlaznaFaktura ?? DBNull.Value;
            cmd.Parameters.Add(ulaznaFaktura);

            SqlParameter beleskeFakture = new SqlParameter();
            beleskeFakture.ParameterName = "@BeleskeFakture";
            beleskeFakture.SqlDbType = SqlDbType.NVarChar;
            beleskeFakture.Size = 500;
            beleskeFakture.Direction = ParameterDirection.Input;
            beleskeFakture.Value = (object)BeleskeFakture ?? DBNull.Value;
            cmd.Parameters.Add(beleskeFakture);

            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            cmd.Transaction = tran;
            bool error = true;
            try
            {
                cmd.ExecuteNonQuery();
                tran.Commit();
                error = false;
                tran = conn.BeginTransaction();
                cmd.Transaction = tran;
            }
            catch (SqlException ex)
            {
                throw new Exception("Neuspešan upis");
                //MessageBox.Show("Greška u SQL izvršavanju: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //tran.Rollback(); // Ne zaboravi i rollback
            }
            finally
            {
                if (!error)
                {
                    tran.Commit();
                    MessageBox.Show("Ažuriranje je uspešno završeno", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
            }
            if (error)
            {
            }
        }


        public void InsStavkeFakture(int? TipFakture, int? FaktureDrumskogID, string IzlaznaFaktura, string UlaznaFaktura, string BeleskeFakture, DateTime? DatumSlanja)
        {

            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertFakturaDrumskiStavka";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter tipFakture = new SqlParameter();
            tipFakture.ParameterName = "@TipFakture";
            tipFakture.SqlDbType = SqlDbType.Int;
            tipFakture.Direction = ParameterDirection.Input;
            tipFakture.Value = TipFakture.HasValue ? (object)TipFakture.Value : DBNull.Value;
            cmd.Parameters.Add(tipFakture);

            SqlParameter faktureDrumskogID = new SqlParameter();
            faktureDrumskogID.ParameterName = "@FaktureDrumskogID";
            faktureDrumskogID.SqlDbType = SqlDbType.Int;
            faktureDrumskogID.Direction = ParameterDirection.Input;
            faktureDrumskogID.Value = FaktureDrumskogID.HasValue ? (object)FaktureDrumskogID.Value : DBNull.Value;
            cmd.Parameters.Add(faktureDrumskogID);

            SqlParameter izlaznaFaktura = new SqlParameter();
            izlaznaFaktura.ParameterName = "@IzlaznaFaktura";
            izlaznaFaktura.SqlDbType = SqlDbType.NVarChar;
            izlaznaFaktura.Size = 50;
            izlaznaFaktura.Direction = ParameterDirection.Input;
            izlaznaFaktura.Value = (object)IzlaznaFaktura ?? DBNull.Value;
            cmd.Parameters.Add(izlaznaFaktura);

            SqlParameter ulaznaFaktura = new SqlParameter();
            ulaznaFaktura.ParameterName = "@UlaznaFaktura";
            ulaznaFaktura.SqlDbType = SqlDbType.NVarChar;
            ulaznaFaktura.Size = 50;
            ulaznaFaktura.Direction = ParameterDirection.Input;
            ulaznaFaktura.Value = (object)UlaznaFaktura ?? DBNull.Value;
            cmd.Parameters.Add(ulaznaFaktura);

            SqlParameter beleskeFakture = new SqlParameter();
            beleskeFakture.ParameterName = "@BeleskeFakture";
            beleskeFakture.SqlDbType = SqlDbType.NVarChar;
            beleskeFakture.Size = 500;
            beleskeFakture.Direction = ParameterDirection.Input;
            beleskeFakture.Value = (object)BeleskeFakture ?? DBNull.Value;
            cmd.Parameters.Add(beleskeFakture);

            SqlParameter datumSlanja = new SqlParameter();
            datumSlanja.ParameterName = "@DatumSlanja";
            datumSlanja.SqlDbType = SqlDbType.DateTime;
            datumSlanja.Direction = ParameterDirection.Input;
            datumSlanja.Value = DatumSlanja.HasValue ? (object)DatumSlanja.Value : DBNull.Value;
            cmd.Parameters.Add(datumSlanja);

            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            cmd.Transaction = tran;
            bool error = true;
            try
            {
                cmd.ExecuteNonQuery();
                tran.Commit();
                error = false;
                tran = conn.BeginTransaction();
                cmd.Transaction = tran;

            }
            catch (SqlException ex)
            {
                //throw new Exception("Neuspešan upis");
                MessageBox.Show("Greška u SQL izvršavanju: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tran.Rollback(); // Ne zaboravi i rollback
            }
            finally
            {
                if (!error)
                {
                    tran.Commit();
                    //MessageBox.Show("Kreiranje je uspešno završeno", "",
                    //MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
            }
            if (error)
            {
            }
        }

        public void DelStavkaFakture(int ID)
        {

            SqlConnection myConnection = new SqlConnection(connect);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "DeleteFakturaDrumskiStavka";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            myCommand.Parameters.Add(id);

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
                throw new Exception("Neuspešno brisanje podataka");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Neuspešno brisanje podataka", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }

        public void InsFaktura(int? RadniNalogDrumskiID)
        {

            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertFakturaDrumski";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter radniNalogDrumskiID = new SqlParameter();
            radniNalogDrumskiID.ParameterName = "@RadniNalogDrumskiID";
            radniNalogDrumskiID.SqlDbType = SqlDbType.Int;
            radniNalogDrumskiID.Direction = ParameterDirection.Input;
            radniNalogDrumskiID.Value = RadniNalogDrumskiID.HasValue ? (object)RadniNalogDrumskiID.Value : DBNull.Value;
            cmd.Parameters.Add(radniNalogDrumskiID);


            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            cmd.Transaction = tran;
            bool error = true;
            try
            {
                cmd.ExecuteNonQuery();
                tran.Commit();
                error = false;
                tran = conn.BeginTransaction();
                cmd.Transaction = tran;

            }
            catch (SqlException ex)
            {
                throw new Exception("Neuspešano kreiranje fakture");
                //MessageBox.Show("Greška u SQL izvršavanju: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //tran.Rollback(); // Ne zaboravi i rollback
            }
            finally
            {
                //if (!error)
                //{
                //    tran.Commit();
                //    MessageBox.Show("Kreiranje je uspešno završeno", "",
                //    MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                conn.Close();
            }
            if (error)
            {
            }
        }

        public void SnimiUFajlBazu(int FakturaDrumskiID, string Naslov, string Napomena, int? DodaoKorisnik, string Putanja, string NazivDokumenta, int? Tip)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertDokumentaFaktureDrumski";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter iD = new SqlParameter();
            iD.ParameterName = "@FakturaDrumskiID";
            iD.SqlDbType = SqlDbType.Int;
            iD.Direction = ParameterDirection.Input;
            iD.Value = FakturaDrumskiID;
            cmd.Parameters.Add(iD);

            SqlParameter naslov = new SqlParameter();
            naslov.ParameterName = "@Naslov";
            naslov.SqlDbType = SqlDbType.NVarChar;
            naslov.Size = 200;
            naslov.Direction = ParameterDirection.Input;
            naslov.Value = (object)Naslov ?? DBNull.Value;
            cmd.Parameters.Add(naslov);

            SqlParameter napomena = new SqlParameter();
            napomena.ParameterName = "@Napomena";
            napomena.SqlDbType = SqlDbType.NVarChar;
            napomena.Size = 200;
            napomena.Direction = ParameterDirection.Input;
            napomena.Value = (object)Napomena ?? DBNull.Value;
            cmd.Parameters.Add(napomena);

            SqlParameter dodaoKorisnik = new SqlParameter();
            dodaoKorisnik.ParameterName = "@DodaoKorisnik";
            dodaoKorisnik.SqlDbType = SqlDbType.Int;
            dodaoKorisnik.Direction = ParameterDirection.Input;
            dodaoKorisnik.Value = DodaoKorisnik.HasValue ? (object)DodaoKorisnik.Value : DBNull.Value;
            cmd.Parameters.Add(dodaoKorisnik);

            SqlParameter putanja = new SqlParameter();
            putanja.ParameterName = "@Putanja";
            putanja.SqlDbType = SqlDbType.NVarChar;
            putanja.Size = 500;
            putanja.Direction = ParameterDirection.Input;
            putanja.Value = (object)Putanja ?? DBNull.Value;
            cmd.Parameters.Add(putanja);

            SqlParameter dokument = new SqlParameter();
            dokument.ParameterName = "@NazivDokumenta";
            dokument.SqlDbType = SqlDbType.NVarChar;
            dokument.Size = 500;
            dokument.Direction = ParameterDirection.Input;
            dokument.Value = (object)NazivDokumenta ?? DBNull.Value;
            cmd.Parameters.Add(dokument);

            SqlParameter tip = new SqlParameter();
            tip.ParameterName = "@Tip";
            tip.SqlDbType = SqlDbType.Int;
            tip.Direction = ParameterDirection.Input;
            tip.Value = Tip.HasValue ? (object)Tip.Value : DBNull.Value;
            cmd.Parameters.Add(tip);

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
                //throw new Exception("Neuspešan upis");
                MessageBox.Show("Greška u SQL izvršavanju: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tran.Rollback(); // Ne zaboravi i rollback
            }
            finally
            {
                if (!error)
                {
                    tran.Commit();
                    MessageBox.Show("Ažuriranje radnog naloga broja je uspešno završeno", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
            }
            if (error)
            {
            }
        }

        public void SnimiUFajlBazuKamioni(string NazivDokumenta, string Putanja, int? DodaoKorisnik, int RadniNalogID, int DodaoVozac)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertDokumentaUploadedFiles";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter nazivDokumenta = new SqlParameter();
            nazivDokumenta.ParameterName = "@NazivDokumenta";
            nazivDokumenta.SqlDbType = SqlDbType.NVarChar;
            nazivDokumenta.Size = 255;
            nazivDokumenta.Direction = ParameterDirection.Input;
            nazivDokumenta.Value = (object)NazivDokumenta ?? DBNull.Value;
            cmd.Parameters.Add(nazivDokumenta);

            SqlParameter putanja = new SqlParameter();
            putanja.ParameterName = "@Putanja";
            putanja.SqlDbType = SqlDbType.NVarChar;
            putanja.Size = 500;
            putanja.Direction = ParameterDirection.Input;
            putanja.Value = (object)Putanja ?? DBNull.Value;
            cmd.Parameters.Add(putanja);

            SqlParameter dodaoKorisnik = new SqlParameter();
            dodaoKorisnik.ParameterName = "@DodaoKorisnik";
            dodaoKorisnik.SqlDbType = SqlDbType.Int;
            dodaoKorisnik.Direction = ParameterDirection.Input;
            dodaoKorisnik.Value = DodaoKorisnik.HasValue ? (object)DodaoKorisnik.Value : DBNull.Value;
            cmd.Parameters.Add(dodaoKorisnik);

            SqlParameter iD = new SqlParameter();
            iD.ParameterName = "@RadniNalogID";
            iD.SqlDbType = SqlDbType.Int;
            iD.Direction = ParameterDirection.Input;
            iD.Value = RadniNalogID;
            cmd.Parameters.Add(iD);


            SqlParameter dodaoVozac = new SqlParameter();
            dodaoVozac.ParameterName = "@DodaoVozac";
            dodaoVozac.SqlDbType = SqlDbType.Int;
            dodaoVozac.Direction = ParameterDirection.Input;
            dodaoVozac.Value = DodaoVozac;
            cmd.Parameters.Add(dodaoVozac);

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
                //throw new Exception("Neuspešan upis");
                MessageBox.Show("Greška u SQL izvršavanju: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tran.Rollback(); // Ne zaboravi i rollback
            }
            finally
            {
                if (!error)
                {
                    tran.Commit();
                    MessageBox.Show("Ažuriranje radnog naloga broja je uspešno završeno", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
            }
            if (error)
            {
            }
        }


        public void UpdateFajlaUBazi(int FakturaDrumskogID, string Naslov, string Napomena, string Putanja, int? DodaoKorisnik, string NazivDokumenta, int Tip)

        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateDokumentaFaktureDrumski";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter fakturaDrumskogID = new SqlParameter();
            fakturaDrumskogID.ParameterName = "@FakturaDrumskogID";
            fakturaDrumskogID.SqlDbType = SqlDbType.Int;
            fakturaDrumskogID.Direction = ParameterDirection.Input;
            fakturaDrumskogID.Value = FakturaDrumskogID;
            cmd.Parameters.Add(fakturaDrumskogID);

            SqlParameter naslov = new SqlParameter();
            naslov.ParameterName = "@Naslov";
            naslov.SqlDbType = SqlDbType.NVarChar;
            naslov.Size = 200;
            naslov.Direction = ParameterDirection.Input;
            naslov.Value = (object)Naslov ?? DBNull.Value;
            cmd.Parameters.Add(naslov);

            SqlParameter napomena = new SqlParameter();
            napomena.ParameterName = "@Napomena";
            napomena.SqlDbType = SqlDbType.NVarChar;
            napomena.Size = 200;
            napomena.Direction = ParameterDirection.Input;
            napomena.Value = (object)Napomena ?? DBNull.Value;
            cmd.Parameters.Add(napomena);

            SqlParameter putanja = new SqlParameter();
            putanja.ParameterName = "@Putanja";
            putanja.SqlDbType = SqlDbType.NVarChar;
            putanja.Size = 500;
            putanja.Direction = ParameterDirection.Input;
            putanja.Value = (object)Putanja ?? DBNull.Value;
            cmd.Parameters.Add(putanja);

            SqlParameter dodaoKorisnik = new SqlParameter();
            dodaoKorisnik.ParameterName = "@DodaoKorisnik";
            dodaoKorisnik.SqlDbType = SqlDbType.Int;
            dodaoKorisnik.Direction = ParameterDirection.Input;
            dodaoKorisnik.Value = DodaoKorisnik.HasValue ? (object)DodaoKorisnik.Value : DBNull.Value;
            cmd.Parameters.Add(dodaoKorisnik);

            SqlParameter dokument = new SqlParameter();
            dokument.ParameterName = "@NazivDokumenta";
            dokument.SqlDbType = SqlDbType.NVarChar;
            dokument.Size = 200;
            dokument.Direction = ParameterDirection.Input;
            dokument.Value = (object)NazivDokumenta ?? DBNull.Value;
            cmd.Parameters.Add(dokument);

            SqlParameter tip = new SqlParameter();
            tip.ParameterName = "@Tip";
            tip.SqlDbType = SqlDbType.Int;
            tip.Direction = ParameterDirection.Input;
            tip.Value = Tip;
            cmd.Parameters.Add(tip);

            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            cmd.Transaction = tran;
            bool error = true;
            try
            {
                cmd.ExecuteNonQuery();
                tran.Commit();
                error = false;
                tran = conn.BeginTransaction();
                cmd.Transaction = tran;
            }
            catch (SqlException ex)
            {
                //throw new Exception("Neuspešan upis");
                MessageBox.Show("Greška u SQL izvršavanju: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tran.Rollback(); // Ne zaboravi i rollback
            }
            finally
            {
                if (!error)
                {
                    tran.Commit();
                    MessageBox.Show("Ažuriranje je uspešno završeno", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
            }
            if (error)
            {
            }
        }

        public void UpdateStatusFajla(int ID, string Status, string Tabela)

        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateStatusFajla";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter status = new SqlParameter();
            status.ParameterName = "@Status";
            status.SqlDbType = SqlDbType.NVarChar;
            status.Size = 50;
            status.Direction = ParameterDirection.Input;
            status.Value = (object)Status ?? DBNull.Value;
            cmd.Parameters.Add(status);

            SqlParameter tabela = new SqlParameter();
            tabela.ParameterName = "@Tabela";
            tabela.SqlDbType = SqlDbType.NVarChar;
            tabela.Size = 100;
            tabela.Direction = ParameterDirection.Input;
            tabela.Value = Tabela;
            cmd.Parameters.Add(tabela);

            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            cmd.Transaction = tran;
            bool error = true;
            try
            {
                cmd.ExecuteNonQuery();
                tran.Commit();
                error = false;
                tran = conn.BeginTransaction();
                cmd.Transaction = tran;
            }
            catch (SqlException ex)
            {
                //throw new Exception("Neuspešan upis");
                MessageBox.Show("Greška u SQL izvršavanju: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tran.Rollback(); // Ne zaboravi i rollback
            }
            finally
            {
                if (!error)
                {
                    tran.Commit();
                    MessageBox.Show("Ažuriranje je uspešno završeno", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
            }
            if (error)
            {
            }
        }

        public void DelDokument(int ID)
        {

            SqlConnection myConnection = new SqlConnection(connect);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "DeleteDokumentFaktureDrumski";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            myCommand.Parameters.Add(id);

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
                throw new Exception("Neuspešna promena podataka");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Neuspešna promena podataka", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }

        public void DelDokumentKamiona(int ID)
        {

            SqlConnection myConnection = new SqlConnection(connect);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "DeleteDokumentKamionaDrumski";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            myCommand.Parameters.Add(id);

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
                throw new Exception("Neuspešna promena podataka");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Neuspešna promena podataka", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }

        public void DelDokumentVozaca(int ID, string Tabela)
        {

            SqlConnection myConnection = new SqlConnection(connect);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "DeleteDokumentVozaca";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            myCommand.Parameters.Add(id);

            SqlParameter tabela = new SqlParameter();
            tabela.ParameterName = "@Tabela";
            tabela.SqlDbType = SqlDbType.NVarChar;
            tabela.Size = 100;
            tabela.Direction = ParameterDirection.Input;
            tabela.Value = Tabela;
            myCommand.Parameters.Add(tabela);

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

            catch (SqlException ex)
            {
                //throw new Exception("Neuspešna promena podataka");
                MessageBox.Show("Greška u SQL izvršavanju: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                myTransaction.Rollback(); // Ne zaboravi i rollback
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Neuspešna promena podataka", "",
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
