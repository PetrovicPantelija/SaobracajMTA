using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.HR
{
    // Godišnji odmor u Leget+ (specifikacija 8): ista pravila kao mobilna aplikacija jer obe strane zovu iste procedure
    public static class HRGO
    {
        // Zaposleni koje prijavljeni korisnik sme da bira: HR svi aktivni, šef sebe i svoj tim
        public static string ListaZaposlenih(bool hr, int ja)
        {
            return "SELECT ID, LTRIM(RTRIM(ISNULL(Prezime, N'') + N' ' + ISNULL(Ime, N''))) + ISNULL(N' (' + Code + N')', N'') AS Naziv " +
                   "FROM dbo.Zaposleni WHERE Status = 'A'" +
                   (hr ? "" : " AND (ID = " + ja + " OR ID IN (SELECT Sifra FROM dbo.fn_Podredjeni(" + ja + ")))") +
                   " ORDER BY 2";
        }

        // Proračun za prozor zahteva: radni dani, povratak, stanje tekuće i prošle godine
        public static string Informacija(object zaposleni, DateTime? od, DateTime? @do)
        {
            if (zaposleni == null || !od.HasValue || !@do.HasValue)
                return "";
            if (@do < od)
                return "Datum do je pre datuma od.";
            try
            {
                int dana = HRBaza.CeoBroj(HRBaza.Skalar("SELECT dbo.fnHR_RadniDani(@Od, @Do)", HRBaza.P("@Od", od.Value.Date), HRBaza.P("@Do", @do.Value.Date)));
                DateTime? povratak = HRBaza.Datum(HRBaza.Skalar("SELECT dbo.fnHR_SledeciRadniDan(@Do)", HRBaza.P("@Do", @do.Value.Date)));
                DataTable st = HRBaza.ProceduraTabela("dbo.sp_GO_Stanje", HRBaza.P("@ZaposleniSifra", zaposleni), HRBaza.P("@Godina", od.Value.Year));
                string stanje = "pravo za " + od.Value.Year + ". nije generisano";
                if (st.Rows.Count > 0 && st.Rows[0]["UkupnoDana"] != DBNull.Value)
                {
                    DataRow r = st.Rows[0];
                    stanje = od.Value.Year + ": preostalo " + r["PreostaloDana"] + " od " + r["UkupnoDana"] +
                             (HRBaza.CeoBroj(r["NaCekanjuDana"]) > 0 ? ", na čekanju " + r["NaCekanjuDana"] : "");
                    if (r["PreostaloProsleGodine"] != DBNull.Value && HRBaza.CeoBroj(r["PreostaloProsleGodine"]) > 0)
                        stanje += " · prošla godina: " + r["PreostaloProsleGodine"] + " do " + HRBaza.DatumTekst(r["RokProsleGodine"]);
                }
                return "Radnih dana: " + dana + " · povratak " + HRBaza.DatumTekst(povratak) + " · " + stanje;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private static List<HRPolje> PoljaZahteva(bool hr, int ja, bool izbor)
        {
            return new List<HRPolje>
            {
                new HRPolje("Zaposleni", "Zaposleni", HRTip.Lista, true) { ListaUpit = ListaZaposlenih(hr, ja), Raspon = 2, SamoCitanje = !izbor },
                HRPolje.Datum("DatumOd", "Od", true),
                HRPolje.Datum("DatumDo", "Do (poslednji dan odmora)", true),
                HRPolje.Tekst("Razlog", "Razlog", 100),
                new HRPolje("Info", "Proračun", HRTip.Tekst) { Snima = false, SamoCitanje = true },
                HRPolje.Napomena("Napomena", "Napomena", 500)
            };
        }

        private static void Proracun(frmHRStavka dlg, string kolona)
        {
            if (kolona == "Zaposleni" || kolona == "DatumOd" || kolona == "DatumDo")
                dlg.Postavi("Info", Informacija(dlg.Vrednost("Zaposleni"), HRBaza.Datum(dlg.Vrednost("DatumOd")), HRBaza.Datum(dlg.Vrednost("DatumDo"))));
        }

        // Novi zahtev (Kanal A = Leget+); zaposleni = 0: bira se u prozoru
        public static bool NoviZahtev(IWin32Window vlasnik, int zaposleni, DateTime? od, DateTime? @do, bool hr, int ja)
        {
            if (!hr && ja == 0 && zaposleni == 0)
            {
                MessageBox.Show(vlasnik, "Prijavljeni korisnik nije povezan sa zaposlenim (Korisnici.ZaposleniSifra).", "Zahtev za GO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            var pocetne = new HRVrednosti
            {
                ["Zaposleni"] = zaposleni > 0 ? zaposleni : (ja > 0 ? (object)ja : null),
                ["DatumOd"] = od ?? DateTime.Today.AddDays(1),
                ["DatumDo"] = @do ?? od ?? DateTime.Today.AddDays(1)
            };
            using (var dlg = new frmHRStavka("Novi zahtev za godišnji odmor", PoljaZahteva(hr, ja, zaposleni == 0), pocetne))
            {
                dlg.Promena += Proracun;
                string upozorenje = null;
                dlg.Snimanje = v =>
                {
                    var izlaz = HRBaza.Izlaz("@Upozorenje", SqlDbType.NVarChar, 500);
                    HRBaza.Procedura("dbo.sp_GO_PodnesiZahtev",
                        HRBaza.P("@ZaposleniSifra", v.Uzmi("Zaposleni")), HRBaza.P("@DatumOd", v.Uzmi("DatumOd")), HRBaza.P("@DatumDo", v.Uzmi("DatumDo")),
                        HRBaza.P("@Razlog", v.Uzmi("Razlog")), HRBaza.P("@Napomena", v.Uzmi("Napomena")), HRBaza.P("@Kanal", "A"),
                        HRBaza.P("@PodneoSifra", ja > 0 ? (object)ja : v.Uzmi("Zaposleni")), HRBaza.P("@Korisnik", HRBaza.Korisnik),
                        HRBaza.Izlaz("@ID", SqlDbType.Int), HRBaza.Izlaz("@BrojDana", SqlDbType.Int), HRBaza.Izlaz("@DatumPovratka", SqlDbType.Date), izlaz);
                    upozorenje = izlaz.Value as string;
                    return null;
                };
                if (dlg.ShowDialog(vlasnik) != DialogResult.OK)
                    return false;
                MessageBox.Show(vlasnik, "Zahtev je podnet i čeka odobrenje nadređenog." + (string.IsNullOrEmpty(upozorenje) ? "" : "\n\nNapomena: " + upozorenje),
                    "Zahtev za GO", MessageBoxButtons.OK, string.IsNullOrEmpty(upozorenje) ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                return true;
            }
        }

        // Izmena perioda: stari zahtev se otkazuje i podnosi se novi, u jednoj transakciji (novo rešenje posle odobrenja)
        public static bool Izmeni(IWin32Window vlasnik, int stavka, bool hr, int ja)
        {
            DataRow r = HRBaza.Red(@"SELECT s.DatumOd, s.DatumDo, s.Razlog, s.Napomena, s.StatusID, d.ZaposleniSifra
                                     FROM dbo.GODopustStavke s JOIN dbo.GODopust d ON d.ID = s.DopustID WHERE s.ID = @S", HRBaza.P("@S", stavka));
            if (r == null)
                return false;
            int status = HRBaza.CeoBroj(r["StatusID"]);
            if (status != 2 && status != 3)
            {
                MessageBox.Show(vlasnik, "Menja se samo zahtev koji čeka odobrenje ili odobren godišnji odmor koji još nije počeo.", "Izmena", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (status == 3 && !UpozoriPetRadnihDana(vlasnik, Convert.ToDateTime(r["DatumOd"])))
                return false;
            var pocetne = new HRVrednosti
            {
                ["Zaposleni"] = r["ZaposleniSifra"], ["DatumOd"] = r["DatumOd"], ["DatumDo"] = r["DatumDo"], ["Razlog"] = r["Razlog"], ["Napomena"] = r["Napomena"]
            };
            using (var dlg = new frmHRStavka("Izmena godišnjeg odmora", PoljaZahteva(true, ja, false), pocetne))
            {
                dlg.Promena += Proracun;
                dlg.Snimanje = v =>
                {
                    HRBaza.Izvrsi(@"SET XACT_ABORT ON;
                                    BEGIN TRAN;
                                    EXEC dbo.sp_GO_Otkazi @ID = @Stari, @Korisnik = @K;
                                    EXEC dbo.sp_GO_PodnesiZahtev @ZaposleniSifra = @Z, @DatumOd = @Od, @DatumDo = @Do, @Razlog = @R, @Napomena = @N,
                                         @Kanal = 'A', @PodneoSifra = @P, @Korisnik = @K;
                                    COMMIT;",
                        HRBaza.P("@Stari", stavka), HRBaza.P("@K", HRBaza.Korisnik), HRBaza.P("@Z", r["ZaposleniSifra"]),
                        HRBaza.P("@Od", v.Uzmi("DatumOd")), HRBaza.P("@Do", v.Uzmi("DatumDo")), HRBaza.P("@R", v.Uzmi("Razlog")),
                        HRBaza.P("@N", v.Uzmi("Napomena")), HRBaza.P("@P", ja > 0 ? (object)ja : r["ZaposleniSifra"]));
                    return null;
                };
                if (dlg.ShowDialog(vlasnik) != DialogResult.OK)
                    return false;
            }
            MessageBox.Show(vlasnik, "Stari zahtev je otkazan, a novi čeka odobrenje; posle odobrenja izdaje se novo rešenje.", "Izmena", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
        }

        // Čl. 75: poslodavac može da promeni termin najkasnije 5 radnih dana pre početka
        private static bool UpozoriPetRadnihDana(IWin32Window vlasnik, DateTime pocetak)
        {
            int dana = HRBaza.CeoBroj(HRBaza.Skalar("SELECT dbo.fnHR_RadniDani(@Od, @Do)",
                HRBaza.P("@Od", DateTime.Today.AddDays(1)), HRBaza.P("@Do", pocetak.AddDays(-1))));
            if (dana >= 5)
                return true;
            return MessageBox.Show(vlasnik, "Do početka odmora je manje od 5 radnih dana (" + dana + "). Prema čl. 75 Zakona o radu poslodavac termin menja najkasnije 5 radnih dana pre početka.\n\nNastaviti?",
                "Godišnji odmor", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
        }

        public static bool Odobri(IWin32Window vlasnik, int stavka, bool hr, int ja)
        {
            DataTable odobrene;
            try
            {
                odobrene = HRBaza.ProceduraTabela("dbo.sp_GO_Odobri", HRBaza.P("@ID", stavka), HRBaza.P("@OdobrioSifra", ja > 0 ? (object)ja : null),
                    HRBaza.P("@HR", hr), HRBaza.P("@Korisnik", HRBaza.Korisnik));
            }
            catch (SqlException ex)
            {
                MessageBox.Show(vlasnik, ex.Message, "Odobravanje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (odobrene.Rows.Count > 1)
                MessageBox.Show(vlasnik, "Zahtev je podeljen na dva dela (deo sa prošle i deo sa tekuće godine); za svaki deo izdaje se rešenje.",
                    "Odobravanje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            foreach (DataRow r in odobrene.Rows)
                HRResenje.PosleOdobrenja(vlasnik, HRBaza.CeoBroj(r["ID"]));
            return true;
        }

        public static bool Odbij(IWin32Window vlasnik, int stavka, bool hr, int ja)
        {
            string razlog = HRPrilozi.Unos(vlasnik, "Odbijanje zahteva", "Razlog odbijanja (obavezan)", "", 250, true, true);
            if (razlog == null)
                return false;
            try
            {
                HRBaza.Procedura("dbo.sp_GO_Odbij", HRBaza.P("@ID", stavka), HRBaza.P("@OdobrioSifra", ja > 0 ? (object)ja : null),
                    HRBaza.P("@HR", hr), HRBaza.P("@Razlog", razlog), HRBaza.P("@Korisnik", HRBaza.Korisnik));
                return true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(vlasnik, ex.Message, "Odbijanje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        public static bool Otkazi(IWin32Window vlasnik, int stavka)
        {
            DataRow r = HRBaza.Red("SELECT StatusID, DatumOd FROM dbo.GODopustStavke WHERE ID = @S", HRBaza.P("@S", stavka));
            if (r == null)
                return false;
            if (HRBaza.CeoBroj(r["StatusID"]) == 3 && !UpozoriPetRadnihDana(vlasnik, Convert.ToDateTime(r["DatumOd"])))
                return false;
            if (MessageBox.Show(vlasnik, "Otkazati zahtev? Dani se vraćaju, a GO se briše iz evidencije rada.", "Otkazivanje",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return false;
            try
            {
                HRBaza.Procedura("dbo.sp_GO_Otkazi", HRBaza.P("@ID", stavka), HRBaza.P("@Korisnik", HRBaza.Korisnik));
                return true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(vlasnik, ex.Message, "Otkazivanje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        // Prekid zbog bolovanja: odmor se skraćuje, neiskorišćeni dani se vraćaju, u evidenciju ide BO
        public static bool Prekid(IWin32Window vlasnik, int stavka)
        {
            DataRow r = HRBaza.Red(@"SELECT s.DatumOd, s.DatumDo, d.ZaposleniSifra FROM dbo.GODopustStavke s JOIN dbo.GODopust d ON d.ID = s.DopustID WHERE s.ID = @S",
                HRBaza.P("@S", stavka));
            if (r == null)
                return false;
            var polja = new[] { HRPolje.Datum("Bolovanje", "Prvi dan bolovanja", true) };
            DateTime predlog = DateTime.Today > Convert.ToDateTime(r["DatumOd"]) ? DateTime.Today : Convert.ToDateTime(r["DatumOd"]).AddDays(1);
            using (var dlg = new frmHRStavka("Prekid godišnjeg odmora zbog bolovanja", polja, new HRVrednosti { ["Bolovanje"] = predlog }, 1))
            {
                dlg.Snimanje = v =>
                {
                    HRBaza.Procedura("dbo.sp_GO_PrekidBolovanje", HRBaza.P("@ID", stavka), HRBaza.P("@BolovanjeOd", v.Uzmi("Bolovanje")), HRBaza.P("@Korisnik", HRBaza.Korisnik));
                    return null;
                };
                if (dlg.ShowDialog(vlasnik) != DialogResult.OK)
                    return false;
            }
            if (MessageBox.Show(vlasnik, "Odmor je skraćen, a ostatak se koristi posle bolovanja kroz novi zahtev.\n\nPriložiti doznaku?",
                    "Prekid", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                HRPrilozi.Dodaj(vlasnik, "ZaposleniPrilozi", "Zaposleni", HRBaza.CeoBroj(r["ZaposleniSifra"]), "Doznaka",
                    new KeyValuePair<string, object>("VezaTip", "Bolovanje"));
            return true;
        }

        public static bool PocetnoStanje(IWin32Window vlasnik, int zaposleni, string ime, int godina)
        {
            object trenutno = HRBaza.Skalar(@"SELECT SUM(s.BrojDana) FROM dbo.GODopustStavke s JOIN dbo.GODopust d ON d.ID = s.DopustID
                                              WHERE d.ZaposleniSifra = @Z AND d.Godina = @G AND s.Razlog = N'Početno stanje'",
                HRBaza.P("@Z", zaposleni), HRBaza.P("@G", godina));
            var polja = new[] { HRPolje.Broj("Dana", "Već iskorišćeni dani u " + godina + ". (pre početka rada u programu)", true, 0, 60) };
            using (var dlg = new frmHRStavka("Početno stanje: " + ime, polja, new HRVrednosti { ["Dana"] = trenutno ?? 0 }, 1))
            {
                dlg.Snimanje = v =>
                {
                    HRBaza.Procedura("dbo.sp_GO_PocetnoStanje", HRBaza.P("@ZaposleniSifra", zaposleni), HRBaza.P("@Godina", godina),
                        HRBaza.P("@BrojDana", v.Uzmi("Dana")), HRBaza.P("@Korisnik", HRBaza.Korisnik));
                    return null;
                };
                return dlg.ShowDialog(vlasnik) == DialogResult.OK;
            }
        }
    }
}
