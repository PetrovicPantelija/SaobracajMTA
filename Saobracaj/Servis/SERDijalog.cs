using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.Servis
{
    // Dijalozi koji se ponavljaju u formama modula: unos zapisa, potvrda, razlog, poruka greške iz procedure
    public static class SERDijalog
    {
        // Dijalog sa poljima; snimanje vraća poruku o grešci ili null. Poruka iz procedure (THROW) ostavlja dijalog otvoren.
        // validacija (opciono) se poziva pre snimanja: vraća poruku o grešci, ili baca SERPrekid kad korisnik odustane (npr. odgovori „Ne“ na upozorenje).
        public static bool Unos(IWin32Window vlasnik, string naslov, IList<SERPolje> polja, SERVrednosti pocetne,
                                Action<SERVrednosti> snimanje, int brojKolona = 2, Action<frmSERStavka, string> promena = null,
                                Func<SERVrednosti, string> validacija = null)
        {
            using (var f = new frmSERStavka(naslov, polja, pocetne, brojKolona))
            {
                if (promena != null)
                    f.Promena += promena;
                f.Validacija = validacija;
                f.Snimanje = v =>
                {
                    try
                    {
                        snimanje(v);
                        return null;
                    }
                    catch (SqlException ex)
                    {
                        return Poruka(ex);
                    }
                };
                return f.ShowDialog(vlasnik) == DialogResult.OK;
            }
        }

        // Poruke za ograničenja u bazi (UNIQUE, CHECK, FOREIGN KEY): ograničenja ostaju u bazi, a korisnik dobija poruku na srpskom.
        // Traži se naziv ograničenja u tekstu greške; prvo podudaranje važi.
        private static readonly string[][] PorukePoOgranicenju =
        {
            new[] { "UQ_SERVrstaSredstva_Naziv", "Vrsta sredstva sa tim nazivom već postoji." },
            new[] { "UQ_SERSredstvo_Oznaka", "Sredstvo sa tom oznakom već postoji." },
            new[] { "UQ_SERPlanNormativ", "Taj deo je već u normativu ovog plana." },
            new[] { "UX_SERDeoPrimenljivost", "Ova vrsta sredstva je već dodata za deo." },
            new[] { "UQ_SERDeoGrupa_Naziv", "Grupa delova sa tim nazivom već postoji." },
            new[] { "UQ_SERDeo_Sifra", "Deo sa tom šifrom već postoji." },
            new[] { "UQ_SERNarudzbenicaStavka", "Taj deo je već na narudžbenici." },
            new[] { "UQ_SERPopisStavka", "Taj deo je već na popisnoj listi." },
            new[] { "UQ_SERRadniNalog_Broj", "Radni nalog sa tim brojem već postoji." },
            new[] { "UQ_SERNarudzbenica_Broj", "Narudžbenica sa tim brojem već postoji." },
            new[] { "CK_SERDeo_Max", "Maksimalna zaliha mora biti veća ili jednaka minimalnoj." },
            new[] { "CK_SERDeo_Min", "Minimalna zaliha ne sme biti negativna." },
            new[] { "CK_SERPlanStavka_Interval", "Unesite bar jedan interval servisa veći od 0 (dani, moto-sati ili km)." },
            new[] { "CK_SERPlanNormativ_Kolicina", "Količina po servisu mora biti veća od 0." },
            new[] { "CK_SERNarudzbenicaStavka", "Količina mora biti veća od 0, a cena ne sme biti negativna." },
            new[] { "CK_SERDeoKretanje", "Količina ne sme biti 0, a cena ne sme biti negativna." },
            new[] { "CK_SERDeoRezervacija_Kol", "Količina mora biti veća od 0." },
            new[] { "CK_SERKorekcija", "Korekcija ne može biti 0 i traži razlog." },
            new[] { "CK_SERPopisStavka_Prebrojano", "Prebrojana količina ne sme biti negativna." }
        };

        // Tekst greške za korisnika. Poruke procedura (THROW 5xxxx) su već na srpskom; greške UNIQUE/CHECK/FK se prevode.
        public static string Poruka(Exception ex)
        {
            var sql = ex as SqlException;
            if (sql == null)
                return ex.Message;
            if (sql.Number >= 50000)
                return sql.Message;
            if (sql.Number == 2601 || sql.Number == 2627 || sql.Number == 547)
            {
                foreach (string[] par in PorukePoOgranicenju)
                    if (sql.Message.IndexOf(par[0], StringComparison.OrdinalIgnoreCase) >= 0)
                        return par[1];
                if (sql.Number != 547)
                    return "Takav zapis već postoji.";
                return sql.Message.IndexOf("FOREIGN KEY", StringComparison.OrdinalIgnoreCase) >= 0 || sql.Message.IndexOf("REFERENCE", StringComparison.OrdinalIgnoreCase) >= 0
                    ? "Zapis je povezan sa drugim podacima, pa se ne može obrisati ni izmeniti na taj način."
                    : "Unesena vrednost nije dozvoljena.";
            }
            return "Akcija nije izvršena.\n\n" + sql.Message;
        }

        // Potvrda sa dugmadima „Da“ i „Ne“ (standardni MessageBox ih piše jezikom operativnog sistema, npr. Yes/No)
        public static bool Potvrdi(IWin32Window vlasnik, string pitanje, string naslov)
        {
            using (var f = new Form
            {
                Text = naslov,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                ShowInTaskbar = false,
                StartPosition = FormStartPosition.CenterParent,
                BackColor = Theme.SavaTheme.Surface,
                AutoScaleMode = AutoScaleMode.None
            })
            {
                var tekst = new Label { Text = pitanje, AutoSize = true, MaximumSize = new System.Drawing.Size(420, 0), Font = Theme.SavaTheme.FieldFont, ForeColor = Theme.SavaTheme.Text, Location = new System.Drawing.Point(24, 22) };
                var da = SERStil.Dugme("Da", "primary");
                var ne = SERStil.Dugme("Ne");
                da.DialogResult = DialogResult.Yes;
                ne.DialogResult = DialogResult.No;
                f.Controls.Add(tekst);
                f.Controls.Add(da);
                f.Controls.Add(ne);
                Theme.ThemeHelper.Apply(f);
                int sirina = Math.Max(tekst.PreferredSize.Width + 48, 260);
                int dno = tekst.Bottom + 24;
                ne.Location = new System.Drawing.Point(sirina - 24 - ne.Width, dno);
                da.Location = new System.Drawing.Point(ne.Left - 8 - da.Width, dno);
                f.ClientSize = new System.Drawing.Size(sirina, dno + da.Height + 20);
                f.AcceptButton = da;
                f.CancelButton = ne;
                return f.ShowDialog(vlasnik) == DialogResult.Yes;
            }
        }

        // Razlog ili komentar; null ako je korisnik odustao
        public static string Razlog(IWin32Window vlasnik, string naslov, string oznaka, bool obavezno)
        {
            var polja = new List<SERPolje> { new SERPolje("Razlog", oznaka, SERTip.VelikiTekst, obavezno, 500) { Raspon = 1 } };
            string rezultat = null;
            using (var f = new frmSERStavka(naslov, polja, null, 1))
            {
                f.Snimanje = v => { rezultat = v.Tekst("Razlog"); return null; };
                return f.ShowDialog(vlasnik) == DialogResult.OK ? rezultat ?? "" : null;
            }
        }

        // Poruka iz procedure (THROW 5xxxx) je za korisnika; ostale greške dobijaju i tehnički opis
        public static void Greska(IWin32Window vlasnik, string naslov, Exception ex)
        {
            string poruka = ex is SqlException ? Poruka(ex) : "Akcija nije izvršena.\n\n" + ex.Message;
            MessageBox.Show(vlasnik, poruka, naslov, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // Izvršava akciju i prikazuje grešku; vraća true ako je uspela.
        // Dok akcija traje, forma pokazuje kursor čekanja i ne prima klikove (nema dvostrukog snimanja).
        public static bool Pokusaj(IWin32Window vlasnik, string naslov, Action akcija)
        {
            Exception greska = null;
            using (new SERCekanje(vlasnik))
            {
                try
                {
                    akcija();
                }
                catch (Exception ex)
                {
                    greska = ex;
                }
            }
            if (greska == null)
                return true;
            Greska(vlasnik, naslov, greska);
            return false;
        }
    }

    // Pokazatelj rada: kursor čekanja i onemogućena forma za vreme dugog poziva baze.
    // Onemogućen prozor ne prima klikove, pa se klik na dugme dok traje snimanje ne ponavlja posle njegovog završetka.
    public sealed class SERCekanje : IDisposable
    {
        private readonly Form forma;

        public SERCekanje(IWin32Window vlasnik)
        {
            forma = (vlasnik as Control)?.FindForm();
            if (forma != null)
            {
                forma.UseWaitCursor = true;
                forma.Enabled = false;
                forma.Update();
            }
            Cursor.Current = Cursors.WaitCursor;
        }

        public void Dispose()
        {
            if (forma != null && !forma.IsDisposed)
            {
                forma.Enabled = true;
                forma.UseWaitCursor = false;
            }
            Cursor.Current = Cursors.Default;
        }
    }

    // Korisnik je odustao (npr. odgovorio „Ne“ na upozorenje u validaciji): dijalog ostaje otvoren, bez poruke o grešci
    public sealed class SERPrekid : Exception
    {
        public SERPrekid() : base("Odustanak korisnika") { }
    }
}
