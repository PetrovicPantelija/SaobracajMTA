using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;

namespace Saobracaj.Dokumenta
{
    public partial class frmParametriObracuna : Form
    {
        bool status = false;
        public frmParametriObracuna()
        {
            InitializeComponent();
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            txtSifra.Text = "";
            txtSifra.Enabled = false;
            status = true;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
          
            if (status == true)
            {
                Sifarnici.InsertParametri ins = new Sifarnici.InsertParametri();
                ins.InsParametri( Convert.ToDouble(txtMinimalna.Text));
                //RefreshDataGrid();
                status = false;
            }
            else
            {
                Sifarnici.InsertParametri upd = new Sifarnici.InsertParametri();
                upd.UpdParametri(Convert.ToInt32(txtSifra.Text), Convert.ToDouble(txtMinimalna.Text));
                status = false;
                txtSifra.Enabled = false;
               // RefreshDataGrid();
            }
        }
    }
}
