using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Saobracaj.Pomocni
{
    /// <summary>
    /// Pretraga u ComboBox-u dok se kuca: lista se sužava na stavke koje sadrže uneti tekst.
    /// Zamenjuje ugrađeni AutoComplete, koji otvara drugu, odvojenu listu.
    /// Poziva se posle punjenja DataSource-a (DataTable ili DataView), a vrednost se čita kao i ranije preko SelectedValue.
    /// </summary>
    public static class ComboPretraga
    {
        public static void Ukljuci(ComboBox cbo)
        {
            DataView pogled = cbo.DataSource is DataTable tabela ? tabela.DefaultView : cbo.DataSource as DataView;
            if (pogled == null)
                return;

            cbo.AutoCompleteMode = AutoCompleteMode.None;
            cbo.AutoCompleteSource = AutoCompleteSource.None;
            cbo.DropDownStyle = ComboBoxStyle.DropDown;

            cbo.TextUpdate += (s, e) => Filtriraj(cbo, pogled);
            cbo.Leave += (s, e) => Zavrsi(cbo, pogled);
        }

        private static void Filtriraj(ComboBox cbo, DataView pogled)
        {
            string tekst = cbo.Text;
            int pozicija = cbo.SelectionStart;

            pogled.RowFilter = tekst.Trim() == ""
                ? ""
                : "[" + cbo.DisplayMember + "] LIKE '%" + Escape(tekst.Trim()) + "%'";

            if (!cbo.DroppedDown)
            {
                cbo.DroppedDown = true;
                Cursor.Current = Cursors.Default; // otvaranje liste sakrije kursor miša
            }

            // promena filtera i otvaranje liste menjaju tekst; vraća se ono što je korisnik otkucao
            cbo.Text = tekst;
            cbo.SelectionStart = Math.Min(pozicija, tekst.Length);
            cbo.SelectionLength = 0;
        }

        private static void Zavrsi(ComboBox cbo, DataView pogled)
        {
            string tekst = cbo.Text.Trim();
            object vrednost = null;

            if (tekst != "")
            {
                if (cbo.SelectedIndex >= 0 && string.Equals(cbo.GetItemText(cbo.SelectedItem).Trim(), tekst, StringComparison.CurrentCultureIgnoreCase))
                    vrednost = cbo.SelectedValue;
                else
                {
                    int i = cbo.FindStringExact(tekst);
                    if (i >= 0)
                        vrednost = ((DataRowView)cbo.Items[i])[cbo.ValueMember];
                    else if (pogled.Count == 1)
                        vrednost = pogled[0][cbo.ValueMember];
                }
            }

            if (pogled.RowFilter != "")
                pogled.RowFilter = "";

            if (vrednost != null)
                cbo.SelectedValue = vrednost;
            else
                cbo.SelectedIndex = -1;
        }

        private static string Escape(string tekst)
        {
            var sb = new StringBuilder();
            foreach (char c in tekst)
            {
                if (c == '[' || c == ']' || c == '*' || c == '%')
                    sb.Append('[').Append(c).Append(']');
                else if (c == '\'')
                    sb.Append("''");
                else
                    sb.Append(c);
            }
            return sb.ToString();
        }
    }
}
