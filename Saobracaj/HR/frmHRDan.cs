using Saobracaj.Theme;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.HR
{
    // Prozor "Dan": sve stavke evidencije jednog zaposlenog za jedan dan (vreme od–do, napomena) i beleške o radu tog dana
    public class frmHRDan : Form
    {
        public frmHRDan(int zaposleni, string ime, DateTime datum, bool zakljucan, bool hr, int ja)
        {
            Text = ime + " – " + datum.ToString("dd.MM.yyyy");
            Size = new Size(980, 640);
            StartPosition = FormStartPosition.CenterParent;
            BackColor = SavaTheme.Surface;
            ShowInTaskbar = false;
            AutoScaleMode = AutoScaleMode.None;

            var evidencija = new HRTabelaKartica(DefEvidencija(zaposleni, datum, zakljucan));
            var beleske = new HRTabelaKartica(HRBeleske.Def(zaposleni, datum, ja, hr));
            var telo = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20, 12, 20, 12) };
            Control podeljeno = HRStil.Podeljeno("Evidencija dana", evidencija, "Beleške o radu", beleske, 55);
            podeljeno.Dock = DockStyle.Fill;
            telo.Controls.Add(podeljeno);
            Controls.Add(telo);
            HRStil.NaslovnaTraka(this, "Evidencija rada › " + ime + " › " + HREvidencija.DaniNedelje[HREvidencija.DanUNedelji(datum)] + " " + datum.ToString("dd.MM.yyyy"));
            HRStil.StatusnaLinija(this).Text = zakljucan
                ? "Mesec je zaključen; stavke se ne menjaju."
                : "GO iz odobrenog zahteva se menja kroz zahtev. Prvi ručni unos zamenjuje generisani redovan rad tog dana.";
            HRStil.PrimeniTemu(this);
            evidencija.Ucitaj(zaposleni);
            beleske.Ucitaj(zaposleni);
        }

        private static HRTabelaDef DefEvidencija(int zaposleni, DateTime datum, bool zakljucan)
        {
            return new HRTabelaDef
            {
                Naslov = "Stavka dana",
                Tabela = "EvidencijaRada",
                Kljuc = "ID",
                RoditeljKolona = "ZaposleniSifra",
                Upit = @"SELECT e.ID, v.Oznaka + N' – ' + v.Naziv AS Vrsta, e.Sati,
                                CONVERT(varchar(5), e.VremeOd, 108) AS Od, CONVERT(varchar(5), e.VremeDo, 108) AS Do, e.Napomena,
                                CASE e.Izvor WHEN 'G' THEN N'Generisano' WHEN 'R' THEN N'Ručno' WHEN 'O' THEN N'Iz zahteva za GO' WHEN 'P' THEN N'Praznik' END AS Izvor,
                                e.NameOper AS Upisao
                         FROM dbo.EvidencijaRada e JOIN dbo.VrstaRada v ON v.ID = e.VrstaRadaID
                         WHERE e.ZaposleniSifra = @Roditelj AND e.Datum = '" + datum.ToString("yyyyMMdd") + @"'
                         ORDER BY v.Redosled, e.ID",
                Kolone = new[]
                {
                    new HRKolona("Vrsta", "Vrsta rada", 230), new HRKolona("Sati", "Sati", 70, "kolicina"), new HRKolona("Od", "Od", 70),
                    new HRKolona("Do", "Do", 70), new HRKolona("Napomena", "Napomena", 220), new HRKolona("Izvor", "Izvor", 130), new HRKolona("Upisao", "Upisao", 100)
                },
                Polja = new List<HRPolje>
                {
                    HRPolje.Lista("VrstaRadaID", "Vrsta rada", "SELECT ID, Oznaka + N' – ' + Naziv AS Naziv FROM dbo.VrstaRada WHERE Aktivno = 1 AND SamoKrozZahtev = 0 ORDER BY ISNULL(Redosled, 999)", true),
                    new HRPolje("Sati", "Sati", HRTip.Decimalni, true) { Min = 0.01m, Max = 24 },
                    HRPolje.Tekst("VremeOd", "Vreme od (HH:mm)", 8),
                    HRPolje.Tekst("VremeDo", "Vreme do (HH:mm)", 8),
                    HRPolje.Napomena("Napomena", "Napomena", 250)
                },
                NoviPodrazumevano = () => new HRVrednosti { ["Sati"] = HRBaza.Decimalni(HRBaza.Skalar("SELECT dbo.fnHR_DnevniFond(@Z, @D)", HRBaza.P("@Z", zaposleni), HRBaza.P("@D", datum.Date))) },
                Validacija = v =>
                {
                    foreach (string k in new[] { "VremeOd", "VremeDo" })
                        if (v.Tekst(k) != "" && !TimeSpan.TryParse(v.Tekst(k), out _))
                            return "Vreme upišite kao HH:mm (npr. 07:30).";
                    return null;
                },
                Snimi = (v, id) =>
                {
                    HRBaza.Procedura("dbo.sp_EvidencijaStavkaSnimi",
                        HRBaza.P("@ID", id), HRBaza.P("@ZaposleniSifra", zaposleni), HRBaza.P("@Datum", datum.Date),
                        HRBaza.P("@VrstaRadaID", v.Uzmi("VrstaRadaID")), HRBaza.P("@Sati", v.Uzmi("Sati")),
                        HRBaza.P("@VremeOd", Vreme(v.Tekst("VremeOd"))), HRBaza.P("@VremeDo", Vreme(v.Tekst("VremeDo"))),
                        HRBaza.P("@Napomena", v.Uzmi("Napomena")), HRBaza.P("@Korisnik", HRBaza.Korisnik));
                    return null;
                },
                Obrisi = id =>
                {
                    HRBaza.Procedura("dbo.sp_EvidencijaStavkaObrisi", HRBaza.P("@ID", id));
                    return null;
                },
                Dozvoljeno = () => !zakljucan,
                PorukaZabranjeno = "Mesec je zaključen; izmene su moguće tek posle otključavanja."
            };
        }

        private static object Vreme(string s)
        {
            if (TimeSpan.TryParse((s ?? "").Trim(), out TimeSpan t))
                return t;
            return null;
        }
    }
}
