using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Saobracaj.TrackModal.Sifarnici
{
    // Pregled skladišnog prostora: Zona -> Lokacija -> Objekat -> Polje -> Pozicija.
    // Izbor (čekiranje) na višem nivou sužava liste svih nivoa ispod; "Uključi sve" uzima sve stavke liste.
    // Aktivni / Neaktivni određuju koji se zapisi uzimaju u obzir na svim nivoima (oba = svi).
    // Otvara se dugmetom "Pregled" sa formi Zone, Lokacije, Objekti, Polja i Pozicije.
    public partial class frmPregledSkladistaGrupa : Form
    {
        // Jedan nivo filtera: lista sa čekiranjem, "Uključi sve" i podaci nivoa (ID, Prikaz, RoditeljID, Aktivan)
        private class Nivo
        {
            public CheckedListBox Lista;
            public CheckBox Sve;
            public DataTable Podaci;
            public readonly HashSet<int> Izabrani = new HashSet<int>();
        }

        private class Stavka
        {
            public int ID;
            public string Tekst;
            public override string ToString() { return Tekst; }
        }

        private Nivo[] nivoi;
        private bool puni;   // liste se pune iz koda: događaji čekiranja se ne obrađuju

        public frmPregledSkladistaGrupa()
        {
            InitializeComponent();
        }

        // Jedan prozor pregleda; ako je već otvoren, prebacuje se na njega
        public static void Otvori()
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f is frmPregledSkladistaGrupa)
                {
                    f.WindowState = FormWindowState.Normal;
                    f.Activate();
                    return;
                }
            }
            new frmPregledSkladistaGrupa().Show();
        }

        // Dugme "Pregled" na formama šifarnika: u traci panelHeader (Leget) i u meniju meniHeader
        public static void DodajDugme(Panel panelHeader, ToolStrip meniHeader)
        {
            var dugme = new Button
            {
                AutoSize = true,
                Dock = DockStyle.Left,
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel),
                ForeColor = System.Drawing.Color.FromArgb(51, 51, 54),
                Text = "Pregled",
                UseVisualStyleBackColor = true
            };
            dugme.FlatAppearance.BorderSize = 0;
            dugme.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            dugme.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            dugme.Click += (s, e) => Otvori();

            // u isti kontejner kao Refresh (na Poljima i Pozicijama dugmad su u unutrašnjem panelu trake),
            // poslednje u nizu dugmadi poravnatih levo, tj. odmah desno od Refresh
            Control kontejner = KontejnerDugmeta(panelHeader, "Refresh") ?? panelHeader;
            kontejner.Controls.Add(dugme);
            dugme.BringToFront();

            var stavka = new ToolStripButton("Pregled") { DisplayStyle = ToolStripItemDisplayStyle.Text };
            stavka.Click += (s, e) => Otvori();
            meniHeader.Items.Add(stavka);
        }

        private static Control KontejnerDugmeta(Control koren, string tekst)
        {
            foreach (Control c in koren.Controls)
            {
                if (c is Button && c.Text == tekst)
                    return koren;
                Control nadjen = KontejnerDugmeta(c, tekst);
                if (nadjen != null)
                    return nadjen;
            }
            return null;
        }

        private void frmPregledSkladistaGrupa_Load(object sender, EventArgs e)
        {
            SifarnikGrid.Podesi(gridGroupingControl1);

            nivoi = new[]
            {
                new Nivo { Lista = clbZone, Sve = chkSveZone },
                new Nivo { Lista = clbLokacije, Sve = chkSveLokacije },
                new Nivo { Lista = clbObjekti, Sve = chkSveObjekti },
                new Nivo { Lista = clbPolja, Sve = chkSvePolja },
            };
            for (int i = 0; i < nivoi.Length; i++)
            {
                int indeks = i;
                nivoi[i].Lista.ItemCheck += (s, ev) => Lista_ItemCheck(indeks, ev);
                nivoi[i].Sve.CheckedChanged += (s, ev) => Sve_CheckedChanged(indeks);
            }

            UcitajNivoe();
        }

        private void UcitajNivoe()
        {
            const string kolone = "ID, ISNULL(Oznaka, '') + ' – ' + ISNULL(Naziv, '') AS Prikaz, ";
            try
            {
                nivoi[0].Podaci = HijerarhijaSkladista.Tabela("SELECT " + kolone
                    + "CAST(NULL AS int) AS RoditeljID, CASE WHEN ISNULL(Aktivna, 0) > 0 THEN 1 ELSE 0 END AS Aktivan "
                    + "FROM dbo.Zone WHERE ID IS NOT NULL ORDER BY Oznaka");
                nivoi[1].Podaci = HijerarhijaSkladista.Tabela("SELECT " + kolone
                    + "ZonaID AS RoditeljID, CASE WHEN ISNULL(Aktivna, 0) > 0 THEN 1 ELSE 0 END AS Aktivan "
                    + "FROM dbo.SkladistaGrupa WHERE ID IS NOT NULL ORDER BY Oznaka");
                nivoi[2].Podaci = HijerarhijaSkladista.Tabela("SELECT " + kolone
                    + "SkladistaGrupaID AS RoditeljID, CASE WHEN Aktivan > 0 THEN 1 ELSE 0 END AS Aktivan "
                    + "FROM dbo.SkladisteObjekat ORDER BY Oznaka");
                nivoi[3].Podaci = HijerarhijaSkladista.Tabela("SELECT " + kolone
                    + "SkladisteObjekatID AS RoditeljID, CASE WHEN ISNULL(Aktivan, 0) > 0 THEN 1 ELSE 0 END AS Aktivan "
                    + "FROM dbo.Skladista WHERE ID IS NOT NULL ORDER BY Oznaka");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Neuspešno učitavanje: " + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            OsveziOd(0);
        }

        private bool AktivnostOdgovara(object aktivan)
        {
            bool a = HijerarhijaSkladista.CeoBroj(aktivan) > 0;
            return (chkAktivni.Checked && a) || (chkNeaktivni.Checked && !a);
        }

        // Puni liste od zadatog nivoa naniže (svaka lista samo sa decom čekiranih stavki iznad), pa grid
        private void OsveziOd(int odNivoa)
        {
            if (nivoi == null || nivoi[0].Podaci == null)
                return;

            puni = true;
            try
            {
                for (int i = odNivoa; i < nivoi.Length; i++)
                {
                    HashSet<int> roditelji = i == 0 ? null : Cekirani(nivoi[i - 1]);
                    Nivo n = nivoi[i];
                    n.Lista.BeginUpdate();
                    n.Lista.Items.Clear();
                    foreach (DataRow r in n.Podaci.Rows)
                    {
                        if (!AktivnostOdgovara(r["Aktivan"]))
                            continue;
                        if (roditelji != null && !roditelji.Contains(HijerarhijaSkladista.CeoBroj(r["RoditeljID"])))
                            continue;
                        int id = HijerarhijaSkladista.CeoBroj(r["ID"]);
                        n.Lista.Items.Add(new Stavka { ID = id, Tekst = r["Prikaz"].ToString() }, n.Sve.Checked || n.Izabrani.Contains(id));
                    }
                    n.Lista.EndUpdate();
                }
            }
            finally
            {
                puni = false;
            }

            OsveziGrid();
        }

        private static HashSet<int> Cekirani(Nivo n)
        {
            return new HashSet<int>(n.Lista.CheckedItems.Cast<Stavka>().Select(s => s.ID));
        }

        private void Lista_ItemCheck(int indeks, ItemCheckEventArgs e)
        {
            if (puni)
                return;

            Nivo n = nivoi[indeks];
            int id = ((Stavka)n.Lista.Items[e.Index]).ID;
            if (e.NewValue == CheckState.Checked)
                n.Izabrani.Add(id);
            else
                n.Izabrani.Remove(id);

            // ručno odčekirana stavka isključuje "Uključi sve"; ostale čekirane stavke ostaju izabrane
            if (e.NewValue != CheckState.Checked && n.Sve.Checked)
            {
                foreach (Stavka s in n.Lista.CheckedItems)
                    n.Izabrani.Add(s.ID);
                n.Izabrani.Remove(id);
                puni = true;
                n.Sve.Checked = false;
                puni = false;
            }

            // ItemCheck dolazi pre promene stanja stavke, pa se niži nivoi osvežavaju posle nje
            BeginInvoke(new Action(() => OsveziOd(indeks + 1)));
        }

        private void Sve_CheckedChanged(int indeks)
        {
            if (puni)
                return;

            Nivo n = nivoi[indeks];
            if (!n.Sve.Checked)
                n.Izabrani.Clear();
            OsveziOd(indeks);
        }

        private void chkAktivnost_CheckedChanged(object sender, EventArgs e)
        {
            OsveziOd(0);
        }

        private void btnOsvezi_Click(object sender, EventArgs e)
        {
            UcitajNivoe();
        }

        // ---------------------------------------------------------------------------------
        // Grid: spisak pozicija čekiranih polja (sa granom Zona -> Lokacija -> Objekat -> Polje), po filterima
        // ---------------------------------------------------------------------------------
        private static string Lista(HashSet<int> idovi)
        {
            return idovi.Count == 0 ? "-1" : string.Join(",", idovi);
        }

        private void OsveziGrid()
        {
            string aktivnostPozicije;
            if (chkAktivni.Checked && chkNeaktivni.Checked)
                aktivnostPozicije = "1 = 1";
            else if (chkAktivni.Checked)
                aktivnostPozicije = "ISNULL(p.Aktivna, 0) > 0";
            else if (chkNeaktivni.Checked)
                aktivnostPozicije = "ISNULL(p.Aktivna, 0) = 0";
            else
                aktivnostPozicije = "1 = 0";

            HashSet<int> zone = Cekirani(nivoi[0]), lokacije = Cekirani(nivoi[1]),
                         objekti = Cekirani(nivoi[2]), polja = Cekirani(nivoi[3]);

            string select =
                "SELECT z.Oznaka AS ZonaSifra, z.Naziv AS Zona, g.Oznaka AS LokacijaSifra, g.Naziv AS Lokacija, "
                + " o.Oznaka AS ObjekatSifra, o.Naziv AS Objekat, ts.Naziv AS TipObjekta, "
                + " s.Oznaka AS PoljeSifra, s.Naziv AS Polje, "
                + " CASE WHEN s.VrstaPozicija = 2 THEN 'Paletne' ELSE 'Kontejnerske' END AS PoljePozicije, "
                + " p.ID AS PozicijaID, p.Oznaka AS PozicijaSifra, p.Opis AS Pozicija, p.Namena AS PozicijaSkNaziv, "
                + " CASE WHEN p.VrstaPozicija = 2 THEN 'Paletna' ELSE 'Kontejnerska' END AS VrstaPozicije, "
                + " CASE WHEN ISNULL(p.Aktivna, 0) > 0 THEN CAST(1 AS bit) ELSE CAST(0 AS bit) END AS PozicijaAktivna "
                // spisak pozicija: samo pozicije izabranih polja, objekata, lokacija i zona koje prolaze filter aktivnosti
                + " FROM dbo.Pozicija p "
                + " INNER JOIN dbo.Skladista s ON s.ID = p.Skladiste AND s.ID IN (" + Lista(polja) + ") "
                + " INNER JOIN dbo.SkladisteObjekat o ON o.ID = s.SkladisteObjekatID AND o.ID IN (" + Lista(objekti) + ") "
                + " INNER JOIN dbo.SkladistaGrupa g ON g.ID = o.SkladistaGrupaID AND g.ID IN (" + Lista(lokacije) + ") "
                + " INNER JOIN dbo.Zone z ON z.ID = g.ZonaID AND z.ID IN (" + Lista(zone) + ") "
                + " LEFT JOIN dbo.TipSkladista ts ON ts.ID = o.TipSkladistaID "
                + " WHERE " + aktivnostPozicije
                + " ORDER BY z.Oznaka, g.Oznaka, o.Oznaka, s.Oznaka, p.Oznaka";

            DataTable t;
            try
            {
                t = HijerarhijaSkladista.Tabela(select);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Neuspešno učitavanje pregleda: " + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SifarnikGrid.Napuni(gridGroupingControl1, t);
            SifarnikGrid.Kolona(gridGroupingControl1, "ZonaSifra", "Zona šifra", 80);
            SifarnikGrid.Kolona(gridGroupingControl1, "Zona", "Zona", 150);
            SifarnikGrid.Kolona(gridGroupingControl1, "LokacijaSifra", "Lokacija šifra", 90);
            SifarnikGrid.Kolona(gridGroupingControl1, "Lokacija", "Lokacija", 150);
            SifarnikGrid.Kolona(gridGroupingControl1, "ObjekatSifra", "Objekat šifra", 90);
            SifarnikGrid.Kolona(gridGroupingControl1, "Objekat", "Objekat", 150);
            SifarnikGrid.Kolona(gridGroupingControl1, "TipObjekta", "Tip objekta", 130);
            SifarnikGrid.Kolona(gridGroupingControl1, "PoljeSifra", "Polje šifra", 80);
            SifarnikGrid.Kolona(gridGroupingControl1, "Polje", "Polje", 150);
            SifarnikGrid.Kolona(gridGroupingControl1, "PoljePozicije", "Pozicije polja", 100);
            SifarnikGrid.Sakrij(gridGroupingControl1, "PozicijaID");
            SifarnikGrid.Kolona(gridGroupingControl1, "PozicijaSifra", "Pozicija šifra", 90);
            SifarnikGrid.Kolona(gridGroupingControl1, "Pozicija", "Pozicija", 180);
            SifarnikGrid.Kolona(gridGroupingControl1, "PozicijaSkNaziv", "SkNaziv", 90);
            SifarnikGrid.Kolona(gridGroupingControl1, "VrstaPozicije", "Vrsta pozicije", 100);
            SifarnikGrid.Kolona(gridGroupingControl1, "PozicijaAktivna", "Aktivna", 60);

            lblBroj.Text = "Zona: " + zone.Count + "   Lokacija: " + lokacije.Count + "   Objekata: " + objekti.Count
                + "   Polja: " + polja.Count + "   Pozicija: " + t.Rows.Count;
        }
    }
}
