using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Saobracaj.Pantheon_Export;
using Syncfusion.GridHelperClasses;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.Windows.Forms.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.IdentityModel.Metadata;
using System.Security.Cryptography.Xml;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Security.Cryptography;

namespace Saobracaj.Sifarnici
{
    public partial class frmPartnerji : Form
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
        int PomDrumskiPrevoz = 0;

        bool status = false;
        string Kor = Sifarnici.frmLogovanje.user.ToString();
        private string connect = Sifarnici.frmLogovanje.connectionString;
        string Dobavljac = "";
        string Kupac = "";
        string Obveznik = "";

        private void ChangeTextBox()
        {
            panelHeader.Visible = false;

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
                panelHeader.Visible = true;
                meniHeader.Visible = false;
                this.BackColor = Color.White;
                this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
                this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
                this.ControlBox = true;
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
                Office2010Colors.ApplyManagedColors(this, Color.White);
                this.Icon = Saobracaj.Properties.Resources.LegetIconPNG;
                // this.FormBorderStyle = FormBorderStyle.None;
                this.BackColor = Color.White;
                Office2010Colors.ApplyManagedColors(this, Color.White);

                foreach (Control control in this.Controls)
                {
                    if (control is System.Windows.Forms.Button buttons)
                    {
                        buttons.BackColor = Color.FromArgb(90, 199, 249); // Example: Change background color  -- Svetlo plava
                        buttons.ForeColor = Color.White;  //51; 51; 54  - Pozadina Bela
                        buttons.Font = new System.Drawing.Font("Helvetica", 9);  // Example: Change font
                        buttons.FlatStyle = FlatStyle.Flat;
                    }
                }


                foreach (Control control in this.Controls)
                {

                    if (control is System.Windows.Forms.TextBox textBox)
                    {

                        textBox.BackColor = Color.White;// Example: Change background color
                        textBox.ForeColor = Color.FromArgb(51, 51, 54); //Boja slova u kvadratu
                        textBox.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                        // Example: Change font
                    }


                    if (control is System.Windows.Forms.Label label)
                    {
                        // Change properties here
                        label.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        label.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);  // Example: Change font

                        // textBox.ReadOnly = true;              // Example: Make text boxes read-only
                    }

                    if (control is DateTimePicker dtp)
                    {
                        dtp.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        dtp.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.CheckBox chk)
                    {
                        chk.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        chk.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.ListBox lb)
                    {
                        lb.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        lb.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.ComboBox cb)
                    {
                        cb.ForeColor = Color.FromArgb(51, 51, 54);
                        cb.BackColor = Color.White;// Example: Change background color
                        cb.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.NumericUpDown nu)
                    {
                        nu.ForeColor = Color.FromArgb(51, 51, 54);
                        nu.BackColor = Color.White;// Example: Change background color
                        nu.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }
                }
            }
            else
            {
                panelHeader.Visible = false;
                meniHeader.Visible = true;
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }

        private void PodesiDatagridView(DataGridView dgv)
        {

            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(90, 199, 249); // Selektovana boja
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.BackgroundColor = Color.White;

            dgv.DefaultCellStyle.Font = new Font("Helvetica", 12F, GraphicsUnit.Pixel);
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(51, 51, 54);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 248);
            dgv.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 248);


            //Header
            dgv.EnableHeadersVisualStyles = false;
            //   header.Style.Font = new Font("Arial", 12F, FontStyle.Bold);
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 54);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgv.ColumnHeadersHeight = 30;
        }

        public frmPartnerji()
        {
            InitializeComponent();
            ChangeTextBox();

        }
        private void frmPartnerji_Load(object sender, EventArgs e)
        {
         //   RefreshDataGrid();
           
            ChecBoxVisible(this.Controls);
            panel1.Visible = false;
            panel4.Visible = false;
            panel5.Visible= false;

            SqlConnection conn = new SqlConnection(connect);
            var valuta = "Select VaSifra,VaNaziv From Valute";
            var valutaDa = new SqlDataAdapter(valuta, conn);
            var valutaDS = new DataSet();
            valutaDa.Fill(valutaDS);
            cboValuta.DataSource = valutaDS.Tables[0];
            cboValuta.DisplayMember = "VaNaziv";
            cboValuta.ValueMember = "VaSifra";
            RefreshGridControl();

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
                    cboValuta.SelectedValue= "RSD";
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

                    //Nije vidljivo za Leget
                    toolStripButton2.Visible = false; //Export PAnteon
                    label21.Visible = false; //VALUTA
                    label22.Visible = false;
                    numFREC.Visible = false; // FREC
                    chkDrumskiPrevoz.Visible = true;
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
                    chkDrumskiPrevoz.Visible = false;
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
                    chkDrumskiPrevoz.Visible = false;
                    break;
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in c.Rows)
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
                        numFREC.Value = Convert.ToInt32(row.Cells["FREC"].Value);
                        if (row.Cells["DrumskiPrevoz"].Value.ToString() == "1") { chkDrumskiPrevoz.Checked = true; } else { chkDrumskiPrevoz.Checked = false; }
                        txtERPID.Text = Convert.ToString(row.Cells["ERPID"].Value);
                        RefreshDataGrid2(txtSifra.Text);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void RefreshGridControl()
        {
            var select = " Select PaSifra, Rtrim(PaNaziv) as Naziv, PaUlicaHisnaSt as Ulica , PaKraj as Grad, PaDelDrzave as Drzava, PaPostnaSt as Posta, " +
            " PaSifDrzave as DrzavaID, PaTelefon1 as Telefon, PaZiroRac as TekRacun,  PaOpomba as Napomena, PaDMatSt as Maticni, PaEMail as EMaill, PaEMatSt1 as PIB, " +
            " Rtrim(UIC) as UIC, (CASE WHEN Prevoznik > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END)  as Prevoznik, " +
            " (CASE WHEN Posiljalac > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END)  as Posiljalac, " +
            " (CASE WHEN Primalac > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END)  as Primalac,  " +
            " Brodar  , Vlasnik , Spediter , Platilac , Organizator, NalogodavacCH, UvoznikCH, UICDrzava,TR2, Faks, PomIzvoznik, " +
            " Logisticar,Kamioner,AgentBrodara,Buyer,Supplier,WayOfSale,Currency,  DrumskiPrevoz " +
            " from Partnerji order by PaSifra desc ";


            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            gridGroupingControl1.DataSource = ds.Tables[0];
            gridGroupingControl1.ShowGroupDropArea = true;
            this.gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;

            //foreach (var column in gridGroupingControl1.TableDescriptor.Columns)
            //{
            //    MessageBox.Show(column.Name);
            //}

            foreach (GridColumnDescriptor column in this.gridGroupingControl1.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }

            GridDynamicFilter dynamicFilter = new GridDynamicFilter();
            dynamicFilter.WireGrid(this.gridGroupingControl1);



        }

        private void RefreshDataGrid()
        {
            var select = " Select PaSifra, Rtrim(PaNaziv) as PaNaziv, PaUlicaHisnaSt , PaKraj, PaDelDrzave, PaPostnaSt, PaSifDrzave, PaTelefon1, PaZiroRac, " +
                " PaOpomba, PaDMatSt, PaEMail, PaEMatSt1, Rtrim(UIC) as UIC, (CASE WHEN Prevoznik > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END)  as Prevoznik, (CASE WHEN Posiljalac > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END)  as Posiljalac, (CASE WHEN Primalac > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END)  as Primalac ,  Brodar " +
            " , Vlasnik , Spediter , Platilac , Organizator, NalogodavacCH, UvoznikCH, UICDrzava,TR2, Faks, PomIzvoznik,Logisticar,Kamioner,AgentBrodara,Buyer,Supplier,WayOfSale,Currency, FREC, DrumskiPrevoz, ERPID from Partnerji order by PaSifra desc";
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


            this.c.ReadOnly = true;
            this.c.DataSource = ds.Tables[0];

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                PodesiDatagridView(this.c);
            }
            else
            {
                this.c.BorderStyle = BorderStyle.None;
                this.c.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                this.c.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                this.c.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                this.c.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                this.c.BackgroundColor = Color.White;

                this.c.EnableHeadersVisualStyles = false;
                this.c.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                this.c.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                this.c.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            }

            DataGridViewColumn column = this.c.Columns[0];
            this.c.Columns[0].HeaderText = "Šifra";
            this.c.Columns[0].Width = 50;

            DataGridViewColumn column2 = this.c.Columns[1];
            this.c.Columns[1].HeaderText = "Naziv";
            this.c.Columns[1].Width = 250;

            DataGridViewColumn column3 = this.c.Columns[2];
            this.c.Columns[2].HeaderText = "Ulica";
            this.c.Columns[2].Width = 100;

            DataGridViewColumn column4 = this.c.Columns[3];
            this.c.Columns[3].HeaderText = "Mesto";
            this.c.Columns[3].Width = 100;

            DataGridViewColumn column5 = this.c.Columns[4];
            this.c.Columns[4].HeaderText = "Oblast";
            this.c.Columns[4].Width = 70;

            DataGridViewColumn column6 = this.c.Columns[5];
            this.c.Columns[5].HeaderText = "Pošta";
            this.c.Columns[5].Width = 60;

            DataGridViewColumn column7 = this.c.Columns[6];
            this.c.Columns[6].HeaderText = "Država";
            this.c.Columns[6].Width = 60;

            DataGridViewColumn column8 = this.c.Columns[7];
            this.c.Columns[7].HeaderText = "Telefon";
            this.c.Columns[7].Width = 100;

            DataGridViewColumn column9 = this.c.Columns[8];
            this.c.Columns[8].HeaderText = "Tekući račun";
            this.c.Columns[8].Width = 100;

            DataGridViewColumn column10 = this.c.Columns[9];
            this.c.Columns[9].HeaderText = "Napomena";
            this.c.Columns[9].Width = 250;

            DataGridViewColumn column11 = this.c.Columns[10];
            this.c.Columns[10].HeaderText = "PIB";
            this.c.Columns[10].Width = 70;

            DataGridViewColumn column12 = this.c.Columns[11];
            this.c.Columns[11].HeaderText = "E mail";
            this.c.Columns[11].Width = 80;


            DataGridViewColumn column13 = this.c.Columns[12];
            this.c.Columns[12].HeaderText = "Matični broj";
            this.c.Columns[12].Width = 70;

            DataGridViewColumn column14 = this.c.Columns[13];
            this.c.Columns[13].HeaderText = "UIC";
            this.c.Columns[13].Width = 50;

            DataGridViewColumn column15 = this.c.Columns[14];
            this.c.Columns[14].HeaderText = "Prevoznik";
            this.c.Columns[14].Width = 40;

            DataGridViewColumn column16 = this.c.Columns[15];
            this.c.Columns[15].HeaderText = "Pošiljalac";
            this.c.Columns[15].Width = 40;

            DataGridViewColumn column17 = this.c.Columns[16];
            this.c.Columns[16].HeaderText = "Primalac";
            this.c.Columns[16].Width = 40;

            DataGridViewColumn column18 = this.c.Columns[17];
            this.c.Columns[17].HeaderText = "UIC DRZAVA";
            this.c.Columns[17].Width = 60;

            DataGridViewColumn column19 = this.c.Columns[18];
            this.c.Columns[18].HeaderText = "TR2";
            this.c.Columns[18].Width = 60;


            DataGridViewColumn column20 = this.c.Columns[19];
            this.c.Columns[19].HeaderText = "FAKS";
            this.c.Columns[19].Width = 60;

            DataGridViewColumn column21 = this.c.Columns[20];
            this.c.Columns[20].HeaderText = "Izvoznik";
            this.c.Columns[20].Width = 60;

            this.c.Columns["Currency"].Visible = false;

            if (Sifarnici.frmLogovanje.Firma == "TA")
            {
                this.c.Columns["Logisticar"].Visible = false;
                this.c.Columns["Kamioner"].Visible = false;
                this.c.Columns["AgentBrodara"].Visible = false;
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

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                PodesiDatagridView(dataGridView2);
            }
            else
            {
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
            }

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


        private void RefreshDataGrid1(string SifraPartnera)
        {
            var select = " SELECT [PartnerID],[SifraApp],[Firma]   FROM [TESTIRANJE].[dbo].[PartnerjiFirma] WHERE PartnerID = "  + SifraPartnera;
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

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                PodesiDatagridView(dataGridView1);
            }
            else
            {
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
            }

            DataGridViewColumn column = dataGridView1.Columns[1];
            dataGridView1.Columns[0].HeaderText = "Šifra";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Šifra APP";
            dataGridView1.Columns[1].Width = 150;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Firma";
            dataGridView1.Columns[2].Width = 150;
  
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

            if (chkDrumskiPrevoz.Checked)
            {
                PomDrumskiPrevoz = 1;
            }
            else
            {
                PomDrumskiPrevoz = 0;
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
            if (cboValuta.SelectedValue == null)
            {
                cboValuta.SelectedValue = "RSD";
            }
            if (txtSifra.Text == "")
            {
                status = true;
            }


            if (status == true)
            {
                //  txtNaziv.Text,  txtUlica.Text,  txtMesto.Text,  txtOblast.Text, txtPosta.Text ,txtDrzava.Text, txtTelefon.Text, txtTR.Text ,  txtNapomena.Text,txtMaticniBroj.Text,  txtEmail.Text,  txtPIB.Text
                InsertPartnerji ins = new InsertPartnerji();
                ins.InsPartneri(txtNaziv.Text, txtUlica.Text, txtMesto.Text, txtPosta.Text, txtDrzava.Text, txtTelefon.Text, txtTR.Text, txtNapomena.Text, txtPIB.Text, txtEmail.Text, txtMaticniBroj.Text, txtUIC.Text, chkPrevoznik.Checked, chkPosiljalac.Checked, chkPrimalac.Checked, PomBrodar, PomVlasnik, PomSpediter, PomPlatilac, PomOrganizator, PomNalogodavac, PomUvoznik, txtMUAdresa.Text, txtMUKontakt.Text, txtUICDrzava.Text, txtTR2.Text, txtFaks.Text, PomIzvoznik, PomLogisticar, PomKamioner, PomAgentBrodara, Kupac, Obveznik, cboValuta.SelectedValue.ToString(), Dobavljac, referent, Convert.ToInt32(numFREC.Value),PomDrumskiPrevoz , txtERPID.Text);
            }
            else
            {
                
                InsertPartnerji upd = new InsertPartnerji();
                upd.UpdPartneri(Convert.ToInt32(txtSifra.Text), txtNaziv.Text, txtUlica.Text, txtMesto.Text, txtOblast.Text, txtPosta.Text, txtDrzava.Text, txtTelefon.Text, txtTR.Text, txtNapomena.Text, txtPIB.Text, txtEmail.Text, txtMaticniBroj.Text, txtUIC.Text, chkPrevoznik.Checked, chkPosiljalac.Checked, chkPrimalac.Checked, PomBrodar, PomVlasnik, PomSpediter, PomPlatilac, PomOrganizator, PomNalogodavac, PomUvoznik, txtMUAdresa.Text, txtMUKontakt.Text, txtUICDrzava.Text, txtTR2.Text, txtFaks.Text, PomIzvoznik, PomLogisticar, PomKamioner, PomAgentBrodara, Kupac, Obveznik, Dobavljac, cboValuta.SelectedValue.ToString(), Convert.ToInt32(numFREC.Value),PomDrumskiPrevoz, txtERPID.Text);
            }
            status = false;
            RefreshDataGrid();
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertPartnerji del = new InsertPartnerji();
            del.DelPartneri(Convert.ToInt32(txtSifra.Text));
            status = false;
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
        private void FillDrzavePoste()
        {
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
        private void btnDrzava_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;

            FillDrzavePoste();

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                PodesiDatagridView(dataGridView3);
            }

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

        private void button2_Click(object sender, EventArgs e)
        {
            InsertPartnerji ins = new InsertPartnerji();
            if (txtUnesiDrzavu.Text != "")
            {
                ins.InsDrzave(Kor, txtUnesiDrzavu.Text.ToString().TrimEnd(), txtUnesiOznaku.ToString().TrimEnd().ToUpper(), txtUnesiValutu.Text.ToString().TrimEnd().ToUpper());
                FillDrzavePoste();
                panel4.Visible = false;
            }
            
        }
        private void button5_Click(object sender, EventArgs e)
        {
            InsertPartnerji ins = new InsertPartnerji();
            if (textBox3.Text != "")
            {
                ins.InsPoste(Kor, textBox1.Text.ToString().TrimEnd(), textBox3.Text.ToString().TrimEnd(), textBox2.Text.ToString().TrimEnd());
                FillDrzavePoste();
                panel5.Visible = false;
            }
        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel5.Visible = true;
        }

        private void VratiPodatkeSelect(int ID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT [PaSifra]      ,[PaNaziv]      ,[PaUlicaHisnaSt]      ,[PaKraj] " +
   "   , [PaDelDrzave], [PaPostnaSt], [PaSifDrzave], [PaTelefon1] " +
   "     ,  [PaZiroRac] " +
   "     , [PaOpomba], [PaDMatSt], [PaEMail], [PaEMatSt1] " +
   "     , [UIC], [Prevoznik], [Posiljalac], [Primalac] " +
   "     , [Brodar], [Vlasnik], [Spediter], [Platilac] " +
   "     , [Organizator], [NalogodavacCH], [UvoznikCH]," +
   "       [UICDrzava], [TR2], [Faks], " +
   "      [PomIzvoznik] " +
   "     , [Logisticar], [Kamioner], [AgentBrodara], [Buyer] " +
   "     , [WayOfSale], [Currency], [Supplier], [SuppSaleMet] " +
   "     , [SuppCurr], [Clerk], [SuppClerk], [Status] " +
   "     , [Referent], [FREC], [DrumskiPrevoz], [ERPID],[MUAdresa] " +
   "     , [MUKontakt], [PaTelefon2], [PaFaks], [PaSifVrst][PaEDavStDDV], [PaDDV], [PaRegija] " +
  "   FROM [dbo].[Partnerji] where PaSifra =" + ID, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtNaziv.Text = dr["PaNaziv"].ToString();
                txtUlica.Text = dr["PaUlicaHisnaSt"].ToString();
                txtMesto.Text = dr["PaKraj"].ToString();
                txtOblast.Text = dr["PaDelDrzave"].ToString();
                txtPosta.Text = dr["PaPostnaSt"].ToString();
                txtDrzava.Text = dr["PaSifDrzave"].ToString();
                txtTelefon.Text = dr["PaTelefon1"].ToString();
                txtTR.Text = dr["PaZiroRac"].ToString();
                txtNapomena.Text = dr["PaOpomba"].ToString();
                txtMaticniBroj.Text = dr["PaDMatSt"].ToString();
                txtEmail.Text = dr["PaEMail"].ToString();
                txtPIB.Text = dr["PaEMatSt1"].ToString();
                txtUIC.Text = dr["UIC"].ToString();
                txtUICDrzava.Text = dr["UICDrzava"].ToString();
                txtTR2.Text = dr["TR2"].ToString();
                txtFaks.Text = dr["Faks"].ToString();
                if (dr["PomIzvoznik"].ToString() == "1")
                { chkIzvoznik.Checked = true; }
                else
                { chkIzvoznik.Checked = false; }

                if (dr["Logisticar"].ToString() == "1")
                { chkLogisitcar.Checked = true; }
                else
                { chkLogisitcar.Checked = false; }

                if (dr["Kamioner"].ToString() == "1")
                { chkKamioner.Checked = true; }
                else
                { chkKamioner.Checked = false; }

                if (dr["AgentBrodara"].ToString() == "1")
                { chkAgentBrodara.Checked = true; }
                else
                { chkAgentBrodara.Checked = false; }
                /*
                if (dr["Supplier"].ToString() == "1")
                { cbDobavljac.Checked = true; }
                else
                { cbDobavljac.Checked = false; }

                if (dr["Buyer"].ToString() == "1")
                { cboKupac.Checked = true; }
                else
                { cboKupac.Checked = false; }

                if (dr["WayOfSale"].ToString() == "Z")
                { cbObveznik.Checked = true; }
                else
                { cbObveznik.Checked = false; }
                */
                cboValuta.SelectedValue = dr["Currency"].ToString();




               
                numFREC.Value = Convert.ToInt32(dr["FREC"].ToString());
                if (dr["DrumskiPrevoz"].ToString() == "1")
                { chkDrumskiPrevoz.Checked = true; }
                else
                { chkDrumskiPrevoz.Checked = false; }

              
                txtERPID.Text = dr["ERPID"].ToString();

                if (dr["UvoznikCH"].ToString() == "1")
                { chkUvoznik.Checked = true; }
                else
                { chkUvoznik.Checked = false; }

                if (dr["NalogodavacCH"].ToString() == "1")
                { chkNalogodavac.Checked = true; }
                else
                { chkNalogodavac.Checked = false; }

                if (dr["Organizator"].ToString() == "1")
                { chkOrganizator.Checked = true; }
                else
                { chkOrganizator.Checked = false; }

                if (dr["Platilac"].ToString() == "1")
                { chkPlatilac.Checked = true; }
                else
                { chkPlatilac.Checked = false; }

                if (dr["Spediter"].ToString() == "1")
                { chkSpediter.Checked = true; }
                else
                { chkSpediter.Checked = false; }

                if (dr["Vlasnik"].ToString() == "1")
                { chkVlasnik.Checked = true; }
                else
                {chkVlasnik.Checked = false; }
                    
               

                if (dr["Brodar"].ToString() == "1")
                {
                    chkBrodar.Checked = true;
                }
                else
                {
                    chkBrodar.Checked = false;
                }
                if (dr["Primalac"].ToString() == "1")
                {
                    chkPrimalac.Checked = true;
                }
                else
                {
                    chkPrimalac.Checked = false;
                }

                if (dr["Prevoznik"].ToString() == "1")
                {
                    chkPrevoznik.Checked = true;
                }
                else
                {
                    chkPrevoznik.Checked = false;
                }

                if (dr["Posiljalac"].ToString() == "1")
                {
                    chkPosiljalac.Checked = true;
                }
                else
                {
                    chkPosiljalac.Checked = false;
                }
            }



            con.Close();


        }

        private void gridGroupingControl1_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            try
            {
                if (gridGroupingControl1.Table.CurrentRecord != null)
                {
                   
                    txtSifra.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("PaSifra").ToString();
                    VratiPodatkeSelect(Convert.ToInt32(txtSifra.Text));

                        RefreshDataGrid2(txtSifra.Text);
                        RefreshDataGrid1(txtSifra.Text);
                }
                
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
         //   Sifarnici.PartnerjiPregled p = new PartnerjiPregled();
           // p.Show();
        }

        private void btnFirme_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtSifra.Text))
            {
                Sifarnici.frmPartnerjiFirme pko = new Sifarnici.frmPartnerjiFirme(Convert.ToInt32(txtSifra.Text));
                pko.Show();
            }
        }
    }
}