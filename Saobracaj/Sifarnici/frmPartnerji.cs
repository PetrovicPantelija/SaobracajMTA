using Newtonsoft.Json;
using Saobracaj.Pantheon_Export;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Saobracaj.Sifarnici
{
    public partial class frmPartnerji : Syncfusion.Windows.Forms.Office2010Form
    {
        int PomBrodar = 0;
        int PomPlatilac = 0;
        int PomOrganizator = 0;
        int PomVlasnik = 0;
        int PomSpediter = 0;
        int PomNalogodavac = 0;
        int PomUvoznik = 0;
        int PomIzvoznik = 0;
        int PomLogisticar = 0;
        int PomKamioner = 0;
        int PomAgentBrodara = 0;

        bool status = false;
        string Kor = Sifarnici.frmLogovanje.user.ToString();
        private string connect = Sifarnici.frmLogovanje.connectionString;
        string Dobavljac = "";
        string Kupac = "";
        string Obveznik = "";

        public frmPartnerji()
        {
            InitializeComponent();

        }
        private void frmPartnerji_Load(object sender, EventArgs e)
        {
            RefreshDataGrid();
            ChecBoxVisible(this.Controls);
            panel1.Visible = false;

            SqlConnection conn = new SqlConnection(connect);
            var valuta = "Select VaSifra,VaNaziv From Valute";
            var valutaDa = new SqlDataAdapter(valuta, conn);
            var valutaDS = new DataSet();
            valutaDa.Fill(valutaDS);
            cboValuta.DataSource = valutaDS.Tables[0];
            cboValuta.DisplayMember = "VaNaziv";
            cboValuta.ValueMember = "VaSifra";

        }
        private void ChecBoxVisible(Control.ControlCollection ctrlCollection)
        {
            foreach (Control c in ctrlCollection)
            {
                if (c is CheckBox)
                {
                    c.Visible = false;
                }
            }

            string firma = Sifarnici.frmLogovanje.Firma;
            switch (firma)
            {
                case "Leget":
                    btnDrzava.Visible = false;
                    //btnPosta.Visible = false;
                    cbDobavljac.Visible = false;
                    cbObveznik.Visible = false;
                    cboValuta.Visible = false;

                    int prviRedX = 16;
                    int prviRedY = 245;

                    int drugiRedX = 159;
                    int drugiRedY = 245;

                    chkLogisitcar.Visible = true;
                    chkLogisitcar.Location = new Point(prviRedX, prviRedY);

                    chkSpediter.Visible = true;
                    chkSpediter.Location = new Point(prviRedX, prviRedY + 30);

                    chkBrodar.Visible = true;
                    chkBrodar.Location = new Point(prviRedX, prviRedY + 60);

                    chkAgentBrodara.Visible = true;
                    chkAgentBrodara.Location = new Point(prviRedX, prviRedY + 90);

                    chkUvoznik.Visible = true;
                    chkUvoznik.Location = new Point(drugiRedX, drugiRedY);

                    chkIzvoznik.Visible = true;
                    chkIzvoznik.Location = new Point(drugiRedX, drugiRedY + 30);

                    chkKamioner.Visible = true;
                    chkKamioner.Location = new Point(drugiRedX, drugiRedY + 60);

                    chkOrganizator.Text = "Železnički operater";
                    chkOrganizator.Visible = true;
                    chkOrganizator.Location = new Point(drugiRedX, drugiRedY + 90);
                    break;

                case "TA":
                    chkPrevoznik.Visible = true;
                    chkPosiljalac.Visible = true;
                    chkPrimalac.Visible = true;
                    chkBrodar.Visible = true;
                    chkVlasnik.Visible = true;
                    chkSpediter.Visible = true;
                    chkPlatilac.Visible = true;
                    chkOrganizator.Visible = true;
                    chkNalogodavac.Visible = true;
                    chkUvoznik.Visible = true;
                    chkIzvoznik.Visible = true;
                    //btnDrzava.Visible = false;
                    //btnPosta.Visible = false;
                    cbDobavljac.Visible = true;
                    cbObveznik.Visible = true;
                    //cboValuta.Visible = false;
                    cboKupac.Visible = true;
                    break;

                case "DPT":
                    chkPrevoznik.Visible = true;
                    chkPosiljalac.Visible = true;
                    chkPrimalac.Visible = true;
                    chkBrodar.Visible = true;
                    chkVlasnik.Visible = true;
                    chkSpediter.Visible = true;
                    chkPlatilac.Visible = true;
                    chkOrganizator.Visible = true;
                    chkNalogodavac.Visible = true;
                    chkUvoznik.Visible = true;
                    chkIzvoznik.Visible = true;
                    //btnDrzava.Visible = false;
                    //btnPosta.Visible = false;
                    cbDobavljac.Visible = true;
                    cbObveznik.Visible = true;
                    //cboValuta.Visible = false;
                    cboKupac.Visible = true;
                    break;
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
                        txtSifra.Text = row.Cells[0].Value.ToString().Trim();
                        txtNaziv.Text = row.Cells[1].Value.ToString().Trim();
                        txtUlica.Text = row.Cells[2].Value.ToString().Trim();
                        txtMesto.Text = row.Cells[3].Value.ToString().Trim();
                        txtOblast.Text = row.Cells[4].Value.ToString().Trim();
                        txtPosta.Text = row.Cells[5].Value.ToString().Trim();
                        txtDrzava.Text = row.Cells[6].Value.ToString().Trim();
                        txtTelefon.Text = row.Cells[7].Value.ToString().Trim();
                        txtTR.Text = row.Cells[8].Value.ToString().Trim();
                        txtNapomena.Text = row.Cells[9].Value.ToString().Trim();
                        txtMaticniBroj.Text = row.Cells[12].Value.ToString().Trim();
                        txtEmail.Text = row.Cells[11].Value.ToString().Trim();
                        txtPIB.Text = row.Cells[10].Value.ToString().Trim();
                        txtUIC.Text = row.Cells[13].Value.ToString().Trim();
                        chkPrevoznik.Checked = Convert.ToBoolean(row.Cells[14].Value.ToString());
                        chkPosiljalac.Checked = Convert.ToBoolean(row.Cells[15].Value.ToString());
                        chkPrimalac.Checked = Convert.ToBoolean(row.Cells[16].Value.ToString());
                        if (row.Cells[17].Value.ToString() == "1")
                        { chkBrodar.Checked = true; }
                        else
                        {
                            chkBrodar.Checked = false;
                        }


                        if (row.Cells[18].Value.ToString() == "1")
                        { chkVlasnik.Checked = true; }
                        else
                        {
                            chkVlasnik.Checked = false;
                        }

                        if (row.Cells[19].Value.ToString() == "1")
                        { chkSpediter.Checked = true; }
                        else
                        {
                            chkSpediter.Checked = false;
                        }

                        if (row.Cells[20].Value.ToString() == "1")
                        { chkPlatilac.Checked = true; }
                        else
                        {
                            chkPlatilac.Checked = false;
                        }

                        if (row.Cells[21].Value.ToString() == "1")
                        { chkOrganizator.Checked = true; }
                        else
                        {
                            chkOrganizator.Checked = false;
                        }


                        if (row.Cells[22].Value.ToString() == "1")
                        { chkNalogodavac.Checked = true; }
                        else
                        {
                            chkNalogodavac.Checked = false;
                        }

                        if (row.Cells[23].Value.ToString() == "1")
                        { chkUvoznik.Checked = true; }
                        else
                        {
                            chkUvoznik.Checked = false;
                        }
                        txtUICDrzava.Text = row.Cells[24].Value.ToString().Trim();
                        txtTR2.Text = row.Cells[25].Value.ToString().Trim();
                        txtFaks.Text = row.Cells[26].Value.ToString().Trim();

                        if (row.Cells[27].Value.ToString() == "1")
                        { chkIzvoznik.Checked = true; }
                        else
                        {
                            chkIzvoznik.Checked = false;
                        }
                        if (row.Cells[28].Value.ToString() == "1") { chkLogisitcar.Checked = true; } else { chkLogisitcar.Checked = false; }
                        if (row.Cells[29].Value.ToString() == "1") { chkKamioner.Checked = true; } else { chkKamioner.Checked = false; }
                        if (row.Cells[30].Value.ToString() == "1") { chkAgentBrodara.Checked = true; } else { chkAgentBrodara.Checked = false; }
                        if (row.Cells["Supplier"].Value.ToString() == "T") { cbDobavljac.Checked = true; } else { cbDobavljac.Checked = false; }
                        if (row.Cells["Buyer"].Value.ToString() == "T") { cboKupac.Checked = true; } else { cboKupac.Checked= false; }
                        
                        if (row.Cells["WayOfSale"].Value.ToString() == "Z") { cbObveznik.Checked = true; } else { cbObveznik.Checked = false; }

                        cboValuta.SelectedValue = row.Cells["Currency"].Value;

                        RefreshDataGrid2(txtSifra.Text);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void tsSave_Click(object sender, EventArgs e)
        {

        }

        private void RefreshDataGrid()
        {
            var select = " Select PaSifra, Rtrim(PaNaziv) as PaNaziv, PaUlicaHisnaSt , PaKraj, PaDelDrzave, PaPostnaSt, PaSifDrzave, PaTelefon1, PaZiroRac, " +
                " PaOpomba, PaDMatSt, PaEMail, PaEMatSt1, Rtrim(UIC) as UIC, (CASE WHEN Prevoznik > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END)  as Prevoznik, (CASE WHEN Posiljalac > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END)  as Posiljalac, (CASE WHEN Primalac > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END)  as Primalac ,  Brodar " +
            " , Vlasnik , Spediter , Platilac , Organizator, NalogodavacCH, UvoznikCH, UICDrzava,TR2, Faks, PomIzvoznik,Logisticar,Kamioner,AgentBrodara,Buyer,Supplier,WayOfSale,Currency from Partnerji order by PaSifra desc";
            SqlConnection myConnection = new SqlConnection(connect);
            var c = new SqlConnection(connect);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);

            /*
            var select4 = " Select Distinct PaSifra, RTrim(PaNaziv) as Partner From Partnerji";
            var s_connection4 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection4 = new SqlConnection(s_connection4);
            var c4 = new SqlConnection(s_connection4);
            var dataAdapter4 = new SqlDataAdapter(select4, c4);

            var commandBuilder4 = new SqlCommandBuilder(dataAdapter4);
            var ds4 = new DataSet();
            dataAdapter4.Fill(ds4);
            cboPrevoznik.DataSource = ds4.Tables[0];
            cboPrevoznik.DisplayMember = "Partner";
            cboPrevoznik.ValueMember = "PaSifra";
            */


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
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Naziv";
            dataGridView1.Columns[1].Width = 250;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Ulica";
            dataGridView1.Columns[2].Width = 100;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Mesto";
            dataGridView1.Columns[3].Width = 100;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Oblast";
            dataGridView1.Columns[4].Width = 70;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Pošta";
            dataGridView1.Columns[5].Width = 60;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Država";
            dataGridView1.Columns[6].Width = 60;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Telefon";
            dataGridView1.Columns[7].Width = 100;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Tekući račun";
            dataGridView1.Columns[8].Width = 100;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Napomena";
            dataGridView1.Columns[9].Width = 250;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "PIB";
            dataGridView1.Columns[10].Width = 70;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "E mail";
            dataGridView1.Columns[11].Width = 80;


            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Matični broj";
            dataGridView1.Columns[12].Width = 70;

            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "UIC";
            dataGridView1.Columns[13].Width = 50;

            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Prevoznik";
            dataGridView1.Columns[14].Width = 40;

            DataGridViewColumn column16 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "Pošiljalac";
            dataGridView1.Columns[15].Width = 40;

            DataGridViewColumn column17 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "Primalac";
            dataGridView1.Columns[16].Width = 40;

            DataGridViewColumn column18 = dataGridView1.Columns[17];
            dataGridView1.Columns[17].HeaderText = "UIC DRZAVA";
            dataGridView1.Columns[17].Width = 60;

            DataGridViewColumn column19 = dataGridView1.Columns[18];
            dataGridView1.Columns[18].HeaderText = "TR2";
            dataGridView1.Columns[18].Width = 60;


            DataGridViewColumn column20 = dataGridView1.Columns[19];
            dataGridView1.Columns[19].HeaderText = "FAKS";
            dataGridView1.Columns[19].Width = 60;

            DataGridViewColumn column21 = dataGridView1.Columns[20];
            dataGridView1.Columns[20].HeaderText = "Izvoznik";
            dataGridView1.Columns[20].Width = 60;

            dataGridView1.Columns["Currency"].Visible = false;

            if (Sifarnici.frmLogovanje.Firma == "TA")
            {
                dataGridView1.Columns["Logisticar"].Visible = false;
                dataGridView1.Columns["Kamioner"].Visible = false;
                dataGridView1.Columns["AgentBrodara"].Visible = false;
            }

        }



        private void RefreshDataGrid2(string SifraPartnera)
        {
            var select = " Select PaKoSifra, (Rtrim(PaKOPriimek) + ' ' + Rtrim(PaKoIme)) as Naziv, PaKoOddelek as Odeljenje, PaKoTel as Telefon, PaKoMail as Mail, PaKOOpomba as Napomena  from PartnerjiKOntOseba   where PaKOSifra =" + SifraPartnera;
            SqlConnection myConnection = new SqlConnection(connect);
            var c = new SqlConnection(connect);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);

            /*
            var select4 = " Select Distinct PaSifra, RTrim(PaNaziv) as Partner From Partnerji";
            var s_connection4 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection4 = new SqlConnection(s_connection4);
            var c4 = new SqlConnection(s_connection4);
            var dataAdapter4 = new SqlDataAdapter(select4, c4);

            var commandBuilder4 = new SqlCommandBuilder(dataAdapter4);
            var ds4 = new DataSet();
            dataAdapter4.Fill(ds4);
            cboPrevoznik.DataSource = ds4.Tables[0];
            cboPrevoznik.DisplayMember = "Partner";
            cboPrevoznik.ValueMember = "PaSifra";
            */


            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

            dataGridView2.BorderStyle = BorderStyle.None;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView2.BackgroundColor = Color.White;

            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "Šifra";
            dataGridView2.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView2.Columns[1];
            dataGridView2.Columns[1].HeaderText = "Naziv";
            dataGridView2.Columns[1].Width = 150;

            DataGridViewColumn column3 = dataGridView2.Columns[2];
            dataGridView2.Columns[2].HeaderText = "Odeljenje";
            dataGridView2.Columns[2].Width = 150;

            DataGridViewColumn column4 = dataGridView2.Columns[3];
            dataGridView2.Columns[3].HeaderText = "Telefon";
            dataGridView2.Columns[3].Width = 150;

            DataGridViewColumn column5 = dataGridView2.Columns[4];
            dataGridView2.Columns[4].HeaderText = "Email";
            dataGridView2.Columns[4].Width = 250;

            DataGridViewColumn column6 = dataGridView2.Columns[5];
            dataGridView2.Columns[5].HeaderText = "Napomena";
            dataGridView2.Columns[5].Width = 250;
        }

        private void cboPartneri_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            txtSifra.Enabled = false;
            txtSifra.Text = "";
            txtNaziv.Text = "";
            txtUlica.Text = "";

            txtMesto.Text = "";
            txtOblast.Text = "";
            txtPosta.Text = "";
            txtDrzava.Text = "";
            txtTelefon.Text = "";
            txtTR.Text = "";
            txtNapomena.Text = "";
            txtMaticniBroj.Text = "";
            txtEmail.Text = "";
            txtPIB.Text = "";
            txtUIC.Text = "";
            txtUICDrzava.Text = "";
            txtTR2.Text = "";
            txtFaks.Text = "";
            chkPrevoznik.Checked = false;
            chkPosiljalac.Checked = false;
            chkPrimalac.Checked = false;
            chkBrodar.Checked = false;
            chkVlasnik.Checked = false;
            chkSpediter.Checked = false;
            chkPlatilac.Checked = false;
            chkOrganizator.Checked = false;
            chkNalogodavac.Checked = false;
            chkUvoznik.Checked = false;
            chkIzvoznik.Checked = false;
            chkLogisitcar.Checked = false;
            chkKamioner.Checked = false;
            chkAgentBrodara.Checked = false;
            cboKupac.Checked = false;
            cbDobavljac.Checked= false;

        }
        int referent;
        private void tsSave_Click_1(object sender, EventArgs e)
        {
            var query = "Select DeSifra From Korisnici Where Korisnik='" + Kor.ToString().TrimEnd() + "'";
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                referent = Convert.ToInt32(dr[0].ToString());
            }
            conn.Close();

            string firma = Sifarnici.frmLogovanje.Firma;
            switch (firma)
            {
                case "TA":
                    txtDrzava.Text = SifDrzave.ToString();
                    break;
            }

            if (chkBrodar.Checked)
            {
                PomBrodar = 1;
            }
            else
            {
                PomBrodar = 0;
            }



            if (chkPlatilac.Checked)
            {
                PomPlatilac = 1;
            }
            else
            {
                PomPlatilac = 0;
            }
            if (chkOrganizator.Checked)
            {
                PomOrganizator = 1;
            }
            else
            {
                PomOrganizator = 0;
            }
            if (chkVlasnik.Checked)
            {
                PomVlasnik = 1;
            }
            else
            {
                PomVlasnik = 0;
            }
            if (chkOrganizator.Checked)
            {
                PomOrganizator = 1;
            }
            else
            {
                PomOrganizator = 0;
            }

            if (chkSpediter.Checked)
            {
                PomSpediter = 1;
            }
            else
            {
                PomSpediter = 0;
            }

            if (chkNalogodavac.Checked)
            {
                PomNalogodavac = 1;
            }
            else
            {
                PomNalogodavac = 0;
            }

            if (chkUvoznik.Checked)
            {
                PomUvoznik = 1;
            }
            else
            {
                PomUvoznik = 0;
            }

            if (chkIzvoznik.Checked)
            {
                PomIzvoznik = 1;
            }
            else
            {
                PomIzvoznik = 0;
            }

            if (chkLogisitcar.Checked)
            {
                PomLogisticar = 1;
            }
            else { PomLogisticar = 0; }
            if (chkKamioner.Checked) { PomKamioner = 1; } else { PomKamioner = 0; }
            if (chkAgentBrodara.Checked) { PomAgentBrodara = 1; } else { PomAgentBrodara = 0; }

            if (cbDobavljac.Checked) { Dobavljac = "T"; } else{ Dobavljac = "F"; }
            if (cboKupac.Checked) { Kupac = "T"; }else{ Kupac = "F"; }
            if (cbObveznik.Checked) { Obveznik = "Z"; } else { Obveznik = "I"; }

            if (status == true)
            {
                //  txtNaziv.Text,  txtUlica.Text,  txtMesto.Text,  txtOblast.Text, txtPosta.Text ,txtDrzava.Text, txtTelefon.Text, txtTR.Text ,  txtNapomena.Text,txtMaticniBroj.Text,  txtEmail.Text,  txtPIB.Text
                InsertPartnerji ins = new InsertPartnerji();
                ins.InsPartneri(txtNaziv.Text, txtUlica.Text, txtMesto.Text, txtPosta.Text, txtDrzava.Text, txtTelefon.Text, txtTR.Text, txtNapomena.Text, txtPIB.Text, txtEmail.Text, txtMaticniBroj.Text, txtUIC.Text, chkPrevoznik.Checked, chkPosiljalac.Checked, chkPrimalac.Checked, PomBrodar, PomVlasnik, PomSpediter, PomPlatilac, PomOrganizator, PomNalogodavac, PomUvoznik, txtMUAdresa.Text, txtMUKontakt.Text, txtUICDrzava.Text, txtTR2.Text, txtFaks.Text, PomIzvoznik, PomLogisticar, PomKamioner, PomAgentBrodara, Kupac, Obveznik, cboValuta.SelectedValue.ToString(), Dobavljac, referent);
            }
            else
            {
                InsertPartnerji upd = new InsertPartnerji();
                upd.UpdPartneri(Convert.ToInt32(txtSifra.Text), txtNaziv.Text, txtUlica.Text, txtMesto.Text, txtOblast.Text, txtPosta.Text, txtDrzava.Text, txtTelefon.Text, txtTR.Text, txtNapomena.Text, txtPIB.Text, txtEmail.Text, txtMaticniBroj.Text, txtUIC.Text, chkPrevoznik.Checked, chkPosiljalac.Checked, chkPrimalac.Checked, PomBrodar, PomVlasnik, PomSpediter, PomPlatilac, PomOrganizator, PomNalogodavac, PomUvoznik, txtMUAdresa.Text, txtMUKontakt.Text, txtUICDrzava.Text, txtTR2.Text, txtFaks.Text, PomIzvoznik, PomLogisticar, PomKamioner, PomAgentBrodara, Kupac, Obveznik, Dobavljac, cboValuta.SelectedValue.ToString());
            }
            RefreshDataGrid();
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertPartnerji del = new InsertPartnerji();
            del.DelPartneri(Convert.ToInt32(txtSifra.Text));
            RefreshDataGrid();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            Dokumenta.frmKontaktOsobe pko = new Dokumenta.frmKontaktOsobe(Convert.ToInt32(txtSifra.Text));
            pko.Show();
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            //dataGridView3.DataSource = null;
            panel1.Visible = false;

        }

        int SifDrzave;
        string Drzava = "";
        private void btnDrzava_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            var select = "Select DrSifra,DrNaziv From Drzave order by DrSifra";
            SqlConnection conn = new SqlConnection(connect);
            var dataAdapter = new SqlDataAdapter(select, conn);
            var ds = new System.Data.DataSet();
            dataAdapter.Fill(ds);
            dataGridView3.ReadOnly = true;
            dataGridView3.DataSource = ds.Tables[0];


            dataGridView3.BorderStyle = BorderStyle.None;
            dataGridView3.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView3.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView3.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView3.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView3.BackgroundColor = Color.White;

            dataGridView3.EnableHeadersVisualStyles = false;
            dataGridView3.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView3.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            var select2 = "select RTrim(PttNaziv)as Naziv,(RTrim(PttDrzava)+'-'+PttSifra) as Posta from Poste order by PttNaziv asc";

            var dataAdapter2 = new SqlDataAdapter(select2, conn);
            var ds2 = new System.Data.DataSet();
            dataAdapter2.Fill(ds2);
            dataGridView4.ReadOnly = true;
            dataGridView4.DataSource = ds2.Tables[0];


            dataGridView4.BorderStyle = BorderStyle.None;
            dataGridView4.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView4.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView4.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView4.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView4.BackgroundColor = Color.White;

            dataGridView4.EnableHeadersVisualStyles = false;
            dataGridView4.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView4.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView4.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

        }
        string postaNaziv = "";
        string Posta = "";
        private void btnPosta_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (row.Selected)
                {
                    SifDrzave = Convert.ToInt32(row.Cells[0].Value.ToString());
                    Drzava = row.Cells[1].Value.ToString();
                }
            }
            foreach (DataGridViewRow row in dataGridView4.Rows)
            {
                if (row.Selected)
                {
                    postaNaziv = row.Cells[0].Value.ToString();
                    Posta = row.Cells[1].Value.ToString();
                }
            }
            txtDrzava.Text = Drzava;
            txtPosta.Text = Posta;
            panel1.Visible = false;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            InsertPatheonExport ins = new InsertPatheonExport();
            string PaSifra = "";
            var query = "Select RTRim(PaNaziv) as Subject,RTrim(PaNaziv) as Name2,RTrim(PaUlicaHisnaSt) as Address,RTrim(PaPostnaSt) as Post,RTrim(DrNaziv) as Country,(SUBSTRING(paPostnaSt,0,CHARINDEX('-',PaPostnaSt,0)))as CountryPIB,RTrim(PaDMatSt) as Code," +
                     "RTrim(PaEMatSt1) as RegNo,'Papirno i elektronski' as WayOfTransaction,Buyer,WayOfSale,Currency,Supplier,'' as SuppSaleMet,'' as SuppCurr,'30' as Clerk,Referent as SuppClerk,PaSifra " +
                     "From Partnerji " +
                     "inner join Drzave on Partnerji.PaSifDrzave = Drzave.DrSifra " +
                     "Where Status = 0";


            List<object> combinedData = new List<object>();
            using (SqlConnection conn = new SqlConnection(connect))
            {
                SqlCommand cmd1 = new SqlCommand(query, conn);

                conn.Open();

                SqlDataReader dr1 = cmd1.ExecuteReader();
                DataTable table1 = new DataTable();
                table1.Load(dr1);

                foreach (DataRow row1 in table1.Rows)
                {
                    Dictionary<string, object> obj = new Dictionary<string, object>();
                    foreach (DataColumn column in table1.Columns)
                    {
                        PaSifra = row1["PaSifra"].ToString();

                        if (column.ColumnName != "PaSifra") // Exclude the field FaStFak from the JSON object
                        {
                            obj.Add(column.ColumnName, row1[column]);
                        }
                    }
                    combinedData.Add(obj);
                }

                conn.Close();
            }
            foreach (var item in combinedData)
            {
                string jsonOutput = JsonConvert.SerializeObject(item, Formatting.Indented);
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://192.168.129.2:3333/api/Subjekt/SubjektPost");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(jsonOutput);
                }
                string response = "";
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    response = result.ToString();
                    if (response.Contains("Error") == true || response.Contains("Greška") == true)
                    {
                        MessageBox.Show("Slanje nije uspelo");
                        ins.InsApiLog("Partner-" + PaSifra.ToString(), jsonOutput, response);
                        return;
                    }
                    else
                    {
                        using (SqlConnection conn = new SqlConnection(connect))
                        {
                            using (SqlCommand cmd = conn.CreateCommand())
                            {
                                cmd.CommandText = "UPDATE Partnerji SET Status = 1  WHERE PaSifra = " + PaSifra;
                                conn.Open();
                                cmd.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                    ins.InsApiLog("Partner-" + PaSifra.ToString(), jsonOutput, response);
                }
            }
        }
    }
}