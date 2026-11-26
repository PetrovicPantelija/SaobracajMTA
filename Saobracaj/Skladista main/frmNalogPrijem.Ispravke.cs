using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Saobracaj.Skladista_main
{
    // Ispravke posle testa carinskog prijema (Carina-Prijemnica-Ispravke, ISP-04, ISP-05, ISP-06, ISP-09).
    // Granice brojčanih polja se postavljaju posle InitializeComponent, da se dizajner ne menja.
    public partial class frmNalogPrijem
    {
        private void PodesiBrojcanaPolja()
        {
            // ISP-05: granice koje primaju kontejner (kg) i vrednost pošiljke; PDV i carina su procenti
            PostaviMaksimum(99999999m, txtKoleta, txtKoletaE, txtKolicinaJM, txtPaleta);
            PostaviMaksimum(999999999.999m, txtNeto, txtBruto, txtNetoE, txtBrutoE, txtVrednost, txtVrednostE);
            PostaviMaksimum(100m, txtPDV, txtCarina);

            // ISP-04: NHM naimenovanja ima 7 cifara (0 = nije unet)
            txtNHMNaimenovanje.Minimum = 0;
            txtNHMNaimenovanje.DecimalPlaces = 0;
            txtNHMNaimenovanje.Maximum = 9999999m;
            // dvoklik na naimenovanje (obrada iz dizajnera puni polja) pamti učitani NHM, da izmena ne traži 7 cifara
            gridGroupingControl1.TableControlCurrentCellControlDoubleClick += (s, e) => ZapamtiUcitaniNHM();

            // ISP-06: klik ili ulazak u brojčano polje označava ceo sadržaj, pa novi broj zamenjuje stari
            foreach (var polje in SvaBrojcanaPolja(this))
            {
                var p = polje;
                p.Enter += (s, e) => { p.Tag = "oznaci"; p.BeginInvoke(new Action(() => p.Select(0, p.Text.Length))); };
                p.MouseUp += (s, e) =>
                {
                    if (Equals(p.Tag, "oznaci"))
                    {
                        p.Select(0, p.Text.Length);
                        p.Tag = null;
                    }
                };
            }
        }

        private static void PostaviMaksimum(decimal maksimum, params NumericUpDown[] polja)
        {
            foreach (var p in polja)
                if (p != null && p.Maximum < maksimum)
                    p.Maximum = maksimum;
        }

        private static IEnumerable<NumericUpDown> SvaBrojcanaPolja(Control roditelj)
        {
            foreach (Control c in roditelj.Controls)
            {
                if (c is NumericUpDown n)
                    yield return n;
                foreach (var dete in SvaBrojcanaPolja(c))
                    yield return dete;
            }
        }

        // NHM naimenovanja učitanog dvoklikom iz tabele (null kad nije učitano)
        decimal? nhmUcitan;

        private void ZapamtiUcitaniNHM()
        {
            nhmUcitan = null;
            try
            {
                var zapis = gridGroupingControl1.Table.CurrentRecord;
                object v = zapis == null ? null : zapis.GetValue("NHM naimenovanja");
                decimal d;
                if (v != null && v != DBNull.Value && decimal.TryParse(v.ToString(), out d))
                    nhmUcitan = d;
            }
            catch
            {
                // bez učitane vrednosti važi pravilo od 7 cifara
            }
        }

        // ISP-04: NHM mora imati tačno 7 cifara ili biti 0 (nije unet).
        // Izmena postojećeg naimenovanja prolazi dok se NHM ne menja: stara naimenovanja imaju NHM unet pre ovog pravila.
        private bool NHMIspravan(bool izmenaPostojeceg = false)
        {
            decimal nhm = txtNHMNaimenovanje.Value;
            if (izmenaPostojeceg && nhmUcitan.HasValue && nhm == nhmUcitan.Value)
                return true;
            if (nhm != 0 && (nhm < 1000000m || nhm > 9999999m))
            {
                MessageBox.Show("NHM mora imati 7 cifara.");
                txtNHMNaimenovanje.Focus();
                return false;
            }
            return true;
        }

        // Raspored kao na Prijemnici: grupe koje se skrivaju i pokazuju, bez praznih delova; tabela naimenovanja
        // popunjava ostatak prozora, a desnim klikom (ili dugmetom Kolone) biraju se kolone koje se prikazuju.
        // Poziva se posle podesiPolja: grupe čije je kontrole forma sakrila (npr. komercijalni nalog) se ne prikazuju.
        GrupeForme grupeForme;
        const string KljucKolona = "NalogPrijemNaimenovanja";

        private void InitRaspored()
        {
            grupeForme = new GrupeForme(this, "NalogPrijem");
            grupeForme.Dodaj("Osnovni", "Osnovni podaci", true, false, false,
                label1, txtID, lblMagacinskiBroj, cboMagacinskiBroj, btnMagacinskiBroj, lblCarinskoSkladiste, txtCarinskoSkladiste,
                label4, cboNalogodavac, lblCarinskiPostupak, cboCarinskiPostupak, label44, txtOpisPosla);
            grupeForme.Dodaj("Vlasnik", "Vlasnik i roba", true, false, true, panel1);
            grupeForme.Dodaj("Predmet", "Magacinski predmet, prevoz i dodatne usluge", true, true, true, panelMP, panelPrijem2, panel4);
            grupeForme.Dodaj("Unos", "Unos naimenovanja", true, false, true, panelUnosStavki);

            var traka = new FlowLayoutPanel { Height = 40, Padding = new Padding(8, 5, 8, 2), WrapContents = false, BackColor = BackColor };
            var btnKolone = new Button { Text = "Kolone", AutoSize = true, MinimumSize = new System.Drawing.Size(100, 30), UseVisualStyleBackColor = true };
            var meni = KoloneGrida.Ukljuci(gridGroupingControl1, KljucKolona);
            btnKolone.Click += (s, e) => meni.Show(btnKolone, new System.Drawing.Point(0, btnKolone.Height));
            traka.Controls.Add(btnKolone);
            traka.Controls.Add(new Label { Text = "Desni klik na tabelu: izbor kolona koje se prikazuju.", AutoSize = true, Margin = new Padding(12, 9, 0, 0) });

            grupeForme.Zavrsi(panelStavke, traka, panelMPEdit);
        }

        // Da li stavke prijemnice upućuju na naimenovanje (kolona IDNaimenovanja postoji tek posle skripte packing liste)
        private bool NaimenovanjeKoristePrijemnice(int idNaimenovanja)
        {
            const string upit = "IF COL_LENGTH('dbo.RNCarinskoPrijemnicaStavke', 'IDNaimenovanja') IS NULL SELECT 0 " +
                "ELSE EXEC sp_executesql N'SELECT COUNT(*) FROM dbo.RNCarinskoPrijemnicaStavke WHERE IDNaimenovanja = @ID', N'@ID int', @ID = @ID";
            using (var conn = new System.Data.SqlClient.SqlConnection(connection))
            using (var cmd = new System.Data.SqlClient.SqlCommand(upit, conn))
            {
                cmd.Parameters.AddWithValue("@ID", idNaimenovanja);
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        // Vrsta komercijalnog naloga stiže kao „Komercijalno“ (TipSkladista, VrstaRN iz baze);
        // ranije poređenje sa „Komercijalo“ nikad nije bilo tačno, pa se lista brojeva nije punila.
        private bool JeKomercijalno
        {
            get { return string.Equals((Vrsta ?? "").Trim(), "Komercijalno", StringComparison.OrdinalIgnoreCase); }
        }

        // Tabela MagacinskiBrojKomercijalni se koristi samo ovde; ako je u bazi nema, lista ostaje prazna kao do sada
        // (upit nad nepostojećom tabelom bi oborio formu pri kliku na „?“).
        private bool PostojiTabelaKomercijalnihBrojeva()
        {
            using (var conn = new System.Data.SqlClient.SqlConnection(connection))
            using (var cmd = new System.Data.SqlClient.SqlCommand("SELECT CASE WHEN OBJECT_ID('dbo.MagacinskiBrojKomercijalni') IS NULL THEN 0 ELSE 1 END", conn))
            {
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()) == 1;
            }
        }

        // ISP-09: lista magacinskih brojeva se puni pri otvaranju forme (ne tek posle klika na „?“)
        private void UcitajMagacinskeBrojeve()
        {
            if (Vrsta != "Carinsko" && !JeKomercijalno)
                return;
            try
            {
                FillMagacinskiBroj();
            }
            catch (Exception)
            {
                // lista će se napuniti pri učitavanju naloga ili klikom na „?“
            }
        }
    }
}
