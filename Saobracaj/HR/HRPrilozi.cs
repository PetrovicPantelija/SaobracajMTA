using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Saobracaj.HR
{
    // Prilozi (skenirani dokumenti, ponude u PDF-u): čuvaju se u bazi (ZaposleniPrilozi, ZahtevZaNabavkuPrilozi)
    public static class HRPrilozi
    {
        public const int NajvecaVelicina = 20 * 1024 * 1024;

        // Bira fajl i upisuje ga; dodatne kolone (VezaTip, VezaSifra, Ponuda...) se prosleđuju kao parovi
        public static bool Dodaj(IWin32Window vlasnik, string tabela, string roditeljKolona, int roditelj, string predlogNaziva,
                                 params KeyValuePair<string, object>[] dodatno)
        {
            using (var ofd = new OpenFileDialog { Title = "Izaberite fajl", Filter = "Dokumenti i slike|*.pdf;*.jpg;*.jpeg;*.png;*.tif;*.tiff;*.doc;*.docx;*.xls;*.xlsx|Svi fajlovi|*.*" })
            {
                if (ofd.ShowDialog(vlasnik) != DialogResult.OK)
                    return false;
                var info = new FileInfo(ofd.FileName);
                if (info.Length > NajvecaVelicina)
                {
                    MessageBox.Show(vlasnik, "Fajl je veći od 20 MB.", "Prilog", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                string naziv = Unos(vlasnik, "Prilog", "Naziv priloga", string.IsNullOrEmpty(predlogNaziva) ? Path.GetFileNameWithoutExtension(info.Name) : predlogNaziva, 100, false);
                if (naziv == null)
                    return false;

                byte[] sadrzaj = File.ReadAllBytes(ofd.FileName);
                var kolone = new List<string> { roditeljKolona, "Naziv", "NazivFajla", "TipFajla", "Sadrzaj", "KreiraoKorisnik" };
                var parametri = new List<SqlParameter>
                {
                    HRBaza.P("@p0", roditelj), HRBaza.P("@p1", naziv), HRBaza.P("@p2", info.Name),
                    HRBaza.P("@p3", TipFajla(info.Extension)), new SqlParameter("@p4", SqlDbType.VarBinary, -1) { Value = sadrzaj },
                    HRBaza.P("@p5", HRBaza.Korisnik)
                };
                if (tabela == "ZaposleniPrilozi")
                {
                    kolone.Add("Velicina");
                    parametri.Add(HRBaza.P("@p" + parametri.Count, (int)info.Length));
                }
                foreach (var d in dodatno)
                {
                    kolone.Add(d.Key);
                    parametri.Add(HRBaza.P("@p" + parametri.Count, d.Value));
                }
                string sql = "INSERT INTO dbo." + tabela + " (" + string.Join(", ", kolone.Select(k => "[" + k + "]")) + ") VALUES ("
                           + string.Join(", ", Enumerable.Range(0, kolone.Count).Select(i => "@p" + i)) + ")";
                HRBaza.Izvrsi(sql, parametri.ToArray());
                return true;
            }
        }

        // Prilog se snima u privremeni folder i otvara programom koji je u Windows-u vezan za tu vrstu fajla
        public static void Otvori(string tabela, int id)
        {
            DataRow red = HRBaza.Red("SELECT NazivFajla, Sadrzaj FROM dbo." + tabela + " WHERE Sifra = @S", HRBaza.P("@S", id));
            if (red == null)
                return;
            string folder = Path.Combine(Path.GetTempPath(), "SaobracajHR");
            Directory.CreateDirectory(folder);
            string ime = id + "_" + string.Concat(Convert.ToString(red["NazivFajla"]).Split(Path.GetInvalidFileNameChars()));
            string putanja = Path.Combine(folder, ime);
            File.WriteAllBytes(putanja, (byte[])red["Sadrzaj"]);
            System.Diagnostics.Process.Start(putanja);
        }

        public static string TipFajla(string ekstenzija)
        {
            switch ((ekstenzija ?? "").ToLowerInvariant())
            {
                case ".pdf": return "application/pdf";
                case ".jpg": case ".jpeg": return "image/jpeg";
                case ".png": return "image/png";
                case ".tif": case ".tiff": return "image/tiff";
                case ".doc": return "application/msword";
                case ".docx": return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                case ".xls": return "application/vnd.ms-excel";
                case ".xlsx": return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                default: return "application/octet-stream";
            }
        }

        // Kratak unos teksta (komentar, razlog); null ako je korisnik odustao
        public static string Unos(IWin32Window vlasnik, string naslov, string oznaka, string pocetno, int duzina, bool viseRedova, bool obavezno = true)
        {
            var polje = new HRPolje("Tekst", oznaka, viseRedova ? HRTip.VelikiTekst : HRTip.Tekst, obavezno, duzina);
            var v = new HRVrednosti { ["Tekst"] = pocetno };
            using (var dlg = new frmHRStavka(naslov, new[] { polje }, v, 1))
            {
                if (dlg.ShowDialog(vlasnik) != DialogResult.OK)
                    return null;
                return dlg.Vrednosti.Tekst("Tekst");
            }
        }
    }
}
