﻿using Saobracaj.Sifarnici;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;


namespace Saobracaj.Pantheon_Export
{
    public partial class NosiociTroskova : Form
    {
        bool status = false;
        private string connect = Sifarnici.frmLogovanje.connectionString;
        string korisnik = frmLogovanje.user;

        public NosiociTroskova()
        {
            InitializeComponent();
            FillCombo();
            FillGV();
            panel1.Visible = false;
        }
        private void FillGV()
        {
            var select = "Select NosiociTroskova.ID,NosilacTroska,RTrim(NazivNosiocaTroska) as NazivNosiocaTroska,Grupa,RTrim(PaNaziv) as Kupac,RTrim(SifraSubjekta) as Odeljenje, " +
                "(RTrim(Opportunity.OppID) + ' - ' + RTrim(Opportunity.NazivPosla)) as Opportunity, Kupac, NosiociTroskova.Odeljenje,NosiociTroskova.OppID,Posao,NosiociTroskova.Status,RTrim(NosiociTroskova.Korisnik) as Korisnik " +
                "From NosiociTroskova " +
                "inner join Partnerji on NosiociTroskova.Kupac = Partnerji.PaSifra " +
                "inner join Odeljenja on NosiociTroskova.Odeljenje = Odeljenja.ID " +
                "Inner join Opportunity on NosiociTroskova.OppID = Opportunity.ID " +
                "WHere NosiociTroskova.Status = 0 order by ID desc";

            SqlConnection conn = new SqlConnection(connect);
            var dataAdapter = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[2].Width = 220;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[3].Width = 150;
            dataGridView1.Columns[4].Width = 300;
            dataGridView1.Columns[5].Width = 150;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Width = 300;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Width = 80;
            dataGridView1.Columns[11].Width = 60;


            dataGridView1.BorderStyle = BorderStyle.FixedSingle;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }
        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connect);
            var query = "Select PaSifra,PaNaziv from Partnerji order by PaSifra";
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            cboKupac.DataSource = ds.Tables[0];
            cboKupac.DisplayMember = "PaNaziv";
            cboKupac.ValueMember = "PaSifra";


            var query2 = "Select ID,SifraSubjekta from Odeljenja order by Naziv2 desc";
            var da2 = new SqlDataAdapter(query2, conn);
            var ds2 = new DataSet();
            da2.Fill(ds2);
            cboOdeljenje.DataSource = ds2.Tables[0];
            cboOdeljenje.DisplayMember = "SifraSubjekta";
            cboOdeljenje.ValueMember = "ID";

            var query3 = "Select ID,NazivPosla from Opportunity order by ID desc";
            var da3 = new SqlDataAdapter(query3, conn);
            var ds3 = new DataSet();
            da3.Fill(ds3);
            cboOpportunity.DataSource = ds3.Tables[0];
            cboOpportunity.DisplayMember = "NazivPosla";
            cboOpportunity.ValueMember = "ID";

            var najava = "Select ID,Oznaka from Najava Where Status <>7 or status <>9 order by ID desc";
            var najavaDA = new SqlDataAdapter(najava, conn);
            var najavaDS = new DataSet();
            najavaDA.Fill(najavaDS);
            cboPosao.DataSource = najavaDS.Tables[0];
            cboPosao.DisplayMember = "Oznaka";
            cboPosao.ValueMember = "ID";

            var filternt = "Select ID,RTrim(NosilacTroska) as NosilacTroska from NosiociTroskova order by ID desc";
            var filterntDA = new SqlDataAdapter(filternt,conn);
            var filterntDS = new DataSet();
            filterntDA.Fill(filterntDS);
            cboFilterNT.DataSource= filterntDS.Tables[0];
            cboFilterNT.DisplayMember = "NosilacTroska";
            cboFilterNT.ValueMember="ID";

            var filterTextNt = "Select ID,RTrim(NazivNosiocaTroska) as NazivNosiocaTroska from NosiociTroskova order by ID desc";
            var filterTextNTDa = new SqlDataAdapter(filterTextNt, conn);
            var filterTextNTDS = new DataSet();
            filterTextNTDa.Fill(filterTextNTDS);
            cboFilterNazivNT.DataSource= filterTextNTDS.Tables[0];
            cboFilterNazivNT.DisplayMember = "NazivNosiocaTroska";
            cboFilterNazivNT.ValueMember = "ID";

        }
        int ID;
        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            var query = "Select (Max(ID)) as ID From NosiociTroskova";
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr["ID"].GetType() == typeof(DBNull))
                { ID = 1; }
                else
                {
                    ID = Convert.ToInt32(dr["ID"].ToString()) + 1;
                }
            }
            conn.Close();
            string g = DateTime.Now.ToString("yyyy");
            txtNosilacTroska.Text = ID.ToString() + "/" + g;
        }
        int statusNT;

        int stariNT;
        private void tsSave_Click(object sender, EventArgs e)
        {
            InsertPatheonExport ins = new InsertPatheonExport();
            if (status == true)
            {
                SqlConnection conn=new SqlConnection(connect);
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select ID from NosiociTroskova Where Posao=" + Convert.ToInt32(cboPosao.SelectedValue), conn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        MessageBox.Show("Već postoji kreiran nosilac troška za ovaj posao! ID:" + dr[0].ToString());
                        return;
                    }
                }
                else
                {
                    ins.InsNosiociTroskova(txtNosilacTroska.Text.ToString().TrimEnd(), txtNazivNosioca.Text.ToString().TrimEnd(), cboGrupa.Text.ToString().TrimEnd(), Convert.ToInt32(cboKupac.SelectedValue), Convert.ToInt32(cboOdeljenje.SelectedValue), Convert.ToInt32(cboOpportunity.SelectedValue), Convert.ToInt32(cboPosao.SelectedValue), korisnik);
                }
                conn.Close();
            }
            else
            {
                if (statusNT == 0)
                {
                    var query = "Select * from Predvidjanje Where NosilacTroska=" + Convert.ToInt32(txtID.Text);
                    SqlConnection conn = new SqlConnection(connect);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ins.RefreshPredvidjanje(Convert.ToInt32(cboPosao.SelectedValue), Convert.ToInt32(txtID.Text));
                    }
                    conn.Close();

                    ins.UpdNosiociTroskova(Convert.ToInt32(txtID.Text), txtNosilacTroska.Text.ToString().TrimEnd(), txtNazivNosioca.Text.ToString().TrimEnd(), cboGrupa.Text.ToString().TrimEnd(), Convert.ToInt32(cboKupac.SelectedValue), Convert.ToInt32(cboOdeljenje.SelectedValue), Convert.ToInt32(cboOpportunity.SelectedValue), Convert.ToInt32(cboPosao.SelectedValue), korisnik);

                }
                else
                {
                    MessageBox.Show("Nije moguće izmeniti zapis koji je već sinhronizovan!");
                    return;
                }
            }
            FillGV();
            status = false;
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            if (frmLogovanje.user.ToString().TrimEnd() == "mikic.d" || frmLogovanje.user.ToString().TrimEnd() == "cvetkovic.a" || frmLogovanje.user.ToString().TrimEnd() == "jovanovic.v")
            {
                InsertPatheonExport ins = new InsertPatheonExport();
                ins.DelNosiociTroskova(Convert.ToInt32(txtID.Text));
                FillGV();
            }
            else
            {
                MessageBox.Show("Nemate pravo brisanja zapisa!");
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        txtID.Text = row.Cells[0].Value.ToString().TrimEnd();
                        txtNosilacTroska.Text = row.Cells[1].Value.ToString().TrimEnd();
                        txtNazivNosioca.Text = row.Cells[2].Value.ToString().TrimEnd();
                        cboGrupa.Text = row.Cells[3].Value.ToString();
                        cboOpportunity.SelectedValue = Convert.ToInt32(row.Cells[9].Value.ToString());
                        cboPosao.SelectedValue = Convert.ToInt32(row.Cells[10].Value.ToString());
                        cboKupac.SelectedValue = Convert.ToInt32(row.Cells[7].Value.ToString());
                        cboOdeljenje.SelectedValue = Convert.ToInt32(row.Cells[8].Value.ToString());
                    }
                }
            }
            catch { }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;

            var select = "Select NosiociTroskova.ID,NosilacTroska,RTrim(NazivNosiocaTroska) as NazivNosiocaTroska,Grupa,RTrim(PaNaziv) as Kupac,RTrim(SifraSubjekta) as Odeljenje, " +
                "(RTrim(Opportunity.OppID) + ' - ' + RTrim(Opportunity.NazivPosla)) as Opportunity, Kupac, NosiociTroskova.Odeljenje,NosiociTroskova.OppID,Posao,NosiociTroskova.Status,RTrim(NosiociTroskova.Korisnik) as Korisnik " +
                "From NosiociTroskova " +
                "inner join Partnerji on NosiociTroskova.Kupac = Partnerji.PaSifra " +
                "inner join Odeljenja on NosiociTroskova.Odeljenje = Odeljenja.ID " +
                "Inner join Opportunity on NosiociTroskova.OppID = Opportunity.ID " +
                "order by ID desc";
            SqlConnection conn = new SqlConnection(connect);
            var dataAdapter = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

            dataGridView2.BorderStyle = BorderStyle.FixedSingle;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView2.BackgroundColor = Color.White;

            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;


            dataGridView2.Columns[0].Width = 50;
            dataGridView2.Columns[1].Width = 150;
            dataGridView2.Columns[2].Width = 220;
            dataGridView2.Columns[3].Width = 150;
            dataGridView2.Columns[3].Visible = false;
            dataGridView2.Columns[4].Width = 300;
            dataGridView2.Columns[5].Width = 150;
            dataGridView2.Columns[5].Visible = false;
            dataGridView2.Columns[6].Width = 300;
            dataGridView2.Columns[7].Visible = false;
            dataGridView2.Columns[8].Visible = false;
            dataGridView2.Columns[9].Visible = false;
            dataGridView2.Columns[10].Width = 80;
            dataGridView2.Columns[11].Width = 60;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void cboOpportunity_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var query = "Select Odeljenje,Klijent from Opportunity Where ID=" + Convert.ToInt32(cboOpportunity.SelectedValue);
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cboOdeljenje.SelectedValue = Convert.ToInt32(dr[0].ToString());
                cboKupac.SelectedValue = Convert.ToInt32(dr[1].ToString());
            }
            conn.Close();
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Selected)
                {
                    txtID.Text = row.Cells[0].Value.ToString().TrimEnd();
                    txtNosilacTroska.Text = row.Cells[1].Value.ToString().TrimEnd();
                    txtNazivNosioca.Text = row.Cells[2].Value.ToString().TrimEnd();
                    cboGrupa.Text = row.Cells[3].Value.ToString();
                    cboOpportunity.SelectedValue = Convert.ToInt32(row.Cells[9].Value.ToString());
                    cboPosao.SelectedValue = Convert.ToInt32(row.Cells[10].Value.ToString());
                    cboKupac.SelectedValue = Convert.ToInt32(row.Cells[7].Value.ToString());
                    cboOdeljenje.SelectedValue = Convert.ToInt32(row.Cells[8].Value.ToString());
                    statusNT = Convert.ToInt32(row.Cells[11].Value.ToString());
                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            InsertPatheonExport ins = new InsertPatheonExport();
            int ID;
            string NT, NTNaziv, Grupa, Kupac, Odeljenje;
            string json;
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        ID = Convert.ToInt32(row.Cells[0].Value.ToString());
                        NT = row.Cells[1].Value.ToString().TrimEnd();
                        NTNaziv = row.Cells[2].Value.ToString().TrimEnd();
                        Grupa = row.Cells[3].Value.ToString().TrimEnd();
                        Kupac = row.Cells[4].Value.ToString().TrimEnd();
                        Odeljenje = row.Cells[5].Value.ToString().TrimEnd();

                        var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://192.168.129.2:3333/api/Strn/StrnPost");
                        httpWebRequest.ContentType = "application/json";
                        httpWebRequest.Method = "POST";

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            json = "{" +
                                   "\n\"CostDrv\":\"" + NT + "\"," +
                                   "\n\"CostName\":\"" + NTNaziv + "\"," +
                                   "\n\"Classif\":\"" + Grupa + "\"," +
                                   "\n\"Consignee\":\"" + Kupac + "\"," +
                                   "\n\"Dept\":\"" + Odeljenje + "\"\n}";
                            streamWriter.Write(json);
                        }

                        string response = "";

                        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            var result = streamReader.ReadToEnd();
                            response = result.ToString();

                            if (response.Contains("Error") == true || response.Contains("Greška") == true || response.Contains("ERROR") == true || response.Contains("Duplikat") == true)
                            {
                                MessageBox.Show("Slanje nije uspelo\n" + response.ToString());
                                ins.InsApiLog("NT-" + ID.ToString(), json, response);
                                return;
                            }
                            else
                            {
                                using (SqlConnection conn = new SqlConnection(connect))
                                {
                                    using (SqlCommand cmd = conn.CreateCommand())
                                    {
                                        cmd.CommandText = "UPDATE NosiociTroskova SET Status = 1 WHERE ID = " + ID;
                                        conn.Open();
                                        cmd.ExecuteNonQuery();
                                        conn.Close();
                                    }
                                }
                            }
                        }
                        ins.InsApiLog("NT-" + ID.ToString(), json, response);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

            FillGV();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int nt = Convert.ToInt32(cboFilterNT.SelectedValue);
            
            foreach(DataGridViewRow row in dataGridView2.Rows)
            {
                if (nt==Convert.ToInt32(row.Cells["ID"].Value.ToString()))
                {
                    row.Selected = true;
                    dataGridView2.CurrentCell = row.Cells[1];
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int nt =Convert.ToInt32(cboFilterNazivNT.SelectedValue);
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (nt == Convert.ToInt32(row.Cells[0].Value.ToString()))
                {
                    row.Selected = true;
                    dataGridView2.CurrentCell = row.Cells[2];
                }
            }
        }
        //Sort prvi DGV
        int gv1 = 0;
        int gv2 = 0;
        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var select = "Select NosiociTroskova.ID,NosilacTroska,RTrim(NazivNosiocaTroska) as NazivNosiocaTroska,Grupa,RTrim(PaNaziv) as Kupac,RTrim(SifraSubjekta) as Odeljenje, " +
                "(RTrim(Opportunity.OppID) + ' - ' + RTrim(Opportunity.NazivPosla)) as Opportunity, Kupac, NosiociTroskova.Odeljenje,NosiociTroskova.OppID,Posao,NosiociTroskova.Status,RTrim(NosiociTroskova.Korisnik) as Korisnik " +
                "From NosiociTroskova " +
                "inner join Partnerji on NosiociTroskova.Kupac = Partnerji.PaSifra " +
                "inner join Odeljenja on NosiociTroskova.Odeljenje = Odeljenja.ID " +
                "Inner join Opportunity on NosiociTroskova.OppID = Opportunity.ID " +
                "WHere NosiociTroskova.Status = 0 ";

            if (dataGridView1.Columns[e.ColumnIndex].Name == "NazivNosiocaTroska")
            {

                if (gv1 % 2 == 0)
                {
                    select = select + "order by NosiociTroskova.ID asc";
                }
                else
                {
                    select = select + " order by NosiociTroskova.ID desc";
                }
                gv1++;
            }

            if (dataGridView1.Columns[e.ColumnIndex].Name == "NazivNosiocaTroska")
            {
                
                if (gv1 % 2 == 0)
                {
                    select = select + "order by NazivNosiocaTroska asc";
                }
                else
                {
                    select = select + " order by NazivNosiocaTroska desc";
                }
                gv1++;
            }

            if (dataGridView1.Columns[e.ColumnIndex].Name == "Kupac")
            {

                if (gv1 % 2 == 0)
                {
                    select = select + "order by NosiociTroskova.Kupac asc";
                }
                else
                {
                    select = select + " order by NosiociTroskova.Kupac desc";
                }
                gv1++;
            }

            if (dataGridView1.Columns[e.ColumnIndex].Name == "Opportunity")
            {

                if (gv1 % 2 == 0)
                {
                    select = select + "order by NosiociTroskova.OppID asc";
                }
                else
                {
                    select = select + " order by NosiociTroskova.OppID desc";
                }
                gv1++;
            }

            SqlConnection conn = new SqlConnection(connect);
            var dataAdapter = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[2].Width = 220;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[3].Width = 150;
            dataGridView1.Columns[4].Width = 300;
            dataGridView1.Columns[5].Width = 150;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Width = 300;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Width = 80;
            dataGridView1.Columns[11].Width = 60;


            dataGridView1.BorderStyle = BorderStyle.FixedSingle;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }
        //Sort drugi DGV
        private void dataGridView2_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var select = "Select NosiociTroskova.ID,NosilacTroska,RTrim(NazivNosiocaTroska) as NazivNosiocaTroska,Grupa,RTrim(PaNaziv) as Kupac,RTrim(SifraSubjekta) as Odeljenje, " +
                "(RTrim(Opportunity.OppID) + ' - ' + RTrim(Opportunity.NazivPosla)) as Opportunity, Kupac, NosiociTroskova.Odeljenje,NosiociTroskova.OppID,Posao,NosiociTroskova.Status,RTrim(NosiociTroskova.Korisnik) as Korisnik " +
                "From NosiociTroskova " +
                "inner join Partnerji on NosiociTroskova.Kupac = Partnerji.PaSifra " +
                "inner join Odeljenja on NosiociTroskova.Odeljenje = Odeljenja.ID " +
                "Inner join Opportunity on NosiociTroskova.OppID = Opportunity.ID ";

            if (dataGridView2.Columns[e.ColumnIndex].Name == "ID")
            {
                if (gv2 % 2 == 0)
                {
                    select = select + "order by NosiociTroskova.ID asc";
                }
                else
                {
                    select = select + " order by NosiociTroskova.ID desc";
                }
                gv2++;
            }

            if (dataGridView2.Columns[e.ColumnIndex].Name == "NazivNosiocaTroska")
            {
                if (gv2 % 2 == 0)
                {
                    select = select + "order by NazivNosiocaTroska asc";
                }
                else
                {
                    select = select + " order by NazivNosiocaTroska desc";
                }
                gv2++;
            }
            if (dataGridView2.Columns[e.ColumnIndex].Name == "Kupac")
            {
                if (gv2 % 2 == 0)
                {
                    select = select + "order by NosiociTroskova.Kupac asc";
                }
                else
                {
                    select = select + " order by NosiociTroskova.Kupac desc";
                }
                gv2++;
            }
            if (dataGridView2.Columns[e.ColumnIndex].Name == "Opportunity")
            {
                if (gv2 % 2 == 0)
                {
                    select = select + "order by NosiociTroskova.OppID asc";
                }
                else
                {
                    select = select + " order by NosiociTroskova.OppID desc";
                }
                gv2++;
            }
            SqlConnection conn = new SqlConnection(connect);
            var dataAdapter = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

            dataGridView2.BorderStyle = BorderStyle.FixedSingle;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView2.BackgroundColor = Color.White;

            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;


            dataGridView2.Columns[0].Width = 50;
            dataGridView2.Columns[1].Width = 150;
            dataGridView2.Columns[2].Width = 220;
            dataGridView2.Columns[3].Width = 150;
            dataGridView2.Columns[3].Visible = false;
            dataGridView2.Columns[4].Width = 300;
            dataGridView2.Columns[5].Width = 150;
            dataGridView2.Columns[5].Visible = false;
            dataGridView2.Columns[6].Width = 300;
            dataGridView2.Columns[7].Visible = false;
            dataGridView2.Columns[8].Visible = false;
            dataGridView2.Columns[9].Visible = false;
            dataGridView2.Columns[10].Width = 80;
            dataGridView2.Columns[11].Width = 60;
        }
    }
}
