using Saobracaj.Theme;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Saobracaj.HR
{
    // Opis tabele koju prikazuje i menja HRTabelaKartica
    public class HRTabelaDef
    {
        public string Naslov;                    // naslov dijaloga, npr. "Radno mesto"
        public string Tabela;                    // tabela u bazi
        public string Kljuc = "Sifra";
        public string RoditeljKolona;            // npr. "Zaposleni"; null za šifarnike
        public string Upit;                      // SELECT za tabelu; parametar @Roditelj
        public List<HRPolje> Polja = new List<HRPolje>();
        public HRKolona[] Kolone;
        public bool Audit = true;                // tabela ima KreiraoKorisnik / IzmenioKorisnik / DatumIzmene
        public bool SamoCitanje;
        public bool BezBrisanja;
        public string DodajTekst = "+ Dodaj";
        public string Napomena;                  // tekst ispod tabele
        public List<Tuple<string, string>> Filteri;   // naziv chip-a i DataView.RowFilter ("" = svi)
        public int BrojKolonaDijaloga = 2;
        public string PorukaBezRoditelja = "Prvo sačuvajte osnovne podatke.";
        public Func<bool> Dozvoljeno;                  // da li se zapisi trenutno smeju menjati (status dokumenta)
        public string PorukaZabranjeno = "Zapisi se u ovom statusu ne mogu menjati.";

        public Func<HRVrednosti, string> Validacija;
        public Action<frmHRStavka, string> PriPromeni;
        public Func<HRVrednosti, int, string> Snimi;   // posebno snimanje (procedura); vraća grešku ili null
        public Func<int, string> Obrisi;               // posebno brisanje; vraća grešku ili null
        public Func<HRVrednosti> NoviPodrazumevano;    // početne vrednosti novog zapisa
        public Func<Record, string, Color?> BojaTeksta;
        public Func<Record, Color?> BojaReda;
        public Func<DataTable, string> Brojac;         // tekst brojača u chip-u, npr. "Zaduženo (4)"
    }

    // Kartica sa tabelom zapisa: filteri levo, komande desno, tabela, napomena.
    public class HRTabelaKartica : UserControl
    {
        public readonly HRTabelaDef Def;
        public readonly GridGroupingControl Grid;
        public readonly FlowLayoutPanel Komande;
        private readonly FlowLayoutPanel filteri;
        private readonly List<HRChip> chipovi = new List<HRChip>();
        private readonly Label napomena;
        private int filter;
        public int Roditelj { get; private set; }
        public DataTable Podaci { get; private set; }
        public event Action Promenjeno;

        public HRTabelaKartica(HRTabelaDef def)
        {
            Def = def;
            BackColor = SavaTheme.Surface;
            Dock = DockStyle.Fill;

            var vrh = new Panel { Dock = DockStyle.Top, Height = 44, BackColor = Color.Transparent };
            Komande = new FlowLayoutPanel { Dock = DockStyle.Right, FlowDirection = FlowDirection.RightToLeft, WrapContents = false, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, BackColor = Color.Transparent };
            filteri = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false, BackColor = Color.Transparent };
            vrh.Controls.Add(filteri);
            vrh.Controls.Add(Komande);

            if (!def.SamoCitanje)
            {
                DodajKomandu(def.DodajTekst, (s, e) => Dodaj());
                DodajKomandu("Izmeni", (s, e) => Izmeni());
                if (!def.BezBrisanja)
                    DodajKomandu("Obriši", (s, e) => ObrisiIzabrani(), "danger");
            }

            if (def.Filteri != null)
            {
                for (int i = 0; i < def.Filteri.Count; i++)
                {
                    int indeks = i;
                    var chip = new HRChip { Text = def.Filteri[i].Item1, Aktivan = i == 0, Margin = new Padding(0, 0, 8, 0) };
                    chip.Click += (s, e) => { filter = indeks; PrikaziFilter(); };
                    chipovi.Add(chip);
                    filteri.Controls.Add(chip);
                }
            }

            var okvir = HRStil.BeliPanel();
            okvir.Dock = DockStyle.Fill;
            Grid = HRGrid.Novi();
            okvir.Controls.Add(Grid);
            HRGrid.Kolone(Grid, def.Kolone);
            if (def.BojaTeksta != null)
                HRGrid.BojaTeksta(Grid, def.BojaTeksta);
            if (def.BojaReda != null)
                HRGrid.BojaReda(Grid, def.BojaReda);
            if (!def.SamoCitanje)
                HRGrid.DuplikKlik(Grid, Izmeni);

            napomena = new Label { Dock = DockStyle.Bottom, Height = 28, TextAlign = ContentAlignment.MiddleLeft, Text = def.Napomena ?? "", Visible = !string.IsNullOrEmpty(def.Napomena) };

            Controls.Add(okvir);
            Controls.Add(new Panel { Dock = DockStyle.Top, Height = 8 });
            Controls.Add(vrh);
            Controls.Add(napomena);
        }

        public Button DodajKomandu(string tekst, EventHandler klik, string uloga = null)
        {
            var b = HRStil.Dugme(tekst, uloga);
            b.Margin = new Padding(8, 0, 0, 0);
            b.Click += klik;
            Komande.Controls.Add(b);
            return b;
        }

        public void PostaviNapomenu(string tekst, Color? boja = null)
        {
            napomena.Text = tekst ?? "";
            napomena.Visible = !string.IsNullOrEmpty(tekst);
            napomena.ForeColor = boja ?? SavaTheme.Label;
        }

        public void Ucitaj(int roditelj)
        {
            Roditelj = roditelj;
            Osvezi();
        }

        public void Osvezi()
        {
            try
            {
                Podaci = HRBaza.Tabela(Def.Upit, HRBaza.P("@Roditelj", Roditelj));
            }
            catch (Exception ex)
            {
                Podaci = new DataTable();
                PostaviNapomenu("Podaci nisu učitani: " + ex.Message, SavaTheme.Danger);
            }
            if (Def.Brojac != null && chipovi.Count > 0)
            {
                try
                {
                    string[] brojevi = Def.Brojac(Podaci).Split('|');
                    for (int i = 0; i < chipovi.Count && i < brojevi.Length; i++)
                        chipovi[i].Text = brojevi[i];
                }
                catch
                {
                    // brojač je samo prikaz; prazni ili neučitani podaci ga ne prikazuju
                }
            }
            PrikaziFilter();
        }

        private void PrikaziFilter()
        {
            for (int i = 0; i < chipovi.Count; i++)
                chipovi[i].Aktivan = i == filter;
            DataTable prikaz = Podaci;
            if (Podaci != null && Def.Filteri != null && filter < Def.Filteri.Count && !string.IsNullOrEmpty(Def.Filteri[filter].Item2))
            {
                var dv = new DataView(Podaci) { RowFilter = Def.Filteri[filter].Item2 };
                prikaz = dv.ToTable();
            }
            HRGrid.Napuni(Grid, prikaz ?? new DataTable());
        }

        public int IzabraniId()
        {
            return HRGrid.IzabraniId(Grid, Def.Kljuc);
        }

        private bool MozeUnos()
        {
            if (Def.RoditeljKolona != null && Roditelj <= 0)
            {
                MessageBox.Show(Def.PorukaBezRoditelja, Def.Naslov, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (Def.Dozvoljeno != null && !Def.Dozvoljeno())
            {
                MessageBox.Show(Def.PorukaZabranjeno, Def.Naslov, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        public void Dodaj()
        {
            if (!MozeUnos())
                return;
            Otvori(0, Def.NoviPodrazumevano?.Invoke() ?? new HRVrednosti());
        }

        public void Izmeni()
        {
            if (!MozeUnos())
                return;
            int id = IzabraniId();
            if (id <= 0)
            {
                MessageBox.Show("Izaberite red u tabeli.", Def.Naslov, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DataRow red = HRBaza.Red("SELECT * FROM dbo." + Def.Tabela + " WHERE " + Def.Kljuc + " = @K", HRBaza.P("@K", id));
            if (red == null)
                return;
            var v = new HRVrednosti();
            foreach (DataColumn c in red.Table.Columns)
                v[c.ColumnName] = red[c];
            Otvori(id, v);
        }

        private void Otvori(int id, HRVrednosti pocetne)
        {
            using (var dlg = new frmHRStavka((id == 0 ? "Novo: " : "Izmena: ") + Def.Naslov, Def.Polja, pocetne, Def.BrojKolonaDijaloga))
            {
                dlg.Validacija = Def.Validacija;
                if (Def.PriPromeni != null)
                    dlg.Promena += Def.PriPromeni;
                dlg.Snimanje = v => Def.Snimi != null ? Def.Snimi(v, id) : SnimiOpste(v, id);
                if (dlg.ShowDialog(FindForm()) == DialogResult.OK)
                {
                    Osvezi();
                    Promenjeno?.Invoke();
                }
            }
        }

        public string SnimiOpste(HRVrednosti v, int id)
        {
            SnimiOpste(Def, Roditelj, v, id);
            return null;
        }

        // Opšte snimanje: INSERT / UPDATE po opisu polja (nazivi kolona su iz koda, vrednosti su parametri).
        // Vraća šifru novog ili izmenjenog zapisa.
        public static int SnimiOpste(HRTabelaDef Def, int Roditelj, HRVrednosti v, int id)
        {
            var parametri = new List<SqlParameter>();
            var kolone = Def.Polja.Where(p => p.Snima).Select(p => p.Kolona).ToList();
            var sql = new StringBuilder();
            for (int i = 0; i < kolone.Count; i++)
                parametri.Add(HRBaza.P("@p" + i, v.Uzmi(kolone[i])));
            parametri.Add(HRBaza.P("@Korisnik", HRBaza.Korisnik));

            if (id == 0)
            {
                var nazivi = new List<string>(kolone.Select(k => "[" + k + "]"));
                var vrednosti = new List<string>(Enumerable.Range(0, kolone.Count).Select(i => "@p" + i));
                if (Def.RoditeljKolona != null)
                {
                    nazivi.Insert(0, "[" + Def.RoditeljKolona + "]");
                    vrednosti.Insert(0, "@Roditelj");
                    parametri.Add(HRBaza.P("@Roditelj", Roditelj));
                }
                if (Def.Audit)
                {
                    nazivi.Add("KreiraoKorisnik");
                    vrednosti.Add("@Korisnik");
                }
                // u transakciji: ako zapis ne dobije šifru (ključ nije IDENTITY), unos se poništava
                sql.Append("SET XACT_ABORT ON; BEGIN TRAN; ")
                   .Append("INSERT INTO dbo.").Append(Def.Tabela).Append(" (").Append(string.Join(", ", nazivi))
                   .Append(") VALUES (").Append(string.Join(", ", vrednosti)).Append("); ")
                   .Append("DECLARE @NoviID int = CAST(SCOPE_IDENTITY() AS int); IF @NoviID IS NULL ROLLBACK; ELSE COMMIT; SELECT @NoviID;");
                int novi = HRBaza.CeoBroj(HRBaza.Skalar(sql.ToString(), parametri.ToArray()));
                if (novi <= 0)
                    throw new InvalidOperationException("Zapis nije snimljen: tabela " + Def.Tabela + " ne dodeljuje šifru (ključ nije IDENTITY). Proverite SQL skripte HR modula.");
                return novi;
            }
            else
            {
                var dodele = kolone.Select((k, i) => "[" + k + "] = @p" + i).ToList();
                if (Def.Audit)
                {
                    dodele.Add("IzmenioKorisnik = @Korisnik");
                    dodele.Add("DatumIzmene = sysdatetime()");
                }
                sql.Append("UPDATE dbo.").Append(Def.Tabela).Append(" SET ").Append(string.Join(", ", dodele))
                   .Append(" WHERE ").Append(Def.Kljuc).Append(" = @Kljuc;");
                parametri.Add(HRBaza.P("@Kljuc", id));
            }
            HRBaza.Izvrsi(sql.ToString(), parametri.ToArray());
            return id;
        }

        private void ObrisiIzabrani()
        {
            if (!MozeUnos())
                return;
            int id = IzabraniId();
            if (id <= 0)
            {
                MessageBox.Show("Izaberite red u tabeli.", Def.Naslov, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Obrisati izabrani zapis?", Def.Naslov, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            try
            {
                string greska = Def.Obrisi != null
                    ? Def.Obrisi(id)
                    : ObrisiOpste(id);
                if (!string.IsNullOrEmpty(greska))
                {
                    MessageBox.Show(greska, Def.Naslov, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                MessageBox.Show("Zapis se koristi u drugim podacima i ne može da se obriše. Za šifarnik umesto brisanja isključite polje Aktivan.",
                    Def.Naslov, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Def.Naslov, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Osvezi();
            Promenjeno?.Invoke();
        }

        private string ObrisiOpste(int id)
        {
            HRBaza.Izvrsi("DELETE FROM dbo." + Def.Tabela + " WHERE " + Def.Kljuc + " = @K", HRBaza.P("@K", id));
            return null;
        }
    }
}
