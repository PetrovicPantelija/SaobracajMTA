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
using System.Net;
using System.Net.Mail;


using Microsoft.Reporting.WinForms;

namespace Saobracaj.Dokumenta
{
    public partial class frmPrekovremeniRad : Form
    {
        Boolean status = false;
        public frmPrekovremeniRad()
        {
            InitializeComponent();
        }

        private void frmPrekovremeniRad_Load(object sender, EventArgs e)
        {
            var select3 = " select DeSifra as ID, (Rtrim(DePriimek) + ' ' + RTrim(DeIme)) as Opis from Delavci where DeSifStat <> 'P' order by opis";
            var s_connection3 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboZaposleni.DataSource = ds3.Tables[0];
            cboZaposleni.DisplayMember = "Opis";
            cboZaposleni.ValueMember = "ID";
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            txtSifra.Text = "";
            txtSifra.Enabled = false;
            dtpVremeOd.Value = DateTime.Now;
            dtpVremeDo.Value = DateTime.Now;
            txtNapomena.Text = "";
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            int Praznik = 0;
            if (chkRadPraznikom.Checked == true)
            {
                Praznik = 1;

            }
            if (status == true)
            {
               
                InsertPrekovremeniRad ins = new InsertPrekovremeniRad();
                ins.InsPrekovremeniRad(dtpVremeOd.Value, dtpVremeDo.Value, Convert.ToInt32(txtUkupno.Text), Convert.ToInt32(cboZaposleni.SelectedValue), txtNapomena.Text, Praznik);
                status = false;
                RefreshDataGrid1();

            }
            else
            {
                InsertPrekovremeniRad upd = new InsertPrekovremeniRad();
                upd.UpdPrekovremeniRad(Convert.ToInt32(txtSifra.Text), dtpVremeOd.Value, dtpVremeDo.Value, Convert.ToInt32(txtUkupno.Text), Convert.ToInt32(cboZaposleni.SelectedValue), txtNapomena.Text, Praznik);
                RefreshDataGrid1();
            }
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertPrekovremeniRad del = new InsertPrekovremeniRad();
            del.DeletePrekovremeniRad(Convert.ToInt32(txtSifra.Text));
            RefreshDataGrid1();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshDataGrid1();
        }

        private void RefreshDataGrid1()
        {
            var select = " Select PrekovremeniRad.ID,  DatumOd, DatumDo, Ukupno, Napomena,  (Rtrim(Delavci.DePriimek) + ' ' + Rtrim(Delavci.DeIme)) as Radnik " +
 " from PrekovremeniRad inner join Delavci on " +
 " PrekovremeniRad.ZaposleniID = Delavci.DeSifra  where PrekovremeniRad.ZaposleniID = " + Convert.ToInt32(cboZaposleni.SelectedValue) + " order by id desc";

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "ID";
            dataGridView2.Columns[0].Width = 40;



            DataGridViewColumn column3 = dataGridView2.Columns[1];
            dataGridView2.Columns[1].HeaderText = "Vreme Od";
            dataGridView2.Columns[1].Width = 100;

            DataGridViewColumn column4 = dataGridView2.Columns[2];
            dataGridView2.Columns[2].HeaderText = "Vreme Do";
            dataGridView2.Columns[2].Width = 100;

            DataGridViewColumn column5 = dataGridView2.Columns[3];
            dataGridView2.Columns[3].HeaderText = "Ukupno";
            dataGridView2.Columns[3].Width = 50;

            DataGridViewColumn column6 = dataGridView2.Columns[4];
            dataGridView2.Columns[4].HeaderText = "Napomena";
            dataGridView2.Columns[4].Width = 120;

            DataGridViewColumn column7 = dataGridView2.Columns[5];
            dataGridView2.Columns[5].HeaderText = "Zaposleni";
            dataGridView2.Columns[5].Width = 120;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var select = " Select PrekovremeiRad.ID,  DatumOd, DatumDo, Ukupno, Napomena,  (Rtrim(Delavci.DePriimek) + ' ' + Rtrim(Delavci.DeIme)) as Radnik " +
" from PrekovremeiRad inner join Delavci on " +
" PrekovremeiRad.ZaposleniID = Delavci.DeSifra order by id desc";


            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "ID";
            dataGridView2.Columns[0].Width = 40;



            DataGridViewColumn column3 = dataGridView2.Columns[1];
            dataGridView2.Columns[1].HeaderText = "Vreme Od";
            dataGridView2.Columns[1].Width = 100;

            DataGridViewColumn column4 = dataGridView2.Columns[2];
            dataGridView2.Columns[2].HeaderText = "Vreme Do";
            dataGridView2.Columns[2].Width = 100;

            DataGridViewColumn column5 = dataGridView2.Columns[3];
            dataGridView2.Columns[3].HeaderText = "Ukupno";
            dataGridView2.Columns[3].Width = 50;

            DataGridViewColumn column6 = dataGridView2.Columns[4];
            dataGridView2.Columns[4].HeaderText = "Napomena";
            dataGridView2.Columns[4].Width = 120;

            DataGridViewColumn column7 = dataGridView2.Columns[5];
            dataGridView2.Columns[5].HeaderText = "Zaposleni";
            dataGridView2.Columns[5].Width = 120;
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Selected)
                    {

                        txtSifra.Text = row.Cells[0].Value.ToString();
                        RefreshDataGrid1();
                        // txtOpis.Text = row.Cells[1].Value.ToString();
                    }
                }


            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }
    }
    }

