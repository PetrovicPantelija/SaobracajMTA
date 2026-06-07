using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Skladista_main.Dokumenta
{
    public partial class Pretovar : Form
    {
        string Tip = "Pretovar";
        int IDInterni;
        string Ulaz;
        string Vrsta;
        public Pretovar(int idInterni,string ulaz,string vrsta)
        {
            InitializeComponent();
            IDInterni = idInterni;
            Ulaz = ulaz;
            Vrsta = vrsta;
            panel5.Visible = false;
            if (Vrsta == "Carinsko")
            {
                textBox1.Text = "1008";
            }
        }
    }
}
