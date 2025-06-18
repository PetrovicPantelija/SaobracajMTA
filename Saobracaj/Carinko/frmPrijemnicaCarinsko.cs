using Microsoft.Office.Interop.Excel;
using Saobracaj.Sifarnici;
using Syncfusion.GridHelperClasses;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Diagram;
using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using static Syncfusion.WinForms.Core.NativeScroll;

namespace Saobracaj.Carinko
{
    public partial class frmPrijemnicaCarinsko : Form
    {

        string Kor = Sifarnici.frmLogovanje.user.ToString();
        string niz = "";

        bool status = false;
        public frmPrijemnicaCarinsko()
        {
            InitializeComponent();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            txtID.Text = "";
            txtID.Enabled = false;
           

            status = true;
        }

        private void button21_Click(object sender, EventArgs e)
        {
          
            if (status == true)
            {
                InsertPrijemnicaCarinska ins = new InsertPrijemnicaCarinska();
                ins.InsPrijemnicaCarinska( 
                txtStatus.Text, DateTime.Now,
Kor, Convert.ToInt32(cboSkladisteID.SelectedValue), txtDokument.Text,
Convert.ToInt32(cboMagacinskiBroj.SelectedValue), cboVrstaSkladista.Text, cboSektor.Text,
Convert.ToInt32(cboVlasnik.SelectedValue), Convert.ToInt32(cboKorisnikRobe.SelectedValue), Convert.ToInt32(cboPosiljalac.SelectedValue),
Convert.ToInt32(cboPrimalac.SelectedValue), txtBrojFakture.Text,
txtPrevoznik.Text, txtBrojKamiona.Text,
txtNapomena1.Text, txtNapomena2.Text,
txtTransportNo.Text, Convert.ToDateTime(dtpOcekivanoVreme.Value));

                // RefreshDataGrid();
              //  RefrechDataGridT();
                status = false;
            }
            else
            {
                InsertPrijemnicaCarinska upd = new InsertPrijemnicaCarinska();
                upd.UpdPrijemnicaCarinska(Convert.ToInt32(txtID.Text), txtStatus.Text, DateTime.Now,
Kor, Convert.ToInt32(cboSkladisteID.SelectedValue), txtDokument.Text,
Convert.ToInt32(cboMagacinskiBroj.SelectedValue), cboVrstaSkladista.Text, cboSektor.Text,
Convert.ToInt32(cboVlasnik.SelectedValue), Convert.ToInt32(cboKorisnikRobe.SelectedValue), Convert.ToInt32(cboPosiljalac.SelectedValue),
Convert.ToInt32(cboPrimalac.SelectedValue), txtBrojFakture.Text,
txtPrevoznik.Text, txtBrojKamiona.Text,
txtNapomena1.Text, txtNapomena2.Text,
txtTransportNo.Text, Convert.ToDateTime(dtpOcekivanoVreme.Value));
                status = false;
               
                // RefreshDataGrid();
               // RefrechDataGridT();
            }
        }
    }
}
