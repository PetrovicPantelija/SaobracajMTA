using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Saobracaj.Skladista_main.Dokumenta
{
    // RAZ-11: Otpremnica je dokument potvrđene otpreme; stavke, magacinski broj, prijemnica i vlasnik dolaze iz Otpreme.
    // RAZ-13: Potvrdi izlaz paleta (palete izabrane na Otpremi postaju izdate, prazne palete se skidaju sa polja).
    // RAZ-14: Dokument razduženja (frmRazduzenje), posle fizičke otpreme.
    // Skripta: Skladista main\Carina_Izlaz_2026_11.sql. Bez nje rade samo izmene iz RAZ-11.
    public partial class Otpremnica
    {
        bool? imaIzlaz;
        Button btnPotvrdiIzlaz, btnRazduzenje;
        readonly ToolTip savetIzlaz = new ToolTip();

        private bool IzlazPostoji()
        {
            if (!imaIzlaz.HasValue)
            {
                try
                {
                    using (var conn = new SqlConnection(connection))
                    using (var cmd = new SqlCommand("SELECT CASE WHEN OBJECT_ID('dbo.sp_Izlaz_Potvrdi', 'P') IS NULL OR OBJECT_ID('dbo.sp_Razduzenje_Snimi', 'P') IS NULL THEN 0 ELSE 1 END", conn))
                    {
                        conn.Open();
                        imaIzlaz = Convert.ToInt32(cmd.ExecuteScalar()) == 1;
                    }
                }
                catch
                {
                    imaIzlaz = false;
                }
            }
            return imaIzlaz.Value;
        }

        // poziva se na kraju konstruktora
        private void PodesiIzlaz()
        {
            // RAZ-11: dugmad bez koda. Usluge, magacinski broj, prijemnica i vlasnik robe se menjaju na Otpremi;
            // Otpremnica ih samo prikazuje (Snimi otpremnice ih i nije upisivao).
            btnDodaj.Visible = false;
            btnIzbaci.Visible = false;
            cboDodatneUsluge.Enabled = false;
            btnPrijemnica.Visible = false;
            btnMagacinskiBroj.Visible = false;
            cboMagacinskiBroj.Enabled = false;
            cboVlasnikRobe.Enabled = false;

            if (!IzlazPostoji())
                return;

            btnPotvrdiIzlaz = NapraviDugmeIzlaza("Potvrdi izlaz paleta", btnPreuzmi.Right + 16, 130);
            btnPotvrdiIzlaz.Click += (s, e) => PotvrdiIzlazPaleta();
            btnRazduzenje = NapraviDugmeIzlaza("Dokument razduženja", btnPotvrdiIzlaz.Right + 8, 140);
            btnRazduzenje.AutoEllipsis = true;
            btnRazduzenje.Click += (s, e) => OtvoriRazduzenje();
            OsveziStanjeRazduzenja();
        }

        private Button NapraviDugmeIzlaza(string tekst, int x, int sirina)
        {
            var b = new Button
            {
                Text = tekst,
                Location = new Point(x, btnSnimi.Top),
                Size = new Size(sirina, btnSnimi.Height),
                Font = btnSnimi.Font,
                Anchor = btnSnimi.Anchor,
                UseVisualStyleBackColor = true
            };
            Controls.Add(b);
            b.BringToFront();
            return b;
        }

        private int IDOtpremnice()
        {
            int id;
            return int.TryParse(txtOtpremnica.Text.Trim(), out id) ? id : 0;
        }

        private DataTable IzlazTabela(string procedura, params SqlParameter[] parametri)
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(connection))
            using (var cmd = new SqlCommand(procedura, conn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddRange(parametri);
                using (var da = new SqlDataAdapter(cmd))
                    da.Fill(dt);
            }
            return dt;
        }

        private void PotvrdiIzlazPaleta()
        {
            int id = IDOtpremnice();
            if (id == 0)
            {
                MessageBox.Show("Prvo snimite otpremnicu (Snimi).");
                return;
            }

            DataTable palete;
            try
            {
                palete = IzlazTabela("sp_Izlaz_PaleteOtpremnice", new SqlParameter("@IDOtpremnice", id));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Palete otpremnice nisu učitane: " + ex.Message);
                return;
            }
            var rezervisane = palete.Rows.Cast<DataRow>().Where(r => Convert.ToString(r["Status"]) == "Rezervisano").ToList();
            if (rezervisane.Count == 0)
            {
                MessageBox.Show(palete.Rows.Count > 0
                    ? "Izlaz paleta ove otpremnice je već potvrđen."
                    : "Nalog otpreme nema izabranih paleta (Otprema, dugme IZBOR PALETA).");
                return;
            }

            int brojPaleta = rezervisane.Select(r => Convert.ToInt64(r["BrojPalete"])).Distinct().Count();
            int koleta = rezervisane.Sum(r => Convert.ToInt32(r["Koleta"]));
            int delimicno = rezervisane.Count(r => Convert.ToString(r["Uzima"]) == "deo");
            if (MessageBox.Show("Potvrđuje se izlaz otpremnice " + id + ": " + brojPaleta + " paleta, " + koleta.ToString("N0") + " koleta"
                    + (delimicno > 0 ? " (" + delimicno + " delimično)" : "") + ".\n"
                    + "Palete bez ostatka dobijaju status Izdata i skidaju se sa polja.\n\nNastaviti?",
                    "Potvrdi izlaz paleta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                var rez = IzlazTabela("sp_Izlaz_Potvrdi", new SqlParameter("@IDOtpremnice", id), new SqlParameter("@Korisnik", Korisnik ?? ""));
                var r = rez.Rows.Count > 0 ? rez.Rows[0] : null;
                MessageBox.Show("Izlaz je potvrđen."
                    + (r == null ? "" : "\nPalete: " + r["Palete"] + ", izdato koleta: " + Convert.ToInt32(r["Koleta"]).ToString("N0")
                        + ", palete bez ostatka (skinute sa polja): " + r["IzdatePalete"]));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Izlaz nije potvrđen: " + ex.Message);
            }
        }

        private void OtvoriRazduzenje()
        {
            int id = IDOtpremnice();
            if (id == 0)
            {
                MessageBox.Show("Prvo snimite otpremnicu (Snimi).");
                return;
            }
            using (var f = new frmRazduzenje(id, MagacinskiBroj))
                f.ShowDialog(this);
            OsveziStanjeRazduzenja();
        }

        // RAZ-14 pravilo 3: otpremnica bez dokumenta razduženja je „bez dokumenta“
        private void OsveziStanjeRazduzenja()
        {
            if (btnRazduzenje == null)
                return;
            int id = IDOtpremnice();
            string tekst = "Dokument razduženja";
            string savet = "Dokument razduženja (C4, IZ3, C3...) po naimenovanjima C7, posle fizičke otpreme.";
            if (id > 0)
            {
                try
                {
                    var dt = IzlazTabela("sp_Razduzenje_Lista", new SqlParameter("@IDOtpremnice", id));
                    if (dt.Rows.Count == 0)
                    {
                        tekst = "Razduženje: bez dokumenta";
                        savet = "Otpremnica još nema dokument razduženja.";
                    }
                    else
                    {
                        string dokumenti = string.Join(", ", dt.Rows.Cast<DataRow>().Select(r => Convert.ToString(r["Dokument"])));
                        tekst = "Razduženje: " + dokumenti;
                        savet = "Dokumenti razduženja otpremnice: " + dokumenti;
                    }
                }
                catch
                {
                    // bez stanja: dugme ostaje
                }
            }
            btnRazduzenje.Text = tekst;
            savetIzlaz.SetToolTip(btnRazduzenje, savet);
        }

        // poziva se na kraju NalogZaViljuskaristu: broj palete po polju (RAZ-13, nalog rukovaocu sa paletama)
        private void DopuniNalogPaletama()
        {
            if (!IzlazPostoji() || IDOtpremnice() == 0)
                return;
            var dt = dataGridView2.DataSource as DataTable;
            if (dt == null)
                return;

            DataTable palete;
            try
            {
                palete = IzlazTabela("sp_Izlaz_PaleteOtpremnice", new SqlParameter("@IDOtpremnice", IDOtpremnice()));
            }
            catch
            {
                return;
            }
            if (palete.Rows.Count == 0)
                return;

            var poPolju = palete.Rows.Cast<DataRow>()
                .GroupBy(r => Convert.ToString(r["Polje"]).Trim(), StringComparer.OrdinalIgnoreCase)
                .ToDictionary(g => g.Key, g => string.Join(", ", g.Select(r => Convert.ToString(r["BrojPalete"])
                    + (Convert.ToString(r["Uzima"]) == "deo" ? " (deo " + r["Koleta"] + ")" : "")).Distinct()), StringComparer.OrdinalIgnoreCase);

            if (!dt.Columns.Contains("Palete"))
                dt.Columns.Add("Palete", typeof(string));
            var iskorisceno = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (DataRow r in dt.Rows)
            {
                string polje = Convert.ToString(r["Pozicija"]).Trim();
                string lista;
                if (poPolju.TryGetValue(polje, out lista))
                {
                    r["Palete"] = lista;
                    iskorisceno.Add(polje);
                }
            }
            var ostala = poPolju.Where(p => !iskorisceno.Contains(p.Key)).ToList();
            if (ostala.Count > 0)
                label49.Text += "   Palete na drugim poljima: " + string.Join("; ", ostala.Select(p => (p.Key == "" ? "bez polja" : p.Key) + ": " + p.Value));

            if (dataGridView2.Columns.Contains("Palete"))
            {
                dataGridView2.Columns["Palete"].ReadOnly = true;
                dataGridView2.Columns["Palete"].Width = 220;
            }
        }
    }
}
