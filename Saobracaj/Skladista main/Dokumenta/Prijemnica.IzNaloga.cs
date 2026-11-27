using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace Saobracaj.Skladista_main.Dokumenta
{
    // Podaci uneti na nalogu za prijem koje VratiPodatke nije prenosio u prijemnicu (27.11.2026). Nalog ih čuva u
    // drugim kolonama nego što ih je prijemnica čitala (NacinPakovanjaInt, KontakOsobaSpediterIntPrijem, AdresaIntPrijem,
    // KontaktOsobaIstovarIntPrijem, PosebniUslovi kao ID, opis robe na magacinskom predmetu, PIB kod vlasnika robe),
    // a dodatne usluge u RadniNalogInterni (RN20). Popunjava se samo prazno polje: ono što je na prijemnici već upisano
    // ostaje. Poziva se iz konstruktora, posle VratiPodatke.
    public partial class Prijemnica
    {
        private void PreuzmiIzNaloga()
        {
            if (RN <= 0)
                return;
            try
            {
                using (var conn = new SqlConnection(connection))
                {
                    conn.Open();
                    PreuzmiZaglavljeNaloga(conn);
                    PreuzmiDodatneUslugeNaloga(conn);
                    PopuniMagacinskeBrojeve(conn);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Deo podataka sa naloga nije prenet u prijemnicu: " + ex.Message);
            }

            // broj C7 u obliku CI/C7/broj/godina (BrojC7.cs)
            new ToolTip().SetToolTip(txtSmestajniDokument, BrojC7.Opis);
            txtSmestajniDokument.Leave += (s, e) => txtSmestajniDokument.Text = NormalizujBrojC7(txtSmestajniDokument.Text);
        }

        private string NormalizujBrojC7(string unos)
        {
            return BrojC7.Normalizuj(unos, BrojC7.OznakaCI(connection, cboCarinarnicaPrijem.SelectedValue), DateTime.Now.Year);
        }

        private void PreuzmiZaglavljeNaloga(SqlConnection conn)
        {
            const string sql = @"
SELECT RTRIM(CONVERT(nvarchar(100), ISNULL(NULLIF(RTRIM(CONVERT(nvarchar(100), rns.SmestajniDokument)), N''), mp.BrojJCI))) AS BrojC7,
       RTRIM(CONVERT(nvarchar(500), ISNULL(mp.OpisRobe, N''))) AS OpisRobe,
       RTRIM(ISNULL(v.PIB, N'')) AS PIB,
       RTRIM(ISNULL(np.Oznaka + N' ', N'') + ISNULL(np.Naziv, N'')) AS NacinPakovanja,
       RTRIM(ISNULL(pu.Naziv, N'')) AS PosebniUslovi,
       RTRIM(ISNULL(ko.PaKOIme, N'')) + N' ' + RTRIM(ISNULL(ko.PaKoPriimek, N'')) AS KontaktSpeditera,
       LTRIM(RTRIM(ISNULL(adr.PaKOOpomba, N''))) AS Adresa,
       RTRIM(ISNULL(kis.PaKOIme, N'')) + N' ' + RTRIM(ISNULL(kis.PaKoPriimek, N'')) AS KontaktIstovara
FROM RadniNalogSkladista rns
LEFT JOIN MagacinskiPredmet mp ON mp.ID = (SELECT MIN(x.ID) FROM MagacinskiPredmet x WHERE x.RN = rns.ID)
LEFT JOIN VlasniciRobe v ON v.id = ISNULL(NULLIF(mp.VlasnikRobe, 0), rns.VlasnikRobe)
LEFT JOIN uvNacinPakovanja np ON np.ID = rns.NacinPakovanjaInt
LEFT JOIN PosebniUsoviSmestaja pu ON pu.ID = rns.PosebniUslovi
LEFT JOIN partnerjiKontOseba ko ON ko.PaKOSifra = rns.SpediterPrijem AND ko.PaKoZapSt = rns.KontakOsobaSpediterIntPrijem
LEFT JOIN partnerjiKontOsebaMU adr ON adr.PaKOSifra = rns.MestoIstovaraPrijem AND adr.PaKoZapSt = rns.AdresaIntPrijem
LEFT JOIN partnerjiKontOsebaMU kis ON kis.PaKOSifra = rns.MestoIstovaraPrijem AND kis.PaKoZapSt = rns.KontaktOsobaIstovarIntPrijem
WHERE rns.ID = @RN";

            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@RN", RN);
                using (var dr = cmd.ExecuteReader())
                {
                    if (!dr.Read())
                        return;
                    Dopuni(txtSmestajniDokument, NormalizujBrojC7(dr["BrojC7"].ToString()));
                    Dopuni(txtVrstaRobe, dr["OpisRobe"].ToString());
                    Dopuni(txtPIB, dr["PIB"].ToString());
                    Dopuni(txtNacinPakovanja, dr["NacinPakovanja"].ToString());
                    Dopuni(txtKontakOsobaSpediterPrijem, dr["KontaktSpeditera"].ToString());
                    Dopuni(txtAdresaPrijem, dr["Adresa"].ToString());
                    Dopuni(txtKontaktOsobaPrijem, dr["KontaktIstovara"].ToString());

                    // VratiPodatke upisuje ID posebnih uslova (broj); umesto broja ide naziv
                    int id;
                    string uslovi = dr["PosebniUslovi"].ToString();
                    if (uslovi != "" && (txtPosebniUslovi.Text.Trim() == "" || int.TryParse(txtPosebniUslovi.Text.Trim(), out id)))
                        txtPosebniUslovi.Text = uslovi;
                }
            }
        }

        private static void Dopuni(TextBox polje, string vrednost)
        {
            vrednost = (vrednost ?? "").Trim();
            if (polje != null && polje.Text.Trim() == "" && vrednost != "")
                polje.Text = vrednost;
        }

        // Nalog upisuje dodatne usluge u RadniNalogInterni (RN20, bez usluge 74 = sam prijem); FillDodatneUsluge čita
        // samo staru tabelu RNCarinskoSkladisteDodatneUsluge
        private void PreuzmiDodatneUslugeNaloga(SqlConnection conn)
        {
            const string sql = @"SELECT d.IDManipulacijaJED AS Usluga, RTRIM(v.Naziv) AS Naziv
                                 FROM RadniNalogInterni d
                                 INNER JOIN VrstaManipulacije v ON d.IDManipulacijaJED = v.ID
                                 WHERE d.BrojRN = @RN AND d.IDManipulacijaJED <> 74 AND d.TipRN = 'RN20'";
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@RN", RN);
                using (var dr = cmd.ExecuteReader())
                    while (dr.Read())
                    {
                        int usluga = Convert.ToInt32(dr["Usluga"]);
                        if (!dtUsluge.Rows.Cast<DataRow>().Any(r => Convert.ToInt32(r["UslugaID"]) == usluga))
                            dtUsluge.Rows.Add(usluga, dr["Naziv"].ToString());
                    }
            }
        }

        // Lista magacinskih brojeva: FillCombo je puni samo kad je forma otvorena sa vrstom „Carinsko“ (sa liste bez
        // otvorenog naloga vrsta stiže prazna), i prikazuje samo naziv, koji je često prazan. Ovde se puni po vrsti naloga
        // iz baze, a prikazuje se magacinski broj (sa nazivom, kad ga ima).
        private void PopuniMagacinskeBrojeve(SqlConnection conn)
        {
            string tabela = "MagacinskiBrojCarinski";
            if (!string.Equals(Vrsta, "Carinsko", StringComparison.OrdinalIgnoreCase))
            {
                using (var cmd = new SqlCommand("SELECT OBJECT_ID('dbo.MagacinskiBrojKomercijalni', 'U')", conn))
                {
                    object o = cmd.ExecuteScalar();
                    if (o != null && o != DBNull.Value)
                        tabela = "MagacinskiBrojKomercijalni";
                }
            }

            var dt = new DataTable();
            // carinski broj kao „broj/godina“ (MagacinskiBrojOznaka.cs)
            string prikaz = tabela == "MagacinskiBrojCarinski"
                ? MagacinskiBrojOznaka.IzrazSaNazivom(connection, "ID", "Naziv")
                : "CONVERT(nvarchar(20), ID) + ISNULL(N' - ' + NULLIF(RTRIM(Naziv), N''), N'')";
            using (var da = new SqlDataAdapter("SELECT ID, " + prikaz + " AS Prikaz FROM " + tabela + " ORDER BY ID DESC", conn))
                da.Fill(dt);

            cboMagacinskiBroj.DataSource = dt;
            cboMagacinskiBroj.DisplayMember = "Prikaz";
            cboMagacinskiBroj.ValueMember = "ID";
            if (MagacinskiBroj != 0)
                cboMagacinskiBroj.SelectedValue = MagacinskiBroj;
            else
                cboMagacinskiBroj.SelectedIndex = -1;
        }
    }
}
