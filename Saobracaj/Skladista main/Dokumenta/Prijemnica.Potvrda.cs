using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace Saobracaj.Skladista_main.Dokumenta
{
    // ISP-08: potvrda prijemnice je odvojena od snimanja. Posle provera (CAR-P04, CAR-P05, CAR-PL01) poziva se
    // ista procedura kao pri potvrdi viljuškariste (PotvrdiPrijem: status ZA i promet skladišta),
    // pa se potvrđuje interni radni nalog (ranije je to radio Snimi).
    public partial class Prijemnica
    {
        Button btnPotvrdiPrijemnicu;

        private Button NapraviDugmePotvrde()
        {
            if (!ImaPLKolone)
                return null;
            btnPotvrdiPrijemnicu = new Button
            {
                Text = "Potvrdi prijemnicu",
                Size = new System.Drawing.Size(160, 32),
                UseVisualStyleBackColor = true,
                Margin = new Padding(24, 0, 0, 0)
            };
            btnPotvrdiPrijemnicu.Click += (s, e) => PotvrdiPrijemnicu();
            return btnPotvrdiPrijemnicu;
        }

        private void PotvrdiPrijemnicu()
        {
            ZavrsiUnosUGridu();
            if (txtPrijemnica.Text == "")
            {
                MessageBox.Show("Prvo snimite prijemnicu (Snimi).");
                return;
            }
            if (PotpisStavki() != stavkeSnimak || list.Any(x => x.ID == 0))
            {
                MessageBox.Show("Prijemnica ima nesnimljene izmene. Snimite (Snimi), pa potvrdite.");
                return;
            }
            if (list.Count == 0)
            {
                MessageBox.Show("Prijemnica nema stavki.");
                return;
            }

            try
            {
                UcitajStatusPrijemnice();
                if (StatusPrijemnice == "ZA")
                {
                    MessageBox.Show("Prijemnica je već potvrđena.");
                    return;
                }

                // CAR-P04 i CAR-P05: uparivanje i kontrola po naimenovanju
                var naimenovanja = UcitajNaimenovanja();
                string napomena = "";
                if (naimenovanja.Rows.Count > 0)
                {
                    var bez = list.Where(x => !x.IDNaimenovanja.HasValue).Select(x => x.RB.ToString()).ToList();
                    if (bez.Count > 0)
                    {
                        MessageBox.Show("Stavke bez naimenovanja (RB " + string.Join(", ", bez) + "). Uparite ih (Uparivanje i kontrola) i snimite.");
                        return;
                    }

                    var kontrola = IzracunajKontrolu(naimenovanja);
                    var prekoraceno = kontrola.Where(k => k.Status == "Prekoračeno").ToList();
                    if (prekoraceno.Count > 0)
                    {
                        MessageBox.Show("Primljeno je više nego što je prijavljeno carini, potvrda nije moguća:\n\n" +
                            string.Join("\n", prekoraceno.Select(k => k.Naimenovanje + ", " + k.Mera + ": razlika " + (-k.Preostalo).ToString("N3"))));
                        return;
                    }
                    var delimicno = kontrola.Where(k => k.Status == "Delimično").ToList();
                    if (delimicno.Count > 0)
                        napomena = "\n\nDelimičan prijem (preostalo za sledeće prijemnice):\n" +
                            string.Join("\n", delimicno.Select(k => k.Naimenovanje + ", " + k.Mera + ": " + k.Preostalo.ToString("N3")));
                }

                // CAR-PL01: sva koleta raspoređena na palete
                if (ImaPalete)
                {
                    var neraspoređene = UcitajPalStavke().Where(x => x.Preostalo != 0).ToList();
                    if (neraspoređene.Count > 0)
                    {
                        MessageBox.Show("Koleta nisu raspoređena na palete (Dodeli palete), potvrda nije moguća:\n\n" +
                            string.Join("\n", neraspoređene.Select(x => "RB " + x.RB + " " + x.Artikal + ": raspoređeno " + x.Rasporedjeno.ToString("N0")
                                + " od " + x.Koleta.ToString("N0"))));
                        return;
                    }
                }

                // CAR-PP08: kad je podešavanje uključeno, svaka paleta mora imati polje
                if (ImaPolja && Administracija.Podesavanja.Get("Carina.Polja.ObaveznoZaPotvrdu", "0").Trim() == "1")
                {
                    int bezPolja;
                    using (var conn = new SqlConnection(connection))
                    using (var cmd = new SqlCommand("SELECT COUNT(*) FROM RNCarinskoPaleta WHERE IDPrijemnice = @P AND IDSkladista IS NULL", conn))
                    {
                        cmd.Parameters.AddWithValue("@P", Convert.ToInt32(txtPrijemnica.Text));
                        conn.Open();
                        bezPolja = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    if (bezPolja > 0)
                    {
                        MessageBox.Show("Paleta bez polja: " + bezPolja + ". Dodelite polja (Dodeli palete > Dodeli polje), pa potvrdite.");
                        return;
                    }
                }

                if (MessageBox.Show("Potvrđuje se prijemnica " + txtPrijemnica.Text + ": status ZA i knjiženje u promet skladišta.\n"
                        + "Posle potvrde stavke i palete menja samo korisnik sa pravom korekcije." + napomena + "\n\nNastaviti?",
                        "Potvrdi prijemnicu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                int idPrijemnice = Convert.ToInt32(txtPrijemnica.Text);
                using (var conn = new SqlConnection(connection))
                using (var cmd = new SqlCommand("PotvrdiPrijem", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add("@Prijemnica", SqlDbType.Int).Value = idPrijemnice;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                int nalogID = VratiNalogIDF();
                if (nalogID > 0)
                    new Saobracaj.Uvoz.InsertRadniNalogInterni().UpdRadniNalogInterniIzvozPrijemnicaPotvrdjen(nalogID);

                UcitajStatusPrijemnice();
                VratiStavkePrijemnice(idPrijemnice);
                stavkeSnimak = PotpisStavki();

                MessageBox.Show(StatusPrijemnice == "ZA"
                    ? "Prijemnica je potvrđena."
                    : "Procedura potvrde je izvršena, ali status prijemnice je „" + StatusPrijemnice + "“ (očekivano ZA). Proverite proceduru PotvrdiPrijem.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Prijemnica nije potvrđena: " + ex.Message);
            }
        }
    }
}
