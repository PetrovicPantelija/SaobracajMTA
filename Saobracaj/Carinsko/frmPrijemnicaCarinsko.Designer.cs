namespace Saobracaj.Carinko
{
    partial class frmPrijemnicaCarinsko
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrijemnicaCarinsko));
            this.txtDokument = new System.Windows.Forms.TextBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDatum = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.cboSkladisteID = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtKorisnik = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cboMagacinskiBroj = new System.Windows.Forms.ComboBox();
            this.cboVrstaSkladista = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboSektor = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cboVlasnik = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cboKorisnikRobe = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cboPosiljalac = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cboPrimalac = new System.Windows.Forms.ComboBox();
            this.txtBrojFakture = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtPrevoznik = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtBrojKamiona = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtNapomena1 = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.txtNapomena2 = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtTransportNo = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.dtpOcekivanoVreme = new System.Windows.Forms.DateTimePicker();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.button23 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button20 = new System.Windows.Forms.Button();
            this.button21 = new System.Windows.Forms.Button();
            this.button22 = new System.Windows.Forms.Button();
            this.sfDataGrid1 = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.button1 = new System.Windows.Forms.Button();
            this.commandBarController1 = new Syncfusion.Windows.Forms.Tools.CommandBarController(this.components);
            this.label20 = new System.Windows.Forms.Label();
            this.cboNalogodavac = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panelHeader.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sfDataGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtDokument
            // 
            this.txtDokument.Location = new System.Drawing.Point(43, 233);
            this.txtDokument.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDokument.Name = "txtDokument";
            this.txtDokument.Size = new System.Drawing.Size(308, 22);
            this.txtDokument.TabIndex = 485;
            // 
            // txtStatus
            // 
            this.txtStatus.Enabled = false;
            this.txtStatus.Location = new System.Drawing.Point(1576, 78);
            this.txtStatus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(112, 22);
            this.txtStatus.TabIndex = 482;
            this.txtStatus.Text = "OTVOREN";
            // 
            // txtID
            // 
            this.txtID.Enabled = false;
            this.txtID.Location = new System.Drawing.Point(49, 78);
            this.txtID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(113, 22);
            this.txtID.TabIndex = 489;
            this.txtID.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1572, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 16);
            this.label4.TabIndex = 486;
            this.label4.Text = "Status prijema";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(45, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 16);
            this.label1.TabIndex = 487;
            this.label1.Text = "ID";
            // 
            // dtpDatum
            // 
            this.dtpDatum.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dtpDatum.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.dtpDatum.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDatum.Location = new System.Drawing.Point(201, 76);
            this.dtpDatum.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpDatum.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dtpDatum.Name = "dtpDatum";
            this.dtpDatum.Size = new System.Drawing.Size(188, 21);
            this.dtpDatum.TabIndex = 483;
            this.dtpDatum.Value = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(39, 210);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(121, 16);
            this.label9.TabIndex = 488;
            this.label9.Text = "Skladisni dokument";
            // 
            // cboSkladisteID
            // 
            this.cboSkladisteID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboSkladisteID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboSkladisteID.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.cboSkladisteID.FormattingEnabled = true;
            this.cboSkladisteID.Location = new System.Drawing.Point(43, 160);
            this.cboSkladisteID.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboSkladisteID.Name = "cboSkladisteID";
            this.cboSkladisteID.Size = new System.Drawing.Size(305, 23);
            this.cboSkladisteID.TabIndex = 484;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(197, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 16);
            this.label2.TabIndex = 490;
            this.label2.Text = "Datum";
            // 
            // txtKorisnik
            // 
            this.txtKorisnik.Enabled = false;
            this.txtKorisnik.Location = new System.Drawing.Point(1421, 78);
            this.txtKorisnik.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtKorisnik.Name = "txtKorisnik";
            this.txtKorisnik.Size = new System.Drawing.Size(112, 22);
            this.txtKorisnik.TabIndex = 491;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1417, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 16);
            this.label3.TabIndex = 492;
            this.label3.Text = "Korisnik";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(39, 139);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 16);
            this.label5.TabIndex = 493;
            this.label5.Text = "Skladiste";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(404, 492);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 16);
            this.label6.TabIndex = 495;
            this.label6.Text = "Magacinski broj";
            // 
            // cboMagacinskiBroj
            // 
            this.cboMagacinskiBroj.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboMagacinskiBroj.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboMagacinskiBroj.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.cboMagacinskiBroj.FormattingEnabled = true;
            this.cboMagacinskiBroj.Location = new System.Drawing.Point(408, 514);
            this.cboMagacinskiBroj.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboMagacinskiBroj.Name = "cboMagacinskiBroj";
            this.cboMagacinskiBroj.Size = new System.Drawing.Size(305, 23);
            this.cboMagacinskiBroj.TabIndex = 496;
            // 
            // cboVrstaSkladista
            // 
            this.cboVrstaSkladista.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboVrstaSkladista.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboVrstaSkladista.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.cboVrstaSkladista.FormattingEnabled = true;
            this.cboVrstaSkladista.Location = new System.Drawing.Point(408, 442);
            this.cboVrstaSkladista.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboVrstaSkladista.Name = "cboVrstaSkladista";
            this.cboVrstaSkladista.Size = new System.Drawing.Size(305, 23);
            this.cboVrstaSkladista.TabIndex = 498;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(404, 420);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 16);
            this.label7.TabIndex = 497;
            this.label7.Text = "Vrsta skladista";
            // 
            // cboSektor
            // 
            this.cboSektor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboSektor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboSektor.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.cboSektor.FormattingEnabled = true;
            this.cboSektor.Items.AddRange(new object[] {
            "UVOZ",
            "IZVOZ"});
            this.cboSektor.Location = new System.Drawing.Point(785, 306);
            this.cboSektor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboSektor.Name = "cboSektor";
            this.cboSektor.Size = new System.Drawing.Size(305, 23);
            this.cboSektor.TabIndex = 500;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(781, 284);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 16);
            this.label8.TabIndex = 499;
            this.label8.Text = "Sektor";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(39, 284);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(50, 16);
            this.label10.TabIndex = 502;
            this.label10.Text = "Vlasnik";
            // 
            // cboVlasnik
            // 
            this.cboVlasnik.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboVlasnik.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboVlasnik.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.cboVlasnik.FormattingEnabled = true;
            this.cboVlasnik.Location = new System.Drawing.Point(43, 306);
            this.cboVlasnik.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboVlasnik.Name = "cboVlasnik";
            this.cboVlasnik.Size = new System.Drawing.Size(305, 23);
            this.cboVlasnik.TabIndex = 501;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(39, 352);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(83, 16);
            this.label11.TabIndex = 504;
            this.label11.Text = "Korisnik robe";
            // 
            // cboKorisnikRobe
            // 
            this.cboKorisnikRobe.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboKorisnikRobe.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboKorisnikRobe.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.cboKorisnikRobe.FormattingEnabled = true;
            this.cboKorisnikRobe.Location = new System.Drawing.Point(43, 375);
            this.cboKorisnikRobe.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboKorisnikRobe.Name = "cboKorisnikRobe";
            this.cboKorisnikRobe.Size = new System.Drawing.Size(305, 23);
            this.cboKorisnikRobe.TabIndex = 503;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(33, 420);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(63, 16);
            this.label12.TabIndex = 506;
            this.label12.Text = "Pošiljalac";
            // 
            // cboPosiljalac
            // 
            this.cboPosiljalac.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboPosiljalac.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboPosiljalac.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.cboPosiljalac.FormattingEnabled = true;
            this.cboPosiljalac.Location = new System.Drawing.Point(43, 441);
            this.cboPosiljalac.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboPosiljalac.Name = "cboPosiljalac";
            this.cboPosiljalac.Size = new System.Drawing.Size(305, 23);
            this.cboPosiljalac.TabIndex = 505;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(39, 492);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(58, 16);
            this.label13.TabIndex = 508;
            this.label13.Text = "Primalac";
            // 
            // cboPrimalac
            // 
            this.cboPrimalac.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboPrimalac.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboPrimalac.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.cboPrimalac.FormattingEnabled = true;
            this.cboPrimalac.Location = new System.Drawing.Point(43, 513);
            this.cboPrimalac.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboPrimalac.Name = "cboPrimalac";
            this.cboPrimalac.Size = new System.Drawing.Size(305, 23);
            this.cboPrimalac.TabIndex = 507;
            // 
            // txtBrojFakture
            // 
            this.txtBrojFakture.Location = new System.Drawing.Point(408, 233);
            this.txtBrojFakture.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBrojFakture.Name = "txtBrojFakture";
            this.txtBrojFakture.Size = new System.Drawing.Size(308, 22);
            this.txtBrojFakture.TabIndex = 509;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(404, 210);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(73, 16);
            this.label14.TabIndex = 510;
            this.label14.Text = "Broj fakture";
            // 
            // txtPrevoznik
            // 
            this.txtPrevoznik.Location = new System.Drawing.Point(408, 306);
            this.txtPrevoznik.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPrevoznik.Name = "txtPrevoznik";
            this.txtPrevoznik.Size = new System.Drawing.Size(308, 22);
            this.txtPrevoznik.TabIndex = 511;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(404, 284);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(63, 16);
            this.label15.TabIndex = 512;
            this.label15.Text = "Prevoznik";
            // 
            // txtBrojKamiona
            // 
            this.txtBrojKamiona.Location = new System.Drawing.Point(408, 378);
            this.txtBrojKamiona.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBrojKamiona.Name = "txtBrojKamiona";
            this.txtBrojKamiona.Size = new System.Drawing.Size(308, 22);
            this.txtBrojKamiona.TabIndex = 513;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(404, 352);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(83, 16);
            this.label16.TabIndex = 514;
            this.label16.Text = "Broj kamiona";
            // 
            // txtNapomena1
            // 
            this.txtNapomena1.BackColor = System.Drawing.Color.White;
            this.txtNapomena1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNapomena1.Location = new System.Drawing.Point(777, 389);
            this.txtNapomena1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNapomena1.Multiline = true;
            this.txtNapomena1.Name = "txtNapomena1";
            this.txtNapomena1.Size = new System.Drawing.Size(311, 186);
            this.txtNapomena1.TabIndex = 515;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.BackColor = System.Drawing.Color.Transparent;
            this.label26.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.Black;
            this.label26.Location = new System.Drawing.Point(780, 367);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(101, 19);
            this.label26.TabIndex = 516;
            this.label26.Text = "Napomena 1";
            // 
            // txtNapomena2
            // 
            this.txtNapomena2.BackColor = System.Drawing.Color.White;
            this.txtNapomena2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNapomena2.Location = new System.Drawing.Point(1141, 389);
            this.txtNapomena2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNapomena2.Multiline = true;
            this.txtNapomena2.Name = "txtNapomena2";
            this.txtNapomena2.Size = new System.Drawing.Size(311, 186);
            this.txtNapomena2.TabIndex = 517;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(1144, 367);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(101, 19);
            this.label17.TabIndex = 518;
            this.label17.Text = "Napomena 2";
            // 
            // txtTransportNo
            // 
            this.txtTransportNo.Location = new System.Drawing.Point(791, 162);
            this.txtTransportNo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtTransportNo.Name = "txtTransportNo";
            this.txtTransportNo.Size = new System.Drawing.Size(308, 22);
            this.txtTransportNo.TabIndex = 519;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(797, 139);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(79, 16);
            this.label18.TabIndex = 520;
            this.label18.Text = "Transport no";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(797, 207);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(105, 16);
            this.label19.TabIndex = 522;
            this.label19.Text = "Očekivano vreme";
            // 
            // dtpOcekivanoVreme
            // 
            this.dtpOcekivanoVreme.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dtpOcekivanoVreme.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.dtpOcekivanoVreme.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpOcekivanoVreme.Location = new System.Drawing.Point(791, 233);
            this.dtpOcekivanoVreme.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpOcekivanoVreme.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dtpOcekivanoVreme.Name = "dtpOcekivanoVreme";
            this.dtpOcekivanoVreme.Size = new System.Drawing.Size(188, 21);
            this.dtpOcekivanoVreme.TabIndex = 521;
            this.dtpOcekivanoVreme.Value = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            // 
            // panelHeader
            // 
            this.panelHeader.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.panelHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelHeader.Controls.Add(this.panel2);
            this.panelHeader.Controls.Add(this.panel3);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1743, 40);
            this.panelHeader.TabIndex = 523;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.button4);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button17);
            this.panel2.Controls.Add(this.button23);
            this.panel2.Location = new System.Drawing.Point(152, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1046, 38);
            this.panel2.TabIndex = 6;
            // 
            // button3
            // 
            this.button3.AutoSize = true;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.Dock = System.Windows.Forms.DockStyle.Left;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.button3.Location = new System.Drawing.Point(518, 0);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(364, 38);
            this.button3.TabIndex = 25;
            this.button3.Text = "Štampaj prijemnicu / Skladisni dokumet";
            this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // button4
            // 
            this.button4.AutoSize = true;
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button4.Dock = System.Windows.Forms.DockStyle.Left;
            this.button4.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.button4.Location = new System.Drawing.Point(439, 0);
            this.button4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(79, 38);
            this.button4.TabIndex = 24;
            this.button4.Text = "Otvori";
            this.button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button2
            // 
            this.button2.AutoSize = true;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.Dock = System.Windows.Forms.DockStyle.Left;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.button2.Location = new System.Drawing.Point(342, 0);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 38);
            this.button2.TabIndex = 22;
            this.button2.Text = "Proknjizi";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button17
            // 
            this.button17.AutoSize = true;
            this.button17.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button17.Dock = System.Windows.Forms.DockStyle.Left;
            this.button17.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.button17.FlatAppearance.BorderSize = 0;
            this.button17.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button17.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button17.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.button17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.button17.Location = new System.Drawing.Point(153, 0);
            this.button17.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(189, 38);
            this.button17.TabIndex = 21;
            this.button17.Text = "Magacinski Brojevi";
            this.button17.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button17.UseVisualStyleBackColor = true;
            this.button17.Click += new System.EventHandler(this.button17_Click);
            // 
            // button23
            // 
            this.button23.AutoSize = true;
            this.button23.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button23.Dock = System.Windows.Forms.DockStyle.Left;
            this.button23.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.button23.FlatAppearance.BorderSize = 0;
            this.button23.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button23.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button23.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button23.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.button23.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.button23.Location = new System.Drawing.Point(0, 0);
            this.button23.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button23.Name = "button23";
            this.button23.Size = new System.Drawing.Size(153, 38);
            this.button23.TabIndex = 16;
            this.button23.Text = "Dokumentacija";
            this.button23.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button23.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.button20);
            this.panel3.Controls.Add(this.button21);
            this.panel3.Controls.Add(this.button22);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(144, 38);
            this.panel3.TabIndex = 2;
            // 
            // button20
            // 
            this.button20.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button20.BackgroundImage")));
            this.button20.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button20.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.button20.FlatAppearance.BorderSize = 0;
            this.button20.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button20.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button20.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.button20.Location = new System.Drawing.Point(93, 4);
            this.button20.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button20.Name = "button20";
            this.button20.Size = new System.Drawing.Size(36, 33);
            this.button20.TabIndex = 15;
            this.button20.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button20.UseVisualStyleBackColor = true;
            this.button20.Click += new System.EventHandler(this.button20_Click);
            // 
            // button21
            // 
            this.button21.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button21.BackgroundImage")));
            this.button21.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button21.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.button21.FlatAppearance.BorderSize = 0;
            this.button21.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button21.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button21.Location = new System.Drawing.Point(52, 4);
            this.button21.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button21.Name = "button21";
            this.button21.Size = new System.Drawing.Size(36, 33);
            this.button21.TabIndex = 14;
            this.button21.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button21.UseVisualStyleBackColor = true;
            this.button21.Click += new System.EventHandler(this.button21_Click);
            // 
            // button22
            // 
            this.button22.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button22.BackgroundImage")));
            this.button22.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button22.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.button22.FlatAppearance.BorderSize = 0;
            this.button22.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button22.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button22.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button22.Location = new System.Drawing.Point(13, 4);
            this.button22.Margin = new System.Windows.Forms.Padding(12, 7, 8, 7);
            this.button22.Name = "button22";
            this.button22.Padding = new System.Windows.Forms.Padding(12, 11, 12, 11);
            this.button22.Size = new System.Drawing.Size(36, 33);
            this.button22.TabIndex = 13;
            this.button22.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button22.UseVisualStyleBackColor = true;
            this.button22.Click += new System.EventHandler(this.button22_Click);
            // 
            // sfDataGrid1
            // 
            this.sfDataGrid1.AccessibleName = "Table";
            this.sfDataGrid1.AddNewRowPosition = Syncfusion.WinForms.DataGrid.Enums.RowPosition.Bottom;
            this.sfDataGrid1.AllowDeleting = true;
            this.sfDataGrid1.Location = new System.Drawing.Point(1268, 185);
            this.sfDataGrid1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.sfDataGrid1.Name = "sfDataGrid1";
            this.sfDataGrid1.PreviewRowHeight = 35;
            this.sfDataGrid1.Size = new System.Drawing.Size(458, 72);
            this.sfDataGrid1.TabIndex = 525;
            this.sfDataGrid1.Text = "sfDataGrid1";
            this.sfDataGrid1.Visible = false;
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(1691, 556);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(36, 33);
            this.button1.TabIndex = 16;
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // commandBarController1
            // 
            this.commandBarController1.HostForm = this;
            this.commandBarController1.MetroBackColor = System.Drawing.Color.White;
            this.commandBarController1.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.commandBarController1.UseBackwardCompatiblity = false;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(404, 139);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(80, 16);
            this.label20.TabIndex = 527;
            this.label20.Text = "Nalogodavac";
            // 
            // cboNalogodavac
            // 
            this.cboNalogodavac.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboNalogodavac.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboNalogodavac.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.cboNalogodavac.FormattingEnabled = true;
            this.cboNalogodavac.Location = new System.Drawing.Point(408, 160);
            this.cboNalogodavac.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboNalogodavac.Name = "cboNalogodavac";
            this.cboNalogodavac.Size = new System.Drawing.Size(305, 23);
            this.cboNalogodavac.TabIndex = 526;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Location = new System.Drawing.Point(37, 596);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1691, 356);
            this.panel1.TabIndex = 528;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1691, 356);
            this.dataGridView1.TabIndex = 567;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView1_CellBeginEdit);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowLeave);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // frmPrijemnicaCarinsko
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1743, 955);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.cboNalogodavac);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.sfDataGrid1);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.dtpOcekivanoVreme);
            this.Controls.Add(this.txtTransportNo);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.txtNapomena2);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.txtNapomena1);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.txtBrojKamiona);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtPrevoznik);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtBrojFakture);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.cboPrimalac);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cboPosiljalac);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cboKorisnikRobe);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cboVlasnik);
            this.Controls.Add(this.cboSektor);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cboVrstaSkladista);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cboMagacinskiBroj);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtKorisnik);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDokument);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpDatum);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cboSkladisteID);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmPrijemnicaCarinsko";
            this.Text = "CARINSKA PRIJEMNICA";
            this.Load += new System.EventHandler(this.frmPrijemnicaCarinsko_Load);
            this.panelHeader.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sfDataGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDokument;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDatum;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cboSkladisteID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtKorisnik;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboMagacinskiBroj;
        private System.Windows.Forms.ComboBox cboVrstaSkladista;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboSektor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cboVlasnik;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cboKorisnikRobe;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cboPosiljalac;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cboPrimalac;
        private System.Windows.Forms.TextBox txtBrojFakture;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtPrevoznik;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtBrojKamiona;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtNapomena1;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txtNapomena2;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtTransportNo;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.DateTimePicker dtpOcekivanoVreme;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.Button button23;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button20;
        private System.Windows.Forms.Button button21;
        private System.Windows.Forms.Button button22;
        private Syncfusion.WinForms.DataGrid.SfDataGrid sfDataGrid1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private Syncfusion.Windows.Forms.Tools.CommandBarController commandBarController1;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox cboNalogodavac;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}