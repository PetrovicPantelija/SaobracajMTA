using Saobracaj.Dokumenta;
using Syncfusion.Windows.Forms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using static Syncfusion.WinForms.Core.NativeScroll;

namespace Saobracaj.Izvoz
{
    public partial class frmIzvozKonacna : Form
    {
        float firstWidth;
        float firstHeight;
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        int NHMObrni = 0;
        string KorisnikTekuci = Sifarnici.frmLogovanje.user.ToString();

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
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                Office2010Colors.ApplyManagedColors(this, Color.White);
                this.Icon = Saobracaj.Properties.Resources.LegetIconPNG;



                foreach (Control control in tabSplitterContainer1.Controls)
                {

                }


                foreach (Control control in tabSplitterPage1.Controls)
                {
                    if (control is System.Windows.Forms.Button buttons)
                    {
                        buttons.BackColor = Color.FromArgb(90, 199, 249); // Example: Change background color  -- Svetlo plava
                        buttons.ForeColor = Color.White;  //51; 51; 54  - Pozadina Bela
                        buttons.Font = new System.Drawing.Font("Helvetica", 9);  // Example: Change font
                        buttons.FlatStyle = FlatStyle.Flat;
                    }

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
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
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


        public frmIzvozKonacna()
        {
            InitializeComponent();
            txtVozilo.Text = "";
            txtVozac.Text = "";
            txtVozilo.Enabled = false;
            txtVozac.Enabled = false;
            ChangeTextBox();
        }

        public frmIzvozKonacna(int ID)
        {
            InitializeComponent();

            txtNadredjeni.Text = ID.ToString();
            //  FillDG();

            FillCombo();
            VratiPodatke(ID);
            FillZaglavlje(ID);
            ChangeTextBox();
            //FillDG2();
            //FillDG3();

            //  FillDG4();

        }

        private void FillZaglavlje(int Sifra)
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select ID, idVoza,Napomena from IzvozKonacnaZaglavlje where ID=" + Sifra, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtNadredjeni.Text = dr["ID"].ToString();
                cboVoz.SelectedValue = Convert.ToInt32(dr["idVoza"].ToString());
                txtNapomenaZaglavlje.Text = dr["Napomena"].ToString();

            }
            con.Close();
            VratiVoz();
        }

        private void VratiVoz()
        {
            var select = "SELECT [ID] ,[BrVoza],[Relacija], " +
                " CONVERT(varchar,VremePolaska,104)      + ' '      + SUBSTRING(CONVERT(varchar,VremePolaska,108),1,5) as VremePolaska, " +
                " CONVERT(varchar,[VremeDolaska],104)      + ' '      + SUBSTRING(CONVERT(varchar,[VremeDolaska],108),1,5)  as VremeDolaska, " +
                " [MaksimalnaBruto],[MaksimalnaDuzina] " +
                " ,[MaksimalanBrojKola] " +
                " ,[Napomena]" +
                " ,[PostNaTerminalD] as Postavka,[KontrolniPregledD] as Kontrolni ,[VremePrimopredajeD] as Primopredaja,[VremeIstovaraD] as Istovar" +
                " ,[Datum] ,[Korisnik],[Dolazeci] " +
                " FROM [dbo].[Voz] where ID =  " + Convert.ToInt32(cboVoz.SelectedValue);
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView4.ReadOnly = true;
            dataGridView4.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView4);


        }

        private void FillDG2()
        {

            var select = " SELECT     IzvozKonacnaNHM.ID, NHM.Broj, IzvozKonacnaNHM.IDNHM, NHM.Naziv FROM NHM INNER JOIN " +
                      " IzvozKonacnaNHM ON NHM.ID = IzvozKonacnaNHM.IDNHM where IzvozKonacnanhm.idnadredjena = " + Convert.ToInt32(txtID.Text) + " order by IzvozKonacnanhm.ID desc ";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];


            PodesiDatagridView(dataGridView2);

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "ID";
            dataGridView2.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView2.Columns[1];
            dataGridView2.Columns[1].HeaderText = "Broj";
            dataGridView2.Columns[1].Width = 80;

            DataGridViewColumn column3 = dataGridView2.Columns[2];
            dataGridView2.Columns[2].HeaderText = "ID";
            dataGridView2.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView2.Columns[3];
            dataGridView2.Columns[3].HeaderText = "NHM";
            dataGridView2.Columns[3].Width = 150;



        }

        private void FillDG3()
        {
            var select = "select IzvozKonacnaVrstaRobeHS.ID, IDVrstaRobeHS, VrstaRobeHS.HSKod as Kod,VrstaRobeHS.Naziv as HS from IzvozKonacnaVrstaRobeHS " +
            " inner join  VrstaRobeHS on VrstaRobeHS.ID = IzvozKonacnaVrstaRobeHS.IDVrstaRobeHS where idnadredjena = " + Convert.ToInt32(txtID.Text) + " order by IzvozKonacnaVrstaRobeHS.ID desc ";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView3.ReadOnly = true;
            dataGridView3.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView3);

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView3.Columns[0];
            dataGridView3.Columns[0].HeaderText = "ID";
            dataGridView3.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView3.Columns[1];
            dataGridView3.Columns[1].HeaderText = "HSID";
            dataGridView3.Columns[1].Width = 20;

            DataGridViewColumn column3 = dataGridView3.Columns[2];
            dataGridView3.Columns[2].HeaderText = "HSKOD";
            dataGridView3.Columns[2].Width = 80;

            DataGridViewColumn column4 = dataGridView3.Columns[3];
            dataGridView3.Columns[3].HeaderText = "HS";
            dataGridView3.Columns[3].Width = 180;

        }


        private void VratiPodatke(int ID)
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT [ID]      ,[BrojVagona] " +
             " ,[BrojKontejnera]      ,[VrstaKontejnera]      ,[BrodskaPlomba], [OstalePlombe] , [BookingBrodara] " +
             " ,[Brodar]      ,[CutOffPort]      ,[NetoRobe]      ,[BrutoRobe] " +
             " ,[BrutoRobeO]      ,[BrojKoleta]      ,[BrojKoletaO]      ,[CBM] " +
             " ,[CBMO]      ,[VrednostRobeFaktura]      ,[Valuta]      ,[KrajnaDestinacija] " +
             " ,[Postupanje]      ,[MestoPreuzimanja]      ,[Cirada]      ,[PlaniraniDatumUtovara] " +
             " ,[MesoUtovara]      ,[KontaktOsoba]      ,[MestoCarinjenja]      ,[Spedicija] " +
             " ,[AdresaSlanjaStatusa]      ,[NaslovSlanjaStatusa]      ,[EtaLeget]      ,[NapomenaReexport] " +
             " ,[Inspekcija]      ,[AutoDana]      ,[NajavaVozila]      ,[NacinPakovanja] " +
             " ,[NacinPretovara]      ,[DodatneNapomeneDrumski]      ,[Vaganje]      ,[VGMTezina] " +
             " ,[Tara]      ,[VGMBrod]      ,[Izvoznik]      ,[Klijent1] " +
             " ,[Napomena1REf]      ,[DobijenNalogKlijent1]      ,[Klijent2]      ,[Napomena2REf] " +
             " ,[Klijent3]      ,[Napomena3REf]      ,[SpediterRijeka] , ADR,Scenario   " +
             "  FROM [IzvozKonacna] where ID=" + ID, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtID.Text = dr["ID"].ToString();
                txtBrojVagona.Text = dr["BrojVagona"].ToString();
                txtBrKont.Text = dr["BrojKontejnera"].ToString();
                txtTipKont.SelectedValue = Convert.ToInt32(dr["VrstaKontejnera"].ToString());
                txtBrodskaPlomba.Text = dr["BrodskaPlomba"].ToString();
                txtOstalePlombe.Text = dr["OstalePlombe"].ToString();
                txtBokingBrodara.Text = dr["BookingBrodara"].ToString();
                cboBrodar.SelectedValue = Convert.ToInt32(dr["Brodar"].ToString());
                dtpCutOffPort.Value = Convert.ToDateTime(dr["CutOffPort"].ToString());
                txtNetoR.Value = Convert.ToDecimal(dr["NetoRobe"].ToString());
                txtBrutoR.Value = Convert.ToDecimal(dr["BrutoRobe"].ToString());
                txtBrutoO.Value = Convert.ToDecimal(dr["BrutoRobeO"].ToString());
                txtKoleta.Value = Convert.ToInt32(dr["BrojKoleta"].ToString());
                txtKoletaO.Value = Convert.ToInt32(dr["BrojKoletaO"].ToString());
                txtCBM.Value = Convert.ToDecimal(dr["CBM"].ToString());
                txtCBMO.Value = Convert.ToDecimal(dr["CBMO"].ToString());
                txtADR.SelectedValue = Convert.ToInt32(dr["ADR"].ToString());
                txtVrednostRobeFaktura.Value = Convert.ToDecimal(dr["VrednostRobeFaktura"].ToString());
                txtValuta.Text = dr["Valuta"].ToString();
                cboKrajnjaDestinacija.SelectedValue = Convert.ToInt32(dr["KrajnaDestinacija"].ToString());
                cboPostupanjeSaRobom.SelectedValue = Convert.ToInt32(dr["Postupanje"].ToString());
                cboPPCNT.SelectedValue = Convert.ToInt32(dr["MestoPreuzimanja"].ToString());
                cboPPCNT2.SelectedValue = Convert.ToInt32(dr["MestoPreuzimanja2"].ToString());
                cboPPCNT3.SelectedValue = Convert.ToInt32(dr["MestoPreuzimanja3"].ToString());

                if (dr["Cirada"].ToString() == "1")
                { chkCirada.Text = "CIRADA"; }
                else
                { chkCirada.Text = "PLATFORMA"; }

                dtpPlanUtovara.Value = Convert.ToDateTime(dr["PlaniraniDatumUtovara"].ToString());
                cboMestoUtovara.SelectedValue = Convert.ToInt32(dr["MestoUtovara"].ToString());
                txtKontaktOsoba.Text = dr["KontaktOsoba"].ToString();
                cboCarina.SelectedValue = Convert.ToInt32(dr["MestoCarinjenja"].ToString());
                cboSpedicija.SelectedValue = Convert.ToInt32(dr["Spedicija"].ToString());
                // txtKontaktSpeditera
                cboAdresaStatusVozila.Text = dr["AdresaSlanjaStatusa"].ToString();
                cboNaslovStatusaVozila.Text = dr["NaslovSlanjaStatusa"].ToString();
                dtpEtaLeget.Value = Convert.ToDateTime(dr["EtaLeget"].ToString());
                cboReexport.SelectedValue = Convert.ToInt32(dr["NapomenaReexport"].ToString());
                cboInspekciskiTretman.SelectedValue = Convert.ToInt32(dr["Inspekcija"].ToString());
                txtAutoDana.Value = Convert.ToDecimal(dr["AutoDana"].ToString());


                if (dr["NajavaVozila"].ToString() == "1")
                { chkNajavaVozila.Checked = true; }
                else
                { chkNajavaVozila.Checked = false; }
                cbNacinPakovanja.SelectedValue = Convert.ToInt32(dr["NacinPakovanja"].ToString());
                if (dr["NacinPretovara"].ToString() == "1")
                {
                    chkNacinPretovara.Checked = true;
                    chkIndirektno.Checked = false;
                }
                else
                {
                    chkNacinPretovara.Checked = false;
                    chkIndirektno.Checked = true;
                }

                txtDodatneNapomene.Text = dr["DodatneNapomeneDrumski"].ToString();

                if (dr["Vaganje"].ToString() == "1")
                { chkVaganje.Checked = true; }
                else
                { chkVaganje.Checked = false; }

                txtOdvaganaTezina.Value = Convert.ToDecimal(dr["VGMTezina"].ToString());
                txtTaraKontejnera.Value = Convert.ToDecimal(dr["Tara"].ToString());
                txVGMBrodBruto.Value = Convert.ToDecimal(dr["VGMBrod"].ToString());
                cboIzvoznik.SelectedValue = Convert.ToInt32(dr["Izvoznik"].ToString());
                cboNalogodavac1.SelectedValue = Convert.ToInt32(dr["Klijent1"].ToString());
                txtRef1.Text = dr["Napomena1REf"].ToString();
                cboNalogodavac2.SelectedValue = Convert.ToInt32(dr["Klijent2"].ToString());
                txtRef2.Text = dr["Napomena2REf"].ToString();
                cboNalogodavac3.SelectedValue = Convert.ToInt32(dr["Klijent3"].ToString());
                txtRef3.Text = dr["Napomena3REf"].ToString();
                cboSpediterURijeci.SelectedValue = Convert.ToInt32(dr["SpediterRijeka"].ToString());

                cboScenario.SelectedValue = Convert.ToInt32(dr["Scenario"].ToString());









            }
            con.Close();
        }

        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connection);

            var tipkontejnera = "Select ID, SkNaziv From TipKontenjera order by SkNaziv";
            var tkAD = new SqlDataAdapter(tipkontejnera, conn);
            var tkDS = new DataSet();
            tkAD.Fill(tkDS);
            txtTipKont.DataSource = tkDS.Tables[0];
            txtTipKont.DisplayMember = "SkNaziv";
            txtTipKont.ValueMember = "ID";

            var bro = "Select PaSifra,PaNaziv From Partnerji where Brodar = 1 order by PaNaziv";
            var broAD = new SqlDataAdapter(bro, conn);
            var broDS = new DataSet();
            broAD.Fill(broDS);
            cboBrodar.DataSource = broDS.Tables[0];
            cboBrodar.DisplayMember = "PaNaziv";
            cboBrodar.ValueMember = "PaSifra";


            var adr = "Select ID, (Naziv + ' - ' + UNKod) as Naziv From VrstaRobeADR order by (UNKod + ' ' + Naziv)";
            var adrSAD = new SqlDataAdapter(adr, conn);
            var adrSDS = new DataSet();
            adrSAD.Fill(adrSDS);
            txtADR.DataSource = adrSDS.Tables[0];
            txtADR.DisplayMember = "Naziv";
            txtADR.ValueMember = "ID";

            //Krajnja destinacija - Kad napravim sifarnik
            var kd = "Select ID, Naziv as Naziv From KrajnjaDestinacija order by ID";
            var kdSAD = new SqlDataAdapter(kd, conn);
            var kdSDS = new DataSet();
            kdSAD.Fill(kdSDS);
            cboKrajnjaDestinacija.DataSource = kdSDS.Tables[0];
            cboKrajnjaDestinacija.DisplayMember = "Naziv";
            cboKrajnjaDestinacija.ValueMember = "ID";

            var dir3 = "Select ID,Naziv from VrstePostupakaUvoz order by Naziv";
            var dirAD3 = new SqlDataAdapter(dir3, conn);
            var dirDS3 = new DataSet();
            dirAD3.Fill(dirDS3);
            cboPostupanjeSaRobom.DataSource = dirDS3.Tables[0];
            cboPostupanjeSaRobom.DisplayMember = "Naziv";
            cboPostupanjeSaRobom.ValueMember = "ID";

            var rl = "Select ID, (Naziv + ' - ' + Oznaka) as Naziv From KontejnerskiTerminali order by (Naziv + ' ' + Oznaka)";
            var rlSAD = new SqlDataAdapter(rl, conn);
            var rlSDS = new DataSet();
            rlSAD.Fill(rlSDS);
            cboPPCNT.DataSource = rlSDS.Tables[0];
            cboPPCNT.DisplayMember = "Naziv";
            cboPPCNT.ValueMember = "ID";


            var rl2 = "Select ID, (Naziv + ' - ' + Oznaka) as Naziv From KontejnerskiTerminali order by (Naziv + ' ' + Oznaka)";
            var rlSAD2 = new SqlDataAdapter(rl2, conn);
            var rlSDS2 = new DataSet();
            rlSAD2.Fill(rlSDS2);
            cboPPCNT2.DataSource = rlSDS2.Tables[0];
            cboPPCNT2.DisplayMember = "Naziv";
            cboPPCNT2.ValueMember = "ID";


            var rl3 = "Select ID, (Naziv + ' - ' + Oznaka) as Naziv From KontejnerskiTerminali order by (Naziv + ' ' + Oznaka)";
            var rlSAD3 = new SqlDataAdapter(rl3, conn);
            var rlSDS3 = new DataSet();
            rlSAD3.Fill(rlSDS3);
            cboPPCNT3.DataSource = rlSDS3.Tables[0];
            cboPPCNT3.DisplayMember = "Naziv";
            cboPPCNT3.ValueMember = "ID";


            //Mesta utovara u Srbiji - Dodati
            var dir = "Select ID,Naziv from MestaUtovara order by Naziv";
            var dirAD = new SqlDataAdapter(dir, conn);
            var dirDS = new DataSet();
            dirAD.Fill(dirDS);
            cboMestoUtovara.DataSource = dirDS.Tables[0];
            cboMestoUtovara.DisplayMember = "Naziv";
            cboMestoUtovara.ValueMember = "ID";

            var car = "Select ID, Naziv From Carinarnice order by Naziv";
            var carAD = new SqlDataAdapter(car, conn);
            var carDS = new DataSet();
            carAD.Fill(carDS);
            cboCarina.DataSource = carDS.Tables[0];
            cboCarina.DisplayMember = "Naziv";
            cboCarina.ValueMember = "ID";

            var partner3 = "Select PaSifra,PaNaziv From Partnerji where Spediter =1 order by PaNaziv";
            var partAD3 = new SqlDataAdapter(partner3, conn);
            var partDS3 = new DataSet();
            partAD3.Fill(partDS3);
            cboSpedicija.DataSource = partDS3.Tables[0];
            cboSpedicija.DisplayMember = "PaNaziv";
            cboSpedicija.ValueMember = "PaSifra";
            var partAD4 = new SqlDataAdapter(partner3, conn);
            var partDS4 = new DataSet();
            partAD4.Fill(partDS4);
            cboSpedicijaJ.DataSource = partDS4.Tables[0];
            cboSpedicijaJ.DisplayMember = "PaNaziv";
            cboSpedicijaJ.ValueMember = "PaSifra";
           
            
            
            /*
            //Adresa statusa vozila
            var dir5 = "Select ID,Naziv from AdresaStatusVozila order by Naziv";
            var dirAD5 = new SqlDataAdapter(dir5, conn);
            var dirDS5 = new DataSet();
            dirAD5.Fill(dirDS5);
            cboAdresaStatusVozila.DataSource = dirDS5.Tables[0];
            cboAdresaStatusVozila.DisplayMember = "Naziv";
            cboAdresaStatusVozila.ValueMember = "ID";
            */
            
            //Naslov statusa vozila
            /*
            var partner40 = "Select ID,Naziv from NaslovStatusaVozila order by Naziv";
            var partAD40 = new SqlDataAdapter(partner40, conn);
            var partDS40 = new DataSet();
            partAD40.Fill(partDS40);
            cboNaslovStatusaVozila.DataSource = partDS40.Tables[0];
            cboNaslovStatusaVozila.DisplayMember = "Naziv";
            cboNaslovStatusaVozila.ValueMember = "ID";
            */



            //carinski postupak
            var dir2 = "Select ID, (Oznaka + ' ' + Naziv) as Naziv from VrstaCarinskogPostupka order by Naziv";
            var dirAD2 = new SqlDataAdapter(dir2, conn);
            var dirDS2 = new DataSet();
            dirAD2.Fill(dirDS2);
            cboReexport.DataSource = dirDS2.Tables[0];
            cboReexport.DisplayMember = "Naziv";
            cboReexport.ValueMember = "ID";

            //Novi sifarnik Inpekciski tretman
            var dir4 = "Select ID,Naziv from InspekciskiTretman order by Naziv";
            var dirAD4 = new SqlDataAdapter(dir4, conn);
            var dirDS4 = new DataSet();
            dirAD4.Fill(dirDS4);
            cboInspekciskiTretman.DataSource = dirDS4.Tables[0];
            cboInspekciskiTretman.DisplayMember = "Naziv";
            cboInspekciskiTretman.ValueMember = "ID";


            var np4 = "Select ID,(Oznaka + ' ' + Naziv) as Naziv from uvNacinPakovanja order by Naziv";
            var npAD4 = new SqlDataAdapter(np4, conn);
            var npDS4 = new DataSet();
            npAD4.Fill(npDS4);
            cbNacinPakovanja.DataSource = npDS4.Tables[0];
            cbNacinPakovanja.DisplayMember = "Naziv";
            cbNacinPakovanja.ValueMember = "ID";

            var partner = "Select PaSifra,PaNaziv From Partnerji order by PaNaziv";
            var partAD = new SqlDataAdapter(partner, conn);
            var partDS = new DataSet();
            partAD.Fill(partDS);
            cboIzvoznik.DataSource = partDS.Tables[0];
            cboIzvoznik.DisplayMember = "PaNaziv";
            cboIzvoznik.ValueMember = "PaSifra";

            var nalogodavac1 = "Select PaSifra,PaNaziv From Partnerji order by PaNaziv";
            var nal1AD = new SqlDataAdapter(nalogodavac1, conn);
            var nal1DS = new DataSet();
            nal1AD.Fill(nal1DS);
            cboNalogodavac1.DataSource = nal1DS.Tables[0];
            cboNalogodavac1.DisplayMember = "PaNaziv";
            cboNalogodavac1.ValueMember = "PaSifra";

            var nalogodavac2 = "Select PaSifra,PaNaziv From Partnerji order by PaNaziv";
            var nal2AD = new SqlDataAdapter(nalogodavac2, conn);
            var nal2DS = new DataSet();
            nal2AD.Fill(nal2DS);
            cboNalogodavac2.DataSource = nal2DS.Tables[0];
            cboNalogodavac2.DisplayMember = "PaNaziv";
            cboNalogodavac2.ValueMember = "PaSifra";

            var nalogodavac3 = "Select PaSifra,PaNaziv From Partnerji order by PaNaziv";
            var nal3AD = new SqlDataAdapter(nalogodavac3, conn);
            var nal3DS = new DataSet();
            nal3AD.Fill(nal3DS);
            cboNalogodavac3.DataSource = nal3DS.Tables[0];
            cboNalogodavac3.DisplayMember = "PaNaziv";
            cboNalogodavac3.ValueMember = "PaSifra";

            var nalogodavac4 = "Select PaSifra,PaNaziv From Partnerji order by PaNaziv";
            var nal4AD = new SqlDataAdapter(nalogodavac4, conn);
            var nal4DS = new DataSet();
            nal4AD.Fill(nal4DS);
            cboSpediterURijeci.DataSource = nal4DS.Tables[0];
            cboSpediterURijeci.DisplayMember = "PaNaziv";
            cboSpediterURijeci.ValueMember = "PaSifra";


            var voz = "select ID, (Cast(ID as NVarChar(10)) + '-'+Cast(BrVoza as NVarchar(15)) + '-' + Relacija + '-' + Cast(VremePolaska as nvarchar(20))) as Naziv   from Voz ";
            var vozSAD = new SqlDataAdapter(voz, conn);
            var vozSDS = new DataSet();
            vozSAD.Fill(vozSDS);
            cboVoz.DataSource = vozSDS.Tables[0];
            cboVoz.DisplayMember = "Naziv";
            cboVoz.ValueMember = "ID";




            //Panta
            var VRHS = "Select ID,(HSKod + '   ' + Rtrim(Naziv)) as Naziv from VrstaRobeHS order by HSKod";
            var VRHSAD = new SqlDataAdapter(VRHS, conn);
            var VRHSDS = new DataSet();
            VRHSAD.Fill(VRHSDS);
            cboNazivRobe.DataSource = VRHSDS.Tables[0];
            cboNazivRobe.DisplayMember = "Naziv";
            cboNazivRobe.ValueMember = "ID";


            var nhm = "";
            if (chkInterni.Checked == true)
            {
                nhm = "Select ID,(RTRIM(Naziv) + '-' + Rtrim(Broj)) as Naziv from NHM where Interni = 1 order by Naziv ";
            }
            else
            {
                nhm = "Select ID,(RTRIM(Naziv) + '-' + Rtrim(Broj)) as Naziv from NHM order by Naziv";
            }

            var nhmSAD = new SqlDataAdapter(nhm, conn);
            var nhmSDS = new DataSet();
            nhmSAD.Fill(nhmSDS);
            cboNHM.DataSource = nhmSDS.Tables[0];
            cboNHM.DisplayMember = "Naziv";
            cboNHM.ValueMember = "ID";

            var val = "Select VaSifra, VaNaziv from Valute order by VaSifra";
            var valSAD = new SqlDataAdapter(val, conn);
            var valSDS = new DataSet();
            valSAD.Fill(valSDS);
            txtValuta.DataSource = valSDS.Tables[0];
            txtValuta.DisplayMember = "VaNaziv";
            txtValuta.ValueMember = "VaSifra";

            var bro2 = "Select PaSifra, PaNaziv From Partnerji where Brodar = 1  order by PaSifra";
            var broAD2 = new SqlDataAdapter(bro2, conn);
            var broDS2 = new DataSet();
            broAD2.Fill(broDS2);
            cboVrstaPlombe.DataSource = broDS2.Tables[0];
            cboVrstaPlombe.DisplayMember = "PaNaziv";
            cboVrstaPlombe.ValueMember = "PaSifra";

            var dir5 = "Select ID,Naziv from PredefinisanePoruke order by Naziv";
            var dirAD5 = new SqlDataAdapter(dir5, conn);
            var dirDS5 = new DataSet();
            dirAD5.Fill(dirDS5);
            cbNapomenaPoz.DataSource = dirDS5.Tables[0];
            cbNapomenaPoz.DisplayMember = "Naziv";
            cbNapomenaPoz.ValueMember = "ID";

            var partner22 = "SELECT ID, Min(Naziv) as Naziv FROM Scenario group by ID order by ID";
            var partAD22 = new SqlDataAdapter(partner22, conn);
            var partDS22 = new DataSet();
            partAD22.Fill(partDS22);
            cboScenario.DataSource = partDS22.Tables[0];
            cboScenario.DisplayMember = "Naziv";
            cboScenario.ValueMember = "ID";


        }

        private void FillDG()
        {
            var select = "      SELECT  IzvozKonacna.ID as ID,  IzvozKonacna.BrojKontejnera,  IzvozKonacna.VrstaKontejnera as VKID, TipKontenjera.Naziv as VRSTAKONTEJNERA, " +
                " IzvozKonacna.BrojVagona,  IzvozKonacna.BrodskaPlomba, " +
 " IzvozKonacna.OstalePlombe, IzvozKonacna.BookingBrodara,      Partnerji.PaNaziv,     IzvozKonacna.CutOffPort, IzvozKonacna.NetoRobe, IzvozKonacna.BrutoRobe, " +
 "  IzvozKonacna.BrutoRobeO, IzvozKonacna.BrojKoleta, IzvozKonacna.BrojKoletaO, IzvozKonacna.CBM, IzvozKonacna.CBMO, " +
  " IzvozKonacna.VrednostRobeFaktura,   (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaManipulacije.Naziv as nvarchar(20))" +
 "  FROM IzvozVrstaManipulacije           inner join VrstaManipulacije on VrstaManipulacije.ID = IzvozVrstaManipulacije.IDVrstaManipulacije" +
 "  where IzvozVrstaManipulacije.IDNadredjena = IzvozKonacna.ID            FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as VrsteUsluga, " +
 "  (SELECT  STUFF((SELECT distinct    '/' + Cast(RTRIM(VrstaRobeHS.HSKod) as nvarchar(20))             FROM IzvozVrstaRobeHS " +
  " inner join VrstaRobeHS on IzvozVrstaRobeHS.IDVrstaRobeHS = VrstaRobeHS.ID" +
 "  where IzvozVrstaRobeHS.IDNadredjena = IzvozKonacna.ID              FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as HS,  " +
 "  (SELECT  STUFF((SELECT distinct    '/' + Cast(RTRIM(NHM.Broj) as nvarchar(20))              FROM IzvozNHM  inner join NHM" +
  " on IzvozNHM.IDNHM = NHM.ID  where IzvozNHM.IDNadredjena = IzvozKonacna.ID   FOR XML PATH('')), 1, 1, ''  ) As Skupljen) as NHM,   " +
 "  IzvozKonacna.Valuta, KrajnjaDestinacija.Naziv AS KrajnjaDestinacija, VrstePostupakaUvoz.Naziv AS Postupak, KontejnerskiTerminali.Naziv AS PPCNT,  " +
  " KontejnerskiTerminali.Oznaka, IzvozKonacna.Cirada, IzvozKonacna.PlaniraniDatumUtovara, MestaUtovara.Naziv AS MestoUtovara, (Rtrim(partnerjiKontOsebaMU.PaKOIme) + ' ' + Rtrim(partnerjiKontOsebaMU.PaKoPriimek)) as KontaktOsoba , PaKOOpomba as AdresaKO, " +
 "  Carinarnice.CIOznaka,Carinarnice.Naziv AS Carinarnica,  Partnerji_1.PaNaziv AS Spedicija, KontaktSpeditera,    VrstaRobeADR.UNKod  as ADR , AdresaSlanjaStatusa AS AdresaStatusVozila,   " +
 "  NaslovSlanjaStatusa AS NaslovStatusaVozila, IzvozKonacna.EtaLeget, (VrstaCarinskogPostupka.Oznaka + ' ' + VrstaCarinskogPostupka.Naziv) AS Reexport, InspekciskiTretman.Naziv AS InspekciskiTretman,  " +
 "  IzvozKonacna.AutoDana, IzvozKonacna.NajavaVozila, IzvozKonacna.Vozilo, IzvozKonacna.Vozac, IzvozKonacna.DodatneNapomeneDrumski, IzvozKonacna.Vaganje, IzvozKonacna.VGMTezina, IzvozKonacna.Tara, " +
 "  IzvozKonacna.VGMBrod,                     Partnerji_2.PaNaziv AS Izvoznik, Partnerji_3.PaNaziv AS Klijent1, IzvozKonacna.Napomena1REf,  " +
 "  IzvozKonacna.DobijenNalogKlijent1, Partnerji_4.PaNaziv AS klijent2, " +
 "  IzvozKonacna.Napomena2REf, Partnerji_5.PaNaziv AS Klijent3, IzvozKonacna.Napomena3REf, Partnerji_6.PaNaziv AS SpediterRijeka, uvNacinPakovanja.Naziv AS NacinPakovanja,  " +
 "  IzvozKonacna.NacinPretovara FROM         IzvozKonacna LEFT JOIN TipKontenjera ON IzvozKonacna.VrstaKontejnera = TipKontenjera.ID Left JOIN " +
  " Partnerji ON IzvozKonacna.Brodar = Partnerji.PaSifra LEFT JOIN         KrajnjaDestinacija ON IzvozKonacna.KrajnaDestinacija = KrajnjaDestinacija.ID " +
  " LEFT JOIN         VrstePostupakaUvoz ON IzvozKonacna.Postupanje = VrstePostupakaUvoz.ID LEFT JOIN     KontejnerskiTerminali " +
  " ON IzvozKonacna.MestoPreuzimanja = KontejnerskiTerminali.id LEFT JOIN         MestaUtovara ON IzvozKonacna.MesoUtovara = MestaUtovara.ID " +
  " LEFT JOIN         Carinarnice ON IzvozKonacna.MestoCarinjenja = Carinarnice.ID Left JOIN        Partnerji AS Partnerji_1 " +
  " ON IzvozKonacna.Spedicija = Partnerji_1.PaSifra " +
 " left JOIN         VrstaCarinskogPostupka ON IzvozKonacna.NapomenaReexport = VrstaCarinskogPostupka.id " +
  " left JOIN        InspekciskiTretman ON IzvozKonacna.Inspekcija = InspekciskiTretman.ID " +
  " left JOIN        Partnerji AS Partnerji_2 ON IzvozKonacna.Izvoznik = Partnerji_2.PaSifra " +
" left JOIN        Partnerji AS Partnerji_3 ON IzvozKonacna.Klijent1 = Partnerji_3.PaSifra " +
  " left JOIN       Partnerji AS Partnerji_4 ON IzvozKonacna.Klijent2 = Partnerji_4.PaSifra " +
  " left JOIN         Partnerji AS Partnerji_5 ON IzvozKonacna.Klijent3 = Partnerji_5.PaSifra " +
 "  LEFT JOIN          Partnerji AS Partnerji_6 ON IzvozKonacna.SpediterRijeka = Partnerji_6.PaSifra " +
  " LEFT JOIN         uvNacinPakovanja ON IzvozKonacna.NacinPakovanja = uvNacinPakovanja.ID  " +
  " LEFT JOIN         partnerjiKontOsebaMU ON IzvozKonacna.KontaktOsoba = partnerjiKontOsebaMU.PaKoZapSt  " +
    " LEFT JOIN         VrstaRobeADR ON IzvozKonacna.ADR = VrstaRobeADR.ID  " +
             " where IzvozKonacna.IdNadredjena = " + Convert.ToInt32(txtNadredjeni.Text) + " order by IzvozKonacna.ID desc";

            //select PaKoZapSt, (Rtrim(PaKOIme) + ' ' + Rtrim(PaKoPriimek)) as Naziv from partnerjiKontOsebaMU
            // Select ID, (  UNKod + ' - ' + Klasa + ' - ' + Naziv  ) as Naziv From VrstaRobeADR order by UNKod
            //Select ID, (Oznaka + ' ' + Naziv) as Naziv from VrstaCarinskogPostupka order by Naziv
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView1);

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Frozen = true;
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "BROJ KONTEJNERA";
            dataGridView1.Columns[1].Frozen = true;
            dataGridView1.Columns[1].Width = 100;

            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "VKID";
            dataGridView1.Columns[2].Width = 90;

            DataGridViewColumn column3 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "VRSTA KONTEJNERA";
            // dataGridView1.Columns[1].Frozen = true;
            dataGridView1.Columns[3].Width = 120;

            DataGridViewColumn column4 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "BROJ VAGONA";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column5 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "BRODSKA PLOMBA BR ";
            dataGridView1.Columns[5].Width = 70;

            DataGridViewColumn column6 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "OSTALE PLOMBE";
            dataGridView1.Columns[6].Width = 70;

            DataGridViewColumn column7 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "BUKIN BRODAR";
            dataGridView1.Columns[7].Width = 80;

            DataGridViewColumn column8 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "BRODAR";
            dataGridView1.Columns[8].Width = 100;

            DataGridViewColumn column9 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Cut off port";
            //   dataGridView1.Columns[7].Frozen = true;
            dataGridView1.Columns[9].Width = 100;

            DataGridViewColumn column10 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "NTTO robe F";
            dataGridView1.Columns[10].Width = 90;

            DataGridViewColumn column11 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "BTTO robe F";
            dataGridView1.Columns[11].Width = 90;

            DataGridViewColumn column12 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "BTTO robe O";
            dataGridView1.Columns[12].Width = 100;

            DataGridViewColumn column13 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "BROJ KOLETA ";
            // dataGridView1.Columns[13].Frozen = true;
            dataGridView1.Columns[13].Width = 90;

            DataGridViewColumn column14 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "BROJ KOLETA O";
            dataGridView1.Columns[14].Width = 90;

            DataGridViewColumn column15 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "CBM";
            dataGridView1.Columns[15].Width = 90;

            DataGridViewColumn column16 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "CBMO";
            dataGridView1.Columns[16].Width = 90;

            DataGridViewColumn column17 = dataGridView1.Columns[17];
            dataGridView1.Columns[17].HeaderText = "VREDNOST ROBE FAKTURA";
            dataGridView1.Columns[17].Width = 90;

            DataGridViewColumn column18 = dataGridView1.Columns[18];
            dataGridView1.Columns[18].HeaderText = "VRSTA USLUGA";
            dataGridView1.Columns[18].Width = 290;

            DataGridViewColumn column19 = dataGridView1.Columns[19];
            dataGridView1.Columns[19].HeaderText = "HS";
            dataGridView1.Columns[19].Width = 100;

            DataGridViewColumn column20 = dataGridView1.Columns[20];
            dataGridView1.Columns[20].HeaderText = "NHM";
            dataGridView1.Columns[20].Width = 100;

            DataGridViewColumn column21 = dataGridView1.Columns[21];
            dataGridView1.Columns[21].HeaderText = "VALUTA";
            dataGridView1.Columns[21].Width = 80;

            DataGridViewColumn column22 = dataGridView1.Columns[22];
            dataGridView1.Columns[22].HeaderText = "KRAJNJA DESTINACIJA";
            dataGridView1.Columns[22].Width = 120;

            DataGridViewColumn column23 = dataGridView1.Columns[23];
            dataGridView1.Columns[23].HeaderText = "POSTUPAL SA ROBOM";
            dataGridView1.Columns[23].Width = 120;


            DataGridViewColumn column24 = dataGridView1.Columns[24];
            dataGridView1.Columns[24].HeaderText = "PPCNT";
            dataGridView1.Columns[24].Width = 120;


            DataGridViewColumn column25 = dataGridView1.Columns[25];
            dataGridView1.Columns[25].HeaderText = "PLATFORMA";
            dataGridView1.Columns[25].Width = 90;

            DataGridViewColumn column26 = dataGridView1.Columns[26];
            dataGridView1.Columns[26].HeaderText = "CIRADA";
            dataGridView1.Columns[26].Width = 50;

            DataGridViewColumn column27 = dataGridView1.Columns[27];
            dataGridView1.Columns[27].HeaderText = "PL DAT UTOVARA";
            dataGridView1.Columns[27].Width = 120;

            DataGridViewColumn column28 = dataGridView1.Columns[28];
            dataGridView1.Columns[28].HeaderText = "MESTO UTOVARA";
            dataGridView1.Columns[28].Width = 120;


            DataGridViewColumn column29 = dataGridView1.Columns[29];
            dataGridView1.Columns[29].HeaderText = "KONTAKT OSOBA";
            dataGridView1.Columns[29].Width = 90;

            DataGridViewColumn column30 = dataGridView1.Columns[30];
            dataGridView1.Columns[30].HeaderText = "ADRESA UTOVARA";
            dataGridView1.Columns[30].Width = 90;


            DataGridViewColumn column31 = dataGridView1.Columns[31];
            dataGridView1.Columns[31].HeaderText = "CIO OZNAKA";
            dataGridView1.Columns[31].Width = 90;

            DataGridViewColumn column32 = dataGridView1.Columns[32];
            dataGridView1.Columns[32].HeaderText = "CARINARNICA";
            dataGridView1.Columns[32].Width = 90;

            DataGridViewColumn column33 = dataGridView1.Columns[33];
            dataGridView1.Columns[33].HeaderText = "SPEDITER";
            dataGridView1.Columns[33].Width = 90;

            DataGridViewColumn column34 = dataGridView1.Columns[34];
            dataGridView1.Columns[34].HeaderText = "KONTAKT SPEDITERA";
            dataGridView1.Columns[34].Width = 90;

            DataGridViewColumn column35 = dataGridView1.Columns[35];
            dataGridView1.Columns[35].HeaderText = "ADR";
            dataGridView1.Columns[35].Width = 90;

            DataGridViewColumn column36 = dataGridView1.Columns[36];
            dataGridView1.Columns[36].HeaderText = "ADRESA ZA SLANJE STAT";
            dataGridView1.Columns[36].Width = 90;

            DataGridViewColumn column37 = dataGridView1.Columns[37];
            dataGridView1.Columns[37].HeaderText = "NASLOV ZA SLANJE STATUSA VOZILA";
            dataGridView1.Columns[37].Width = 90;

            DataGridViewColumn column38 = dataGridView1.Columns[38];
            dataGridView1.Columns[38].HeaderText = "ETA LEGET";
            dataGridView1.Columns[38].Width = 90;

            DataGridViewColumn column39 = dataGridView1.Columns[39];
            dataGridView1.Columns[39].HeaderText = "REEXPORT";
            dataGridView1.Columns[39].Width = 90;


            DataGridViewColumn column40 = dataGridView1.Columns[40];
            dataGridView1.Columns[40].HeaderText = "INPEKCISKI TRETMAN";
            dataGridView1.Columns[40].Width = 90;

            DataGridViewColumn column41 = dataGridView1.Columns[41];
            dataGridView1.Columns[41].HeaderText = "AUTO DAN";
            dataGridView1.Columns[41].Width = 90;


            DataGridViewColumn column42 = dataGridView1.Columns[42];
            dataGridView1.Columns[42].HeaderText = "NAJAVA VOZILA/CNT I VOZAČA";
            dataGridView1.Columns[42].Width = 90;

            DataGridViewColumn column43 = dataGridView1.Columns[43];
            dataGridView1.Columns[43].HeaderText = "VOZILO";
            dataGridView1.Columns[43].Width = 90;


            DataGridViewColumn column44 = dataGridView1.Columns[44];
            dataGridView1.Columns[44].HeaderText = "VOZAC";
            dataGridView1.Columns[44].Width = 90;

            DataGridViewColumn column45 = dataGridView1.Columns[45];
            dataGridView1.Columns[45].HeaderText = "DODATNE NAPOMENE";
            dataGridView1.Columns[45].Width = 190;


            DataGridViewColumn column46 = dataGridView1.Columns[46];
            dataGridView1.Columns[46].HeaderText = "VAGANJE";
            dataGridView1.Columns[46].Width = 90;

            DataGridViewColumn column47 = dataGridView1.Columns[47];
            dataGridView1.Columns[47].HeaderText = "BTTO ROBE (ODVAGA)";
            dataGridView1.Columns[47].Width = 90;

            DataGridViewColumn column48 = dataGridView1.Columns[48];
            dataGridView1.Columns[48].HeaderText = "TARA";
            dataGridView1.Columns[48].Width = 90;




            DataGridViewColumn column49 = dataGridView1.Columns[49];
            dataGridView1.Columns[49].HeaderText = "BTTO KONTEJENRA (ODVAGA)";
            dataGridView1.Columns[49].Width = 90;

            DataGridViewColumn column50 = dataGridView1.Columns[50];
            dataGridView1.Columns[50].HeaderText = "IZVOZNIK";
            dataGridView1.Columns[50].Width = 130;


            DataGridViewColumn column51 = dataGridView1.Columns[51];
            dataGridView1.Columns[51].HeaderText = "NALOGAVAC ZA VOZ";
            dataGridView1.Columns[51].Width = 190;

            DataGridViewColumn column52 = dataGridView1.Columns[52];
            dataGridView1.Columns[52].HeaderText = "REF1 FAK";
            dataGridView1.Columns[52].Width = 80;

            DataGridViewColumn column53 = dataGridView1.Columns[53];
            dataGridView1.Columns[53].HeaderText = "NALOGOAVAC ZA USLUGE";
            dataGridView1.Columns[53].Width = 190;

            DataGridViewColumn column54 = dataGridView1.Columns[54];
            dataGridView1.Columns[54].HeaderText = "REF2 FAK";
            dataGridView1.Columns[54].Width = 90;

            DataGridViewColumn column55 = dataGridView1.Columns[55];
            dataGridView1.Columns[55].HeaderText = "NALOGODAVAC ZA DP";
            dataGridView1.Columns[55].Width = 170;

            DataGridViewColumn column56 = dataGridView1.Columns[56];
            dataGridView1.Columns[56].HeaderText = "REF3 FAK";
            dataGridView1.Columns[56].Width = 90;

            DataGridViewColumn column57 = dataGridView1.Columns[57];
            dataGridView1.Columns[57].HeaderText = "VGMBROD";
            dataGridView1.Columns[57].Width = 70;



            // RefreshDataGridColor();

        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            InsertIzvoz ins = new InsertIzvoz();
            int pomCirada = 0;
            if (chkCirada.Text == "CIRADA")
            {
                pomCirada = 1;
            }

            int pomNajavaVozila = 0;
            if (chkNajavaVozila.Checked == true)
            {
                pomNajavaVozila = 1;
            }

            int pomNacinPretovara = 0;
            if (chkNacinPretovara.Checked == true)
            {
                pomNacinPretovara = 1;
            }

            int pomVaganje = 0;
            if (chkVaganje.Checked == true)
            {
                pomVaganje = 1;
            }

            int pomDobijenNalog = 0;

            if (cboDobijenNalog.Checked == true)
            {
                pomDobijenNalog = 1;
            }

            int pomTerminal = 0;

            if (chkTerminal.Checked == true)
            {
                pomTerminal = 1;
            }

            ins.UpdIzvozKonacna(Convert.ToInt32(txtID.Text), txtBrojVagona.Text, txtBrKont.Text, Convert.ToInt32(txtTipKont.SelectedValue),
                txtBrodskaPlomba.Text, Convert.ToInt32(txtBokingBrodara.Text), Convert.ToInt32(cboBrodar.SelectedValue), Convert.ToDateTime(dtpCutOffPort.Value),
                Convert.ToDecimal(txtNetoR.Value), Convert.ToDecimal(txtBrutoR.Value), Convert.ToDecimal(txtBrutoO.Value), Convert.ToInt32(txtKoleta.Value),
                Convert.ToInt32(txtKoletaO.Value), Convert.ToDecimal(txtCBM.Value), Convert.ToDecimal(txtCBMO.Value), Convert.ToDecimal(txtVrednostRobeFaktura.Value),
                Convert.ToString(txtValuta.SelectedValue), Convert.ToInt32(cboKrajnjaDestinacija.SelectedValue), Convert.ToInt32(cboPostupanjeSaRobom.SelectedValue),
                Convert.ToInt32(cboPPCNT.SelectedValue), pomCirada, Convert.ToDateTime(dtpPlanUtovara.Value), Convert.ToInt32(cboMestoUtovara.SelectedValue),
                txtKontaktOsoba.Text, Convert.ToInt32(cboCarina.SelectedValue), Convert.ToInt32(cboSpedicija.SelectedValue),
                cboAdresaStatusVozila.Text, cboNaslovStatusaVozila.Text, Convert.ToDateTime(dtpEtaLeget.Value),
                Convert.ToInt32(cboReexport.SelectedValue), Convert.ToInt32(cboInspekciskiTretman.SelectedValue), Convert.ToDecimal(txtAutoDana.Value),
                pomNajavaVozila, Convert.ToInt32(cbNacinPakovanja.SelectedValue), pomNacinPretovara, txtDodatneNapomene.Text, pomVaganje, Convert.ToDecimal(txtOdvaganaTezina.Value),
                Convert.ToDecimal(txtTaraKontejnera.Value), Convert.ToDecimal(txVGMBrodBruto.Value), Convert.ToInt32(cboIzvoznik.SelectedValue),
                Convert.ToInt32(cboNalogodavac1.SelectedValue), Convert.ToInt32(txtRef1.Text), pomDobijenNalog,
                Convert.ToInt32(cboNalogodavac2.SelectedValue), Convert.ToInt32(txtRef2.Text),
                Convert.ToInt32(cboNalogodavac3.SelectedValue), Convert.ToInt32(txtRef3.Text),
                Convert.ToInt32(cboSpediterURijeci.SelectedValue), txtOstalePlombe.Text, Convert.ToInt32(txtADR.SelectedValue),
                Convert.ToInt32(txtNadredjeni.Text), txtVozilo.Text, txtVozac.Text, Convert.ToInt32(cboSpedicijaJ.SelectedValue),
                Convert.ToDateTime(dtpPeriodSkladistenjaOd.Value), Convert.ToDateTime(dtpPeriodSkladistenjaDo.Value), Convert.ToInt32(cboVrstaPlombe.SelectedValue),
                 txtNapomenaZaRobu.Text, Convert.ToDecimal(txtVGMBrod.Value), txtKontaktSpeditera.Text, txtKontaktOsobe.Text, Convert.ToInt32(txtUvozniID.Text), pomTerminal, Convert.ToInt32(cboScenario.SelectedValue), Convert.ToDecimal(txtTaraKontejneraZ.Value), Convert.ToInt32(cboPPCNT2.SelectedValue), Convert.ToInt32(cboPPCNT3.SelectedValue));
            //Fale ostale plombe
            // Convert.ToDecimal(txtDodatneNapomene.Text -- treba staviti nvarchar

            /*
            ins.UpdUvoz(Convert.ToInt32(txtID.Text), Convert.ToDateTime(dtEtaRijeka.Value.ToString()),
                Convert.ToDateTime(dtAtaRijeka.Value.ToString()), txtStatus.Text.ToString().TrimEnd(), txtBrKont.Text,
                Convert.ToInt32(txtTipKont.SelectedValue), Convert.ToDateTime(dtNalogBrodara.Value.ToString()), txtBZ.Text.ToString().TrimEnd(),
                txtNapomena.Text.ToString().TrimEnd(), txtPIN.Text.ToString().TrimEnd(), Convert.ToInt32(cbDirigacija.SelectedValue), Convert.ToInt32(cbBrod.SelectedValue),
                txtTeretnica.Text, Convert.ToInt32(txtADR.SelectedValue), Convert.ToInt32(cbVlasnikKont.SelectedValue), Convert.ToInt32(txtBuking.Text), nalogodavci,
                usluge, Convert.ToInt32(cboUvoznik.SelectedValue), Convert.ToInt32(cboNHM.SelectedValue), cboNazivRobe.SelectedValue.ToString(), Convert.ToInt32(cboSpedicijaG.SelectedValue),
                Convert.ToInt32(cboSpedicijaRTC.SelectedValue), Convert.ToInt32(cboCarinskiPostupak.SelectedValue), Convert.ToInt32(cbPostupak.SelectedValue),
                Convert.ToInt32(cbNacinPakovanja.SelectedValue), Convert.ToInt32(cbOcarina.SelectedValue), Convert.ToInt32(cbOspedicija.SelectedValue),
                txtMesto.Text.ToString().TrimEnd(), txtKontaktOsoba.Text.ToString().TrimEnd(), txtMail.Text.ToString(), txtPlomba1.Text,
                txtPlomba2.Text, Convert.ToDecimal(txtNetoR.Value), Convert.ToDecimal(txtBrutoR.Value), Convert.ToDecimal(txtTaraK.Value),
                Convert.ToDecimal(txtBrutoK.Value), Convert.ToInt32(cbNapomenaPoz.SelectedValue), Convert.ToDateTime(dtAtaOtprema.Value.ToString()),
                Convert.ToInt32(txtBrojVoza.Text), txtRelacija.Text.ToString().TrimEnd(), Convert.ToDateTime(dtAtaDolazak.Value.ToString()), Convert.ToDecimal(txtKoleta.Value), Convert.ToInt32(cboRLTerminal.SelectedValue), txtNapomena1.Text, txtVrstaPregleda.Text,
                Convert.ToInt32(cboNalogodavac1.SelectedValue), txtRef1.Text,
                Convert.ToInt32(cboNalogodavac2.SelectedValue), txtRef2.Text,
                Convert.ToInt32(cboNalogodavac3.SelectedValue), txtRef3.Text, Convert.ToInt32(cboBrodar.SelectedValue));
            */
            //  FillGV();
            //  RefreshDataGridColor();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            InsertIzvoz uvK = new InsertIzvoz();
            uvK.InsIzvozKonacnaNHM(Convert.ToInt32(txtID.Text), Convert.ToInt32(cboNHM.SelectedValue));
            FillDG2();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InsertIzvoz uvK = new InsertIzvoz();
            uvK.DelIzvozKonacnaNHM(Convert.ToInt32(txtIDNHM.Text));
            FillDG2();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            InsertIzvoz uvK = new InsertIzvoz();
            uvK.InsIzvozKonacnaVrstaRobeHS(Convert.ToInt32(txtID.Text), Convert.ToInt32(cboNazivRobe.SelectedValue));
            FillDG3();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InsertIzvoz uvK = new InsertIzvoz();
            uvK.DelIzvozKonacnaVrstaRobeHS(Convert.ToInt32(txtVrstaRobeHS.Text));
            FillDG3();
        }
        private void FillDG6()
        {

            var select = "select Cene.ID, VrstaManipulacije.Naziv, Cene.Cena, VrstaManipulacije.ID from Cene " +
            " inner join VrstaManipulacije on VrstaManipulacije.ID = Cene.VrstaManipulacije " +
            " where TipCenovnika = 1 order by VrstaManipulacije.Naziv ";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView6.ReadOnly = true;
            dataGridView6.DataSource = ds.Tables[0];


            PodesiDatagridView(dataGridView6);

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView6.Columns[0];
            dataGridView6.Columns[0].HeaderText = "ID";
            dataGridView6.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView6.Columns[1];
            dataGridView6.Columns[1].HeaderText = "Man";
            dataGridView6.Columns[1].Width = 120;

            DataGridViewColumn column3 = dataGridView6.Columns[2];
            dataGridView6.Columns[2].HeaderText = "Cena";
            dataGridView6.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView6.Columns[3];
            dataGridView6.Columns[3].HeaderText = "IDVM";
            dataGridView6.Columns[3].Width = 50;
            dataGridView6.Columns[3].Visible = false;
        }
        private void button10_Click(object sender, EventArgs e)
        {
            FillDG6();
        }
        private void FillDG7()
        {

            var select = "select Cene.ID, VrstaManipulacije.Naziv, Cene.Cena, VrstaManipulacije.ID from Cene " +
            " inner join VrstaManipulacije on VrstaManipulacije.ID = Cene.VrstaManipulacije " +
            " where TipCenovnika <> 1 and Cene.PostupakSaRobom = " + Convert.ToInt32(cboPostupanjeSaRobom.SelectedValue) + " and Cene.Uvoznik = " + Convert.ToInt32(cboIzvoznik.SelectedValue) + "  order by VrstaManipulacije.Naziv ";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView6.ReadOnly = true;
            dataGridView6.DataSource = ds.Tables[0];


            PodesiDatagridView(dataGridView6);

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView6.Columns[0];
            dataGridView6.Columns[0].HeaderText = "ID";
            dataGridView6.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView6.Columns[1];
            dataGridView6.Columns[1].HeaderText = "Man";
            dataGridView6.Columns[1].Width = 120;

            DataGridViewColumn column3 = dataGridView6.Columns[2];
            dataGridView6.Columns[2].HeaderText = "Cena";
            dataGridView6.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView6.Columns[3];
            dataGridView6.Columns[3].HeaderText = "IDVM";
            dataGridView6.Columns[3].Width = 50;
            dataGridView6.Columns[3].Visible = false;
        }
        private void button8_Click(object sender, EventArgs e)
        {
            FillDG7();
        }

        private void UbaciStavkuUsluge(int ID, int Manipulacija, double Cena)
        {
            InsertIzvoz uvK = new InsertIzvoz();
            //Panta nije dobro
            // uvK.InsUbaciUsluguKonacna(Convert.ToInt32(txtID.Text), Manipulacija, Cena,Kolicina, Org);
            FillDG8();
        }

        private void FillDG8()
        {
            var select = "select  IzvozKonacnaVrstaManipulacije.ID, VrstaManipulacije.Naziv, IzvozKonacnaVrstaManipulacije.Cena, VrstaManipulacije.ID from IzvozKonacnaVrstaManipulacije " +
            " inner join VrstaManipulacije on VrstaManipulacije.ID = IzvozKonacnaVrstaManipulacije.IDVrstaManipulacije " +
                " where IzvozKonacnaVrstaManipulacije.IDNadredjena = " + Convert.ToInt32(txtID.Text);
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView5.ReadOnly = true;
            dataGridView5.DataSource = ds.Tables[0];


            PodesiDatagridView(dataGridView5);

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView5.Columns[0];
            dataGridView5.Columns[0].HeaderText = "ID";
            dataGridView5.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView5.Columns[1];
            dataGridView5.Columns[1].HeaderText = "Man";
            dataGridView5.Columns[1].Width = 120;

            DataGridViewColumn column3 = dataGridView5.Columns[2];
            dataGridView5.Columns[2].HeaderText = "Cena";
            dataGridView5.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView5.Columns[3];
            dataGridView5.Columns[3].HeaderText = "IDVM";
            dataGridView5.Columns[3].Width = 50;
            dataGridView5.Columns[3].Visible = false;

        }

        private void button9_Click(object sender, EventArgs e)
        {
            int pomID = 0;
            int pomManupulacija = 0;
            double pomCena = 0;
            try
            {
                foreach (DataGridViewRow row in dataGridView6.Rows)
                {
                    if (row.Selected)
                    {
                        pomID = Convert.ToInt32(row.Cells[0].Value.ToString());
                        pomManupulacija = Convert.ToInt32(row.Cells[3].Value.ToString());
                        pomCena = Convert.ToDouble(row.Cells[2].Value.ToString());
                        UbaciStavkuUsluge(pomID, pomManupulacija, pomCena);
                    }
                }


            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Selected)
                    {
                        txtIDNHM.Text = row.Cells[0].Value.ToString();

                    }
                }
            }
            catch { }
        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    if (row.Selected)
                    {
                        txtVrstaRobeHS.Text = row.Cells[0].Value.ToString();

                    }
                }
            }
            catch { }
        }

        private void chkNajavaVozila_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNajavaVozila.Checked == true)
            {
                txtVozilo.Enabled = true;
                txtVozac.Enabled = true;
            }
            else
            {
                txtVozilo.Enabled = false;
                txtVozac.Enabled = false;
            }
        }

        private void frmIzvozKonacna_Load(object sender, EventArgs e)
        {
            firstWidth = this.Size.Width;
            firstHeight = this.Size.Height;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            InsertIzvozKonacnaZaglavlje ins = new InsertIzvozKonacnaZaglavlje();
            ins.InsIzvozKonacnaZaglavlje(Convert.ToInt32(cboVoz.SelectedValue), txtNapomenaZaglavlje.Text, 1, "", Convert.ToDateTime("1.1.1900"), "", "",1);
            //refreshStavke(); - Dodati
        }

        private void button6_Click(object sender, EventArgs e)
        {
            InsertIzvozKonacnaZaglavlje upd = new InsertIzvozKonacnaZaglavlje();
            upd.UpdIzvozKonacnaZaglavlje(Convert.ToInt32(txtNadredjeni.Text), Convert.ToInt32(cboVoz.SelectedValue), txtNapomenaZaglavlje.Text, 1, "", Convert.ToDateTime("1.1.1900"), "", "",1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            InsertIzvozKonacnaZaglavlje del = new InsertIzvozKonacnaZaglavlje();
            del.DelIzvozKonacnaZaglavlje(Convert.ToInt32(txtNadredjeni.Text));
        }
        private void VratiPodatkeSelect(int ID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT [ID],[BrojVagona]      ,[BrojKontejnera],[VrstaKontejnera] " +
   "   ,[BrodskaPlomba],[BookingBrodara],[Brodar],[CutOffPort] " +
   "      ,[NetoRobe],[BrutoRobe],[BrutoRobeO],[BrojKoleta] " +
   "      ,[BrojKoletaO],[CBM],[CBMO],[VrednostRobeFaktura] " +
   "      ,[Valuta],[KrajnaDestinacija],[Postupanje],[MestoPreuzimanja] " +
   "      ,[Cirada],[PlaniraniDatumUtovara],[MesoUtovara],[KontaktOsoba] " +
   "      ,[MestoCarinjenja],[Spedicija],[AdresaSlanjaStatusa],[NaslovSlanjaStatusa] " +
   "      ,[EtaLeget],[NapomenaReexport],[Inspekcija],[AutoDana] " +
    "     ,[NajavaVozila],[NacinPakovanja],[NacinPretovara],[DodatneNapomeneDrumski] " +
    "     ,[Vaganje],[VGMTezina],[Tara],[VGMBrod] " +
   "      ,[Izvoznik],[Klijent1],[Napomena1REf],[DodatneNapomeneDrumski] " +
   "      ,[Klijent2],[Napomena2REf],[Klijent3],[Napomena3REf] " +
   "      ,[SpediterRijeka],[OstalePlombe],[ADR],[Vozilo],[Vozac], SpedicijaJ, PeriodSkladistenjaOd, PeriodSkladistenjaDo , VrstaBrodskePlombe, NapomenaZaRobu, VGMBrod2,KontaktSpeditera" +
   " KontaktOsobe, UvozID, Scenario, MestoPreuzimanja2, MestoPreuzimanja3" +
 "  FROM [IzvozKonacna] where ID=" + ID, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtUvozniID.Text = dr["UvozID"].ToString();
                txtKontaktOsobe.Text = dr["KontaktOsobe"].ToString();
                txtVozilo.Text = dr["Vozilo"].ToString();
                txtVozac.Text = dr["Vozac"].ToString();
                txtOstalePlombe.Text = dr["OstalePlombe"].ToString();
                cboSpediterURijeci.SelectedValue = Convert.ToInt32(dr["SpediterRijeka"].ToString());
                if (dr["DodatneNapomeneDrumski"].ToString() == "1")
                {
                    cboDobijenNalog.Checked = true;
                }
                else
                {
                    cboDobijenNalog.Checked = false;
                }
                txtRef3.Text = dr["Napomena3REf"].ToString();
                txtRef2.Text = dr["Napomena2REf"].ToString();
                txtRef1.Text = dr["Napomena1REf"].ToString();
                cboNalogodavac1.SelectedValue = Convert.ToInt32(dr["Klijent1"].ToString());
                cboNalogodavac2.SelectedValue = Convert.ToInt32(dr["Klijent2"].ToString());
                cboNalogodavac3.SelectedValue = Convert.ToInt32(dr["Klijent3"].ToString());
                cboIzvoznik.SelectedValue = Convert.ToInt32(dr["Izvoznik"].ToString());
                txVGMBrodBruto.Value = Convert.ToDecimal(dr["VGMBrod"].ToString());
                txtTaraKontejnera.Value = Convert.ToDecimal(dr["Tara"].ToString());
                txtOdvaganaTezina.Value = Convert.ToDecimal(dr["VGMTezina"].ToString());
                txtVGMBrod.Value = Convert.ToDecimal(dr["VGMBrod2"].ToString());
                if (dr["Vaganje"].ToString() == "1")
                {
                    chkVaganje.Checked = true;
                }
                else
                {
                    chkVaganje.Checked = false;
                }
                txtDodatneNapomene.Text = dr["DodatneNapomeneDrumski"].ToString();
                if (dr["NacinPretovara"].ToString() == "1")
                {
                    chkNacinPretovara.Checked = true;
                    chkIndirektno.Checked = false;
                }
                else
                {
                    chkNacinPretovara.Checked = false;
                    chkIndirektno.Checked = true;
                }
                cbNacinPakovanja.SelectedValue = Convert.ToInt32(dr["NacinPakovanja"].ToString());
                if (dr["NajavaVozila"].ToString() == "1")
                {
                    chkNajavaVozila.Checked = true;
                }
                else
                {
                    chkNajavaVozila.Checked = false;
                }
                dtpPeriodSkladistenjaDo.Value = Convert.ToDateTime(dr["PeriodSkladistenjaDo"].ToString());
                dtpPeriodSkladistenjaOd.Value = Convert.ToDateTime(dr["PeriodSkladistenjaOd"].ToString());
                cboSpedicijaJ.SelectedValue = Convert.ToInt32(dr["SpedicijaJ"].ToString());
                txtAutoDana.Value = Convert.ToDecimal(dr["AutoDana"].ToString());
                cboInspekciskiTretman.SelectedValue = Convert.ToInt32(dr["Inspekcija"].ToString());
                cboReexport.SelectedValue = Convert.ToInt32(dr["NapomenaReexport"].ToString());
                dtpEtaLeget.Value = Convert.ToDateTime(dr["EtaLeget"].ToString());
                cboNaslovStatusaVozila.Text = dr["NaslovSlanjaStatusa"].ToString();
                cboAdresaStatusVozila.Text = dr["AdresaSlanjaStatusa"].ToString();
                txtADR.SelectedValue = Convert.ToInt32(dr["ADR"].ToString());
                cboSpedicija.SelectedValue = Convert.ToInt32(dr["Spedicija"].ToString());
                cboCarina.SelectedValue = Convert.ToInt32(dr["MestoCarinjenja"].ToString());
                txtKontaktOsoba.Text = Convert.ToString(dr["KontaktOsoba"].ToString());
                cboMestoUtovara.SelectedValue = Convert.ToInt32(dr["MesoUtovara"].ToString());
                dtpPlanUtovara.Value = Convert.ToDateTime(dr["PlaniraniDatumUtovara"].ToString());
                if (dr["Cirada"].ToString() == "1")
                {
                    chkCirada.Text = "CIRADA";
                }
                else
                {
                    chkCirada.Text = "PLATFORMA";
                }
                cboPPCNT.SelectedValue = Convert.ToInt32(dr["MestoPreuzimanja"].ToString());
                cboPPCNT2.SelectedValue = Convert.ToInt32(dr["MestoPreuzimanja2"].ToString());
                cboPPCNT3.SelectedValue = Convert.ToInt32(dr["MestoPreuzimanja3"].ToString());
                cboPostupanjeSaRobom.SelectedValue = Convert.ToInt32(dr["Postupanje"].ToString());
                cboKrajnjaDestinacija.SelectedValue = Convert.ToInt32(dr["KrajnaDestinacija"].ToString());
                txtValuta.SelectedValue = dr["Valuta"].ToString();
                txtVrednostRobeFaktura.Value = Convert.ToDecimal(dr["VrednostRobeFaktura"].ToString());
                txtCBMO.Value = Convert.ToDecimal(dr["CBMO"].ToString());
                txtCBM.Value = Convert.ToDecimal(dr["CBM"].ToString());
                txtKoletaO.Value = Convert.ToDecimal(dr["BrojKoletaO"].ToString());
                txtKoleta.Value = Convert.ToDecimal(dr["BrojKoleta"].ToString());
                txtBrutoO.Value = Convert.ToDecimal(dr["BrutoRobeO"].ToString());
                txtBrutoR.Value = Convert.ToDecimal(dr["BrutoRobe"].ToString());
                txtNetoR.Value = Convert.ToDecimal(dr["NetoRobe"].ToString());
                dtpCutOffPort.Value = Convert.ToDateTime(dr["CutOffPort"].ToString());
                cboBrodar.SelectedValue = Convert.ToInt32(dr["Brodar"].ToString());
                txtBokingBrodara.Text = dr["BookingBrodara"].ToString();
                txtOstalePlombe.Text = dr["OstalePlombe"].ToString();
                txtBrodskaPlomba.Text = dr["BrodskaPlomba"].ToString();
                txtTipKont.SelectedValue = Convert.ToInt32(dr["VrstaKontejnera"].ToString());
                txtBrKont.Text = dr["BrojKontejnera"].ToString();
                txtBrojVagona.Text = dr["BrojVagona"].ToString();
                cboVrstaPlombe.SelectedValue = Convert.ToInt32(dr["VrstaBrodskePlombe"].ToString());
                txtNapomenaZaRobu.Text = dr["NapomenaZaRobu"].ToString();

                cboScenario.SelectedValue = Convert.ToInt32(dr["Scenario"].ToString());
               
                
                //txtKontaktSpeditera.Text = dr["KontaktSpeditera"].ToString();

                /*

                dtEtaRijeka.Value = Convert.ToDateTime(dr["EtaBroda"].ToString());
                dtAtaRijeka.Value = Convert.ToDateTime(dr["AtaBroda"].ToString());
                txtStatus.Text = dr["StatusPrijema"].ToString();
                txtBrKont.Text = dr["BrojKontejnera"].ToString();
                txtTipKont.SelectedValue = Convert.ToInt32(dr["TipKontejnera"].ToString());
                dtNalogBrodara.Value = Convert.ToDateTime(dr["DobijenNalogBrodara"].ToString());
                txtBZ.Text = dr["DobijeBZ"].ToString();
                txtNapomena.Text = dr["Napomena"].ToString();
                txtPIN.Text = dr["PIN"].ToString();
                cbDirigacija.SelectedValue = Convert.ToInt32(dr["DirigacijaKontejeraZa"].ToString());
                cbBrod.SelectedValue = Convert.ToInt32(dr["NazivBroda"].ToString());
                txtTeretnica.Text = dr["BrodskaTeretnica"].ToString();
                txtADR.SelectedValue = Convert.ToInt32(dr["ADR"].ToString());
                cbVlasnikKont.SelectedValue = Convert.ToInt32(dr["VlasnikKontejnera"].ToString());
                txtBuking.Text = dr["BukingBrodara"].ToString();
                cboUvoznik.SelectedValue = Convert.ToInt32(dr["Uvoznik"].ToString());
                cboNHM.SelectedValue = 1;
                cboSpedicijaG.SelectedValue = Convert.ToInt32(dr["SpedicijaGranica"].ToString());
                cboSpedicijaRTC.SelectedValue = Convert.ToInt32(dr["SpedicijaRTC"].ToString());
                cboCarinskiPostupak.SelectedValue = Convert.ToInt32(dr["CarinskiPostupak"].ToString());
                cbPostupak.SelectedValue = Convert.ToInt32(dr["PostupakSaRobom"].ToString());
                cbNacinPakovanja.SelectedValue = Convert.ToInt32(dr["NacinPakovanja"].ToString());
                cbOcarina.SelectedValue = Convert.ToInt32(dr["OdredisnaCarina"].ToString());
                cbOspedicija.SelectedValue = Convert.ToInt32(dr["OdredisnaSpedicija"].ToString());
                txtMesto.Text = dr["MestoIstovara"].ToString();
                txtKontaktOsoba.Text = dr["KontaktOsoba"].ToString();
                txtMail.Text = dr["Email"].ToString();
                txtPlomba1.Text = dr["BrojPlombe1"].ToString();
                txtPlomba2.Text = dr["BrojPlombe2"].ToString();
                txtNetoR.Value = Convert.ToDecimal(dr["NetoRobe"].ToString());
                txtBrutoR.Value = Convert.ToDecimal(dr["BrutoRobe"].ToString());
                txtTaraK.Value = Convert.ToDecimal(dr["TaraKontejnera"].ToString());
                txtBrutoK.Value = Convert.ToDecimal(dr["BrutoKontejnera"].ToString());
                cbNapomenaPoz.SelectedValue = Convert.ToInt32(dr["NapomenaZaPozicioniranje"].ToString());
                dtAtaOtprema.Value = Convert.ToDateTime(dr["AtaOtpreme"].ToString());
                txtBrojVoza.Text = dr["BrojVoza"].ToString();
                txtRelacija.Text = dr["RelacijaVoza"].ToString();
                dtAtaDolazak.Value = Convert.ToDateTime(dr["AtaDolazak"].ToString());
                txtKoleta.Value = Convert.ToDecimal(dr["Koleta"].ToString());
                cboRLTerminal.SelectedValue = Convert.ToInt32(dr["RLTerminali"].ToString());
                txtNapomena1.Text = dr["Napomena1"].ToString();
                txtVrstaPregleda.Text = dr["VrstaPregleda"].ToString();
                cboNalogodavac1.SelectedValue = Convert.ToInt32(dr["Nalogodavac1"].ToString());
                txtRef1.Text = dr["Ref1"].ToString();
                cboNalogodavac2.SelectedValue = Convert.ToInt32(dr["Nalogodavac2"].ToString());
                txtRef2.Text = dr["Ref2"].ToString();
                cboNalogodavac3.SelectedValue = Convert.ToInt32(dr["Nalogodavac3"].ToString());
                txtRef3.Text = dr["Ref3"].ToString();

                cboBrodar.SelectedValue = Convert.ToInt32(dr["Brodar"].ToString());
                */




            }



            con.Close();
            FillDGUsluge();
            FillDGUslugeUvozne();

        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    txtID.Text = row.Cells[0].Value.ToString();
                    VratiPodatkeSelect(Convert.ToInt32(txtID.Text));
                    FillDG2();
                    FillDG3();

                    FillDG8();
                    FillDGUsluge();
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Pokrenuli ste proceduru pravljenja naloga za službu terminal", "Radni nalog", MessageBoxButtons.YesNo);
            int PostojeRn = 0;
            PostojeRn = VratiPostojeceRN();
            if (dialogResult == DialogResult.Yes)
            {
                if (chkTerminal.Checked == true)
                {
                    Uvoz.InsertRadniNalogInterni ins = new Uvoz.InsertRadniNalogInterni();
                    //ins.InsRadniNalogInterni(Convert.ToInt32(1), Convert.ToInt32(4), Convert.ToDateTime(DateTime.Now), Convert.ToDateTime("1.1.1900. 00:00:00"), "", Convert.ToInt32(0), "PlanUtovara", Convert.ToInt32(txtNadredjeni.Text), KorisnikTekuci, "");
                    ins.InsRadniNalogInterniIzvoz(Convert.ToInt32(4), Convert.ToInt32(4), Convert.ToDateTime(DateTime.Now), Convert.ToDateTime("1.1.1900. 00:00:00"), "", Convert.ToInt32(0), "PlanUtovaraTER", Convert.ToInt32(txtNadredjeni.Text), KorisnikTekuci, " ");
                }
                else
                {
                    Uvoz.InsertRadniNalogInterni ins = new Uvoz.InsertRadniNalogInterni();
                    //ins.InsRadniNalogInterni(Convert.ToInt32(1), Convert.ToInt32(4), Convert.ToDateTime(DateTime.Now), Convert.ToDateTime("1.1.1900. 00:00:00"), "", Convert.ToInt32(0), "PlanUtovara", Convert.ToInt32(txtNadredjeni.Text), KorisnikTekuci, "");
                    ins.InsRadniNalogInterniIzvoz(Convert.ToInt32(2), Convert.ToInt32(4), Convert.ToDateTime(DateTime.Now), Convert.ToDateTime("1.1.1900. 00:00:00"), " ", Convert.ToInt32(0), "PlanUtovaraIZ", Convert.ToInt32(txtNadredjeni.Text), KorisnikTekuci, " ");
                }
                
            }
            else
            {
                // FormirajOpstiExcel();
            }


        }

        int VratiPostojeceRN()
        {
            return 0;
        }
        int terminal = 0;
        string pickUp;
        string pickUp2;
        string pickUp3;
        int ADR = 0;
        int ScenarioGL = 0;
        int Zeleznina = 0;
        int Repozicija = 0;
        string relacija;



        private void MoguciScenario()
        {
            string Moguce = "";
            if (pickUp == "Leget -" && pickUp2 == "Leget -" && pickUp3 != "Leget -")
            {
                ScenarioGL = 1; ///Leget - Leget - Krajnja destinacija

            }
            else if (pickUp != "Leget -" && pickUp3 != "Leget -")
            {
                ScenarioGL = 2; //Drugi terminal - Leget - Krajnja destinacija
            }

            else
            {
                MessageBox.Show("Relacije ne pripadaju ni jednom scenariju");
                return;
            }



            //Provera SCENARIJA UKLJUCITI ADR
            if (chkTerminal.Checked == true)
            {
                Moguce = "15";
            }
            else if (ScenarioGL == 1 && Convert.ToInt32(txtADR.SelectedValue) == 0 && pp > 1)
            {
                Moguce = "7,8,9"; // Leget - Leget - NestoDrugo - BEZ ADR - pun
            }
            else if (ScenarioGL == 1 && Convert.ToInt32(txtADR.SelectedValue) > 1 && pp > 1)
            {
                Moguce = "23,24,25";  // PUN
            }
            else if (ScenarioGL == 2 && Convert.ToInt32(txtADR.SelectedValue) == 0 && pp > 1)
            {
                Moguce = "13"; // PUN Ostaje na terminalu \"11,12,13,14,29
            }
            else if (ScenarioGL == 2 && Convert.ToInt32(txtADR.SelectedValue) == 0 && pp == 0)
            {
                Moguce = "12,14,29"; // PUN Ostaje na terminalu \"11,12,13,14,29
            }
            else if (ScenarioGL == 2 && Convert.ToInt32(txtADR.SelectedValue) > 1 && pp == 1)
            {
                Moguce = "25,26";
            }


            int poklapase = 0;
            string[] split = Moguce.Split(',');
            foreach (string item in split)
            {
                if (item == cboScenario.SelectedValue.ToString())
                {
                    poklapase = 1;
                }

            }



            if (poklapase == 0)
            {
                int k = ProveriImaUsluga(txtID.Text);
                if (k == 0)
                {
                    //Kada jos nisu definisane usluge
                }
                else
                {
                    DialogResult result = MessageBox.Show("Trenutni scenario je" + cboScenario.Text + " moguci scenariji " + Moguce + " se ne poklapaju sa njim, da li želite brisanje terminalskih usluge?", "Confirmation", MessageBoxButtons.YesNoCancel);
                    if (result == DialogResult.Yes)
                    {
                        InsertIzvozKonacna uvK = new InsertIzvozKonacna();
                        uvK.DelIzvozUslugaTerminalskih(Convert.ToInt32(txtID.Text));
                    }
                    else
                    {
                        //...
                    }
                }


            }
        }
        int ProveriImaUsluga(string UvozID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT Count(*)  as Broj" +
        "  FROM [IzvozKonacnaVrstaManipulacije] where IDNAdredjena=" + UvozID, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                return Convert.ToInt32(dr["Broj"].ToString());
            }
            con.Close();
            return 0;

        }


        private void VratiZelezninu(int pickUp, int pickUp2, int pickUp3, string TipKOntejnera)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select VrstaManipulacije.ID, Relacija from VrstaManipulacije  " +
                " inner join TipKontenjera on VrstaManipulacije.TipKontejnera = TipKontenjera.ID" +
                " where GrupaVrsteManipulacijeID = 1 and Substring(TipKontenjera.SkNaziv,1,3) = '" + TipKOntejnera + "'' AND RLTerminali = " +
                pickUp + " and RLTerminali2 = " + pickUp2 + " AND RLTerminali3 = " + pickUp3, con);
            SqlDataReader dr = cmd.ExecuteReader();




            while (dr.Read())
            {

                Zeleznina = Convert.ToInt32(dr["ID"].ToString());
                relacija = dr["Relacija"].ToString();
            }
            con.Close();


        }


        private void VratiRepoziciju(int pickUp, int pickUp2, int pickUp3, string TipKOntejnera)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select VrstaManipulacije.ID, Relacija from VrstaManipulacije  " +
                " inner join TipKontenjera on VrstaManipulacije.TipKontejnera = TipKontenjera.ID " +
                " where GrupaVrsteManipulacijeID = 2 and Substring(TipKontenjera.SkNaziv,1,3) = '" + TipKOntejnera + "'' AND RLTerminali = " +
                pickUp + " and RLTerminali2 = " + pickUp2 + " AND RLTerminali3 = " + pickUp3, con);
            SqlDataReader dr = cmd.ExecuteReader();




            while (dr.Read())
            {

                Repozicija = Convert.ToInt32(dr["ID"].ToString());
                //  relacija = dr["Relacija"].ToString();
            }
            con.Close();


        }

        private void button14_Click(object sender, EventArgs e)
        {

            int terminal = 0;
            if (txtID.Text == "")
            { txtID.Text = "0"; }
            if (chkTerminal.Checked) { terminal = 1; }
            pickUp = cboPPCNT.Text.ToString().TrimEnd();
            pickUp2 = cboPPCNT2.Text.ToString().TrimEnd();
            pickUp3 = cboPPCNT3.Text.ToString().TrimEnd();

            ADR = Convert.ToInt32(txtADR.SelectedValue);

            if (txtTipKont.Text.Length > 3)
            {
                if (pp == 0)
                {
                    //PRAZAN
                    VratiRepoziciju(Convert.ToInt32(cboPPCNT.SelectedValue), Convert.ToInt32(cboPPCNT2.SelectedValue), Convert.ToInt32(cboPPCNT3.SelectedValue), txtTipKont.Text.Substring(0, 3));
                }
                else
                {
                    //PUN
                    VratiZelezninu(Convert.ToInt32(cboPPCNT.SelectedValue), Convert.ToInt32(cboPPCNT2.SelectedValue), Convert.ToInt32(cboPPCNT3.SelectedValue), txtTipKont.Text.Substring(0, 3));

                }
            }



            MoguciScenario();

            // int IDPlana, int ID, int Nalogodavac1, int Nalogodavac2, int Nalogodavac3
            frmIzvozUnosManipulacije um = new frmIzvozUnosManipulacije(Convert.ToInt32(0), Convert.ToInt32(txtID.Text), Convert.ToInt32(cboNalogodavac1.SelectedValue), Convert.ToInt32(cboNalogodavac2.SelectedValue), Convert.ToInt32(cboNalogodavac3.SelectedValue), Convert.ToInt32(cboIzvoznik.SelectedValue), terminal, pickUp, ScenarioGL, ADR,pp, Zeleznina, Repozicija);
            um.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connection);

            var nhm = "Select ID,Rtrim(Broj) + '-' + (Rtrim(Naziv)) as Naziv from NHM order by NHM.Broj";
            var nhmSAD = new SqlDataAdapter(nhm, conn);
            var nhmSDS = new DataSet();
            nhmSAD.Fill(nhmSDS);
            cboNHM.DataSource = nhmSDS.Tables[0];
            cboNHM.DisplayMember = "Naziv";
            cboNHM.ValueMember = "ID";
        }

        private void button15_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connection);
            if (chkInterni.Checked == true)
            {
                switch (NHMObrni)
                {
                    case 1:
                        {
                            var nhm = "Select ID,Rtrim(Broj) + '-' + (Rtrim(Naziv)) as Naziv from NHM where Interni = 1 order by NHM.Broj";
                            var nhmSAD = new SqlDataAdapter(nhm, conn);
                            var nhmSDS = new DataSet();
                            nhmSAD.Fill(nhmSDS);
                            cboNHM.DataSource = nhmSDS.Tables[0];
                            cboNHM.DisplayMember = "Naziv";
                            cboNHM.ValueMember = "ID";
                            NHMObrni = 0;
                            break;

                        }
                    case 0:
                        {
                            var nhm = "Select ID,Rtrim(Naziv) + '-' + (Rtrim(Broj)) as Naziv from NHM where Interni = 1 order by NHM.Naziv";
                            var nhmSAD = new SqlDataAdapter(nhm, conn);
                            var nhmSDS = new DataSet();
                            nhmSAD.Fill(nhmSDS);
                            cboNHM.DataSource = nhmSDS.Tables[0];
                            cboNHM.DisplayMember = "Naziv";
                            cboNHM.ValueMember = "ID";
                            NHMObrni = 1;
                            break;
                        }
                    default:
                        break;
                }
            }
            else
            {
                switch (NHMObrni)
                {
                    case 1:
                        {
                            var nhm = "Select ID,Rtrim(Broj) + '-' + (Rtrim(Naziv)) as Naziv from NHM order by NHM.Broj";
                            var nhmSAD = new SqlDataAdapter(nhm, conn);
                            var nhmSDS = new DataSet();
                            nhmSAD.Fill(nhmSDS);
                            cboNHM.DataSource = nhmSDS.Tables[0];
                            cboNHM.DisplayMember = "Naziv";
                            cboNHM.ValueMember = "ID";
                            NHMObrni = 0;
                            break;

                        }
                    case 0:
                        {
                            var nhm = "Select ID,Rtrim(Naziv) + '-' + (Rtrim(Broj)) as Naziv from NHM order by NHM.Naziv";
                            var nhmSAD = new SqlDataAdapter(nhm, conn);
                            var nhmSDS = new DataSet();
                            nhmSAD.Fill(nhmSDS);
                            cboNHM.DataSource = nhmSDS.Tables[0];
                            cboNHM.DisplayMember = "Naziv";
                            cboNHM.ValueMember = "ID";
                            NHMObrni = 1;
                            break;
                        }
                    default:
                        break;
                }
            }

        }
        private void VratiAdresuKontaktaIzNapomene(int Sifra)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select PaKOOpomba from partnerjiKontOsebaMU where PaKOZapSt  =" + Sifra, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtAdresaMestaUtovara.Text = dr["PaKOOpomba"].ToString();


            }
            con.Close();


        }

        private void button12_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connection);

            var ko = "select PaKoZapSt, (Rtrim(PaKOIme) + ' ' + Rtrim(PaKoPriimek)) as Naziv from partnerjiKontOsebaMU where PaKOSifra = '" + Convert.ToInt32(cboMestoUtovara.SelectedValue) + "'  order by PaKOIme";
            var koAD = new SqlDataAdapter(ko, conn);
            var koDS = new DataSet();
            koAD.Fill(koDS);
            txtKontaktOsoba.DataSource = koDS.Tables[0];
            txtKontaktOsoba.DisplayMember = "Naziv";
            txtKontaktOsoba.ValueMember = "PaKoZapSt";


            // VratiAdresuKontaktaIzNapomene(Convert.ToInt32(cboMestoUtovara.SelectedValue));
        }

        private void button13_Click(object sender, EventArgs e)
        {
            VratiAdresuKontaktaIzNapomene(Convert.ToInt32(txtKontaktOsoba.SelectedValue));
            /*
            using (var detailForm = new frmKontaktOsobeMU(Convert.ToInt32(cboMestoUtovara.SelectedValue)))
            {
                detailForm.ShowDialog();

                txtAdresaMestaUtovara.Text = detailForm.GetKontaktAdresa(Convert.ToInt32(cboMestoUtovara.SelectedValue));
            }
            */
        }

        private void button16_Click(object sender, EventArgs e)
        {
            using (var detailForm = new Dokumenta.frmKontaktOsobe(Convert.ToInt32(cboSpedicija.SelectedValue)))
            {
                detailForm.ShowDialog();
                txtKontaktSpeditera.Text = detailForm.GetKontakt(Convert.ToInt32(cboSpedicija.SelectedValue));
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            using (var detailForm = new Dokumenta.frmKontaktOsobe(Convert.ToInt32(cboNalogodavac3.SelectedValue)))
            {
                detailForm.ShowDialog();
                cboAdresaStatusVozila.Text = detailForm.GetKontaktMailSVISelektovani(Convert.ToInt32(cboNalogodavac3.SelectedValue));
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            cboNaslovStatusaVozila.Text = "";
            if (checkedListBox2.GetItemCheckState(0) == CheckState.Checked)
            {
                cboNaslovStatusaVozila.Text = cboNaslovStatusaVozila.Text + " " + cboNalogodavac3.Text;
            }
            if (checkedListBox2.GetItemCheckState(1) == CheckState.Checked)
            {
                cboNaslovStatusaVozila.Text = cboNaslovStatusaVozila.Text + "; " + cboIzvoznik.Text;
            }

            if (checkedListBox2.GetItemCheckState(2) == CheckState.Checked)
            {
                cboNaslovStatusaVozila.Text = cboNaslovStatusaVozila.Text + "; " + txtBrKont.Text;
            }
            if (checkedListBox2.GetItemCheckState(3) == CheckState.Checked)
            {
                cboNaslovStatusaVozila.Text = cboNaslovStatusaVozila.Text + ", " + cboBrodar.Text;
            }

            if (checkedListBox2.GetItemCheckState(4) == CheckState.Checked)
            {
                cboNaslovStatusaVozila.Text = cboNaslovStatusaVozila.Text + "; " + txtBokingBrodara.Text;
            }




        }

        private void VratiNHM(int Sifra)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select TOP 1 ID from NHM Where ADRID =  " + Sifra, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    cboNHM.SelectedValue = Convert.ToInt32(dr["ID"].ToString());


                }
            }
            else
            {
                cboNHM.SelectedValue = 0;
            }
            con.Close();


        }

        private void button19_Click(object sender, EventArgs e)
        {
            VratiNHM(Convert.ToInt32(txtADR.SelectedValue));
        }

        private void txtTaraKontejnera_Leave(object sender, EventArgs e)
        {
            txVGMBrodBruto.Value = txtOdvaganaTezina.Value + txtTaraKontejnera.Value;
        }

        private void FillDGUsluge()
        {
            var select = "";

            select = "select  IzvozKonacnaVrstaManipulacije.ID as ID, IzvozKonacnaVrstaManipulacije.IDNadredjena as KontejnerID, IzvozKonacna.BrojKontejnera, " +
  " IzvozKonacnaVrstaManipulacije.Kolicina,  VrstaManipulacije.ID as ManipulacijaID,VrstaManipulacije.Naziv as ManipulacijaNaziv, " +
  " IzvozKonacnaVrstaManipulacije.Cena,OrganizacioneJedinice.ID,   OrganizacioneJedinice.Naziv as OrganizacionaJedinica, " +
  " Partnerji.PaSifra as NalogodavacID,PArtnerji.PaNaziv as Platilac, SaPDV,IzvozKonacnaVrstaManipulacije.Pokret, KontejnerStatus.Naziv , 'IZVOZNA'  as Tip" +
  " from IzvozKonacnaVrstaManipulacije " +
  " Inner    join VrstaManipulacije on VrstaManipulacije.ID = IzvozKonacnaVrstaManipulacije.IDVrstaManipulacije " +
  "  inner " +
  " join PArtnerji on IzvozKonacnaVrstaManipulacije.Platilac = PArtnerji.PaSifra " +
  " inner " +
  " join OrganizacioneJedinice on OrganizacioneJedinice.ID = IzvozKonacnaVrstaManipulacije.OrgJed " +
  "  inner " +
  " join IzvozKonacna on IzvozKonacnaVrstaManipulacije.IDNadredjena = IzvozKonacna.ID " +
  " left " +
  " join KontejnerStatus on KontejnerStatus.ID = StatusKontejnera" +
  " where IzvozKonacna.ID = " + Convert.ToInt32(txtID.Text) + " Order by IzvozKonacnaVrstaManipulacije.ID asc";
            



            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView8.ReadOnly = true;
            dataGridView8.DataSource = ds.Tables[0];


            PodesiDatagridView(dataGridView8);

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView8.Columns[0];
            dataGridView8.Columns[0].HeaderText = "ID";
            dataGridView8.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView8.Columns[1];
            dataGridView8.Columns[1].HeaderText = "IDU";
            dataGridView8.Columns[1].Width = 30;

            DataGridViewColumn column3 = dataGridView8.Columns[2];
            dataGridView8.Columns[2].HeaderText = "Kontejner";
            dataGridView8.Columns[2].Width = 100;

            DataGridViewColumn column4 = dataGridView8.Columns[3];
            dataGridView8.Columns[3].HeaderText = "Kolicina";
            dataGridView8.Columns[3].Width = 70;

            DataGridViewColumn column5 = dataGridView8.Columns[4];
            dataGridView8.Columns[4].HeaderText = "UslugaID";
            dataGridView8.Columns[4].Width = 70;

            DataGridViewColumn column6 = dataGridView8.Columns[5];
            dataGridView8.Columns[5].HeaderText = "Usluga";
            dataGridView8.Columns[5].Width = 370;

            DataGridViewColumn column7 = dataGridView8.Columns[6];
            dataGridView8.Columns[6].HeaderText = "Cena";
            dataGridView8.Columns[6].Width = 100;

            DataGridViewColumn column8 = dataGridView8.Columns[7];
            dataGridView8.Columns[7].HeaderText = "OJID";
            dataGridView8.Columns[7].Width = 50;

            DataGridViewColumn column9 = dataGridView8.Columns[8];
            dataGridView8.Columns[8].HeaderText = "OJ NAziv";
            dataGridView8.Columns[8].Width = 150;

            DataGridViewColumn column10 = dataGridView8.Columns[9];
            dataGridView8.Columns[9].HeaderText = "Pa Sifra";
            dataGridView8.Columns[9].Width = 70;

            DataGridViewColumn column11 = dataGridView8.Columns[10];
            dataGridView8.Columns[10].HeaderText = "Partner";
            dataGridView8.Columns[10].Width = 270;

        }

        private void FillDGUslugeUvozne()
        {
            var select = "";

            select =
  " select  UvozKonacnaVrstaManipulacije.ID as ID, UvozKonacnaVrstaManipulacije.IDNadredjena as KontejnerID, UvozKonacna.BrojKontejnera, " +
  " UvozKonacnaVrstaManipulacije.Kolicina,  VrstaManipulacije.ID as ManipulacijaID,VrstaManipulacije.Naziv as ManipulacijaNaziv, " +
  " UvozKonacnaVrstaManipulacije.Cena,OrganizacioneJedinice.ID,   OrganizacioneJedinice.Naziv as OrganizacionaJedinica,  " +
  " Partnerji.PaSifra as NalogodavacID,PArtnerji.PaNaziv as Platilac, SaPDV,UvozKonacnaVrstaManipulacije.Pokret, KontejnerStatus.Naziv,  'UVOZNA'" +
  " from UvozKonacnaVrstaManipulacije" +
  " Inner    join VrstaManipulacije on VrstaManipulacije.ID = UvozKonacnaVrstaManipulacije.IDVrstaManipulacije" +
  " inner" +
  " join PArtnerji on UvozKonacnaVrstaManipulacije.Platilac = PArtnerji.PaSifra" +
  " inner" +
  " join OrganizacioneJedinice on OrganizacioneJedinice.ID = UvozKonacnaVrstaManipulacije.OrgJed" +
  " inner" +
  " join UvozKonacna on UvozKonacnaVrstaManipulacije.IDNadredjena = UvozKonacna.ID" +
  " left" +
  " join KontejnerStatus on KontejnerStatus.ID = StatusKontejnera where UvozKonacna.ID  = " + Convert.ToInt32(txtUvozniID.Text) + " Order by UvozKonacnaVrstaManipulacije.ID asc";



            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView9.ReadOnly = true;
            dataGridView9.DataSource = ds.Tables[0];


            PodesiDatagridView(dataGridView9);
            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView9.Columns[0];
            dataGridView9.Columns[0].HeaderText = "ID";
            dataGridView9.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView9.Columns[1];
            dataGridView9.Columns[1].HeaderText = "IDU";
            dataGridView9.Columns[1].Width = 30;

            DataGridViewColumn column3 = dataGridView9.Columns[2];
            dataGridView9.Columns[2].HeaderText = "Kontejner";
            dataGridView9.Columns[2].Width = 50;

        }

        private void FillDGIK()
        {
            var select = "select IzvozKonacnaNapomenePozicioniranja.ID, IzvozKonacnaNapomenePozicioniranja.IDNapomene, PredefinisanePoruke.Naziv " +
" from IzvozKonacnaNapomenePozicioniranja inner join PredefinisanePoruke on PredefinisanePoruke.ID = IzvozKonacnaNapomenePozicioniranja.IDNapomene " +
" where IzvozKonacnaNapomenePozicioniranja.IdNadredjena = " + Convert.ToInt32(txtID.Text) + " order by IzvozKOnacnaNapomenePozicioniranja.ID desc ";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView7.ReadOnly = true;
            dataGridView7.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView7);

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView7.Columns[0];
            dataGridView7.Columns[0].HeaderText = "ID";
            dataGridView7.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView7.Columns[1];
            dataGridView7.Columns[1].HeaderText = "NapomenaID";
            dataGridView7.Columns[1].Width = 20;

            DataGridViewColumn column3 = dataGridView7.Columns[2];
            dataGridView7.Columns[2].HeaderText = "Napomena";
            dataGridView7.Columns[2].Width = 160;

        }

        private void button21_Click(object sender, EventArgs e)
        {
            InsertIzvozKonacna uvK = new InsertIzvozKonacna();
            uvK.InsIzvozKonacnaNapomenePozicioniranja(Convert.ToInt32(txtID.Text), Convert.ToInt32(cbNapomenaPoz.SelectedValue));
            FillDGIK();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            InsertIzvozKonacna uvK = new InsertIzvozKonacna();
            uvK.DelIzvozKonacnaNapomenePozicioniranja(Convert.ToInt32(txtNapomenaPoz.Text));
            FillDGIK();
        }

        private void dataGridView7_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView7.Rows)
                {
                    if (row.Selected)
                    {
                        txtNapomenaPoz.Text = row.Cells[0].Value.ToString();

                    }
                }
            }
            catch { }
        }

        private void chkVaganje_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVaganje.Checked == true)
            {
                txtOdvaganaTezina.Enabled = true;
                txtTaraKontejnera.Enabled = true;
                txVGMBrodBruto.Enabled = true;
                txtVGMBrod.Enabled = true;
            }
            else
            {
                txtOdvaganaTezina.Enabled = false;
                txtTaraKontejnera.Enabled = false;
                txVGMBrodBruto.Enabled = false;
                txtVGMBrod.Enabled = false;
                ;
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            frmIzvozDokumenta fid = new frmIzvozDokumenta(txtID.Text, txtBokingBrodara.Text, cboVoz.SelectedValue.ToString());
            fid.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            frmFormiranjePlanaIzvoz fplan = new frmFormiranjePlanaIzvoz(Convert.ToInt32(txtNadredjeni.Text));
            fplan.Show();
        }

        private void chkNacinPretovara_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNacinPretovara.Checked == true)
                chkIndirektno.Checked = false;
            else
            {
                chkIndirektno.Checked = true;
            }
        }

        private void chkIndirektno_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIndirektno.Checked == true)
                chkNacinPretovara.Checked = false;
            else
            {
                chkNacinPretovara.Checked = true;
            }
        }

        private void txVGMBrodBruto_Leave(object sender, EventArgs e)
        {
            txtOdvaganaTezina.Value = txVGMBrodBruto.Value - txtTaraKontejnera.Value;
        }

        private void frmIzvozKonacna_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == System.Windows.Forms.Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            using (var detailForm = new frmKontaktOsobeMU(txtAdresaMestaUtovara.Text))
            {
                detailForm.ShowDialog();
                txtKontaktOsobe.Text = detailForm.GetSviKontaktiPoAdresi(txtAdresaMestaUtovara.Text);
            }

            SqlConnection conn = new SqlConnection(connection);
            // PaKOOpomba
            //Bilo  var ko = "select PaKoZapSt, (Rtrim(PaKOIme) + ' ' + Rtrim(PaKoPriimek)) as Naziv from partnerjiKontOsebaMU where PaKOSifra = '" + Convert.ToInt32(cboMestoUtovara.SelectedValue) + "'  order by PaKOIme";
            var ko = "select PaKoZapSt, (Rtrim(PaKOIme) + ' ' + Rtrim(PaKoPriimek)) as Naziv from partnerjiKontOsebaMU where PaKoZapSt = '" + Convert.ToInt32(txtAdresaMestaUtovara.SelectedValue) + "'  order by PaKOIme";

            var koAD = new SqlDataAdapter(ko, conn);
            var koDS = new DataSet();
            koAD.Fill(koDS);
            txtKontaktOsoba.DataSource = koDS.Tables[0];
            txtKontaktOsoba.DisplayMember = "Naziv";
            txtKontaktOsoba.ValueMember = "PaKoZapSt";
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            InsertIzvozKonacna ins = new InsertIzvozKonacna();
            ins.VratiUNerasporedjene(Convert.ToInt32(txtID.Text));
        }

        int ProveriPrazanPun()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);
            int idnhm = 0;
            con.Open();

            SqlCommand cmd = new SqlCommand("select top 1 IDNHM from IzvozkonacnaNHM where IDNadredjena  = " + Convert.ToInt32(txtID.Text), con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                idnhm = Convert.ToInt32(dr["IDNHM"].ToString());

            }

            con.Close();

            return idnhm;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
           

            using (var detailForm = new frmIzvozKonacnaTable(txtNadredjeni.Text))
            {
                detailForm.ShowDialog();
                txtID.Text = detailForm.GetID();
                if (txtID.Text == "")
                { return; }
                { VratiPodatkeSelect(Convert.ToInt32(txtID.Text));
                    FillDG2();
                }
                
            }
        }
        int pp = 0;

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "")
            {

                MessageBox.Show("Morate izabrati kontejner");
                return;
            }
            pp = ProveriPrazanPun();

            int terminal = 0;
            if (txtID.Text == "")
            { txtID.Text = "0"; }
            if (chkTerminal.Checked) { terminal = 1; }
            pickUp = cboPPCNT.Text.ToString().TrimEnd();
            pickUp2 = cboPPCNT2.Text.ToString().TrimEnd();
            pickUp3 = cboPPCNT3.Text.ToString().TrimEnd();

            ADR = Convert.ToInt32(txtADR.SelectedValue);




            MoguciScenario();

            // int IDPlana, int ID, int Nalogodavac1, int Nalogodavac2, int Nalogodavac3
            frmIzvozUnosManipulacije um = new frmIzvozUnosManipulacije(Convert.ToInt32(txtNadredjeni.Text), Convert.ToInt32(txtID.Text), Convert.ToInt32(cboNalogodavac1.SelectedValue), Convert.ToInt32(cboNalogodavac2.SelectedValue), Convert.ToInt32(cboNalogodavac3.SelectedValue), Convert.ToInt32(cboIzvoznik.SelectedValue), terminal, pickUp, ScenarioGL, ADR,pp, Zeleznina, Repozicija);
            um.Show();
            FillDG2();
           
        }

        private void frmIzvozKonacna_SizeChanged(object sender, EventArgs e)
        {
            /*
            float size1 = this.Size.Width / firstWidth;
            float size2 = this.Size.Height / firstHeight;

            SizeF scale = new SizeF(size1, size2);
            firstWidth = this.Size.Width;
            firstHeight = this.Size.Height;

            foreach (Control control in this.Controls)
            {

                control.Font = new Font(control.Font.FontFamily, control.Font.Size * ((size1 + size2) / 2));

                control.Scale(scale);

                string ff = control.GetType().Name;
                if (control.GetType().Name == "ComboBox")
                {
                    ComboBox cb = (ComboBox)control;
                    cb.SelectedIndex = -1;
                }
            }
            */
        }

        private void txtVrednostRobeFaktura_Enter(object sender, EventArgs e)
        {
            txtVrednostRobeFaktura.Select(0, txtVrednostRobeFaktura.Text.Length);
        }

        private void txtNetoR_Enter(object sender, EventArgs e)
        {
            txtNetoR.Select(0, txtNetoR.Text.Length);
        }

        private void txtBrutoR_Enter(object sender, EventArgs e)
        {
            txtBrutoR.Select(0, txtBrutoR.Text.Length);
        }

        private void txtBrutoO_Enter(object sender, EventArgs e)
        {
            txtBrutoO.Select(0, txtBrutoO.Text.Length);
        }

        private void txtKoleta_Enter(object sender, EventArgs e)
        {
            txtKoleta.Select(0, txtKoleta.Text.Length);
        }

        private void txtKoletaO_Enter(object sender, EventArgs e)
        {
            txtKoletaO.Select(0, txtKoletaO.Text.Length);
        }

        private void txtCBM_Enter(object sender, EventArgs e)
        {
            txtCBM.Select(0, txtCBM.Text.Length);
        }

        private void txtCBMO_Enter(object sender, EventArgs e)
        {
            txtCBMO.Select(0, txtCBMO.Text.Length);
        }

        private void txtOdvaganaTezina_Enter(object sender, EventArgs e)
        {
            txtOdvaganaTezina.Select(0, txtOdvaganaTezina.Text.Length);
        }

        private void txtTaraKontejnera_Enter(object sender, EventArgs e)
        {
            txtTaraKontejnera.Select(0, txtTaraKontejnera.Text.Length);
        }

        private void txVGMBrodBruto_Enter(object sender, EventArgs e)
        {
            txVGMBrodBruto.Select(0, txVGMBrodBruto.Text.Length);
        }

        private void txtVGMBrod_Enter(object sender, EventArgs e)
        {
            txtVGMBrod.Select(0, txtVGMBrod.Text.Length);
        }

        private void txtAutoDana_Enter(object sender, EventArgs e)
        {
            txtAutoDana.Select(0, txtAutoDana.Text.Length);
        }

        private void txtVrednostRobeFaktura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
            {
                e.KeyChar = ((System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture).NumberFormat.NumberDecimalSeparator.ToCharArray()[0];
            }
        }

        private void txtNetoR_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
            {
                e.KeyChar = ((System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture).NumberFormat.NumberDecimalSeparator.ToCharArray()[0];
            }
        }

        private void txtBrutoR_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
            {
                e.KeyChar = ((System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture).NumberFormat.NumberDecimalSeparator.ToCharArray()[0];
            }
        }

        private void txtBrutoO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
            {
                e.KeyChar = ((System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture).NumberFormat.NumberDecimalSeparator.ToCharArray()[0];
            }
        }

        private void txtKoleta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
            {
                e.KeyChar = ((System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture).NumberFormat.NumberDecimalSeparator.ToCharArray()[0];
            }
        }

        private void txtKoletaO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
            {
                e.KeyChar = ((System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture).NumberFormat.NumberDecimalSeparator.ToCharArray()[0];
            }
        }

        private void txtCBM_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
            {
                e.KeyChar = ((System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture).NumberFormat.NumberDecimalSeparator.ToCharArray()[0];
            }
        }

        private void txtCBMO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
            {
                e.KeyChar = ((System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture).NumberFormat.NumberDecimalSeparator.ToCharArray()[0];
            }
        }

        private void txtAutoDana_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
            {
                e.KeyChar = ((System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture).NumberFormat.NumberDecimalSeparator.ToCharArray()[0];
            }
        }

        private void txtOdvaganaTezina_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
            {
                e.KeyChar = ((System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture).NumberFormat.NumberDecimalSeparator.ToCharArray()[0];
            }
        }

        private void txtTaraKontejnera_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
            {
                e.KeyChar = ((System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture).NumberFormat.NumberDecimalSeparator.ToCharArray()[0];
            }
        }

        private void txVGMBrodBruto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
            {
                e.KeyChar = ((System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture).NumberFormat.NumberDecimalSeparator.ToCharArray()[0];
            }
        }

        private void txtVGMBrod_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
            {
                e.KeyChar = ((System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture).NumberFormat.NumberDecimalSeparator.ToCharArray()[0];
            }
        }

        private void frmIzvozKonacna_KeyDown(object sender, KeyEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift && e.KeyCode == Keys.D)
            {
                frmIzvozDokumenta fid = new frmIzvozDokumenta(txtID.Text, txtBokingBrodara.Text, "0");
                fid.Show();
            }
            else if (Control.ModifierKeys == Keys.Shift && e.KeyCode == Keys.T)
            {
                using (var detailForm = new frmIzvozKonacnaTable())
                {
                    detailForm.ShowDialog();
                    txtID.Text = detailForm.GetID();
                    VratiPodatkeSelect(Convert.ToInt32(txtID.Text));
                }
            }
            else if (Control.ModifierKeys == Keys.Shift && e.KeyCode == Keys.U)
            {
                int terminal = 0;
                if (txtID.Text == "")
                { txtID.Text = "0"; }
                if (chkTerminal.Checked) { terminal = 1; }
                pickUp = cboPPCNT.Text.ToString().TrimEnd();
                pickUp2 = cboPPCNT2.Text.ToString().TrimEnd();
                pickUp3 = cboPPCNT3.Text.ToString().TrimEnd();
                ADR = Convert.ToInt32(txtADR.SelectedValue);
                MoguciScenario();
                // int IDPlana, int ID, int Nalogodavac1, int Nalogodavac2, int Nalogodavac3
                frmIzvozUnosManipulacije um = new frmIzvozUnosManipulacije(Convert.ToInt32(0), Convert.ToInt32(txtID.Text), Convert.ToInt32(cboNalogodavac1.SelectedValue), Convert.ToInt32(cboNalogodavac2.SelectedValue), Convert.ToInt32(cboNalogodavac3.SelectedValue), Convert.ToInt32(cboIzvoznik.SelectedValue), terminal, pickUp, ScenarioGL, ADR,pp, Zeleznina, Repozicija);
                um.Show();

            }
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            frmOtpremaVozaIzPlana ovizpl = new frmOtpremaVozaIzPlana();
            ovizpl.Show();
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {
            FillDGUsluge();
            FillDGUslugeUvozne();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {

        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        int ProveriDaliPostojiIzdatRNI(string Kont)
        {
            int pom = 0;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);
            con.Open();

            SqlCommand cmd = new SqlCommand("select count(*) as Broj from RAdniNalogInterni where OjIzdavanja = 2 and BrojOsnov=" + Kont, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                pom = Convert.ToInt32(dr["Broj"].ToString());
                
            }
            con.Close();
            

            return pom;
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            int i = ProveriDaliPostojiIzdatRNI(txtID.Text);

            if (i > 0)
            {
                MessageBox.Show("Postoje već izdati radni nalozi za Terminal ne možete obrisati, kontaktirajte terminal");
                return;
            
            }
            else
            {
                InsertIzvozKonacna del = new InsertIzvozKonacna();
                del.DelIzvozKonacnaSve(Convert.ToInt32(txtID.Text));
            }


            
        }

        private void tabSplitterPage1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button30_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connection);

            var partner5 = "select Distinct PaKOsifra, PaNaziv from partnerjiKontOseba inner join Partnerji on Partnerji.PaSifra = PaKOsifra where Carinarnica = " + Convert.ToInt32(cboCarina.SelectedValue);
            var partAD5 = new SqlDataAdapter(partner5, conn);
            var partDS5 = new DataSet();
            partAD5.Fill(partDS5);
            cboSpedicija.DataSource = partDS5.Tables[0];
            cboSpedicija.DisplayMember = "PaNaziv";
            cboSpedicija.ValueMember = "PaKOsifra";
        }

        private void button31_Click(object sender, EventArgs e)
        {
            frmPovezivanjeKontejneraIVagona pkv = new frmPovezivanjeKontejneraIVagona(Convert.ToInt32(txtNadredjeni.Text));
            pkv.Show();
        }
    }
}
