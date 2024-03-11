using iTextSharp.text.xml;
using Saobracaj.Sifarnici;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.RadniNalozi
{
    public partial class Medjuskladisni : Form
    {
        string connect = frmLogovanje.connectionString;
        string korisnik;
        public Medjuskladisni()
        {
            InitializeComponent();
        }
        private void Medjuskladisni_Load(object sender, EventArgs e)
        {
            korisnik = frmLogovanje.user;
            FillCombo();
            DGVCombo();
        }
        private void FillCombo()
        {
            var query = "Select SkSifra,SkNaziv from Sklad";/* Where SkSifra in (Select Sif_Skladiste From Skladiste_Korisnik Where Korisnik='" + korisnik.ToString() + "')";*/
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cbo_SkladisteIZ.DataSource = ds.Tables[0];
            cbo_SkladisteIZ.DisplayMember = "SkNaziv";
            cbo_SkladisteIZ.ValueMember = "SkSifra";

            var query2 = "Select SkSifra,SkNaziv from Sklad";
            SqlDataAdapter da2 = new SqlDataAdapter(query2, conn);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2);
            cbo_SkladisteU.DataSource = ds2.Tables[0];
            cbo_SkladisteU.DisplayMember = "SkNaziv";
            cbo_SkladisteU.ValueMember = "SkSifra";

            var query5 = "Select DeSifra as ID,(RTrim(DePriimek)+' '+RTrim(DeIme)) as Opis From Delavci Where DeSifStat <>'P' order by Opis";
            SqlDataAdapter da5 = new SqlDataAdapter(query5, conn);
            DataSet ds5 = new DataSet();
            da5.Fill(ds5);
            cboVozac.DataSource = ds5.Tables[0];
            cboVozac.DisplayMember = "Opis";
            cboVozac.ValueMember = "Opis";

            var query6 = "Select Naziv From Vozila";
            SqlDataAdapter da6 = new SqlDataAdapter(query6, conn);
            DataSet ds6 = new DataSet();
            da6.Fill(ds6);
            cboVozilo.DataSource = ds6.Tables[0];
            cboVozilo.DisplayMember = "Naziv";
            cboVozilo.ValueMember = "Naziv";

        }
        private void cbo_SkladisteIZ_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var query = "select LokSifra,LokOpis from Lokac Where LokSkSifra=" + cbo_SkladisteIZ.SelectedValue;
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cbo_LokacijaIZ.DataSource = ds.Tables[0];
            cbo_LokacijaIZ.DisplayMember = "LokOpis";
            cbo_LokacijaIZ.ValueMember = "LokSifra";
        }

        private void cbo_SkladisteU_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var query = "select LokSifra,LokOpis from Lokac Where LokSkSifra=" + cbo_SkladisteU.SelectedValue;
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cbo_LokacijaU.DataSource = ds.Tables[0];
            cbo_LokacijaU.DisplayMember = "LokOpis";
            cbo_LokacijaU.ValueMember = "LokSifra";
        }
        private void DGVCombo()
        {
            DataGridViewComboBoxColumn cbo = new DataGridViewComboBoxColumn();
            cbo.HeaderText = "MPNaziv";
            cbo.Name = "MPNaziv";
            var query = "Select MpSifra,MpNaziv from MaticniPodatki order by MpNaziv";
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cbo.DataSource = ds.Tables[0];
            cbo.DisplayMember = "MpNaziv";
            cbo.ValueMember = "MpSifra";

            DataGridViewTextBoxColumn cbo2 = new DataGridViewTextBoxColumn();
            cbo2.HeaderText = "Kolicina";
            cbo2.Name = "Kolicina";

            cbo.Width = 350;
            cbo2.Width = 100;

            dataGridView1.Columns.Add(cbo);
            dataGridView1.Columns.Add(cbo2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InsertIsporuka isporuka = new InsertIsporuka();
            isporuka.InsertMedju(txtNapomena.Text.ToString().TrimEnd(), dtpVreme.Value.ToString("yyyy-MM-dd")) ;
           
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row != null && row.Cells[0].Value != null)
                {
                    isporuka.InsertMedjuPostav(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToDecimal(row.Cells[1].Value.ToString()),  Convert.ToInt32(cbo_SkladisteIZ.SelectedValue), Convert.ToInt32(cbo_SkladisteU.SelectedValue), cbo_LokacijaIZ.SelectedValue.ToString(), cbo_LokacijaU.SelectedValue.ToString());
                    
                }
            }
            MessageBox.Show("Formiran je Međuskladišni prenos");
        }
    }
}
