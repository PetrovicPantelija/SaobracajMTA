﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Izvoz
{
    public partial class frmPlanoviIzvoza : Syncfusion.Windows.Forms.Office2010Form
    {
        int Terminal = 0;

        public frmPlanoviIzvoza()
        {
            InitializeComponent();

        }

        public frmPlanoviIzvoza(int Ter)
        {
            InitializeComponent();
            Terminal = Ter;
        }

        private void RefreshDataGrid()
        {


            var select = "Select IzvozKonacnaZaglavlje.ID, IzvozKonacnaZaglavlje.IDVoza, Voz.BrVoza, Voz.VremePolaska, Voz.VremeDolaska, s1.Opis as StanicaOd, s2.Opis as StanicaDo, Voz.Relacija, IzvozKonacnaZaglavlje.Napomena from IzvozKonacnaZaglavlje " +
                " inner join Voz on Voz.ID = IzvozKonacnaZaglavlje.IDVoza " +
                " inner join stanice s1 on s1.ID = Voz.StanicaOd " +
                " inner join stanice s2 on s2.ID = Voz.StanicaDo " +
                " where IzvozKonacnaZaglavlje.Vozom = 1 and Terminal = " + Terminal +
                " order by IzvozKonacnaZaglavlje.ID desc";
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
            /*
                        DataGridViewColumn column = dataGridView1.Columns[0];
                        dataGridView1.Columns[0].HeaderText = "ID";
                        dataGridView1.Columns[0].Width = 50;

                        DataGridViewColumn column2 = dataGridView1.Columns[1];
                        dataGridView1.Columns[1].HeaderText = "Br voza";
                        dataGridView1.Columns[1].Width = 50;

                        DataGridViewColumn column3 = dataGridView1.Columns[2];
                        dataGridView1.Columns[2].HeaderText = "Relacija";
                        dataGridView1.Columns[2].Width = 150;

                        DataGridViewColumn column4 = dataGridView1.Columns[3];
                        dataGridView1.Columns[3].HeaderText = "Vr polaska";
                        dataGridView1.Columns[3].Width = 70;

                        DataGridViewColumn column5 = dataGridView1.Columns[4];
                        dataGridView1.Columns[4].HeaderText = "Vr dolaska";
                        dataGridView1.Columns[4].Width = 70;

                        DataGridViewColumn column6 = dataGridView1.Columns[5];
                        dataGridView1.Columns[5].HeaderText = "Max bruto";
                        dataGridView1.Columns[5].Width = 70;

                        DataGridViewColumn column7 = dataGridView1.Columns[6];
                        dataGridView1.Columns[6].HeaderText = "Max duž";
                        dataGridView1.Columns[6].Width = 70;

                        DataGridViewColumn column8 = dataGridView1.Columns[7];
                        dataGridView1.Columns[7].HeaderText = "Max br kola";
                        dataGridView1.Columns[7].Width = 70;

                        DataGridViewColumn column9 = dataGridView1.Columns[8];
                        dataGridView1.Columns[8].HeaderText = "Napomena";
                        dataGridView1.Columns[8].Width = 100;
            */

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

                    }
                }


            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void frmPlanoviIzvoza_Load(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            frmIzvozKonacna pUvoz = new frmIzvozKonacna(Convert.ToInt32(txtSifra.Text));
            pUvoz.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            frmIzvozKonacnaZaglavlje fukz = new frmIzvozKonacnaZaglavlje();
            fukz.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (txtSifra.Text == "")
            {
                MessageBox.Show("Izaberite plan/mesec odlaska");
                return;
            }
            //Otvaranje forme za Terminal, sa Planom
            frmIzvoz ik = new frmIzvoz(1, Convert.ToInt32(txtSifra.Text));
            ik.Show();
        }
    }
}
