using Saobracaj.Theme;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Saobracaj.HR
{
    // Godišnji odmori – pravo (specifikacija 7): ukupno, potrošeno, preostalo, ostatak prošle godine i rok;
    // Generiši za godinu, Dodeli osnov (grupno), Početno stanje, Stavke.
    public class frmHRGOPravo : Form
    {
        private readonly GridGroupingControl grid;
        private readonly ComboBox cboGodina;
        private readonly TextBox pretraga;
        private readonly Timer odlozeno = new Timer { Interval = 300 };
        private DataTable podaci;
        private int godina = DateTime.Today.Year;

        public frmHRGOPravo()
        {
            HRStil.PripremiFormu(this, "Godišnji odmori");

            var telo = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20, 12, 20, 0), BackColor = SavaTheme.Surface };
            var okvir = HRStil.BeliPanel();
            okvir.Dock = DockStyle.Fill;
            grid = HRGrid.Novi();
            okvir.Controls.Add(grid);
            telo.Controls.Add(okvir);
            HRGrid.Kolone(grid,
                new HRKolona("Code", "Code", 90),
                new HRKolona("Zaposleni", "Zaposleni", 200),
                new HRKolona("Jedinica", "Org. jedinica", 170),
                new HRKolona("UkupnoDana", "Ukupno", 70, "broj"),
                new HRKolona("PotrosenoDana", "Potrošeno", 80, "broj"),
                new HRKolona("PreostaloDana", "Preostalo", 80, "broj"),
                new HRKolona("NaCekanju", "Na čekanju", 85, "broj"),
                new HRKolona("ProslaPreostalo", "Ostatak prošle", 110, "broj"),
                new HRKolona("ProslaRok", "Rok prošle", 95, "datum"),
                new HRKolona("Srazmerno", "Srazmerno", 110),
                new HRKolona("Osnovi", "Osnovi", 260));
            HRGrid.BojaTeksta(grid, (r, k) =>
            {
                if (k == "UkupnoDana" && r.GetValue("UkupnoDana") == DBNull.Value)
                    return SavaTheme.Danger;
                if (k == "PreostaloDana" && r.GetValue("PreostaloDana") != DBNull.Value && HRBaza.CeoBroj(r.GetValue("PreostaloDana")) <= 0)
                    return SavaTheme.Label;
                return null;
            });
            HRGrid.DuplikKlik(grid, Stavke);

            var alati = HRStil.AlatnaTraka(this);
            pretraga = alati.Pretraga("Pretraga: ime, Code…", 200);
            pretraga.TextChanged += (s, e) => { odlozeno.Stop(); odlozeno.Start(); };
            odlozeno.Tick += (s, e) => { odlozeno.Stop(); Filtriraj(); };
            cboGodina = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 80, Margin = new Padding(0, 2, 12, 0) };
            for (int g = DateTime.Today.Year + 1; g >= DateTime.Today.Year - 3; g--)
                cboGodina.Items.Add(g);
            cboGodina.SelectedItem = godina;
            cboGodina.SelectedIndexChanged += (s, e) => { godina = (int)cboGodina.SelectedItem; Ucitaj(); };
            alati.Levo.Controls.Add(cboGodina);
            alati.Dugme("Dodeli osnov", (s, e) => DodeliOsnov());
            alati.Dugme("Početno stanje", (s, e) => PocetnoStanje());
            alati.Dugme("Stavke", (s, e) => Stavke());
            alati.Dugme("Zahtevi", (s, e) => HRStil.Otvori(this, new frmHRGOZahtevi()));
            alati.Dugme("Osveži", (s, e) => Ucitaj());
            alati.DugmeDesno("Generiši za godinu", (s, e) => Generisi());

            HRStil.NaslovnaTraka(this, "Godišnji odmori › Pravo");
            HRStil.StatusnaLinija(this).Text = "Pravo = zbir osnova (OSN 20 + ostali). U godini zasnivanja ili prestanka radnog odnosa srazmerni deo (čl. 72). Ostatak se koristi do 30. juna naredne godine. Dupli klik: stavke zaposlenog.";
            Controls.Add(telo);
            telo.BringToFront();
            HRStil.PrimeniTemu(this);
            VisibleChanged += (s, e) => { if (Visible) Ucitaj(); };
        }

        private void Ucitaj()
        {
            try
            {
                podaci = HRBaza.Tabela(@"
                    SELECT z.ID, ISNULL(z.Code, N'') AS Code, t.PrezimeIme AS Zaposleni, ISNULL(t.OrganizacionaJedinicaNaziv, N'') AS Jedinica,
                           d.UkupnoDana, d.PotrosenoDana, d.PreostaloDana,
                           (SELECT SUM(s.BrojDana) FROM dbo.GODopustStavke s WHERE s.DopustID = d.ID AND s.StatusID = 2) AS NaCekanju,
                           p.PreostaloDana AS ProslaPreostalo, p.RokKoriscenja AS ProslaRok,
                           CASE WHEN d.Srazmerno = 1 THEN N'da (' + CAST(d.MeseciRada AS nvarchar(3)) + N' mes.)' ELSE N'' END AS Srazmerno,
                           ISNULL(STUFF((SELECT N', ' + o.Oznaka + N' ' + CAST(x.BrojDana AS nvarchar(5))
                                         FROM dbo.GODopustOsnov x JOIN dbo.OsnovGO o ON o.ID = x.OsnovGOID WHERE x.DopustID = d.ID
                                         FOR XML PATH(''), TYPE).value('.', 'nvarchar(max)'), 1, 2, N''), N'') AS Osnovi
                    FROM dbo.Zaposleni z
                    JOIN dbo.vZaposleniTrenutno t ON t.Zaposleni = z.ID
                    LEFT JOIN dbo.GODopust d ON d.ZaposleniSifra = z.ID AND d.Godina = @G
                    LEFT JOIN dbo.GODopust p ON p.ZaposleniSifra = z.ID AND p.Godina = @G - 1
                    WHERE (z.Status = 'A' OR z.DatumIzlaska >= DATEFROMPARTS(@G, 1, 1) OR d.ID IS NOT NULL)
                    ORDER BY t.PrezimeIme", HRBaza.P("@G", godina));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Podaci nisu učitani. Proverite da li je na bazi pokrenuta skripta HR_Modul_Faza2_2026_10.sql.\n\n" + ex.Message,
                    "Godišnji odmori", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Filtriraj();
        }

        private void Filtriraj()
        {
            if (podaci == null)
                return;
            string t = pretraga.Text.Trim().Replace("'", "''").Replace("[", "[[]").Replace("*", "[*]").Replace("%", "[%]");
            HRGrid.Napuni(grid, new DataView(podaci) { RowFilter = t == "" ? "" : "(Zaposleni LIKE '%" + t + "%' OR Code LIKE '%" + t + "%')" }.ToTable());
        }

        private int Izabran(out string ime)
        {
            ime = Convert.ToString(HRGrid.Vrednost(grid, "Zaposleni"));
            int id = HRGrid.IzabraniId(grid, "ID");
            if (id <= 0)
                MessageBox.Show("Izaberite zaposlenog u tabeli.", "Godišnji odmori", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return id;
        }

        private void Stavke()
        {
            int id = Izabran(out _);
            if (id > 0)
                HRStil.Otvori(this, new frmHRGOZahtevi(id));
        }

        private void PocetnoStanje()
        {
            int id = Izabran(out string ime);
            if (id > 0 && HRGO.PocetnoStanje(this, id, ime, godina))
                Ucitaj();
        }

        private void Generisi()
        {
            int izabran = HRGrid.IzabraniId(grid, "ID");
            var polja = new List<HRPolje>
            {
                new HRPolje("Godina", "Godina", HRTip.CeoBroj, true) { Min = 2000, Max = 2100 },
                HRPolje.DaNe("Samo", "Samo izabrani zaposleni" + (izabran > 0 ? " (" + HRGrid.Vrednost(grid, "Zaposleni") + ")" : "")),
                HRPolje.DaNe("Prepisi", "Prepiši postojeće pravo (potrošeni dani ostaju)")
            };
            DataTable rezultat = null;
            using (var dlg = new frmHRStavka("Generiši godišnji odmor za godinu", polja, new HRVrednosti { ["Godina"] = godina }, 1))
            {
                dlg.Snimanje = v =>
                {
                    bool samo = Convert.ToBoolean(v.Uzmi("Samo") ?? false);
                    if (samo && izabran <= 0)
                        return "Izaberite zaposlenog u tabeli ili isključite „Samo izabrani zaposleni“.";
                    rezultat = HRBaza.ProceduraTabela("dbo.sp_GO_Generisi", HRBaza.P("@Godina", v.Uzmi("Godina")),
                        HRBaza.P("@ZaposleniSifra", samo ? (object)izabran : null), HRBaza.P("@Prepisi", v.Uzmi("Prepisi") ?? false),
                        HRBaza.P("@Korisnik", HRBaza.Korisnik));
                    return null;
                };
                if (dlg.ShowDialog(this) != DialogResult.OK)
                    return;
            }
            PrikaziRezultat(rezultat);
            Ucitaj();
        }

        private void PrikaziRezultat(DataTable rezultat)
        {
            using (var f = new Form { Text = "Rezultat generisanja", Size = new Size(820, 520), StartPosition = FormStartPosition.CenterParent, BackColor = SavaTheme.Surface, ShowInTaskbar = false })
            {
                var g = HRGrid.Novi();
                HRGrid.Kolone(g, new HRKolona("Zaposleni", "Zaposleni", 240), new HRKolona("UkupnoDana", "Ukupno dana", 100, "broj"),
                    new HRKolona("Ishod", "Ishod", 120), new HRKolona("Razlog", "Razlog preskakanja", 300));
                HRGrid.BojaTeksta(g, (r, k) => k == "Ishod" && Convert.ToString(r.GetValue("Ishod")) == "preskočeno" ? HRStil.StatusIstice : (Color?)null);
                var okvir = HRStil.BeliPanel();
                okvir.Dock = DockStyle.Fill;
                okvir.Controls.Add(g);
                var telo = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20) };
                telo.Controls.Add(okvir);
                f.Controls.Add(telo);
                int gen = rezultat.Select("Ishod = 'generisano'").Length, azu = rezultat.Select("Ishod = 'ažurirano'").Length;
                HRStil.NaslovnaTraka(f, "Generisano: " + gen + " · ažurirano: " + azu + " · preskočeno: " + (rezultat.Rows.Count - gen - azu));
                HRStil.PrimeniTemu(f);
                HRGrid.Napuni(g, rezultat);
                f.ShowDialog(this);
            }
        }

        // Grupno dodeljivanje osnova: označi zaposlene, izaberi osnov i broj dana
        private void DodeliOsnov()
        {
            DataTable zap = HRBaza.Tabela(HRListe.AktivniZaposleni);
            int izabran = HRGrid.IzabraniId(grid, "ID");
            using (var f = new Form { Text = "Dodeli osnov godišnjeg odmora", Size = new Size(780, 560), StartPosition = FormStartPosition.CenterParent,
                                      FormBorderStyle = FormBorderStyle.FixedDialog, MaximizeBox = false, MinimizeBox = false, BackColor = SavaTheme.Surface,
                                      ShowInTaskbar = false, AutoScaleMode = AutoScaleMode.None })
            {
                var lista = new CheckedListBox { Dock = DockStyle.Fill, CheckOnClick = true, IntegralHeight = false };
                foreach (DataRow r in zap.Rows)
                    lista.Items.Add(new KeyValuePair<int, string>(HRBaza.CeoBroj(r[0]), Convert.ToString(r[1])), HRBaza.CeoBroj(r[0]) == izabran);
                lista.DisplayMember = "Value";
                var sve = new CheckBox { Text = "Označi sve", Dock = DockStyle.Top, Height = 30 };
                sve.CheckedChanged += (s, e) => { for (int i = 0; i < lista.Items.Count; i++) lista.SetItemChecked(i, sve.Checked); };
                var levo = new Panel { Dock = DockStyle.Left, Width = 340, Padding = new Padding(20, 12, 10, 12) };
                levo.Controls.Add(lista);
                levo.Controls.Add(sve);

                var obrazac = new HRObrazac(new List<HRPolje>
                {
                    HRPolje.Lista("Osnov", "Osnov", "SELECT ID, Oznaka + N' – ' + Naziv AS Naziv FROM dbo.OsnovGO WHERE Aktivno = 1 ORDER BY Oznaka", true),
                    HRPolje.Broj("BrojDana", "Broj dana", true, 0, 60),
                    HRPolje.Datum("VaziOd", "Važi od", true),
                    HRPolje.Datum("VaziDo", "Važi do"),
                    HRPolje.Tekst("Napomena", "Napomena", 250)
                }, 1);
                obrazac.Promena += k =>
                {
                    if (k == "Osnov" && obrazac.Vrednost("Osnov") != null)
                        obrazac.Postavi("BrojDana", HRBaza.Skalar("SELECT BrojDana FROM dbo.OsnovGO WHERE ID = @O", HRBaza.P("@O", obrazac.Vrednost("Osnov"))));
                };
                var desno = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10, 12, 20, 12) };
                desno.Controls.Add(obrazac.Panel);

                var dno = new Panel { Dock = DockStyle.Bottom, Height = 60, BackColor = SavaTheme.White, Padding = new Padding(20, 14, 20, 14) };
                var dodeli = HRStil.Dugme("Dodeli", "primary");
                var otkazi = HRStil.Dugme("Otkaži");
                dodeli.Dock = otkazi.Dock = DockStyle.Right;
                dno.Controls.Add(otkazi);
                dno.Controls.Add(new Panel { Dock = DockStyle.Right, Width = 8 });
                dno.Controls.Add(dodeli);
                otkazi.Click += (s, e) => f.Close();
                dodeli.Click += (s, e) =>
                {
                    string greska = obrazac.Ocitaj(out HRVrednosti v);
                    var oznaceni = lista.CheckedItems.Cast<KeyValuePair<int, string>>().ToList();
                    if (greska == null && oznaceni.Count == 0)
                        greska = "Označite zaposlene.";
                    if (greska == null && v.Datum("VaziDo").HasValue && v.Datum("VaziDo") < v.Datum("VaziOd"))
                        greska = "Važi do je pre Važi od.";
                    if (greska != null)
                    {
                        MessageBox.Show(f, greska, f.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    foreach (var z in oznaceni)
                        HRBaza.Izvrsi("INSERT dbo.ZaposleniOsnovGO (ZaposleniSifra, OsnovGOID, BrojDana, VaziOd, VaziDo, Napomena) VALUES (@Z, @O, @B, @Od, @Do, @N)",
                            HRBaza.P("@Z", z.Key), HRBaza.P("@O", v.Uzmi("Osnov")), HRBaza.P("@B", v.Uzmi("BrojDana")),
                            HRBaza.P("@Od", v.Uzmi("VaziOd")), HRBaza.P("@Do", v.Uzmi("VaziDo")), HRBaza.P("@N", v.Uzmi("Napomena")));
                    MessageBox.Show(f, "Osnov je dodeljen za " + oznaceni.Count + " zaposlenih. Pravo za godinu se menja tek posle Generiši za godinu (Prepiši).",
                        f.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    f.DialogResult = DialogResult.OK;
                };

                f.Controls.Add(desno);
                f.Controls.Add(levo);
                f.Controls.Add(dno);
                ThemeHelper.Apply(f);
                obrazac.PostaviSve(new HRVrednosti { ["VaziOd"] = new DateTime(godina, 1, 1) });
                f.ShowDialog(this);
            }
        }
    }
}
