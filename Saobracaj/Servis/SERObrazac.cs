using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace Saobracaj.Servis
{
    // Skup polja napravljen iz opisa (SERPolje): pravi kontrole u SERPoljaPanel, postavlja i čita vrednosti,
    // proverava obavezna polja i brojeve. Koriste ga dijalog frmSERStavka i forme zaposlenog i zahteva.
    public class SERObrazac
    {
        private readonly List<SERPolje> polja;
        private readonly Dictionary<string, Control> kontrole = new Dictionary<string, Control>(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<string, Label> oznake = new Dictionary<string, Label>(StringComparer.OrdinalIgnoreCase);
        private bool puni;
        public readonly SERPoljaPanel Panel;
        public event Action<string> Promena;

        public SERObrazac(IEnumerable<SERPolje> polja, int brojKolona)
        {
            this.polja = new List<SERPolje>(polja);
            Panel = new SERPoljaPanel(brojKolona);
            foreach (SERPolje p in this.polja)
            {
                Control k = NapraviKontrolu(p);
                kontrole[p.Kolona] = k;
                Panel.Dodaj(p.Oznaka + (p.Obavezno ? " *" : ""), k, Math.Min(p.Raspon, brojKolona));
                oznake[p.Kolona] = (Label)k.Parent.Controls[1];
            }
        }

        public IList<SERPolje> Polja => polja;

        private Control NapraviKontrolu(SERPolje p)
        {
            Control k;
            switch (p.Tip)
            {
                case SERTip.VelikiTekst:
                    k = new TextBox { Multiline = true, Height = 64, ScrollBars = ScrollBars.Vertical, MaxLength = p.Duzina > 0 ? p.Duzina : 32767 };
                    break;
                case SERTip.CeoBroj:
                case SERTip.Decimalni:
                    k = new TextBox { TextAlign = HorizontalAlignment.Right };
                    break;
                case SERTip.Datum:
                case SERTip.DatumVreme:
                    var dtp = new DateTimePicker
                    {
                        Format = DateTimePickerFormat.Custom,
                        CustomFormat = p.Tip == SERTip.Datum ? "dd.MM.yyyy" : "dd.MM.yyyy HH:mm",
                        ShowCheckBox = !p.Obavezno
                    };
                    dtp.ValueChanged += (s, e) => Promenjeno(p.Kolona);
                    k = dtp;
                    break;
                case SERTip.Lista:
                    var cb = new ComboBox
                    {
                        DropDownStyle = ComboBoxStyle.DropDown,
                        AutoCompleteMode = AutoCompleteMode.SuggestAppend,
                        AutoCompleteSource = AutoCompleteSource.ListItems
                    };
                    PuniListu(cb, p);
                    cb.SelectedIndexChanged += (s, e) => Promenjeno(p.Kolona);
                    k = cb;
                    break;
                case SERTip.DaNe:
                    var chk = new CheckBox { Text = "Da", AutoSize = false, Height = 28 };
                    chk.CheckedChanged += (s, e) => Promenjeno(p.Kolona);
                    k = chk;
                    break;
                default:
                    k = new TextBox { MaxLength = p.Duzina > 0 ? p.Duzina : 32767 };
                    break;
            }
            if (k is TextBox tb)
            {
                tb.ReadOnly = p.SamoCitanje;
                tb.Leave += (s, e) => Promenjeno(p.Kolona);
            }
            else if (p.SamoCitanje)
                k.Enabled = false;
            return k;
        }

        private static void PuniListu(ComboBox cb, SERPolje p)
        {
            try
            {
                DataTable dt = SERBaza.Lista(p.ListaUpit, !p.Obavezno);
                // sopstveni BindingContext: stavke se pune odmah, i pre nego što ComboBox dobije roditelja
                // (dijalog postavlja vrednosti u konstruktoru, dok kontrola još nije na formi)
                if (cb.BindingContext == null || cb.Parent == null)
                    cb.BindingContext = new BindingContext();
                cb.DisplayMember = dt.Columns[1].ColumnName;
                cb.ValueMember = dt.Columns[0].ColumnName;
                cb.DataSource = dt;
            }
            catch (Exception ex)
            {
                cb.Enabled = false;
                cb.Text = "Lista nije učitana: " + ex.Message;
            }
        }

        // Ponovo učitava listu (npr. posle dodavanja stavke u šifarnik); izabrana vrednost ostaje
        public void OsveziListu(string kolona)
        {
            SERPolje p = polja.Find(x => string.Equals(x.Kolona, kolona, StringComparison.OrdinalIgnoreCase));
            if (p == null || !(Kontrola(kolona) is ComboBox cb))
                return;
            object v = Vrednost(kolona);
            PuniListu(cb, p);
            Postavi(kolona, v);
        }

        private void Promenjeno(string kolona)
        {
            if (!puni)
                Promena?.Invoke(kolona);
        }

        public Control Kontrola(string kolona)
        {
            return kontrole.TryGetValue(kolona, out Control k) ? k : null;
        }

        public void Omoguci(string kolona, bool omoguceno, bool isprazni = false)
        {
            Control k = Kontrola(kolona);
            if (k == null)
                return;
            if (k is TextBox tb)
            {
                tb.ReadOnly = !omoguceno;
                tb.BackColor = omoguceno ? Theme.SavaTheme.White : Theme.SavaTheme.Surface;
            }
            else
                k.Enabled = omoguceno;
            if (!omoguceno && isprazni)
                Postavi(kolona, null);
        }

        public bool Omoguceno(string kolona)
        {
            Control k = Kontrola(kolona);
            return k != null && (k is TextBox tb ? !tb.ReadOnly : k.Enabled);
        }

        public void PostaviOznaku(string kolona, string tekst)
        {
            if (oznake.TryGetValue(kolona, out Label l))
                l.Text = tekst;
        }

        public void PostaviSve(SERVrednosti v)
        {
            puni = true;
            try
            {
                foreach (SERPolje p in polja)
                {
                    object x = v != null && v.ContainsKey(p.Kolona) ? v[p.Kolona] : p.Podrazumevano;
                    Postavi(p.Kolona, x);
                }
            }
            finally
            {
                puni = false;
            }
        }

        public void Postavi(string kolona, object v)
        {
            Control k = Kontrola(kolona);
            if (k == null)
                return;
            bool prazno = v == null || v == DBNull.Value;
            switch (k)
            {
                case DateTimePicker dtp:
                    if (prazno)
                    {
                        dtp.Value = DateTime.Today;
                        if (dtp.ShowCheckBox)
                            dtp.Checked = false;
                    }
                    else
                    {
                        dtp.Value = Convert.ToDateTime(v);
                        if (dtp.ShowCheckBox)
                            dtp.Checked = true;
                    }
                    break;
                case ComboBox cb:
                    if (prazno)
                    {
                        // prazna stavka na vrhu postoji samo kod neobaveznog polja, i samo kad su stavke već napunjene
                        var dt = cb.DataSource as DataTable;
                        bool praznaPrva = cb.Items.Count > 0 && dt != null && dt.Rows.Count > 0 && dt.Rows[0][0] == DBNull.Value;
                        cb.SelectedIndex = praznaPrva ? 0 : -1;
                        if (!praznaPrva)
                            cb.Text = "";
                    }
                    else if (cb.Items.Count > 0)
                        cb.SelectedValue = v is string s ? (object)s.Trim() : v;
                    break;
                case CheckBox chk:
                    chk.Checked = !prazno && Convert.ToBoolean(v);
                    break;
                case TextBox tb:
                    if (prazno)
                        tb.Text = "";
                    else if (v is decimal dec)
                        tb.Text = dec.ToString("0.###", CultureInfo.CurrentCulture);
                    else
                        tb.Text = Convert.ToString(v).Trim();
                    break;
            }
        }

        public object Vrednost(string kolona)
        {
            SERPolje p = polja.Find(x => string.Equals(x.Kolona, kolona, StringComparison.OrdinalIgnoreCase));
            Control k = Kontrola(kolona);
            if (p == null || k == null)
                return null;
            switch (k)
            {
                case DateTimePicker dtp:
                    if (dtp.ShowCheckBox && !dtp.Checked)
                        return null;
                    return p.Tip == SERTip.Datum ? dtp.Value.Date : new DateTime(dtp.Value.Year, dtp.Value.Month, dtp.Value.Day, dtp.Value.Hour, dtp.Value.Minute, 0);
                case ComboBox cb:
                    if (cb.SelectedIndex < 0 || cb.SelectedValue == null || cb.SelectedValue == DBNull.Value)
                        return null;
                    // tekst upisan rukom koji ne odgovara stavci liste se ne prihvata
                    if (cb.Text.Trim() != cb.GetItemText(cb.SelectedItem).Trim())
                        return null;
                    return cb.SelectedValue;
                case CheckBox chk:
                    return chk.Checked;
                case TextBox tb:
                    string t = tb.Text.Trim();
                    if (t == "")
                        return null;
                    if (p.Tip == SERTip.CeoBroj)
                        return int.TryParse(t, out int n) ? (object)n : t;
                    if (p.Tip == SERTip.Decimalni)
                        return ParsirajDecimalni(t, out decimal d) ? (object)d : t;
                    return t;
            }
            return null;
        }

        public static bool ParsirajDecimalni(string t, out decimal d)
        {
            t = t.Replace(" ", "");
            if (decimal.TryParse(t, NumberStyles.Number, CultureInfo.CurrentCulture, out d))
                return true;
            return decimal.TryParse(t.Replace(',', '.'), NumberStyles.Number, CultureInfo.InvariantCulture, out d);
        }

        // Čita sva polja; vraća poruku o prvoj grešci (i fokusira polje) ili null
        public string Ocitaj(out SERVrednosti v)
        {
            v = new SERVrednosti();
            foreach (SERPolje p in polja)
            {
                object x = Vrednost(p.Kolona);
                Control k = Kontrola(p.Kolona);
                string naziv = p.NazivUPoruci ?? p.Oznaka;
                if (p.Obavezno && Omoguceno(p.Kolona) && (x == null || (x is string s && s.Trim() == "")))
                {
                    k.Focus();
                    return "Unesite polje „" + naziv + "“.";
                }
                if (x is string tekst && (p.Tip == SERTip.CeoBroj || p.Tip == SERTip.Decimalni))
                {
                    k.Focus();
                    return "Polje „" + naziv + "“ mora biti broj (upisano: " + tekst + ").";
                }
                if ((p.Tip == SERTip.CeoBroj || p.Tip == SERTip.Decimalni) && x != null)
                {
                    decimal broj = Convert.ToDecimal(x);
                    bool imaGornjuGranicu = p.Max < 999999999;
                    if (p.Pozitivan && (broj <= 0 || broj > p.Max))
                    {
                        k.Focus();
                        // brojevi u srpskom formatu (20, a ne 20,000)
                        return "Polje „" + naziv + "“ mora biti veće od 0" + (imaGornjuGranicu ? " i najviše " + SERBaza.Kolicina(p.Max) : "") + ".";
                    }
                    if (p.NeNula && broj == 0)
                    {
                        k.Focus();
                        return "Polje „" + naziv + "“ ne sme biti 0.";
                    }
                    if (broj < p.Min || broj > p.Max)
                    {
                        k.Focus();
                        return "Polje „" + naziv + "“ mora biti između " + SERBaza.Kolicina(p.Min) + " i " + SERBaza.Kolicina(p.Max) + ".";
                    }
                }
                v[p.Kolona] = x;
            }
            return null;
        }
    }
}
