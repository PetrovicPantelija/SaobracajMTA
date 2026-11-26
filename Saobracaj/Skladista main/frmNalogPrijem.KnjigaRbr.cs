using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Skladista_main
{
    // RAZ-17: redni broj iz carinske knjige (C7 polje 49: skladište / RBR / datum, npr. 42072/1008/690/03.04.2025).
    // Brojač je godišnji po carinskom skladištu; svaki C7 ima svoj magacinski broj, pa redni broj stoji na magacinskom
    // predmetu (MagacinskiPredmet.RbrKnjige), a godina je godina magacinskog predmeta (MagacinskiPredmet.Godina).
    // Interni magacinski broj ostaje tehnički ključ. Kolona se dodaje skriptom Carina_RbrKnjige_2026_11.sql;
    // bez nje je polje zaključano.
    public partial class frmNalogPrijem
    {
        Label lblRbrKnjige;
        TextBox txtRbrKnjige;
        Button btnRbrKnjigeSledeci;
        bool? kolonaRbrKnjige;

        // poziva se iz InitMagacinskiPredmet, pre InitRaspored (grupa uzima visinu panela)
        private void InitRbrKnjige()
        {
            int vrh = panelMP.Height;
            panelMP.Height += 56;

            lblRbrKnjige = new Label
            {
                Text = "RBR U CARINSKOJ KNJIZI",
                AutoSize = true,
                Font = label36.Font,
                ForeColor = label36.ForeColor,
                Location = new Point(label36.Left, vrh + 2)
            };
            txtRbrKnjige = new TextBox
            {
                Font = txtSklad.Font,
                Location = new Point(txtKoleta.Left, vrh + 21),
                Width = txtKoleta.Width,
                MaxLength = 7,
                TextAlign = HorizontalAlignment.Right
            };
            txtRbrKnjige.KeyPress += (s, e) => { if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true; };
            btnRbrKnjigeSledeci = new Button
            {
                Text = "Sledeći broj",
                Font = txtSklad.Font,
                Location = new Point(txtNeto.Left, vrh + 20),
                Width = txtNeto.Width,
                Height = txtKoleta.Height + 2
            };
            btnRbrKnjigeSledeci.Click += (s, e) => PredloziRbrKnjige();

            var tip = new ToolTip();
            tip.SetToolTip(txtRbrKnjige, "Redni broj iz carinske knjige, iz polja 49 C7 (npr. 42072/1008/690/03.04.2025 → 690).\nJedinstven po godini i carinskom skladištu.");
            tip.SetToolTip(btnRbrKnjigeSledeci, "Predlog: najveći redni broj u knjizi za ovu godinu i skladište + 1");

            panelMP.Controls.Add(lblRbrKnjige);
            panelMP.Controls.Add(txtRbrKnjige);
            panelMP.Controls.Add(btnRbrKnjigeSledeci);
        }

        private bool KolonaRbrKnjigePostoji(SqlConnection conn)
        {
            if (!kolonaRbrKnjige.HasValue)
            {
                using (var cmd = new SqlCommand("SELECT COL_LENGTH('dbo.MagacinskiPredmet', 'RbrKnjige')", conn))
                {
                    object v = cmd.ExecuteScalar();
                    kolonaRbrKnjige = v != null && v != DBNull.Value;
                }
                if (!kolonaRbrKnjige.Value)
                {
                    txtRbrKnjige.Enabled = btnRbrKnjigeSledeci.Enabled = false;
                    new ToolTip().SetToolTip(lblRbrKnjige, "Polje radi posle skripte Carina_RbrKnjige_2026_11.sql");
                }
            }
            return kolonaRbrKnjige.Value;
        }

        private int? RbrKnjigeIzPolja()
        {
            int rbr;
            if (int.TryParse(txtRbrKnjige.Text.Trim(), out rbr) && rbr > 0)
                return rbr;
            return null;
        }

        // poziva se na kraju VratiPodatkeMP
        private void UcitajRbrKnjige()
        {
            txtRbrKnjige.Text = "";
            int rn;
            if (!int.TryParse(txtID.Text, out rn))
                return;
            try
            {
                using (var conn = new SqlConnection(connection))
                {
                    conn.Open();
                    if (!KolonaRbrKnjigePostoji(conn))
                        return;
                    using (var cmd = new SqlCommand("SELECT TOP 1 RbrKnjige FROM MagacinskiPredmet WHERE RN = @RN ORDER BY ID", conn))
                    {
                        cmd.Parameters.AddWithValue("@RN", rn);
                        object v = cmd.ExecuteScalar();
                        if (v != null && v != DBNull.Value)
                            txtRbrKnjige.Text = v.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Redni broj iz carinske knjige nije učitan: " + ex.Message);
            }
        }

        // isti uslov za predlog i za proveru duplikata: godina magacinskog predmeta i carinsko skladište
        const string UslovKnjige = @"mp.Godina = @Godina
              AND RTRIM(CONVERT(nvarchar(50), ISNULL(mp.SifraSkladista, rns.CarinskoSkladiste))) = @Skladiste";

        private void PredloziRbrKnjige()
        {
            try
            {
                using (var conn = new SqlConnection(connection))
                {
                    conn.Open();
                    if (!KolonaRbrKnjigePostoji(conn))
                        return;
                    using (var cmd = new SqlCommand(@"SELECT ISNULL(MAX(mp.RbrKnjige), 0) + 1
                        FROM MagacinskiPredmet mp
                        JOIN RadniNalogSkladista rns ON rns.ID = mp.RN
                        WHERE " + UslovKnjige, conn))
                    {
                        cmd.Parameters.AddWithValue("@Godina", (int)numGodina.Value);
                        cmd.Parameters.AddWithValue("@Skladiste", txtSklad.Text.Trim());
                        txtRbrKnjige.Text = Convert.ToString(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Predlog rednog broja nije uspeo: " + ex.Message);
            }
        }

        // vrednost iz polja u trenutku provere: Snimi posle upisa ponovo učitava magacinski predmet (VratiPodatkeMP)
        int? rbrKnjigeZaSnimanje;
        bool rbrKnjigeProveren;

        // pre snimanja: isti redni broj iste godine i skladišta ne sme imati drugi nalog
        private bool RbrKnjigeIspravan()
        {
            rbrKnjigeProveren = false;
            rbrKnjigeZaSnimanje = null;
            if (txtRbrKnjige == null)
                return true;
            if (txtRbrKnjige.Text.Trim() == "")
            {
                rbrKnjigeProveren = true;
                return true;
            }
            int? rbr = RbrKnjigeIzPolja();
            if (!rbr.HasValue)
            {
                MessageBox.Show("Redni broj u carinskoj knjizi mora biti ceo broj veći od 0.", "Carinska knjiga",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRbrKnjige.Focus();
                return false;
            }
            int rn;
            int.TryParse(txtID.Text, out rn);
            try
            {
                using (var conn = new SqlConnection(connection))
                {
                    conn.Open();
                    if (!KolonaRbrKnjigePostoji(conn))
                        return true;
                    using (var cmd = new SqlCommand(@"SELECT TOP 1 mp.RN
                        FROM MagacinskiPredmet mp
                        JOIN RadniNalogSkladista rns ON rns.ID = mp.RN
                        WHERE mp.RbrKnjige = @Rbr AND mp.RN <> @RN AND " + UslovKnjige, conn))
                    {
                        cmd.Parameters.AddWithValue("@Rbr", rbr.Value);
                        cmd.Parameters.AddWithValue("@RN", rn);
                        cmd.Parameters.AddWithValue("@Godina", (int)numGodina.Value);
                        cmd.Parameters.AddWithValue("@Skladiste", txtSklad.Text.Trim());
                        object drugi = cmd.ExecuteScalar();
                        if (drugi != null && drugi != DBNull.Value)
                        {
                            MessageBox.Show("Redni broj " + rbr.Value + " u carinskoj knjizi za " + (int)numGodina.Value +
                                ". godinu već ima nalog " + drugi + ".", "Carinska knjiga", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtRbrKnjige.Focus();
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Provera rednog broja u knjizi nije uspela: " + ex.Message);
                return false;
            }
            rbrKnjigeZaSnimanje = rbr;
            rbrKnjigeProveren = true;
            return true;
        }

        // posle Snimi: magacinski predmet postoji tek kad ga upiše procedura (status LO)
        private void SnimiRbrKnjige()
        {
            int rn;
            if (txtRbrKnjige == null || !rbrKnjigeProveren || !int.TryParse(txtID.Text, out rn))
                return;
            rbrKnjigeProveren = false;
            int? rbr = rbrKnjigeZaSnimanje;
            txtRbrKnjige.Text = rbr.HasValue ? rbr.Value.ToString() : "";
            try
            {
                using (var conn = new SqlConnection(connection))
                {
                    conn.Open();
                    if (!KolonaRbrKnjigePostoji(conn))
                        return;
                    using (var cmd = new SqlCommand("UPDATE MagacinskiPredmet SET RbrKnjige = @Rbr WHERE RN = @RN", conn))
                    {
                        cmd.Parameters.AddWithValue("@Rbr", rbr.HasValue ? (object)rbr.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@RN", rn);
                        if (cmd.ExecuteNonQuery() == 0 && rbr.HasValue)
                            MessageBox.Show("Magacinski predmet još ne postoji, pa redni broj iz carinske knjige nije upisan.\n" +
                                "Unesite ga ponovo kad se magacinski predmet napravi.", "Carinska knjiga",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Redni broj iz carinske knjige nije snimljen: " + ex.Message);
            }
        }
    }
}
