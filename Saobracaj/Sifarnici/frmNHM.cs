﻿using System;
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

namespace Saobracaj.Sifarnici
{
    public partial class frmNHM : Form
    {
        public static string code = "frmNHM";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Sifarnici.frmLogovanje.user.ToString();
        string niz = "";

        bool status = false;
        int chekiran = 0;
        public frmNHM()
        {
            InitializeComponent();
            IdGrupe();
            IdForme();
            PravoPristupa();
        }
        public string IdGrupe()
        {
            var s_connection = frmLogovanje.connectionString;
            string query = "Select IdGrupe from KorisnikGrupa Where Korisnik = " + "'" + Kor.TrimEnd() + "'";
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            int count = 0;

            while (dr.Read())
            {
                if (count == 0)
                {
                    niz = dr["IdGrupe"].ToString();
                    count++;
                }
                else
                {
                    niz = niz + "," + dr["IdGrupe"].ToString();
                    count++;
                }

            }
            conn.Close();
            return niz;
        }
        private int IdForme()
        {
            var s_connection = frmLogovanje.connectionString;
            string query = "Select IdForme from Forme where Rtrim(Code)=" + "'" + code + "'";
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                idForme = Convert.ToInt32(dr["IdForme"].ToString());
            }
            conn.Close();
            return idForme;
        }

        private void PravoPristupa()
        {
            var s_connection = frmLogovanje.connectionString;
            string query = "Select * From GrupeForme Where IdGrupe in (" + niz + ") and IdForme=" + idForme;
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows == false)
            {
                MessageBox.Show("Nemate prava za pristup ovoj formi", code);
                Pravo = false;
            }
            else
            {
                Pravo = true;
                while (reader.Read())
                {
                    insert = Convert.ToBoolean(reader["Upis"]);
                    if (insert == false)
                    {
                        tsNew.Enabled = false;
                    }
                    update = Convert.ToBoolean(reader["Izmena"]);
                    if (update == false)
                    {
                        tsSave.Enabled = false;
                    }
                    delete = Convert.ToBoolean(reader["Brisanje"]);
                    if (delete == false)
                    {
                        tsDelete.Enabled = false;
                    }
                }
            }

            conn.Close();
        }
        private void RefreshDataGrid()
        {
            var select = " Select NHM.ID,NHM.Broj, NHM.Naziv, CASE WHEN NHM.RID > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as RID, ADRID, Uvozni, Interni " +
" , VrstaRobeADR.NAziv, VrstaRobeADR.Klasa, VrstaRobeADR.Grupa from NHM " +
"  left join VrstaRobeADR on NHM.ADRID = VrstaRobeADR.ID";
            var s_connection = frmLogovanje.connectionString;
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
            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Broj";
            dataGridView1.Columns[1].Width = 150;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Naziv";
            dataGridView1.Columns[2].Width = 300;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "RID";
            dataGridView1.Columns[3].Width = 100;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "ADRID";
            dataGridView1.Columns[4].Width = 100;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Uvozni";
            dataGridView1.Columns[5].Width = 100;

        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            int tmpUvozni = 0;

            int tmpInterni = 0;

            if (chkRid.Checked == true)
            {
                chekiran = 1;
            }
            else
            {
                chekiran = 0;
            }


            if (chkUvozni.Checked == true)
            {
                tmpUvozni = 1;
            }
            else
            {
                tmpUvozni = 0;
            }

            if (chkInterni.Checked == true)
            {
                tmpInterni = 1;
            }
            else
            {
                tmpInterni = 0;
            }

            if (status == true)
            {
                Insertnhm ins = new Insertnhm();
                ins.InsNHM(txtBroj.Text,  txtNaziv.Text, chekiran, Convert.ToInt32(txtADR.SelectedValue), tmpUvozni,tmpInterni);
                RefreshDataGrid();
                status = false;
            }
            else
            {
                Insertnhm upd = new Insertnhm();
                upd.UpdNHM(Convert.ToInt32(txtSifra.Text), txtBroj.Text, txtNaziv.Text, chekiran, Convert.ToInt32(txtADR.SelectedValue), tmpUvozni, tmpInterni);
                status = false;
                txtSifra.Enabled = false;
                RefreshDataGrid();
            }
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            txtSifra.Text = "";
            txtSifra.Enabled = false;
            txtBroj.Text = "";
            txtNaziv.Text = "";

            status = true;
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            Insertnhm del = new Insertnhm();
            del.DeleteNHM(Convert.ToInt32(txtSifra.Text));
            status = false;
            txtSifra.Enabled = false;
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
                        txtBroj.Text = row.Cells[1].Value.ToString();
                        txtNaziv.Text = row.Cells[2].Value.ToString();
                        chkRid.Checked = Convert.ToBoolean(row.Cells[3].Value.ToString());
                        if (row.Cells[4].Value.ToString() != "")
                        {txtADR.SelectedValue= Convert.ToInt32(row.Cells[4].Value.ToString()); }
                        else
                        {
                            txtADR.SelectedValue = 0;
                        }    
                        if (row.Cells[5].Value.ToString() == "1")
                        {
                            chkUvozni.Checked = true;
                        }
                        else
                        {
                            chkUvozni.Checked = false;
                        }

                        if (row.Cells[6].Value.ToString() == "1")
                        {
                            chkInterni.Checked = true;
                        }
                        else
                        {
                            chkInterni.Checked = false;
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela promena stavki");
            }
        }

        private void frmNHM_Load(object sender, EventArgs e)
        {
            RefreshDataGrid();
            var connect = frmLogovanje.connectionString;

            var conn = new SqlConnection(connect);
            var adr = "Select ID, (Naziv + ' - ' + UNKod) as Naziv From VrstaRobeADR order by (UNKod + ' ' + Naziv)";
            var adrSAD = new SqlDataAdapter(adr, conn);
            var adrSDS = new DataSet();
            adrSAD.Fill(adrSDS);
            txtADR.DataSource = adrSDS.Tables[0];
            txtADR.DisplayMember = "Naziv";
            txtADR.ValueMember = "ID";
        }
        }
    }

