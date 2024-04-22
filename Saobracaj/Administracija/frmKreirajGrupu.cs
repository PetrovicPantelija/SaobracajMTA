﻿using Saobracaj.Sifarnici;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Administracija
{
    public partial class frmKreirajGrupu : Form
    {
        public string connect = frmLogovanje.connectionString;
        private bool status = false;
        private string niz = "";

        public frmKreirajGrupu()
        {
            InitializeComponent();
            txt_IdGrupe.Enabled = false;
            txt_Sifra.Enabled = false;

        }

        public static string code = "frmKreirajGrupu";
        public bool Pravo;
        private int idGrupe;
        private int idForme;
        private bool insert;
        private bool update;
        private bool delete;
        private string Kor = Sifarnici.frmLogovanje.user.ToString();

        private void frmKreirajGrupu_Load(object sender, EventArgs e)
        {
            RefreshDG();
        }

        private void RefreshDG()
        {
            var query = "select * from GrupeKorisnik";
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
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

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].HeaderText = "ID Grupe";
            dataGridView1.Columns[1].Width = 70;
            dataGridView1.Columns[2].HeaderText = "Naziv Grupe";
            dataGridView1.Columns[2].Width = 200;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        txt_Sifra.Text = row.Cells[0].Value.ToString();
                        txt_IdGrupe.Text = row.Cells[1].Value.ToString();
                        txt_Naziv.Text = row.Cells[2].Value.ToString();
                    }
                }
            }
            catch
            {
            }
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            txt_IdGrupe.Text = "";
            txt_Sifra.Text = "";
            status = true;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            InsertGrupe grupe = new InsertGrupe();
            if (status == true)
            {
                grupe.InsGrupe(txt_Naziv.Text.TrimEnd().ToString());
                RefreshDG();
                status = false;
            }
            else
            {
                grupe.UpdateGrupe(Convert.ToInt32(txt_Sifra.Text.TrimEnd()), Convert.ToInt32(txt_IdGrupe.Text.TrimEnd()), txt_Naziv.Text.TrimEnd().ToString());
                RefreshDG();
            }
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertGrupe grupe = new InsertGrupe();
            grupe.DeleteGrupe(Convert.ToInt32(txt_Sifra.Text));
            RefreshDG();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Administracija.frmDodeliGrupu dodeliGrupu = new frmDodeliGrupu();
            dodeliGrupu.Show();
        }
    }
}