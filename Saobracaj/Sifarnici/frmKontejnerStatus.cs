﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Sifarnici
{
    public partial class frmKontejnerStatus : Syncfusion.Windows.Forms.Office2010Form
    {
        bool status = false;
        public frmKontejnerStatus()
        {
            InitializeComponent();
          
        }

        private void RefreshDataGrid()
        {
            var select = " SELECT ID, Naziv " +
                     " FROM  KontejnerStatus ";



            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);


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
            dataGridView1.Columns[0].HeaderText = "Šifra";
            dataGridView1.Columns[0].Width = 40;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Naziv";
            dataGridView1.Columns[1].Width = 150;





        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            //   txtMeSifra.Enabled = false;
            txtSifra.Text = "";
            txtNaziv.Text = "";
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            if (status == true)
            {
                Saobracaj.Sifarnici.InsertKontejnerStatus ins = new Saobracaj.Sifarnici.InsertKontejnerStatus();
                ins.InsKontejnerStatus(txtNaziv.Text.TrimEnd());
            }
            else
            {
                Saobracaj.Sifarnici.InsertKontejnerStatus upd = new Saobracaj.Sifarnici.InsertKontejnerStatus();
                upd.UpdKontejnerStatus(Convert.ToInt32(txtSifra.Text), txtNaziv.Text);
            }
            RefreshDataGrid();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        txtSifra.Text = row.Cells[0].Value.ToString();
                        txtNaziv.Text = row.Cells[1].Value.ToString();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            Saobracaj.Sifarnici.InsertKontejnerStatus del = new Saobracaj.Sifarnici.InsertKontejnerStatus();
            del.DelKontejnerStoatus(Convert.ToInt32(txtSifra.Text));

            RefreshDataGrid();
        }

        private void frmKontejnerStatus_Load(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }
    }
}
