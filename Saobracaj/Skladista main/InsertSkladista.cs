using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Windows.Input;
using Syncfusion.Presentation;


namespace Saobracaj.Skladista_main
{
    public class InsertSkladista
    {
        string connection = Sifarnici.frmLogovanje.connectionString;

        public void DeleteDodatneUsluge(int RN)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteRnCarinskoSkladisteDodatneUsluge";
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

            catch (SqlException ex)
            {
                throw new Exception(ex.Message.ToString());
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
        public void InsertDodatneUsluge(int ID, int RN, int Usluga)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertRnCarinskoSkladisteDodatneUsluge";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);


            SqlParameter rn = new SqlParameter();
            rn.ParameterName = "@RN";
            rn.SqlDbType = SqlDbType.Int;
            rn.Direction = ParameterDirection.Input;
            rn.Value = RN;
            cmd.Parameters.Add(rn);

            SqlParameter usluga = new SqlParameter();
            usluga.ParameterName = "@Usluga";
            usluga.SqlDbType = SqlDbType.Int;
            usluga.Direction = ParameterDirection.Input;
            usluga.Value = Usluga;
            cmd.Parameters.Add(usluga);

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

            catch (SqlException ex)
            {
                throw new Exception(ex.Message.ToString());
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
        public void UpdateRNInterni(int IdInterni, int BrojRN)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateRNInterniSkladista";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter idInterni = new SqlParameter();
            idInterni.ParameterName = "@IDInterni";
            idInterni.SqlDbType = SqlDbType.Int;
            idInterni.Direction = ParameterDirection.Input;
            idInterni.Value = IdInterni;
            cmd.Parameters.Add(idInterni);

            SqlParameter brojRN = new SqlParameter();
            brojRN.ParameterName = "@BrojRN";
            brojRN.SqlDbType = SqlDbType.Int;
            brojRN.Direction = ParameterDirection.Input;
            brojRN.Value = BrojRN;
            cmd.Parameters.Add(brojRN);

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

            catch (SqlException ex)
            {
                throw new Exception(ex.Message.ToString());
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
        //status konejnera 3-Skladisnina,4-Pretovar
        public void InsertRNInterni(int StatusKontejnera, string Korisnik)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertRadniNalogInterniSkladista";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter status = new SqlParameter();
            status.ParameterName = "@StatusKontejnera";
            status.SqlDbType = SqlDbType.Int;
            status.Direction = ParameterDirection.Input;
            status.Value = StatusKontejnera;
            cmd.Parameters.Add(status);

            SqlParameter korisnik = new SqlParameter();
            korisnik.ParameterName = "@Korisnik";
            korisnik.SqlDbType = SqlDbType.Int;
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

            catch (SqlException ex)
            {
                throw new Exception(ex.Message.ToString());
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
        private object DbInt(int? value)
        {
            return value.HasValue ? (object)value.Value : DBNull.Value;
        }

        private object DbDate(DateTime? value)
        {
            return value.HasValue ? (object)value.Value : DBNull.Value;
        }

        private object DbString(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? (object)DBNull.Value : value.TrimEnd();
        }

        public void InsertRadniNalog(
            string Status,
            DateTime Datum,
            string Korisnik,
            string VrstaRN,
            string TipRN,
            string CarinskoSkladiste,
            int MagacinskiBroj,
            int Nalogodavac,
            int CarinskiPostupak,
            string OpisPosla,
            int VlasnikRobe,
            string VrstaRobe,
            string NacinPakovanja,
            int OstalaSkladista,
            int PIB,

            int? VrstaPrevoznogSredstvaOtprema,
            int? VrstaKamionaOtprema,
            string VoziloOtprema,
            string VozacOtprema,
            string BrojLKOtprema,
            string BrojTelefonaOtprema,
            int? OdredisnaCarinarnicaOtpremaOtprema,
            int? SpediterOtprema,
            string KontakOsobaSpediteraOtprema,
            int? MestoIstovaraOtprema,
            string AdresaOtprema,
            string KontaktOsobaIstovarOtprema,
            DateTime? PlaniraniDatumOtpema,
            DateTime? PlaniraniDatum2Otprema,
            string BrojKontejneraOtprema,

            int? VrstaPrevoznogSredstvaPrijem,
            int? VrstaKamionaPrijem,
            string VoziloPrijem,
            string VozacPrijem,
            string BrojLKPrijem,
            string BrojTelefonaPrijem,
            int? OdredisnaCarinarnicaPrijem,
            int? SpediterPrijem,
            string KontakOsobaSpediteraPrijem,
            int? MestoIstovaraPrijem,
            string AdresaPrijem,
            string KontaktOsobaIstovarPrijem,
            DateTime? PlaniraniDatumPrijem,
            DateTime? PlaniraniDatum2Prijem,
            string BrojKontejneraPrijem,

            string PosebniUslovi,
            int DodatneUslugeID,
            string Napomena,
            int Aktivan,
            int Formiran)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertRadniNalogSkladista";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.NVarChar) { Value = DbString(Status) });
            cmd.Parameters.Add(new SqlParameter("@Datum", SqlDbType.DateTime) { Value = Datum });
            cmd.Parameters.Add(new SqlParameter("@Korisnik", SqlDbType.NVarChar) { Value = DbString(Korisnik) });
            cmd.Parameters.Add(new SqlParameter("@VrstaRN", SqlDbType.NVarChar) { Value = DbString(VrstaRN) });
            cmd.Parameters.Add(new SqlParameter("@TipRN", SqlDbType.NVarChar) { Value = DbString(TipRN) });
            cmd.Parameters.Add(new SqlParameter("@CarinskoSkladiste", SqlDbType.NVarChar) { Value = DbString(CarinskoSkladiste) });
            cmd.Parameters.Add(new SqlParameter("@MagacinskiBroj", SqlDbType.Int) { Value = MagacinskiBroj });
            cmd.Parameters.Add(new SqlParameter("@Nalogodavac", SqlDbType.Int) { Value = Nalogodavac });
            cmd.Parameters.Add(new SqlParameter("@CarinskiPostupak", SqlDbType.Int) { Value = CarinskiPostupak });
            cmd.Parameters.Add(new SqlParameter("@OpisPosla", SqlDbType.NVarChar) { Value = DbString(OpisPosla) });
            cmd.Parameters.Add(new SqlParameter("@VlasnikRobe", SqlDbType.Int) { Value = VlasnikRobe });
            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.NVarChar) { Value = DbString(VrstaRobe) });
            cmd.Parameters.Add(new SqlParameter("@NacinPakovanja", SqlDbType.NVarChar) { Value = DbString(NacinPakovanja) });
            cmd.Parameters.Add(new SqlParameter("@OstalaSkladista", SqlDbType.Int) { Value = OstalaSkladista });
            cmd.Parameters.Add(new SqlParameter("@PIB", SqlDbType.Int) { Value = PIB });

            // OTPREMA
            cmd.Parameters.Add(new SqlParameter("@VrstaPrevoznogSredstvaOtprema", SqlDbType.Int) { Value = DbInt(VrstaPrevoznogSredstvaOtprema) });
            cmd.Parameters.Add(new SqlParameter("@VrstaKamionaOtprema", SqlDbType.Int) { Value = DbInt(VrstaKamionaOtprema) });
            cmd.Parameters.Add(new SqlParameter("@VoziloOtprema", SqlDbType.NVarChar) { Value = DbString(VoziloOtprema) });
            cmd.Parameters.Add(new SqlParameter("@VozacOtprema", SqlDbType.NVarChar) { Value = DbString(VozacOtprema) });
            cmd.Parameters.Add(new SqlParameter("@BrojLKOtprema", SqlDbType.NVarChar) { Value = DbString(BrojLKOtprema) });
            cmd.Parameters.Add(new SqlParameter("@BrojTelefonaOtprema", SqlDbType.NVarChar) { Value = DbString(BrojTelefonaOtprema) });
            cmd.Parameters.Add(new SqlParameter("@OdredisnaCarinarnicaOtpremaOtprema", SqlDbType.Int) { Value = DbInt(OdredisnaCarinarnicaOtpremaOtprema) });
            cmd.Parameters.Add(new SqlParameter("@SpediterOtprema", SqlDbType.Int) { Value = DbInt(SpediterOtprema) });
            cmd.Parameters.Add(new SqlParameter("@KontakOsobaSpediteraOtprema", SqlDbType.NVarChar) { Value = DbString(KontakOsobaSpediteraOtprema) });
            cmd.Parameters.Add(new SqlParameter("@MestoIstovaraOtprema", SqlDbType.Int) { Value = DbInt(MestoIstovaraOtprema) });
            cmd.Parameters.Add(new SqlParameter("@AdresaOtprema", SqlDbType.NVarChar) { Value = DbString(AdresaOtprema) });
            cmd.Parameters.Add(new SqlParameter("@KontaktOsobaIstovarOtprema", SqlDbType.NVarChar) { Value = DbString(KontaktOsobaIstovarOtprema) });
            cmd.Parameters.Add(new SqlParameter("@PlaniraniDatumOtpema", SqlDbType.DateTime) { Value = DbDate(PlaniraniDatumOtpema) });
            cmd.Parameters.Add(new SqlParameter("@PlaniraniDatum2Otprema", SqlDbType.DateTime) { Value = DbDate(PlaniraniDatum2Otprema) });
            cmd.Parameters.Add(new SqlParameter("@BrojKontejneraOtprema", SqlDbType.NVarChar) { Value = DbString(BrojKontejneraOtprema) });

            // PRIJEM
            cmd.Parameters.Add(new SqlParameter("@VrstaPrevoznogSredstvaPrijem", SqlDbType.Int) { Value = DbInt(VrstaPrevoznogSredstvaPrijem) });
            cmd.Parameters.Add(new SqlParameter("@VrstaKamionaPrijem", SqlDbType.Int) { Value = DbInt(VrstaKamionaPrijem) });
            cmd.Parameters.Add(new SqlParameter("@VoziloPrijem", SqlDbType.NVarChar) { Value = DbString(VoziloPrijem) });
            cmd.Parameters.Add(new SqlParameter("@VozacPrijem", SqlDbType.NVarChar) { Value = DbString(VozacPrijem) });
            cmd.Parameters.Add(new SqlParameter("@BrojLKPrijem", SqlDbType.NVarChar) { Value = DbString(BrojLKPrijem) });
            cmd.Parameters.Add(new SqlParameter("@BrojTelefonaPrijem", SqlDbType.NVarChar) { Value = DbString(BrojTelefonaPrijem) });
            cmd.Parameters.Add(new SqlParameter("@OdredisnaCarinarnicaPrijem", SqlDbType.Int) { Value = DbInt(OdredisnaCarinarnicaPrijem) });
            cmd.Parameters.Add(new SqlParameter("@SpediterPrijem", SqlDbType.Int) { Value = DbInt(SpediterPrijem) });
            cmd.Parameters.Add(new SqlParameter("@KontakOsobaSpediteraPrijem", SqlDbType.NVarChar) { Value = DbString(KontakOsobaSpediteraPrijem) });
            cmd.Parameters.Add(new SqlParameter("@MestoIstovaraPrijem", SqlDbType.Int) { Value = DbInt(MestoIstovaraPrijem) });
            cmd.Parameters.Add(new SqlParameter("@AdresaPrijem", SqlDbType.NVarChar) { Value = DbString(AdresaPrijem) });
            cmd.Parameters.Add(new SqlParameter("@KontaktOsobaIstovarPrijem", SqlDbType.NVarChar) { Value = DbString(KontaktOsobaIstovarPrijem) });
            cmd.Parameters.Add(new SqlParameter("@PlaniraniDatumPrijem", SqlDbType.DateTime) { Value = DbDate(PlaniraniDatumPrijem) });
            cmd.Parameters.Add(new SqlParameter("@PlaniraniDatum2Prijem", SqlDbType.DateTime) { Value = DbDate(PlaniraniDatum2Prijem) });
            cmd.Parameters.Add(new SqlParameter("@BrojKontejneraPrijem", SqlDbType.NVarChar) { Value = DbString(BrojKontejneraPrijem) });

            cmd.Parameters.Add(new SqlParameter("@PosebniUslovi", SqlDbType.NVarChar) { Value = DbString(PosebniUslovi) });
            cmd.Parameters.Add(new SqlParameter("@DodatneUslugeID", SqlDbType.Int) { Value = DodatneUslugeID });
            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar) { Value = DbString(Napomena) });
            cmd.Parameters.Add(new SqlParameter("@Aktivan", SqlDbType.TinyInt) { Value = Aktivan });
            cmd.Parameters.Add(new SqlParameter("@Formiran", SqlDbType.TinyInt) { Value = Formiran });

            conn.Open();
            SqlTransaction myTransaction = conn.BeginTransaction();
            cmd.Transaction = myTransaction;
            bool error = false;

            try
            {
                cmd.ExecuteNonQuery();
                myTransaction.Commit();
            }
            catch (SqlException ex)
            {
                error = true;
                try
                {
                    myTransaction.Rollback();
                }
                catch { }

                throw new Exception(ex.Message, ex);
            }
            finally
            {
                conn.Close();

                if (!error)
                {
                    // MessageBox.Show("Kreiranje uspešno završeno", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        public void UpdateRadniNalog(
            int ID,
            string Status,
            DateTime Datum,
            string Korisnik,
            string VrstaRN,
            string TipRN,
            string CarinskoSkladiste,
            int MagacinskiBroj,
            int Nalogodavac,
            int CarinskiPostupak,
            string OpisPosla,
            int VlasnikRobe,
            string VrstaRobe,
            string NacinPakovanja,
            int OstalaSkladista,
            int PIB,

            int? VrstaPrevoznogSredstvaOtprema,
            int? VrstaKamionaOtprema,
            string VoziloOtprema,
            string VozacOtprema,
            string BrojLKOtprema,
            string BrojTelefonaOtprema,
            int? OdredisnaCarinarnicaOtpremaOtprema,
            int? SpediterOtprema,
            string KontakOsobaSpediteraOtprema,
            int? MestoIstovaraOtprema,
            string AdresaOtprema,
            string KontaktOsobaIstovarOtprema,
            DateTime? PlaniraniDatumOtpema,
            DateTime? PlaniraniDatum2Otprema,
            string BrojKontejneraOtprema,

            int? VrstaPrevoznogSredstvaPrijem,
            int? VrstaKamionaPrijem,
            string VoziloPrijem,
            string VozacPrijem,
            string BrojLKPrijem,
            string BrojTelefonaPrijem,
            int? OdredisnaCarinarnicaPrijem,
            int? SpediterPrijem,
            string KontakOsobaSpediteraPrijem,
            int? MestoIstovaraPrijem,
            string AdresaPrijem,
            string KontaktOsobaIstovarPrijem,
            DateTime? PlaniraniDatumPrijem,
            DateTime? PlaniraniDatum2Prijem,
            string BrojKontejneraPrijem,

            string PosebniUslovi,
            int DodatneUslugeID,
            string Napomena,
            int Aktivan,
            int Formiran)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateRadniNalogSkladista";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
            cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.NVarChar) { Value = DbString(Status) });
            cmd.Parameters.Add(new SqlParameter("@Datum", SqlDbType.DateTime) { Value = Datum });
            cmd.Parameters.Add(new SqlParameter("@Korisnik", SqlDbType.NVarChar) { Value = DbString(Korisnik) });
            cmd.Parameters.Add(new SqlParameter("@VrstaRN", SqlDbType.NVarChar) { Value = DbString(VrstaRN) });
            cmd.Parameters.Add(new SqlParameter("@TipRN", SqlDbType.NVarChar) { Value = DbString(TipRN) });
            cmd.Parameters.Add(new SqlParameter("@CarinskoSkladiste", SqlDbType.NVarChar) { Value = DbString(CarinskoSkladiste) });
            cmd.Parameters.Add(new SqlParameter("@MagacinskiBroj", SqlDbType.Int) { Value = MagacinskiBroj });
            cmd.Parameters.Add(new SqlParameter("@Nalogodavac", SqlDbType.Int) { Value = Nalogodavac });
            cmd.Parameters.Add(new SqlParameter("@CarinskiPostupak", SqlDbType.Int) { Value = CarinskiPostupak });
            cmd.Parameters.Add(new SqlParameter("@OpisPosla", SqlDbType.NVarChar) { Value = DbString(OpisPosla) });
            cmd.Parameters.Add(new SqlParameter("@VlasnikRobe", SqlDbType.Int) { Value = VlasnikRobe });
            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.NVarChar) { Value = DbString(VrstaRobe) });
            cmd.Parameters.Add(new SqlParameter("@NacinPakovanja", SqlDbType.NVarChar) { Value = DbString(NacinPakovanja) });
            cmd.Parameters.Add(new SqlParameter("@OstalaSkladista", SqlDbType.Int) { Value = OstalaSkladista });
            cmd.Parameters.Add(new SqlParameter("@PIB", SqlDbType.Int) { Value = PIB });

            // OTPREMA
            cmd.Parameters.Add(new SqlParameter("@VrstaPrevoznogSredstvaOtprema", SqlDbType.Int) { Value = DbInt(VrstaPrevoznogSredstvaOtprema) });
            cmd.Parameters.Add(new SqlParameter("@VrstaKamionaOtprema", SqlDbType.Int) { Value = DbInt(VrstaKamionaOtprema) });
            cmd.Parameters.Add(new SqlParameter("@VoziloOtprema", SqlDbType.NVarChar) { Value = DbString(VoziloOtprema) });
            cmd.Parameters.Add(new SqlParameter("@VozacOtprema", SqlDbType.NVarChar) { Value = DbString(VozacOtprema) });
            cmd.Parameters.Add(new SqlParameter("@BrojLKOtprema", SqlDbType.NVarChar) { Value = DbString(BrojLKOtprema) });
            cmd.Parameters.Add(new SqlParameter("@BrojTelefonaOtprema", SqlDbType.NVarChar) { Value = DbString(BrojTelefonaOtprema) });
            cmd.Parameters.Add(new SqlParameter("@OdredisnaCarinarnicaOtpremaOtprema", SqlDbType.Int) { Value = DbInt(OdredisnaCarinarnicaOtpremaOtprema) });
            cmd.Parameters.Add(new SqlParameter("@SpediterOtprema", SqlDbType.Int) { Value = DbInt(SpediterOtprema) });
            cmd.Parameters.Add(new SqlParameter("@KontakOsobaSpediteraOtprema", SqlDbType.NVarChar) { Value = DbString(KontakOsobaSpediteraOtprema) });
            cmd.Parameters.Add(new SqlParameter("@MestoIstovaraOtprema", SqlDbType.Int) { Value = DbInt(MestoIstovaraOtprema) });
            cmd.Parameters.Add(new SqlParameter("@AdresaOtprema", SqlDbType.NVarChar) { Value = DbString(AdresaOtprema) });
            cmd.Parameters.Add(new SqlParameter("@KontaktOsobaIstovarOtprema", SqlDbType.NVarChar) { Value = DbString(KontaktOsobaIstovarOtprema) });
            cmd.Parameters.Add(new SqlParameter("@PlaniraniDatumOtpema", SqlDbType.DateTime) { Value = DbDate(PlaniraniDatumOtpema) });
            cmd.Parameters.Add(new SqlParameter("@PlaniraniDatum2Otprema", SqlDbType.DateTime) { Value = DbDate(PlaniraniDatum2Otprema) });
            cmd.Parameters.Add(new SqlParameter("@BrojKontejneraOtprema", SqlDbType.NVarChar) { Value = DbString(BrojKontejneraOtprema) });

            // PRIJEM
            cmd.Parameters.Add(new SqlParameter("@VrstaPrevoznogSredstvaPrijem", SqlDbType.Int) { Value = DbInt(VrstaPrevoznogSredstvaPrijem) });
            cmd.Parameters.Add(new SqlParameter("@VrstaKamionaPrijem", SqlDbType.Int) { Value = DbInt(VrstaKamionaPrijem) });
            cmd.Parameters.Add(new SqlParameter("@VoziloPrijem", SqlDbType.NVarChar) { Value = DbString(VoziloPrijem) });
            cmd.Parameters.Add(new SqlParameter("@VozacPrijem", SqlDbType.NVarChar) { Value = DbString(VozacPrijem) });
            cmd.Parameters.Add(new SqlParameter("@BrojLKPrijem", SqlDbType.NVarChar) { Value = DbString(BrojLKPrijem) });
            cmd.Parameters.Add(new SqlParameter("@BrojTelefonaPrijem", SqlDbType.NVarChar) { Value = DbString(BrojTelefonaPrijem) });
            cmd.Parameters.Add(new SqlParameter("@OdredisnaCarinarnicaPrijem", SqlDbType.Int) { Value = DbInt(OdredisnaCarinarnicaPrijem) });
            cmd.Parameters.Add(new SqlParameter("@SpediterPrijem", SqlDbType.Int) { Value = DbInt(SpediterPrijem) });
            cmd.Parameters.Add(new SqlParameter("@KontakOsobaSpediteraPrijem", SqlDbType.NVarChar) { Value = DbString(KontakOsobaSpediteraPrijem) });
            cmd.Parameters.Add(new SqlParameter("@MestoIstovaraPrijem", SqlDbType.Int) { Value = DbInt(MestoIstovaraPrijem) });
            cmd.Parameters.Add(new SqlParameter("@AdresaPrijem", SqlDbType.NVarChar) { Value = DbString(AdresaPrijem) });
            cmd.Parameters.Add(new SqlParameter("@KontaktOsobaIstovarPrijem", SqlDbType.NVarChar) { Value = DbString(KontaktOsobaIstovarPrijem) });
            cmd.Parameters.Add(new SqlParameter("@PlaniraniDatumPrijem", SqlDbType.DateTime) { Value = DbDate(PlaniraniDatumPrijem) });
            cmd.Parameters.Add(new SqlParameter("@PlaniraniDatum2Prijem", SqlDbType.DateTime) { Value = DbDate(PlaniraniDatum2Prijem) });
            cmd.Parameters.Add(new SqlParameter("@BrojKontejneraPrijem", SqlDbType.NVarChar) { Value = DbString(BrojKontejneraPrijem) });

            cmd.Parameters.Add(new SqlParameter("@PosebniUslovi", SqlDbType.NVarChar) { Value = DbString(PosebniUslovi) });
            cmd.Parameters.Add(new SqlParameter("@DodatneUslugeID", SqlDbType.Int) { Value = DodatneUslugeID });
            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar) { Value = DbString(Napomena) });
            cmd.Parameters.Add(new SqlParameter("@Aktivan", SqlDbType.TinyInt) { Value = Aktivan });
            cmd.Parameters.Add(new SqlParameter("@Formiran", SqlDbType.TinyInt) { Value = Formiran });

            conn.Open();
            SqlTransaction myTransaction = conn.BeginTransaction();
            cmd.Transaction = myTransaction;
            bool error = false;

            try
            {
                cmd.ExecuteNonQuery();
                myTransaction.Commit();
            }
            catch (SqlException ex)
            {
                error = true;
                try
                {
                    myTransaction.Rollback();
                }
                catch { }

                throw new Exception(ex.Message, ex);
            }
            finally
            {
                conn.Close();

                if (!error)
                {
                    MessageBox.Show("Izmena uspešno završena", "",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        public void UpdateMagacinskiBrojCarinski(int ID, string Naziv)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateMagacinskiBrojCarinski";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter naziv = new SqlParameter();
            naziv.ParameterName = "@Naziv";
            naziv.SqlDbType = SqlDbType.NVarChar;
            naziv.Direction = ParameterDirection.Input;
            naziv.Value = Naziv;
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

            catch (SqlException ex)
            {
                throw new Exception(ex.Message.ToString());
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


        public void InsertMagacinskiBrojCarinski(int ID, string Naziv)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertMagacinskiBrojCarinski";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter naziv = new SqlParameter();
            naziv.ParameterName = "@Naziv";
            naziv.SqlDbType = SqlDbType.NVarChar;
            naziv.Direction = ParameterDirection.Input;
            naziv.Value = Naziv;
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

            catch (SqlException ex)
            {
                throw new Exception(ex.Message.ToString());
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
        public void DeleteMagacinskiBrojCarinski(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteMagacinskiBrojCarinski";
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

            catch (SqlException ex)
            {
                throw new Exception(ex.Message.ToString());
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
        public void InsertPrijemnicaCarinska(string Kreirao, int RN, int? CarinskiPostupak, string SmestajniDokument, string Rok, int? Posiljalac, string Faktura, string CRM, string Status)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertRNCarinskoSkladistePrijemnica";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter kreirao = new SqlParameter();
            kreirao.ParameterName = "@Kreirao";
            kreirao.SqlDbType = SqlDbType.NVarChar;
            kreirao.Direction = ParameterDirection.Input;
            kreirao.Value = Kreirao;
            cmd.Parameters.Add(kreirao);

            SqlParameter rn = new SqlParameter();
            rn.ParameterName = "@RN";
            rn.SqlDbType = SqlDbType.Int;
            rn.Direction = ParameterDirection.Input;
            rn.Value = RN;
            cmd.Parameters.Add(rn);

            SqlParameter postupak = new SqlParameter();
            postupak.ParameterName = "@CarinskiPostupak";
            postupak.SqlDbType = SqlDbType.Int;
            postupak.Direction = ParameterDirection.Input;
            postupak.Value = CarinskiPostupak.HasValue ? (object)CarinskiPostupak.Value : DBNull.Value;
            cmd.Parameters.Add(postupak);

            SqlParameter smestajniDokument = new SqlParameter();
            smestajniDokument.ParameterName = "@SmestajniDokument";
            smestajniDokument.SqlDbType = SqlDbType.NVarChar;
            smestajniDokument.Direction = ParameterDirection.Input;
            smestajniDokument.Value = SmestajniDokument;
            cmd.Parameters.Add(smestajniDokument);

            SqlParameter rok = new SqlParameter();
            rok.ParameterName = "@Rok";
            rok.SqlDbType = SqlDbType.NVarChar;
            rok.Direction = ParameterDirection.Input;
            rok.Value = Rok;
            cmd.Parameters.Add(rok);

            SqlParameter posiljalac = new SqlParameter();
            posiljalac.ParameterName = "@Posiljalac";
            posiljalac.SqlDbType = SqlDbType.Int;
            posiljalac.Value = Posiljalac.HasValue ? (object)Posiljalac.Value : DBNull.Value;
            cmd.Parameters.Add(posiljalac);

            SqlParameter faktura = new SqlParameter();
            faktura.ParameterName = "@Faktura";
            faktura.SqlDbType = SqlDbType.NVarChar;
            faktura.Direction = ParameterDirection.Input;
            faktura.Value = Faktura;
            cmd.Parameters.Add(faktura);

            SqlParameter crm = new SqlParameter();
            crm.ParameterName = "@CRM";
            crm.SqlDbType = SqlDbType.NVarChar;
            crm.Direction = ParameterDirection.Input;
            crm.Value = CRM;
            cmd.Parameters.Add(crm);

            SqlParameter status = new SqlParameter();
            status.ParameterName = "@Status";
            status.SqlDbType = SqlDbType.NVarChar;
            status.Direction = ParameterDirection.Input;
            status.Value = Status;
            cmd.Parameters.Add(status);

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

            catch (SqlException ex)
            {
                throw new Exception(ex.Message.ToString());
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

        public void UpdatePrijemnicaCarinska(string Kreirao, int ID, int RN, int? CarinskiPostupak, string SmestajniDokument, string Rok, int? Posiljalac, string Faktura, string CRM, string Status, int Proces)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateRNCarinskoSkladistePrijemnica";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter proces = new SqlParameter();
            proces.ParameterName = "@Uprocesu";
            proces.SqlDbType = SqlDbType.Int;
            proces.Direction = ParameterDirection.Input;
            proces.Value = Proces;
            cmd.Parameters.Add(proces);

            SqlParameter kreirao = new SqlParameter();
            kreirao.ParameterName = "@Kreirao";
            kreirao.SqlDbType = SqlDbType.NVarChar;
            kreirao.Direction = ParameterDirection.Input;
            kreirao.Value = Kreirao;
            cmd.Parameters.Add(kreirao);

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter rn = new SqlParameter();
            rn.ParameterName = "@RN";
            rn.SqlDbType = SqlDbType.Int;
            rn.Direction = ParameterDirection.Input;
            rn.Value = RN;
            cmd.Parameters.Add(rn);

            SqlParameter postupak = new SqlParameter();
            postupak.ParameterName = "@CarinskiPostupak";
            postupak.SqlDbType = SqlDbType.Int;
            postupak.Direction = ParameterDirection.Input;
            postupak.Value = CarinskiPostupak.HasValue ? (object)CarinskiPostupak.Value : DBNull.Value;
            cmd.Parameters.Add(postupak);

            SqlParameter smestajniDokument = new SqlParameter();
            smestajniDokument.ParameterName = "@SmestajniDokument";
            smestajniDokument.SqlDbType = SqlDbType.NVarChar;
            smestajniDokument.Direction = ParameterDirection.Input;
            smestajniDokument.Value = SmestajniDokument;
            cmd.Parameters.Add(smestajniDokument);

            SqlParameter rok = new SqlParameter();
            rok.ParameterName = "@Rok";
            rok.SqlDbType = SqlDbType.NVarChar;
            rok.Direction = ParameterDirection.Input;
            rok.Value = Rok;
            cmd.Parameters.Add(rok);

            SqlParameter posiljalac = new SqlParameter();
            posiljalac.ParameterName = "@Posiljalac";
            posiljalac.SqlDbType = SqlDbType.Int;
            posiljalac.Value = Posiljalac.HasValue ? (object)Posiljalac.Value : DBNull.Value;
            cmd.Parameters.Add(posiljalac);

            SqlParameter faktura = new SqlParameter();
            faktura.ParameterName = "@Faktura";
            faktura.SqlDbType = SqlDbType.NVarChar;
            faktura.Direction = ParameterDirection.Input;
            faktura.Value = Faktura;
            cmd.Parameters.Add(faktura);

            SqlParameter crm = new SqlParameter();
            crm.ParameterName = "@CRM";
            crm.SqlDbType = SqlDbType.NVarChar;
            crm.Direction = ParameterDirection.Input;
            crm.Value = CRM;
            cmd.Parameters.Add(crm);

            SqlParameter status = new SqlParameter();
            status.ParameterName = "@Status";
            status.SqlDbType = SqlDbType.NVarChar;
            status.Direction = ParameterDirection.Input;
            status.Value = Status;
            cmd.Parameters.Add(status);

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

            catch (SqlException ex)
            {
                throw new Exception(ex.Message.ToString());
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

        public void InsertPrijemnicaCarinskaStavke(int IDNadredjena, int RB, int NHM, string Naziv, string Naimenovanje, string JM, decimal Koleta, decimal Bruto, decimal Vrednost,
            string Valuta, string Pozicija, int Paleta, int VrstaPaleta, int PDV, int Carina, decimal Neto, string Napomena)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertRNCarinskoPrijemnicaStavke";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter napomena = new SqlParameter();
            napomena.ParameterName = "@Napomena";
            napomena.SqlDbType = SqlDbType.NVarChar;
            napomena.Direction = ParameterDirection.Input;
            napomena.Value = Napomena;
            cmd.Parameters.Add(napomena);


            SqlParameter neto = new SqlParameter();
            neto.ParameterName = "@Neto";
            neto.DbType = DbType.Decimal;
            neto.Direction = ParameterDirection.Input;
            neto.Value = Neto;
            cmd.Parameters.Add(neto);

            SqlParameter carina = new SqlParameter();
            carina.ParameterName = "@Carina";
            carina.DbType = DbType.Int32;
            carina.Direction = ParameterDirection.Input;
            carina.Value = Carina;
            cmd.Parameters.Add(carina);

            SqlParameter pdv = new SqlParameter();
            pdv.ParameterName = "@PDV";
            pdv.DbType = DbType.Int32;
            pdv.Direction = ParameterDirection.Input;
            pdv.Value = PDV;
            cmd.Parameters.Add(pdv);

            SqlParameter vrstaPalete = new SqlParameter();
            vrstaPalete.ParameterName = "@VrstaPaleta";
            vrstaPalete.SqlDbType = SqlDbType.Int;
            vrstaPalete.Direction = ParameterDirection.Input;
            vrstaPalete.Value = VrstaPaleta;
            cmd.Parameters.Add(vrstaPalete);

            SqlParameter paleta = new SqlParameter();
            paleta.ParameterName = "@Paleta";
            paleta.SqlDbType = SqlDbType.Int;
            paleta.Direction = ParameterDirection.Input;
            paleta.Value = Paleta;
            cmd.Parameters.Add(paleta);


            SqlParameter pozicija = new SqlParameter();
            pozicija.ParameterName = "@Pozicija";
            pozicija.SqlDbType = SqlDbType.NVarChar;
            pozicija.Direction = ParameterDirection.Input;
            pozicija.Value = Pozicija;
            cmd.Parameters.Add(pozicija);

            SqlParameter valuta = new SqlParameter();
            valuta.ParameterName = "@Valuta";
            valuta.SqlDbType = SqlDbType.NVarChar;
            valuta.Direction = ParameterDirection.Input;
            valuta.Value = Valuta;
            cmd.Parameters.Add(valuta);

            SqlParameter vrednost = new SqlParameter();
            vrednost.ParameterName = "@Vrednost";
            vrednost.DbType = DbType.Decimal;
            vrednost.Direction = ParameterDirection.Input;
            vrednost.Value = Vrednost;
            cmd.Parameters.Add(vrednost);

            SqlParameter bruto = new SqlParameter();
            bruto.ParameterName = "@Bruto";
            bruto.DbType = DbType.Decimal;
            bruto.Direction = ParameterDirection.Input;
            bruto.Value = Bruto;
            cmd.Parameters.Add(bruto);

            SqlParameter koleta = new SqlParameter();
            koleta.ParameterName = "@Koleta";
            koleta.SqlDbType = SqlDbType.Decimal;
            koleta.Direction = ParameterDirection.Input;
            koleta.Value = Koleta;
            cmd.Parameters.Add(koleta);


            SqlParameter jm = new SqlParameter();
            jm.ParameterName = "@JM";
            jm.SqlDbType = SqlDbType.NVarChar;
            jm.Direction = ParameterDirection.Input;
            jm.Value = JM;
            cmd.Parameters.Add(jm);

            SqlParameter naimenovanje = new SqlParameter();
            naimenovanje.ParameterName = "@Naimenovanje";
            naimenovanje.SqlDbType = SqlDbType.NVarChar;
            naimenovanje.Direction = ParameterDirection.Input;
            naimenovanje.Value = Naimenovanje;
            cmd.Parameters.Add(naimenovanje);


            SqlParameter naziv = new SqlParameter();
            naziv.ParameterName = "@Naziv";
            naziv.SqlDbType = SqlDbType.NVarChar;
            naziv.Direction = ParameterDirection.Input;
            naziv.Value = Naziv;
            cmd.Parameters.Add(naziv);


            SqlParameter nhm = new SqlParameter();
            nhm.ParameterName = "@NHM";
            nhm.SqlDbType = SqlDbType.Int;
            nhm.Direction = ParameterDirection.Input;
            nhm.Value = NHM;
            cmd.Parameters.Add(nhm);

            SqlParameter rb = new SqlParameter();
            rb.ParameterName = "@RB";
            rb.SqlDbType = SqlDbType.Int;
            rb.Direction = ParameterDirection.Input;
            rb.Value = RB;
            cmd.Parameters.Add(rb);

            SqlParameter idNadredjena = new SqlParameter();
            idNadredjena.ParameterName = "@IDNadredjena";
            idNadredjena.SqlDbType = SqlDbType.Int;
            idNadredjena.Direction = ParameterDirection.Input;
            idNadredjena.Value = IDNadredjena;
            cmd.Parameters.Add(idNadredjena);

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

            catch (SqlException ex)
            {
                throw new Exception(ex.Message.ToString());
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

        public void InsertNalogRukovalac(int Prijemnica, int Rukovalac, string Pozicija, decimal Koleta, int Paleta, int PaletaTip, decimal Bruto, int DodatneUsluge, string Napomena,
            string Vozilo, int Postupak, string Izdao)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertRNCarinskoRukovalac";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter izdao = new SqlParameter();
            izdao.ParameterName = "@Izdao";
            izdao.SqlDbType = SqlDbType.NVarChar;
            izdao.Direction = ParameterDirection.Input;
            izdao.Value = Izdao;
            cmd.Parameters.Add(izdao);

            SqlParameter postupak = new SqlParameter();
            postupak.ParameterName = "@Postupak";
            postupak.SqlDbType = SqlDbType.Int;
            postupak.Direction = ParameterDirection.Input;
            postupak.Value = Postupak;
            cmd.Parameters.Add(postupak);

            SqlParameter vozilo = new SqlParameter();
            vozilo.ParameterName = "@Vozilo";
            vozilo.SqlDbType = SqlDbType.NVarChar;
            vozilo.Direction = ParameterDirection.Input;
            vozilo.Value = Vozilo;
            cmd.Parameters.Add(vozilo);


            SqlParameter napomena = new SqlParameter();
            napomena.ParameterName = "@Napomena";
            napomena.SqlDbType = SqlDbType.NVarChar;
            napomena.Direction = ParameterDirection.Input;
            napomena.Value = Napomena;
            cmd.Parameters.Add(napomena);


            SqlParameter dodatneUsluge = new SqlParameter();
            dodatneUsluge.ParameterName = "@DodatneUslugeID";
            dodatneUsluge.SqlDbType = SqlDbType.Int;
            dodatneUsluge.Direction = ParameterDirection.Input;
            dodatneUsluge.Value = DodatneUsluge;
            cmd.Parameters.Add(dodatneUsluge);

            SqlParameter bruto = new SqlParameter();
            bruto.ParameterName = "@Bruto";
            bruto.DbType = DbType.Decimal;
            bruto.Direction = ParameterDirection.Input;
            bruto.Value = Bruto;
            cmd.Parameters.Add(bruto);


            SqlParameter paleta = new SqlParameter();
            paleta.ParameterName = "@Paleta";
            paleta.SqlDbType = SqlDbType.Int;
            paleta.Direction = ParameterDirection.Input;
            paleta.Value = Paleta;
            cmd.Parameters.Add(paleta);

            SqlParameter paletaTip = new SqlParameter();
            paletaTip.ParameterName = "@PaletaTip";
            paletaTip.SqlDbType = SqlDbType.Int;
            paletaTip.Direction = ParameterDirection.Input;
            paletaTip.Value = PaletaTip;
            cmd.Parameters.Add(paletaTip);


            SqlParameter koleta = new SqlParameter();
            koleta.ParameterName = "@Koleta";
            koleta.SqlDbType = SqlDbType.Decimal;
            koleta.Direction = ParameterDirection.Input;
            koleta.Value = Koleta;
            cmd.Parameters.Add(koleta);

            SqlParameter pozicija = new SqlParameter();
            pozicija.ParameterName = "@Pozicija";
            pozicija.SqlDbType = SqlDbType.NVarChar;
            pozicija.Direction = ParameterDirection.Input;
            pozicija.Value = Pozicija;
            cmd.Parameters.Add(pozicija);

            SqlParameter rukovalac = new SqlParameter();
            rukovalac.ParameterName = "@Rukovalac";
            rukovalac.SqlDbType = SqlDbType.Int;
            rukovalac.Direction = ParameterDirection.Input;
            rukovalac.Value = Rukovalac;
            cmd.Parameters.Add(rukovalac);

            SqlParameter prijemnica = new SqlParameter();
            prijemnica.ParameterName = "@Prijemnica";
            prijemnica.SqlDbType = SqlDbType.Int;
            prijemnica.Direction = ParameterDirection.Input;
            prijemnica.Value = Prijemnica;
            cmd.Parameters.Add(prijemnica);

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

            catch (SqlException ex)
            {
                throw new Exception(ex.Message.ToString());
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
        public void RadniNalogStorniranInterni(int IDInterni, int RN)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "StornoRadniNalogInterni";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter interni = new SqlParameter();
            interni.ParameterName = "@IDInterni";
            interni.SqlDbType = SqlDbType.Int;
            interni.Direction = ParameterDirection.Input;
            interni.Value = IDInterni;
            cmd.Parameters.Add(interni);

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

            catch (SqlException ex)
            {
                throw new Exception(ex.Message.ToString());
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

        public void InsertOtpremaStavke(int RB, int NHM, string Naziv, string Naimenovanje, string JM, decimal Koleta, decimal Bruto, decimal Vrednost,
            string Valuta, string Pozicija, int Paleta, int VrstaPaleta, decimal PDV, decimal Carina, decimal Neto, string Napomena,string Kreirao,int Prijemnica)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertRnCarinskoSkladisteOtpremaStavkePom";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter prijemnica = new SqlParameter();
            prijemnica.ParameterName = "@Prijemnica";
            prijemnica.DbType = DbType.Int32;
            prijemnica.Direction= ParameterDirection.Input;
            prijemnica.Value = Prijemnica;
            cmd.Parameters.Add(prijemnica);

            SqlParameter kreirao = new SqlParameter();
            kreirao.ParameterName = "@Kreirao";
            kreirao.SqlDbType = SqlDbType.NVarChar;
            kreirao.Direction = ParameterDirection.Input;
            kreirao.Value= Kreirao;
            cmd.Parameters.Add(kreirao);

            SqlParameter napomena = new SqlParameter();
            napomena.ParameterName = "@Napomena";
            napomena.SqlDbType = SqlDbType.NVarChar;
            napomena.Direction = ParameterDirection.Input;
            napomena.Value = Napomena;
            cmd.Parameters.Add(napomena);


            SqlParameter neto = new SqlParameter();
            neto.ParameterName = "@Neto";
            neto.DbType = DbType.Decimal;
            neto.Direction = ParameterDirection.Input;
            neto.Value = Neto;
            cmd.Parameters.Add(neto);

            SqlParameter carina = new SqlParameter();
            carina.ParameterName = "@Carina";
            carina.DbType = DbType.Decimal;
            carina.Direction = ParameterDirection.Input;
            carina.Value = Carina;
            cmd.Parameters.Add(carina);

            SqlParameter pdv = new SqlParameter();
            pdv.ParameterName = "@PDV";
            pdv.DbType = DbType.Decimal;
            pdv.Direction = ParameterDirection.Input;
            pdv.Value = PDV;
            cmd.Parameters.Add(pdv);

            SqlParameter vrstaPalete = new SqlParameter();
            vrstaPalete.ParameterName = "@VrstaPaleta";
            vrstaPalete.SqlDbType = SqlDbType.Int;
            vrstaPalete.Direction = ParameterDirection.Input;
            vrstaPalete.Value = VrstaPaleta;
            cmd.Parameters.Add(vrstaPalete);

            SqlParameter paleta = new SqlParameter();
            paleta.ParameterName = "@Paleta";
            paleta.SqlDbType = SqlDbType.Int;
            paleta.Direction = ParameterDirection.Input;
            paleta.Value = Paleta;
            cmd.Parameters.Add(paleta);


            SqlParameter pozicija = new SqlParameter();
            pozicija.ParameterName = "@Pozicija";
            pozicija.SqlDbType = SqlDbType.NVarChar;
            pozicija.Direction = ParameterDirection.Input;
            pozicija.Value = Pozicija;
            cmd.Parameters.Add(pozicija);

            SqlParameter valuta = new SqlParameter();
            valuta.ParameterName = "@Valuta";
            valuta.SqlDbType = SqlDbType.NVarChar;
            valuta.Direction = ParameterDirection.Input;
            valuta.Value = Valuta;
            cmd.Parameters.Add(valuta);

            SqlParameter vrednost = new SqlParameter();
            vrednost.ParameterName = "@Vrednost";
            vrednost.DbType = DbType.Decimal;
            vrednost.Direction = ParameterDirection.Input;
            vrednost.Value = Vrednost;
            cmd.Parameters.Add(vrednost);

            SqlParameter bruto = new SqlParameter();
            bruto.ParameterName = "@Bruto";
            bruto.DbType = DbType.Decimal;
            bruto.Direction = ParameterDirection.Input;
            bruto.Value = Bruto;
            cmd.Parameters.Add(bruto);

            SqlParameter koleta = new SqlParameter();
            koleta.ParameterName = "@Koleta";
            koleta.SqlDbType = SqlDbType.Decimal;
            koleta.Direction = ParameterDirection.Input;
            koleta.Value = Koleta;
            cmd.Parameters.Add(koleta);


            SqlParameter jm = new SqlParameter();
            jm.ParameterName = "@JM";
            jm.SqlDbType = SqlDbType.NVarChar;
            jm.Direction = ParameterDirection.Input;
            jm.Value = JM;
            cmd.Parameters.Add(jm);

            SqlParameter naimenovanje = new SqlParameter();
            naimenovanje.ParameterName = "@Naimenovanje";
            naimenovanje.SqlDbType = SqlDbType.NVarChar;
            naimenovanje.Direction = ParameterDirection.Input;
            naimenovanje.Value = Naimenovanje;
            cmd.Parameters.Add(naimenovanje);


            SqlParameter naziv = new SqlParameter();
            naziv.ParameterName = "@Naziv";
            naziv.SqlDbType = SqlDbType.NVarChar;
            naziv.Direction = ParameterDirection.Input;
            naziv.Value = Naziv;
            cmd.Parameters.Add(naziv);


            SqlParameter nhm = new SqlParameter();
            nhm.ParameterName = "@NHM";
            nhm.SqlDbType = SqlDbType.Int;
            nhm.Direction = ParameterDirection.Input;
            nhm.Value = NHM;
            cmd.Parameters.Add(nhm);

            SqlParameter rb = new SqlParameter();
            rb.ParameterName = "@RB";
            rb.SqlDbType = SqlDbType.Int;
            rb.Direction = ParameterDirection.Input;
            rb.Value = RB;
            cmd.Parameters.Add(rb);

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

            catch (SqlException ex)
            {
                throw new Exception(ex.Message.ToString());
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
        public void InsertCarinskaOtpremnica(string Kreirao,int RN,int CarinskiPostupak,string DokumentRazduzenja,DateTime DatumRazduzenja,
            string Destinacija,DateTime DatumOtpreme,int Prijemnica)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertRNCarinskoSkladisteOtpremnica";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter kreirao = new SqlParameter();
            kreirao.ParameterName = "@Kreirao";
            kreirao.SqlDbType = SqlDbType.NVarChar;
            kreirao.Direction = ParameterDirection.Input;
            kreirao.Value = Kreirao;
            cmd.Parameters.Add(kreirao);


            SqlParameter rn = new SqlParameter();
            rn.ParameterName = "@RN";
            rn.SqlDbType = SqlDbType.Int;
            rn.Direction = ParameterDirection.Input;
            rn.Value = RN;
            cmd.Parameters.Add(rn);

            SqlParameter postupak = new SqlParameter();
            postupak.ParameterName = "@CarinskiPostupak";
            postupak.SqlDbType = SqlDbType.Int;
            postupak.Direction = ParameterDirection.Input;
            postupak.Value = CarinskiPostupak;
            cmd.Parameters.Add(postupak);

            SqlParameter dokumentRazduzenja = new SqlParameter();
            dokumentRazduzenja.ParameterName = "@DokumentRazduzenja";
            dokumentRazduzenja.SqlDbType= SqlDbType.NVarChar;
            dokumentRazduzenja.Direction= ParameterDirection.Input;
            dokumentRazduzenja.Value = DokumentRazduzenja;
            cmd.Parameters.Add(dokumentRazduzenja);

            SqlParameter datumRazduzenja = new SqlParameter();
            datumRazduzenja.ParameterName = "@DatumRazduzenja";
            datumRazduzenja.SqlDbType = SqlDbType.DateTime;
            datumRazduzenja.Direction=ParameterDirection.Input;
            datumRazduzenja.Value = DatumRazduzenja;
            cmd.Parameters.Add(datumRazduzenja);

            SqlParameter destinacija = new SqlParameter();
            destinacija.ParameterName = "@Destinacija";
            destinacija.SqlDbType = SqlDbType.NVarChar;
            destinacija.Direction = ParameterDirection.Input;
            destinacija.Value = Destinacija;
            cmd.Parameters.Add(destinacija);

            SqlParameter datumOtpreme = new SqlParameter();
            datumOtpreme.ParameterName = "@DatumOtpreme";
            datumOtpreme.SqlDbType = SqlDbType.DateTime;
            datumOtpreme.Direction=ParameterDirection.Input;
            datumOtpreme.Value= DatumOtpreme;
            cmd.Parameters.Add(datumOtpreme);

            SqlParameter prijemnica = new SqlParameter();
            prijemnica.ParameterName = "@Prijemnica";
            prijemnica.SqlDbType = SqlDbType.Int;
            prijemnica.Direction = ParameterDirection.Input;
            prijemnica.Value = Prijemnica;
            cmd.Parameters.Add(prijemnica);


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

            catch (SqlException ex)
            {
                throw new Exception(ex.Message.ToString());
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
        public void UpdateCarinskaOtpremnica(int ID,string Kreirao, int RN, int CarinskiPostupak, string DokumentRazduzenja, DateTime DatumRazduzenja,
           string Destinacija, DateTime DatumOtpreme, int Prijemnica)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateRNCarinskoSkladisteOtpremnica";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter kreirao = new SqlParameter();
            kreirao.ParameterName = "@Kreirao";
            kreirao.SqlDbType = SqlDbType.NVarChar;
            kreirao.Direction = ParameterDirection.Input;
            kreirao.Value = Kreirao;
            cmd.Parameters.Add(kreirao);


            SqlParameter rn = new SqlParameter();
            rn.ParameterName = "@RN";
            rn.SqlDbType = SqlDbType.Int;
            rn.Direction = ParameterDirection.Input;
            rn.Value = RN;
            cmd.Parameters.Add(rn);

            SqlParameter postupak = new SqlParameter();
            postupak.ParameterName = "@CarinskiPostupak";
            postupak.SqlDbType = SqlDbType.Int;
            postupak.Direction = ParameterDirection.Input;
            postupak.Value = CarinskiPostupak;
            cmd.Parameters.Add(postupak);

            SqlParameter dokumentRazduzenja = new SqlParameter();
            dokumentRazduzenja.ParameterName = "@DokumentRazduzenja";
            dokumentRazduzenja.SqlDbType = SqlDbType.NVarChar;
            dokumentRazduzenja.Direction = ParameterDirection.Input;
            dokumentRazduzenja.Value = DokumentRazduzenja;
            cmd.Parameters.Add(dokumentRazduzenja);

            SqlParameter datumRazduzenja = new SqlParameter();
            datumRazduzenja.ParameterName = "@DatumRazduzenja";
            datumRazduzenja.SqlDbType = SqlDbType.DateTime;
            datumRazduzenja.Direction = ParameterDirection.Input;
            datumRazduzenja.Value = DatumRazduzenja;
            cmd.Parameters.Add(datumRazduzenja);

            SqlParameter destinacija = new SqlParameter();
            destinacija.ParameterName = "@Destinacija";
            destinacija.SqlDbType = SqlDbType.NVarChar;
            destinacija.Direction = ParameterDirection.Input;
            destinacija.Value = Destinacija;
            cmd.Parameters.Add(destinacija);

            SqlParameter datumOtpreme = new SqlParameter();
            datumOtpreme.ParameterName = "@DatumOtpreme";
            datumOtpreme.SqlDbType = SqlDbType.DateTime;
            datumOtpreme.Direction = ParameterDirection.Input;
            datumOtpreme.Value = DatumOtpreme;
            cmd.Parameters.Add(datumOtpreme);

            SqlParameter prijemnica = new SqlParameter();
            prijemnica.ParameterName = "@Prijemnica";
            prijemnica.SqlDbType = SqlDbType.Int;
            prijemnica.Direction = ParameterDirection.Input;
            prijemnica.Value = Prijemnica;
            cmd.Parameters.Add(prijemnica);


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

            catch (SqlException ex)
            {
                throw new Exception(ex.Message.ToString());
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
