using Syncfusion.Styles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.TerminalMap
{
    public static class Prompt
    {
        public static string Show(string text,string caption)
        {
            var f = new Form { Width = 300, Height = 160, Text = caption, StartPosition = FormStartPosition.CenterParent };
            var lbl = new Label { Left = 10, Top = 10, Width = 260, Text = text };
            var tb = new TextBox { Left = 10, Top = 40, Width = 260 };
            var ok = new Button { Text = "OK", Left = 90, Width = 80, Top = 75, DialogResult = DialogResult.OK };
            var cn = new Button { Text = "Cancel", Left = 190, Width = 80, Top = 75, DialogResult = DialogResult.Cancel };

            f.Controls.AddRange(new Control[] { lbl, tb, ok, cn });
            f.AcceptButton = ok;
            f.CancelButton = cn;

            return f.ShowDialog() == DialogResult.OK ? tb.Text : "";
        }
    }
}
