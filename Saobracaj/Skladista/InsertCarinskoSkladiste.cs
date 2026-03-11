using Syncfusion.ProjIO;
using Syncfusion.Windows.Forms.Tools.XPMenus;
using Syncfusion.WinForms.Controls.Styles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Skladista
{
    public class InsertCarinskoSkladiste
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
        public void InsertDodatneUsluge(int ID,int RN,int Usluga)
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
        public void InsertRN(string TipRn,string CarinskoSkladiste,int MagacinskiBroj,string TipMB,int Nalogodavac,int VlasnikRobe,string VrstaRobe,string NacinPakovanja,int OstalaSkladista,
            decimal VrednostRobe,string Valuta,
            int PIB,int VrstaPrevoznogSredstva,int VrstaKamiona,string Vozilo,string Vozac,string BrojLk,string Telefon,int OdredisnaCarinarnica,int Spediter,string KontakOsobaSpeditera,
            int MestoIstovara,string Adresa,string KontaktOsobaIstovar,DateTime PlaniraniDatum,DateTime PlaniraniDatum2,string PosebniUslovi,int DodatneUslugeID,string Napomena,int Aktivan,int Formiran)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertRNCarinskoSkladiste";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter formiran = new SqlParameter();
            formiran.ParameterName = "@Formiran";
            formiran.SqlDbType = SqlDbType.Int;
            formiran.Direction = ParameterDirection.Input;
            formiran.Value = Formiran;
            cmd.Parameters.Add(formiran);

            SqlParameter aktivan = new SqlParameter();
            aktivan.ParameterName = "@Aktivan";
            aktivan.SqlDbType = SqlDbType.Int;
            aktivan.Direction = ParameterDirection.Input;
            aktivan.Value = Aktivan;
            cmd.Parameters.Add(aktivan);

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
            dodatneUsluge.Value = DodatneUslugeID;
            cmd.Parameters.Add(dodatneUsluge);

            SqlParameter posebniUslovi = new SqlParameter();
            posebniUslovi.ParameterName = "@PosebniUslovi";
            posebniUslovi.SqlDbType = SqlDbType.NVarChar;
            posebniUslovi.Direction = ParameterDirection.Input;
            posebniUslovi.Value = PosebniUslovi;
            cmd.Parameters.Add(posebniUslovi);

            SqlParameter planiraniDatum2 = new SqlParameter();
            planiraniDatum2.ParameterName = "@PlaniraniDatum2";
            planiraniDatum2.SqlDbType = SqlDbType.DateTime;
            planiraniDatum2.Direction = ParameterDirection.Input;
            planiraniDatum2.Value = PlaniraniDatum2;
            cmd.Parameters.Add(planiraniDatum2);

            SqlParameter planiraniDatum = new SqlParameter();
            planiraniDatum.ParameterName = "@PlaniraniDatum";
            planiraniDatum.SqlDbType = SqlDbType.DateTime;
            planiraniDatum.Direction = ParameterDirection.Input;
            planiraniDatum.Value = PlaniraniDatum;
            cmd.Parameters.Add(planiraniDatum);

            SqlParameter kontaktOsobaIstovar = new SqlParameter();
            kontaktOsobaIstovar.ParameterName = "@KontaktOsobaIstovar";
            kontaktOsobaIstovar.SqlDbType = SqlDbType.NVarChar;
            kontaktOsobaIstovar.Direction = ParameterDirection.Input;
            kontaktOsobaIstovar.Value = KontaktOsobaIstovar;
            cmd.Parameters.Add(kontaktOsobaIstovar);

            SqlParameter adresa = new SqlParameter();
            adresa.ParameterName = "@Adresa";
            adresa.SqlDbType = SqlDbType.NVarChar;
            adresa.Direction = ParameterDirection.Input;
            adresa.Value = Adresa;
            cmd.Parameters.Add(adresa);

            SqlParameter mestoIstovara = new SqlParameter();
            mestoIstovara.ParameterName = "@MestoIstovara";
            mestoIstovara.SqlDbType = SqlDbType.Int;
            mestoIstovara.Direction = ParameterDirection.Input;
            mestoIstovara.Value = MestoIstovara;
            cmd.Parameters.Add(mestoIstovara);

            SqlParameter kontakOsobaSpediter = new SqlParameter();
            kontakOsobaSpediter.ParameterName = "@KontaktOsobaSpeditera";
            kontakOsobaSpediter.SqlDbType = SqlDbType.NVarChar;
            kontakOsobaSpediter.Direction = ParameterDirection.Input;
            kontakOsobaSpediter.Value = KontakOsobaSpeditera;
            cmd.Parameters.Add(kontakOsobaSpediter);


            SqlParameter spediter = new SqlParameter();
            spediter.ParameterName = "@Spediter";
            spediter.SqlDbType = SqlDbType.Int;
            spediter.Direction = ParameterDirection.Input;
            spediter.Value = Spediter;
            cmd.Parameters.Add(spediter);

            SqlParameter odredisnaCarinarnica = new SqlParameter();
            odredisnaCarinarnica.ParameterName = "@OdredisnaCarinarnica";
            odredisnaCarinarnica.SqlDbType = SqlDbType.Int;
            odredisnaCarinarnica.Direction = ParameterDirection.Input;
            odredisnaCarinarnica.Value = OdredisnaCarinarnica;
            cmd.Parameters.Add(odredisnaCarinarnica);

            SqlParameter telefon = new SqlParameter();
            telefon.ParameterName = "@BrojTelefona";
            telefon.SqlDbType = SqlDbType.NVarChar;
            telefon.Direction = ParameterDirection.Input;
            telefon.Value = Telefon;
            cmd.Parameters.Add(telefon);


            SqlParameter brojLk = new SqlParameter();
            brojLk.ParameterName = "@BrojLK";
            brojLk.SqlDbType = SqlDbType.NVarChar;
            brojLk.Direction = ParameterDirection.Input;
            brojLk.Value = BrojLk;
            cmd.Parameters.Add(brojLk);

            SqlParameter vozac = new SqlParameter();
            vozac.ParameterName = "@Vozac";
            vozac.SqlDbType = SqlDbType.NVarChar;
            vozac.Direction = ParameterDirection.Input;
            vozac.Value = Vozac;
            cmd.Parameters.Add(vozac);

            SqlParameter vozilo = new SqlParameter();
            vozilo.ParameterName = "@Vozilo";
            vozilo.SqlDbType = SqlDbType.NVarChar;
            vozilo.Direction = ParameterDirection.Input;
            vozilo.Value = Vozilo;
            cmd.Parameters.Add(vozilo);

            SqlParameter vrstaKamiona = new SqlParameter();
            vrstaKamiona.ParameterName = "@VrstaKamiona";
            vrstaKamiona.SqlDbType = SqlDbType.NVarChar;
            vrstaKamiona.Direction = ParameterDirection.Input;
            vrstaKamiona.Value = VrstaKamiona;
            cmd.Parameters.Add(vrstaKamiona);

            SqlParameter vrstaPrevoznogSredstva = new SqlParameter();
            vrstaPrevoznogSredstva.ParameterName = "@VrstaPrevoznogSredstva";
            vrstaPrevoznogSredstva.SqlDbType = SqlDbType.Int;
            vrstaPrevoznogSredstva.Direction = ParameterDirection.Input;
            vrstaPrevoznogSredstva.Value = VrstaPrevoznogSredstva;
            cmd.Parameters.Add(vrstaPrevoznogSredstva);

            SqlParameter pib = new SqlParameter();
            pib.ParameterName = "@PIB";
            pib.SqlDbType = SqlDbType.NVarChar;
            pib.Direction = ParameterDirection.Input;
            pib.Value = PIB;
            cmd.Parameters.Add(pib);

            SqlParameter tipRn = new SqlParameter();
            tipRn.ParameterName = "@TipRN";
            tipRn.SqlDbType = SqlDbType.NVarChar;
            tipRn.Direction = ParameterDirection.Input;
            tipRn.Value = TipRn;
            cmd.Parameters.Add(tipRn);

            SqlParameter carinskoSkladiste = new SqlParameter();
            carinskoSkladiste.ParameterName = "@CarinskoSkladiste";
            carinskoSkladiste.SqlDbType = SqlDbType.NVarChar;
            carinskoSkladiste.Direction = ParameterDirection.Input;
            carinskoSkladiste.Value = CarinskoSkladiste;
            cmd.Parameters.Add(carinskoSkladiste);

            SqlParameter magacinskiBroj = new SqlParameter();
            magacinskiBroj.ParameterName = "@MagacinskiBroj";
            magacinskiBroj.SqlDbType = SqlDbType.Int;
            magacinskiBroj.Direction = ParameterDirection.Input;
            magacinskiBroj.Value = MagacinskiBroj;
            cmd.Parameters.Add(magacinskiBroj);

            SqlParameter tipMb = new SqlParameter();
            tipMb.ParameterName = "@TipMB";
            tipMb.SqlDbType = SqlDbType.NVarChar;
            tipMb.Direction = ParameterDirection.Input;
            tipMb.Value = TipMB;
            cmd.Parameters.Add(tipMb);

            SqlParameter nalogodavac = new SqlParameter();
            nalogodavac.ParameterName = "@Nalogodavac";
            nalogodavac.SqlDbType = SqlDbType.Int;
            nalogodavac.Direction = ParameterDirection.Input;
            nalogodavac.Value = Nalogodavac;
            cmd.Parameters.Add(nalogodavac);

            SqlParameter vlasnikRobe = new SqlParameter();
            vlasnikRobe.ParameterName = "@VlasnikRobe";
            vlasnikRobe.SqlDbType = SqlDbType.Int;
            vlasnikRobe.Direction = ParameterDirection.Input;
            vlasnikRobe.Value = VlasnikRobe;
            cmd.Parameters.Add(vlasnikRobe);

            SqlParameter vrstaRobe = new SqlParameter();
            vrstaRobe.ParameterName = "@VrstaRobe";
            vrstaRobe.SqlDbType = SqlDbType.NVarChar;
            vrstaRobe.Direction = ParameterDirection.Input;
            vrstaRobe.Value = VrstaRobe;
            cmd.Parameters.Add(vrstaRobe);

            SqlParameter ostalaSkladista = new SqlParameter();
            ostalaSkladista.ParameterName = "@OstalaSkladista";
            ostalaSkladista.SqlDbType = SqlDbType.Int;
            ostalaSkladista.Direction = ParameterDirection.Input;
            ostalaSkladista.Value = OstalaSkladista;
            cmd.Parameters.Add(ostalaSkladista);

            SqlParameter vrednostRobe = new SqlParameter();
            vrednostRobe.ParameterName = "@VrednostRobe";
            vrednostRobe.SqlDbType = SqlDbType.Decimal;
            vrednostRobe.Direction = ParameterDirection.Input;
            vrednostRobe.Value = VrednostRobe;
            cmd.Parameters.Add(vrednostRobe);

            SqlParameter valuta = new SqlParameter();
            valuta.ParameterName = "@Valuta";
            valuta.SqlDbType = SqlDbType.NVarChar;
            valuta.Direction = ParameterDirection.Input;
            valuta.Value = Valuta;
            cmd.Parameters.Add(valuta);

            SqlParameter nacinPakovanja = new SqlParameter();
            nacinPakovanja.ParameterName = "@NacinPakovanja";
            nacinPakovanja.SqlDbType = SqlDbType.NVarChar;
            nacinPakovanja.Direction = ParameterDirection.Input;
            nacinPakovanja.Value = NacinPakovanja;
            cmd.Parameters.Add(nacinPakovanja);

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
        public void UpdateRN(int ID,string TipRn, string CarinskoSkladiste, int MagacinskiBroj, string TipMB, int Nalogodavac, int VlasnikRobe, string VrstaRobe, string NacinPakovanja, int OstalaSkladista,
            decimal VrednostRobe, string Valuta,
            int PIB, int VrstaPrevoznogSredstva, int VrstaKamiona, string Vozilo, string Vozac, string BrojLk, string Telefon, int OdredisnaCarinarnica, int Spediter, string KontakOsobaSpeditera,
            int MestoIstovara, string Adresa, string KontaktOsobaIstovar, DateTime PlaniraniDatum, DateTime PlaniraniDatum2, string PosebniUslovi, int DodatneUslugeID, string Napomena, int Aktivan, int Formiran)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateRNCarinskoSkladiste";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);


            SqlParameter formiran = new SqlParameter();
            formiran.ParameterName = "@Formiran";
            formiran.SqlDbType = SqlDbType.Int;
            formiran.Direction = ParameterDirection.Input;
            formiran.Value = Formiran;
            cmd.Parameters.Add(formiran);

            SqlParameter aktivan = new SqlParameter();
            aktivan.ParameterName = "@Aktivan";
            aktivan.SqlDbType = SqlDbType.Int;
            aktivan.Direction = ParameterDirection.Input;
            aktivan.Value = Aktivan;
            cmd.Parameters.Add(aktivan);

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
            dodatneUsluge.Value = DodatneUslugeID;
            cmd.Parameters.Add(dodatneUsluge);

            SqlParameter posebniUslovi = new SqlParameter();
            posebniUslovi.ParameterName = "@PosebniUslovi";
            posebniUslovi.SqlDbType = SqlDbType.NVarChar;
            posebniUslovi.Direction = ParameterDirection.Input;
            posebniUslovi.Value = PosebniUslovi;
            cmd.Parameters.Add(posebniUslovi);

            SqlParameter planiraniDatum2 = new SqlParameter();
            planiraniDatum2.ParameterName = "@PlaniraniDatum2";
            planiraniDatum2.SqlDbType = SqlDbType.DateTime;
            planiraniDatum2.Direction = ParameterDirection.Input;
            planiraniDatum2.Value = PlaniraniDatum2;
            cmd.Parameters.Add(planiraniDatum2);

            SqlParameter planiraniDatum = new SqlParameter();
            planiraniDatum.ParameterName = "@PlaniraniDatum";
            planiraniDatum.SqlDbType = SqlDbType.DateTime;
            planiraniDatum.Direction = ParameterDirection.Input;
            planiraniDatum.Value = PlaniraniDatum;
            cmd.Parameters.Add(planiraniDatum);

            SqlParameter kontaktOsobaIstovar = new SqlParameter();
            kontaktOsobaIstovar.ParameterName = "@KontaktOsobaIstovar";
            kontaktOsobaIstovar.SqlDbType = SqlDbType.NVarChar;
            kontaktOsobaIstovar.Direction = ParameterDirection.Input;
            kontaktOsobaIstovar.Value = KontaktOsobaIstovar;
            cmd.Parameters.Add(kontaktOsobaIstovar);

            SqlParameter adresa = new SqlParameter();
            adresa.ParameterName = "@Adresa";
            adresa.SqlDbType = SqlDbType.NVarChar;
            adresa.Direction = ParameterDirection.Input;
            adresa.Value = Adresa;
            cmd.Parameters.Add(adresa);

            SqlParameter mestoIstovara = new SqlParameter();
            mestoIstovara.ParameterName = "@MestoIstovara";
            mestoIstovara.SqlDbType = SqlDbType.Int;
            mestoIstovara.Direction = ParameterDirection.Input;
            mestoIstovara.Value = MestoIstovara;
            cmd.Parameters.Add(mestoIstovara);

            SqlParameter kontakOsobaSpediter = new SqlParameter();
            kontakOsobaSpediter.ParameterName = "@KontaktOsobaSpeditera";
            kontakOsobaSpediter.SqlDbType = SqlDbType.NVarChar;
            kontakOsobaSpediter.Direction = ParameterDirection.Input;
            kontakOsobaSpediter.Value = KontakOsobaSpeditera;
            cmd.Parameters.Add(kontakOsobaSpediter);


            SqlParameter spediter = new SqlParameter();
            spediter.ParameterName = "@Spediter";
            spediter.SqlDbType = SqlDbType.Int;
            spediter.Direction = ParameterDirection.Input;
            spediter.Value = Spediter;
            cmd.Parameters.Add(spediter);

            SqlParameter odredisnaCarinarnica = new SqlParameter();
            odredisnaCarinarnica.ParameterName = "@OdredisnaCarinarnica";
            odredisnaCarinarnica.SqlDbType = SqlDbType.Int;
            odredisnaCarinarnica.Direction = ParameterDirection.Input;
            odredisnaCarinarnica.Value = OdredisnaCarinarnica;
            cmd.Parameters.Add(odredisnaCarinarnica);

            SqlParameter telefon = new SqlParameter();
            telefon.ParameterName = "@BrojTelefona";
            telefon.SqlDbType = SqlDbType.NVarChar;
            telefon.Direction = ParameterDirection.Input;
            telefon.Value = Telefon;
            cmd.Parameters.Add(telefon);


            SqlParameter brojLk = new SqlParameter();
            brojLk.ParameterName = "@BrojLK";
            brojLk.SqlDbType = SqlDbType.NVarChar;
            brojLk.Direction = ParameterDirection.Input;
            brojLk.Value = BrojLk;
            cmd.Parameters.Add(brojLk);

            SqlParameter vozac = new SqlParameter();
            vozac.ParameterName = "@Vozac";
            vozac.SqlDbType = SqlDbType.NVarChar;
            vozac.Direction = ParameterDirection.Input;
            vozac.Value = Vozac;
            cmd.Parameters.Add(vozac);

            SqlParameter vozilo = new SqlParameter();
            vozilo.ParameterName = "@Vozilo";
            vozilo.SqlDbType = SqlDbType.NVarChar;
            vozilo.Direction = ParameterDirection.Input;
            vozilo.Value = Vozilo;
            cmd.Parameters.Add(vozilo);

            SqlParameter vrstaKamiona = new SqlParameter();
            vrstaKamiona.ParameterName = "@VrstaKamiona";
            vrstaKamiona.SqlDbType = SqlDbType.NVarChar;
            vrstaKamiona.Direction = ParameterDirection.Input;
            vrstaKamiona.Value = VrstaKamiona;
            cmd.Parameters.Add(vrstaKamiona);

            SqlParameter vrstaPrevoznogSredstva = new SqlParameter();
            vrstaPrevoznogSredstva.ParameterName = "@VrstaPrevoznogSredstva";
            vrstaPrevoznogSredstva.SqlDbType = SqlDbType.Int;
            vrstaPrevoznogSredstva.Direction = ParameterDirection.Input;
            vrstaPrevoznogSredstva.Value = VrstaPrevoznogSredstva;
            cmd.Parameters.Add(vrstaPrevoznogSredstva);

            SqlParameter pib = new SqlParameter();
            pib.ParameterName = "@PIB";
            pib.SqlDbType = SqlDbType.NVarChar;
            pib.Direction = ParameterDirection.Input;
            pib.Value = PIB;
            cmd.Parameters.Add(pib);

            SqlParameter tipRn = new SqlParameter();
            tipRn.ParameterName = "@TipRN";
            tipRn.SqlDbType = SqlDbType.NVarChar;
            tipRn.Direction = ParameterDirection.Input;
            tipRn.Value = TipRn;
            cmd.Parameters.Add(tipRn);

            SqlParameter carinskoSkladiste = new SqlParameter();
            carinskoSkladiste.ParameterName = "@CarinskoSkladiste";
            carinskoSkladiste.SqlDbType = SqlDbType.NVarChar;
            carinskoSkladiste.Direction = ParameterDirection.Input;
            carinskoSkladiste.Value = CarinskoSkladiste;
            cmd.Parameters.Add(carinskoSkladiste);

            SqlParameter magacinskiBroj = new SqlParameter();
            magacinskiBroj.ParameterName = "@MagacinskiBroj";
            magacinskiBroj.SqlDbType = SqlDbType.Int;
            magacinskiBroj.Direction = ParameterDirection.Input;
            magacinskiBroj.Value = MagacinskiBroj;
            cmd.Parameters.Add(magacinskiBroj);

            SqlParameter tipMb = new SqlParameter();
            tipMb.ParameterName = "@TipMB";
            tipMb.SqlDbType = SqlDbType.NVarChar;
            tipMb.Direction = ParameterDirection.Input;
            tipMb.Value = TipMB;
            cmd.Parameters.Add(tipMb);

            SqlParameter nalogodavac = new SqlParameter();
            nalogodavac.ParameterName = "@Nalogodavac";
            nalogodavac.SqlDbType = SqlDbType.Int;
            nalogodavac.Direction = ParameterDirection.Input;
            nalogodavac.Value = Nalogodavac;
            cmd.Parameters.Add(nalogodavac);

            SqlParameter vlasnikRobe = new SqlParameter();
            vlasnikRobe.ParameterName = "@VlasnikRobe";
            vlasnikRobe.SqlDbType = SqlDbType.Int;
            vlasnikRobe.Direction = ParameterDirection.Input;
            vlasnikRobe.Value = VlasnikRobe;
            cmd.Parameters.Add(vlasnikRobe);

            SqlParameter vrstaRobe = new SqlParameter();
            vrstaRobe.ParameterName = "@VrstaRobe";
            vrstaRobe.SqlDbType = SqlDbType.NVarChar;
            vrstaRobe.Direction = ParameterDirection.Input;
            vrstaRobe.Value = VrstaRobe;
            cmd.Parameters.Add(vrstaRobe);

            SqlParameter ostalaSkladista = new SqlParameter();
            ostalaSkladista.ParameterName = "@OstalaSkladista";
            ostalaSkladista.SqlDbType = SqlDbType.Int;
            ostalaSkladista.Direction = ParameterDirection.Input;
            ostalaSkladista.Value = OstalaSkladista;
            cmd.Parameters.Add(ostalaSkladista);

            SqlParameter vrednostRobe = new SqlParameter();
            vrednostRobe.ParameterName = "@VrednostRobe";
            vrednostRobe.SqlDbType = SqlDbType.Decimal;
            vrednostRobe.Direction = ParameterDirection.Input;
            vrednostRobe.Value = VrednostRobe;
            cmd.Parameters.Add(vrednostRobe);

            SqlParameter valuta = new SqlParameter();
            valuta.ParameterName = "@Valuta";
            valuta.SqlDbType = SqlDbType.NVarChar;
            valuta.Direction = ParameterDirection.Input;
            valuta.Value = Valuta;
            cmd.Parameters.Add(valuta);


            SqlParameter nacinPakovanja = new SqlParameter();
            nacinPakovanja.ParameterName = "@NacinPakovanja";
            nacinPakovanja.SqlDbType = SqlDbType.NVarChar;
            nacinPakovanja.Direction = ParameterDirection.Input;
            nacinPakovanja.Value = NacinPakovanja;
            cmd.Parameters.Add(nacinPakovanja);

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
