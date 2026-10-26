using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace Saobracaj.HR
{
    // Skup polja napravljen iz opisa (HRPolje): pravi kontrole u HRPoljaPanel, postavlja i čita vrednosti,
    // proverava obavezna polja i brojeve. Koriste ga dijalog frmHRStavka i forme zaposlenog i zahteva.
    public class HRObrazac
    {
        private readonly List<HRPolje> polja;
        private readonly Dictionary<string, Control> kontrole = new Dictionary<string, Control>(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<string, Label> oznake = new Dictionary<string, Label>(StringComparer.OrdinalIgnoreCase);
        private bool puni;
        public readonly HRPoljaPanel Panel;
        public event Action<string> Promena;

        public HRObrazac(IEnumerable<HRPolje> polja, int brojKolona)
        {
            this.polja = new List<HRPolje>(polja);
            Panel = new HRPoljaPanel(brojKolona);
            foreach (HRPolje p in this.polja)
            {
                Control k = NapraviKontrolu(p);
                kontrole[p.Kolona] = k;
                Panel.Dodaj(p.Oznaka + (p.Obavezno ? " *" : ""), k, Math.Min(p.Raspon, brojKolona));
                oznake[p.Kolona] = (Label)k.Parent.Controls[1];
            }
        }

        public IList<HRPolje> Polja => polja;

        private Control NapraviKontrolu(HRPolje p)
        {
            Control k;
            switch (p.Tip)
            {
                case HRTip.VelikiTekst:
                    k = new TextBox { Multiline = true, Height = 64, ScrollBars = ScrollBars.Vertical, MaxLength = p.Duzina > 0 ? p.Duzina : 32767 };
                    break;
                case HRTip.CeoBroj:
                case HRTip.Decimalni:
                    k = new TextBox { TextAlign = HorizontalAlignment.Right };
                    break;
                case HRTip.Datum:
                case HRTip.DatumVreme:
                    var dtp = new DateTimePicker
                    {
                        Format = DateTimePickerFormat.Custom,
                        CustomFormat = p.Tip == HRTip.Datum ? "dd.MM.yyyy" : "dd.MM.yyyy HH:mm",
                        ShowCheckBox = !p.Obavezno
                    };
                    dtp.ValueChanged += (s, e) => Promenjeno(p.Kolona);
                    k = dtp;
                    break;
                case HRTip.Lista:
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
                case HRTip.DaNe:
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

        private static void PuniListu(ComboBox cb, HRPolje p)
        {
            try
            {
                DataTable dt = HRBaza.Lista(p.ListaUpit, !p.Obavezno);
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
            HRPolje p = polja.Find(x => string.Equals(x.Kolona, kolona, StringComparison.OrdinalIgnoreCase));
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

        public void PostaviSve(HRVrednosti v)
        {
            puni = true;
            try
            {
                foreach (HRPolje p in polja)
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
                        var dt = cb.DataSource as DataTable;
                        cb.SelectedIndex = dt != null && dt.Rows.Count > 0 && dt.Rows[0][0] == DBNull.Value ? 0 : -1;
                        if (cb.SelectedIndex < 0)
                            cb.Text = "";
                    }
                    else
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
            HRPolje p = polja.Find(x => string.Equals(x.Kolona, kolona, StringComparison.OrdinalIgnoreCase));
            Control k = Kontrola(kolona);
            if (p == null || k == null)
                return null;
            switch (k)
            {
                case DateTimePicker dtp:
                    if (dtp.ShowCheckBox && !dtp.Checked)
                        return null;
                    return p.Tip == HRTip.Datum ? dtp.Value.Date : new DateTime(dtp.Value.Year, dtp.Value.Month, dtp.Value.Day, dtp.Value.Hour, dtp.Value.Minute, 0);
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
                    if (p.Tip == HRTip.CeoBroj)
                        return int.TryParse(t, out int n) ? (object)n : t;
                    if (p.Tip == HRTip.Decimalni)
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
        public string Ocitaj(out HRVrednosti v)
        {
            v = new HRVrednosti();
            foreach (HRPolje p in polja)
            {
                object x = Vrednost(p.Kolona);
                Control k = Kontrola(p.Kolona);
                if (p.Obavezno && Omoguceno(p.Kolona) && (x == null || (x is string s && s.Trim() == "")))
                {
                    k.Focus();
                    return "Unesite polje „" + p.Oznaka + "“.";
                }
                if (x is string tekst && (p.Tip == HRTip.CeoBroj || p.Tip == HRTip.Decimalni))
                {
                    k.Focus();
                    return "Polje „" + p.Oznaka + "“ mora biti broj (upisano: " + tekst + ").";
                }
                if ((p.Tip == HRTip.CeoBroj || p.Tip == HRTip.Decimalni) && x != null)
                {
                    decimal broj = Convert.ToDecimal(x);
                    if (broj < p.Min || broj > p.Max)
                    {
                        k.Focus();
                        return "Polje „" + p.Oznaka + "“ mora biti između " + p.Min + " i " + p.Max + ".";
                    }
                }
                v[p.Kolona] = x;
            }
            return null;
        }
    }
}
