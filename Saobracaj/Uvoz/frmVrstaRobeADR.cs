﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Uvoz
{
    public partial class frmVrstaRobeADR : Syncfusion.Windows.Forms.Office2010Form
    {
        public static string code = "frmVrstaRobeADR";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Saobracaj.Sifarnici.frmLogovanje.user.ToString();
        string niz = "";

        bool status = false;
        string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        public frmVrstaRobeADR()
        {
            InitializeComponent();
            FillGV();

        }

        private void FillGV()
        {
            var select = "Select * From VrstaRobeADR order by ID desc";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;


            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Naziv";
            dataGridView1.Columns[1].Width = 250;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "UN kod";
            dataGridView1.Columns[2].Width = 70;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Klasa";
            dataGridView1.Columns[3].Width = 70;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Grupa";
            dataGridView1.Columns[4].Width = 70;
        }

        private void frmVrstaRobeADR_Load(object sender, EventArgs e)
        {

        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            tsNew.Enabled = false;
            status = true;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            insertVrstaRobeADR ins = new insertVrstaRobeADR();
            if (status == true)
            {
                ins.InsVrstaRobeADR(txtNaziv.Text.ToString().TrimEnd(), txtUNCode.Text, txtKlasa.Text, txtGrupa.Text);
            }
            else
            {
                ins.UpdVrstaRobeADR(Convert.ToInt32(txtID.Text.ToString()), txtNaziv.Text.ToString().TrimEnd(), txtUNCode.Text, txtKlasa.Text, txtGrupa.Text);
            }
            FillGV();
            tsNew.Enabled = true;
            status = false;
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            insertVrstaRobeADR ins = new insertVrstaRobeADR();
            ins.DelVrstaRobeADR(Convert.ToInt32(txtID.Text.ToString()));
            FillGV();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        txtID.Text = row.Cells[0].Value.ToString();
                        txtNaziv.Text = row.Cells[1].Value.ToString();
                        txtUNCode.Text = row.Cells[2].Value.ToString();
                        txtKlasa.Text = row.Cells[3].Value.ToString();
                        txtGrupa.Text = row.Cells[4].Value.ToString();
                    }
                }
            }
            catch { }
        }
    }
}
