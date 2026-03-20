using Saobracaj.eDokumenta;
using Syncfusion.ProjIO;
using Syncfusion.Windows.Forms.Tools.XPMenus;
using Syncfusion.WinForms.Controls.Styles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
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
        public void InsertRN(string TipRn,string CarinskoSkladiste,int MagacinskiBroj,int Nalogodavac,int VlasnikRobe,string VrstaRobe,string NacinPakovanja,int OstalaSkladista,
            int PIB,int VrstaPrevoznogSredstva,int VrstaKamiona,string Vozilo,string Vozac,string BrojLk,string Telefon,int OdredisnaCarinarnica,int Spediter,string KontakOsobaSpeditera,
            int MestoIstovara,string Adresa,string KontaktOsobaIstovar,DateTime PlaniraniDatum,DateTime PlaniraniDatum2,string PosebniUslovi,int DodatneUslugeID,string Napomena,int Aktivan,int Formiran,
            string Korisnik,string BrojKontejnera)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertRNCarinskoSkladiste";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter kontejner = new SqlParameter();
            kontejner.ParameterName = "@BrojKontejnera";
            kontejner.SqlDbType = SqlDbType.NVarChar;
            kontejner.Direction = ParameterDirection.Input;
            kontejner.Value = BrojKontejnera;
            cmd.Parameters.Add(kontejner);

            SqlParameter korisnik = new SqlParameter();
            korisnik.ParameterName = "@Korisnik";
            korisnik.SqlDbType = SqlDbType.NVarChar;
            korisnik.Direction= ParameterDirection.Input;
            korisnik.Value = Korisnik;
            cmd.Parameters.Add(korisnik);

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
        public void UpdateRN(int ID,string TipRn, string CarinskoSkladiste, int MagacinskiBroj, int Nalogodavac, int VlasnikRobe, string VrstaRobe, string NacinPakovanja, int OstalaSkladista,
            int PIB, int VrstaPrevoznogSredstva, int VrstaKamiona, string Vozilo, string Vozac, string BrojLk, string Telefon, int OdredisnaCarinarnica, int Spediter, string KontakOsobaSpeditera,
            int MestoIstovara, string Adresa, string KontaktOsobaIstovar, DateTime PlaniraniDatum, DateTime PlaniraniDatum2, string PosebniUslovi, int DodatneUslugeID, 
            string Napomena, int Aktivan, int Formiran,string Status,string Korisnik,string BrojKontejnera)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateRNCarinskoSkladiste";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter kontejner=new SqlParameter();
            kontejner.ParameterName = "@BrojKontejnera";
            kontejner.SqlDbType = SqlDbType.NVarChar;
            kontejner.Direction = ParameterDirection.Input;
            kontejner.Value = BrojKontejnera;
            cmd.Parameters.Add(kontejner);

            SqlParameter korisnik = new SqlParameter();
            korisnik.ParameterName= "@Korisnik";
            korisnik.SqlDbType = SqlDbType.NVarChar;
            korisnik.Direction = ParameterDirection.Input;
            korisnik.Value = Korisnik;
            cmd.Parameters.Add(korisnik);

            SqlParameter status = new SqlParameter();
            status.ParameterName = "@Status";
            status.SqlDbType = SqlDbType.NVarChar;
            status.Direction = ParameterDirection.Input;
            status.Value = Status;
            cmd.Parameters.Add(status);

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


        public void InsertMagacinskiBrojCarinski(int ID,string Naziv)
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


        public void InsertPrijemnicaCarinska(string Kreirao,int RN,int? CarinskiPostupak,string SmestajniDokument,string Rok,int? Posiljalac,string Faktura,string CRM,string Status)
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

            SqlParameter postupak=new SqlParameter();
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

            SqlParameter status= new SqlParameter();
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

        public void UpdatePrijemnicaCarinska(string Kreirao, int ID,int RN, int? CarinskiPostupak, string SmestajniDokument, string Rok, int? Posiljalac, string Faktura, string CRM, string Status,int Proces)
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

        public void InsertPrijemnicaCarinskaStavke(int IDNadredjena,int RB,int NHM,string Naziv,string Naimenovanje,string JM,decimal Koleta,decimal Bruto,decimal Vrednost,
            string Valuta,string Pozicija,int Paleta,int VrstaPaleta,int PDV,int Carina)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertRNCarinskoPrijemnicaStavke";
            cmd.CommandType = CommandType.StoredProcedure;

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

            SqlParameter bruto= new SqlParameter();
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
            naziv.ParameterName= "@Naziv";
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

        public void InsertNalogRukovalac(int ID,int Prijemnica,int Rukovalac,string Pozicija,decimal Koleta,int Paleta,int PaletaTip,decimal Bruto,int DodatneUsluge,string Napomena,
            string Vozilo,int Postupak,string Izdao)
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

            SqlParameter id = new SqlParameter();
            id.ParameterName= "@ID";
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
    }
}
