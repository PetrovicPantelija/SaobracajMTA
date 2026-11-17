using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Saobracaj.Skladista_main.Dokumenta
{
    // Raspored Prijemnice: grupe polja koje se skrivaju i pokazuju (stanje se pamti po korisniku na računaru)
    // i tabela stavki u kojoj se stavke unose i menjaju direktno (polja za stavku ispod grupa su sklonjena).
    // Kontrole iz dizajnera se samo premeštaju u nove kontejnere; dizajner se ne menja.
    public partial class Prijemnica
    {
        private class Sekcija
        {
            public string Kljuc, Naslov;
            public Panel Okvir, Sadrzaj;
            public Button Traka;
            public bool Otvorena;
        }

        readonly List<Sekcija> sekcije = new List<Sekcija>();
        SplitContainer splitRaspored;
        PrijemnicaStavke ciljArtikla, ciljNHM;   // stavka kojoj se dodeljuje izabrani artikal / NHM

        static readonly string FajlSekcija = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Saobracaj", "PrijemnicaSekcije.txt");

        #region Grupe polja

        private void InitRaspored()
        {
            SuspendLayout();

            splitRaspored = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Horizontal,
                FixedPanel = FixedPanel.Panel1,
                SplitterWidth = 6,
                BackColor = Color.FromArgb(0xD4, 0xDD, 0xE6)
            };
            splitRaspored.Panel1.BackColor = BackColor;
            splitRaspored.Panel2.BackColor = BackColor;
            splitRaspored.Panel1.AutoScroll = true;
            splitRaspored.Panel2MinSize = 150;
            Controls.Add(splitRaspored);
            splitRaspored.BringToFront();

            var stanje = UcitajStanjeSekcija();

            // gornji red (ID, carinsko skladište, magacinski broj, nalogodavac, postupak, smeštajni dokument, rok, broj prijemnice)
            NapraviSekciju("Osnovni", "Osnovni podaci", stanje, true,
                label1, txtID, label2, textBox1, label3, cboMagacinskiBroj, label4, cboNalogodavac, label43, cboCarinskiPostupak,
                label30, txtSmestajniDokument, label31, txtRok, chkUprocesu, label29, txtPrijemnica);
            NapraviSekciju("Vlasnik", "Vlasnik i roba", stanje, true, panel1);
            NapraviSekciju("Prevoz", "Prevoz", stanje, false, panel2);
            NapraviSekciju("Carina", "Carina, špedicija i dokumenti", stanje, true, panel3);
            PoredjajDodatneUsluge();
            NapraviSekciju("Usluge", "Dodatne usluge i napomene", stanje, false, panel4);

            // Dock Top: poslednja dodata je na vrhu, pa se dodaju obrnutim redom
            for (int i = sekcije.Count - 1; i >= 0; i--)
                splitRaspored.Panel1.Controls.Add(sekcije[i].Okvir);
            splitRaspored.Panel1.AutoScrollMinSize = new Size(
                sekcije.Max(s => s.Sadrzaj.Controls.Cast<Control>().Max(c => c.Right)) + 12, 0);

            InitPalete();
            NapraviDeoZaStavke();

            // polja za stavku se ne prikazuju: stavke se unose u tabeli
            panel5.Visible = false;

            // paneli za izbor (NHM, artikli, nalog rukovaocu, uvoz, palete) ostaju iznad rasporeda
            foreach (Control c in new Control[] { panel9, panel6, panel7, pnlUvozPL, pnlUparivanje, pnlPalete })
                if (c != null) c.BringToFront();

            ResumeLayout(true);

            splitRaspored.SizeChanged += (s, e) => PodesiRazdelnik();
            Load += (s, e) => PodesiRazdelnik();
        }

        private void NapraviSekciju(string kljuc, string naslov, Dictionary<string, bool> stanje, bool podrazumevano, params Control[] kontrole)
        {
            int levo = kontrole.Min(c => c.Left), gore = kontrole.Min(c => c.Top);
            int desno = kontrole.Max(c => c.Right), dole = kontrole.Max(c => c.Bottom);

            var sekcija = new Sekcija
            {
                Kljuc = kljuc,
                Naslov = naslov,
                Otvorena = stanje.ContainsKey(kljuc) ? stanje[kljuc] : podrazumevano,
                Okvir = new Panel { Dock = DockStyle.Top },
                Sadrzaj = new Panel { Dock = DockStyle.Top, Height = dole - gore + 10 },
                Traka = new Button
                {
                    Dock = DockStyle.Top,
                    Height = 26,
                    FlatStyle = FlatStyle.Flat,
                    TextAlign = ContentAlignment.MiddleLeft,
                    BackColor = Color.FromArgb(0xEA, 0xF0, 0xF6),
                    Font = new Font(Font, FontStyle.Bold),
                    Cursor = Cursors.Hand,
                    TabStop = false
                }
            };
            sekcija.Traka.FlatAppearance.BorderColor = Color.FromArgb(0xD4, 0xDD, 0xE6);

            foreach (Control c in kontrole)
            {
                var stara = c.Location;
                c.Parent.Controls.Remove(c);
                c.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                c.Location = new Point(stara.X - levo + 12, stara.Y - gore + 5);
                sekcija.Sadrzaj.Controls.Add(c);
            }

            sekcija.Okvir.Controls.Add(sekcija.Sadrzaj);
            sekcija.Okvir.Controls.Add(sekcija.Traka);   // poslednja dodata je na vrhu
            sekcija.Traka.Click += (s, e) => { sekcija.Otvorena = !sekcija.Otvorena; PrimeniSekciju(sekcija); SacuvajStanjeSekcija(); PodesiRazdelnik(); };

            PrimeniSekciju(sekcija);
            sekcije.Add(sekcija);
        }

        private static void PrimeniSekciju(Sekcija s)
        {
            s.Traka.Text = (s.Otvorena ? "▼  " : "►  ") + s.Naslov;
            s.Sadrzaj.Visible = s.Otvorena;
            s.Okvir.Height = s.Traka.Height + (s.Otvorena ? s.Sadrzaj.Height : 0);
        }

        // Razdelnik: grupe dobijaju koliko im treba, tabela stavki ostatak (najmanje Panel2MinSize)
        private void PodesiRazdelnik()
        {
            try
            {
                if (splitRaspored == null || !splitRaspored.IsHandleCreated)
                    return;
                int potrebno = sekcije.Sum(s => s.Okvir.Height) + 2;
                int najvise = splitRaspored.Height - splitRaspored.Panel2MinSize - splitRaspored.SplitterWidth;
                if (najvise < splitRaspored.Panel1MinSize)
                    return;
                splitRaspored.SplitterDistance = Math.Max(splitRaspored.Panel1MinSize, Math.Min(potrebno, najvise));
            }
            catch (InvalidOperationException)
            {
                // prozor još nema konačnu veličinu
            }
        }

        // Žuti panel (posebni uslovi, dodatne usluge, napomena) je uzak i visok; u grupi se slaže u jedan red
        private void PoredjajDodatneUsluge()
        {
            float s = panel4.Width / 200f;   // dizajn je 200 px širok; forma se skalira po fontu
            txtPosebniUslovi.Size = new Size((int)(300 * s), (int)(60 * s));
            txtNapomena.Size = new Size((int)(300 * s), (int)(60 * s));
            dgvUsluge.Size = new Size((int)(320 * s), (int)(110 * s));

            var grupe = new[]
            {
                new Control[] { label25, txtPosebniUslovi },
                new Control[] { label26, cboDodatneUsluge, btnDodajUsluge, btnIzbaci },
                new Control[] { dgvUsluge },
                new Control[] { label27, txtNapomena }
            };

            int x = (int)(6 * s), vrh = (int)(6 * s), dno = 0;
            foreach (var grupa in grupe)
            {
                int levo = grupa.Min(c => c.Left), gore = grupa.Min(c => c.Top);
                foreach (Control c in grupa)
                    c.Location = new Point(c.Left - levo + x, c.Top - gore + vrh);
                x = grupa.Max(c => c.Right) + (int)(20 * s);
                dno = Math.Max(dno, grupa.Max(c => c.Bottom));
            }
            panel4.Size = new Size(x, dno + (int)(6 * s));
        }

        private static Dictionary<string, bool> UcitajStanjeSekcija()
        {
            var stanje = new Dictionary<string, bool>();
            try
            {
                if (File.Exists(FajlSekcija))
                    foreach (string red in File.ReadAllLines(FajlSekcija))
                    {
                        var delovi = red.Split('=');
                        if (delovi.Length == 2)
                            stanje[delovi[0].Trim()] = delovi[1].Trim() == "1";
                    }
            }
            catch
            {
                // bez zapamćenog stanja: podrazumevano
            }
            return stanje;
        }

        private void SacuvajStanjeSekcija()
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(FajlSekcija));
                File.WriteAllLines(FajlSekcija, sekcije.Select(s => s.Kljuc + "=" + (s.Otvorena ? "1" : "0")));
            }
            catch
            {
                // stanje grupa nije bitno za rad
            }
        }

        #endregion

        #region Tabela stavki za unos

        private void NapraviDeoZaStavke()
        {
            var traka = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 44,
                Padding = new Padding(8, 6, 8, 4),
                WrapContents = false,
                BackColor = BackColor
            };

            traka.Controls.Add(DugmeStavke("Nova stavka", (s, e) => NovaStavkaUGridu()));
            traka.Controls.Add(DugmeStavke("Ukloni stavku", (s, e) => btnUkloni_Click(btnUkloni, EventArgs.Empty)));
            traka.Controls.Add(DugmeStavke("Izaberi artikal", (s, e) => IzaberiArtikalZaStavku()));
            traka.Controls.Add(DugmeStavke("NHM", (s, e) => IzaberiNHMZaStavku()));

            // uvoz packing liste i uparivanje (vidljivi kad je skripta za carinski prijem puštena)
            // (Visible se ne čita: dok forma nije prikazana, uvek vraća false)
            foreach (Button b in new[] { btnPackingLista, btnUparivanje })
            {
                if (b == null) continue;
                b.Parent.Controls.Remove(b);
                b.Margin = new Padding(16, 0, 0, 0);
                b.Size = new Size(b.Width, 32);
                traka.Controls.Add(b);
                b.Visible = ImaPLKolone;
            }
            if (btnDodeliPalete != null)
            {
                btnDodeliPalete.Margin = new Padding(16, 0, 0, 0);
                traka.Controls.Add(btnDodeliPalete);
            }
            var potvrda = NapraviDugmePotvrde();
            if (potvrda != null)
                traka.Controls.Add(potvrda);

            var deo = splitRaspored.Panel2;
            gridGroupingControl1.Parent.Controls.Remove(gridGroupingControl1);
            gridGroupingControl1.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            gridGroupingControl1.Dock = DockStyle.Fill;
            deo.Controls.Add(gridGroupingControl1);
            deo.Controls.Add(traka);   // traka na vrhu, tabela popunjava ostatak

            gridGroupingControl1.ActivateCurrentCellBehavior = GridCellActivateAction.ClickOnCell;
            gridGroupingControl1.TableControlCellDoubleClick += GridStavke_CellDoubleClick;

            // izbor artikla / NHM iz postojećih panela dodeljuje se stavki iz tabele
            btnDodajArtikal.Click += (s, e) =>
            {
                if (ciljArtikla == null || panel9.Visible) return;
                ciljArtikla.IDArtikla = idArtikla;
                ciljArtikla.NHM = 0;
                ciljArtikla.Naziv = txtArtikal.Text.Trim();
                if (cboJM.SelectedValue != null) ciljArtikla.JM = cboJM.SelectedValue.ToString();
                ciljArtikla = null;
                OsveziGrid();
            };
            btnNazadArtikal.Click += (s, e) => ciljArtikla = null;
            btnNHMDodaj.Click += (s, e) =>
            {
                if (ciljNHM == null || panel6.Visible) return;
                ciljNHM.NHM = nhm;
                ciljNHM.IDArtikla = 0;
                ciljNHM.Naziv = txtArtikal.Text.Trim();
                ciljNHM = null;
                OsveziGrid();
            };
            btnNHMNazad.Click += (s, e) => ciljNHM = null;

            OsveziGrid();
        }

        private static Button DugmeStavke(string tekst, EventHandler klik)
        {
            var b = new Button { Text = tekst, AutoSize = true, Height = 32, MinimumSize = new Size(110, 32), UseVisualStyleBackColor = true, Margin = new Padding(0, 0, 8, 0) };
            b.Click += klik;
            return b;
        }

        // Izvor tabele je sama lista stavki (BindingList je omotač, ne kopija): izmena u tabeli menja stavku
        private BindingList<PrijemnicaStavke> IzvorStavki()
        {
            var izvor = new BindingList<PrijemnicaStavke>(list) { AllowNew = true, AllowEdit = true, AllowRemove = false };
            izvor.AddingNew += (s, e) => e.NewObject = NovaPraznaStavka();
            return izvor;
        }

        private PrijemnicaStavke NovaPraznaStavka()
        {
            return new PrijemnicaStavke
            {
                RB = list.Count > 0 ? list.Max(x => x.RB) + 1 : 1,
                Naziv = "", Naimenovanje = "", JM = "", Valuta = "", Pozicija = "",
                PaletaOpis = "", DimenzijePaleta = "", Napomena = ""
            };
        }

        private void NovaStavkaUGridu()
        {
            ZavrsiUnosUGridu();
            list.Add(NovaPraznaStavka());
            ReorderRB();
            OsveziGrid();
            try
            {
                var zapisi = gridGroupingControl1.Table.Records;
                if (zapisi.Count > 0)
                    gridGroupingControl1.Table.CurrentRecord = zapisi[zapisi.Count - 1];
            }
            catch
            {
                // izbor reda nije bitan
            }
        }

        private void IzaberiArtikalZaStavku()
        {
            ZavrsiUnosUGridu();
            if (VratiSelektovanuStavkuIzGrida() == null)
                NovaStavkaUGridu();
            ciljArtikla = VratiSelektovanuStavkuIzGrida();
            btnArtikli_Click(btnArtikli, EventArgs.Empty);
            panel9.BringToFront();
        }

        private void IzaberiNHMZaStavku()
        {
            ZavrsiUnosUGridu();
            if (VratiSelektovanuStavkuIzGrida() == null)
                NovaStavkaUGridu();
            ciljNHM = VratiSelektovanuStavkuIzGrida();
            btnNHM_Click(btnNHM, EventArgs.Empty);
            panel6.BringToFront();
        }

        // dvoklik na kolonu Artikal otvara izbor artikla, na NHM izbor NHM
        private void GridStavke_CellDoubleClick(object sender, GridTableControlCellClickEventArgs e)
        {
            var stil = e.TableControl.Model[e.Inner.RowIndex, e.Inner.ColIndex] as GridTableCellStyleInfo;
            string kolona = stil?.TableCellIdentity?.Column?.Name;
            if (stil == null || stil.TableCellIdentity.DisplayElement == null || stil.TableCellIdentity.DisplayElement.GetRecord() == null)
                return;
            if (kolona == "NHM")
                IzaberiNHMZaStavku();
            else if (kolona == "Naziv" && !StavkeZakljucaneZaUnos())
                IzaberiArtikalZaStavku();
        }

        private bool StavkeZakljucaneZaUnos()
        {
            return ImaPLKolone && StavkeZakljucane();
        }

        // pre snimanja: izmena u ćeliji koja se još kuca se upisuje u stavku
        private void ZavrsiUnosUGridu()
        {
            try
            {
                if (gridGroupingControl1.TableControl.CurrentCell.IsEditing)
                    gridGroupingControl1.TableControl.CurrentCell.EndEdit();
                gridGroupingControl1.Table.EndEdit();
            }
            catch
            {
                // tabela nije u izmeni
            }
        }

        // Kolone tabele: redosled, nazivi, izbor iz liste, samo za čitanje
        private void PodesiGridZaUnos()
        {
            try
            {
                var td = gridGroupingControl1.TableDescriptor;
                bool zakljucano = StavkeZakljucaneZaUnos();
                td.AllowEdit = !zakljucano;
                td.AllowNew = !zakljucano;
                td.AllowRemove = false;

                bool naimenovanjeIzListe = false;
                DataTable naimenovanja = null;
                if (ImaPLKolone && txtID.Text != "")
                {
                    try
                    {
                        naimenovanja = UcitajNaimenovanja();
                        naimenovanjeIzListe = naimenovanja.Rows.Count > 0;
                    }
                    catch
                    {
                        naimenovanjeIzListe = false;
                    }
                }

                var redosled = new List<string> { "RB", "Naziv", naimenovanjeIzListe ? "IDNaimenovanja" : "Naimenovanje", "NHM", "JM", "Koleta" };
                if (ImaPLKolone) redosled.AddRange(new[] { "KoletaPL", "KolicinaJM" });
                redosled.AddRange(new[] { "Bruto", "Neto", "Vrednost", "Valuta", "Pozicija", "Paleta", "VrstaPaleta", "DimenzijePaleta", "PDV", "Carina", "Napomena" });

                td.VisibleColumns.Clear();
                foreach (string k in redosled)
                    if (td.Columns.IndexOf(k) >= 0)
                        td.VisibleColumns.Add(k);

                Naslov(td, "RB", "RB"); SamoCitanje(td, "RB");
                Naslov(td, "Naziv", "Artikal");
                Naslov(td, "Naimenovanje", "Naimenovanje");
                Naslov(td, "NHM", "NHM"); SamoCitanje(td, "NHM");
                Naslov(td, "Koleta", "Koleta"); Broj(td, "Koleta", "N0");
                Naslov(td, "KoletaPL", "Koleta PL"); Broj(td, "KoletaPL", "N0"); SamoCitanje(td, "KoletaPL");
                Naslov(td, "KolicinaJM", "Količina JM"); Broj(td, "KolicinaJM", "N3"); SamoCitanje(td, "KolicinaJM");
                Naslov(td, "Bruto", "Bruto kg"); Broj(td, "Bruto", "N3");
                Naslov(td, "Neto", "Neto kg"); Broj(td, "Neto", "N3");
                Naslov(td, "Vrednost", "Vrednost"); Broj(td, "Vrednost", "N2");
                Naslov(td, "Paleta", "Broj paleta"); Broj(td, "Paleta", "N0");
                Naslov(td, "DimenzijePaleta", "Dimenzije palete"); SamoCitanje(td, "DimenzijePaleta");
                Naslov(td, "PDV", "PDV %"); Broj(td, "PDV", "N2");
                Naslov(td, "Carina", "Carina %"); Broj(td, "Carina", "N2");
                Naslov(td, "Napomena", "Napomena");

                Lista(td, "JM", "JM", cboJM.DataSource, "MeSifra", "MeSifra");
                Lista(td, "Valuta", "Valuta", cboValutaStavka.DataSource, "VaNaziv", "VaSifra");
                Lista(td, "Pozicija", "Polje", cboPozicija.DataSource, "Naziv", "Naziv");
                Lista(td, "VrstaPaleta", "Vrsta palete", cboVrstaPaleta.DataSource, "Naziv", "ID");
                if (naimenovanjeIzListe)
                    Lista(td, "IDNaimenovanja", "Naimenovanje", naimenovanja, "Prikaz", "ID");

                // sa dodelom paleta broj i vrsta palete stavke se računaju iz paleta (Dodeli palete)
                if (ImaPalete)
                {
                    SamoCitanje(td, "Paleta");
                    SamoCitanje(td, "VrstaPaleta");
                }
                // CAR-PP06: polje stavke se računa iz polja njenih paleta
                if (ImaPolja)
                    SamoCitanje(td, "Pozicija");
            }
            catch
            {
                // izgled kolona nije bitan za rad forme
            }
        }

        private static void Naslov(GridTableDescriptor td, string kolona, string naslov)
        {
            if (td.Columns.IndexOf(kolona) >= 0)
                td.Columns[kolona].HeaderText = naslov;
        }

        private static void SamoCitanje(GridTableDescriptor td, string kolona)
        {
            if (td.Columns.IndexOf(kolona) < 0) return;
            var stil = td.Columns[kolona].Appearance.AnyRecordFieldCell;
            stil.ReadOnly = true;
            stil.BackColor = Color.FromArgb(0xF4, 0xF7, 0xFA);
        }

        private static void Broj(GridTableDescriptor td, string kolona, string format)
        {
            if (td.Columns.IndexOf(kolona) < 0) return;
            var stil = td.Columns[kolona].Appearance.AnyRecordFieldCell;
            stil.HorizontalAlignment = GridHorizontalAlignment.Right;
            stil.Format = format;
        }

        private static void Lista(GridTableDescriptor td, string kolona, string naslov, object izvor, string prikaz, string vrednost)
        {
            if (td.Columns.IndexOf(kolona) < 0 || izvor == null) return;
            td.Columns[kolona].HeaderText = naslov;
            var stil = td.Columns[kolona].Appearance.AnyRecordFieldCell;
            stil.CellType = "ComboBox";
            stil.DataSource = izvor;
            stil.DisplayMember = prikaz;
            stil.ValueMember = vrednost;
            stil.ExclusiveChoiceList = true;
            stil.DropDownStyle = GridDropDownStyle.Exclusive;
        }

        #endregion
    }
}
